using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection.Metadata;

[Route("api/[controller]")]
[ApiController]
public class RentingController : ControllerBase
{
    private static AmazonDynamoDBClient dynamoDbClient = new AmazonDynamoDBClient();
    private static string dynamoDbTableName = Environment.GetEnvironmentVariable("DYNAMODB_TABLE") ?? "development-renting";
    private static string inventoryApiEndpoint = Environment.GetEnvironmentVariable("INVENTORY_API_ENDPOINT") ?? "http://localhost:5001";
    private static string clientsApiEndpoint = Environment.GetEnvironmentVariable("CLIENTS_API_ENDPOINT") ?? "http://localhost:5003";
    private static string resourcesApiEndpoint = Environment.GetEnvironmentVariable("RESOURCES_API_ENDPOINT") ?? "http://localhost:5000";

    [HttpGet("list")]
    public IActionResult List()
    {
        var scanRequest = new ScanRequest
        {
            TableName = dynamoDbTableName
        };

        try
        {
            var scanResponse = dynamoDbClient.ScanAsync(scanRequest).Result;
            var items = scanResponse.Items.Select(item => item.ToDictionary(kv => kv.Key, kv => kv.Value.S)).ToList();
            var response = new { items, count = scanResponse.Count };
            return Ok(response);
        }
        catch (AmazonDynamoDBException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("list_by_client_id")]
    public IActionResult ListByClientId(string id, bool returned = false)
    {
        var filterExpression = returned ? "#ci = :ci and #r = :r" : "#ci = :ci and attribute_not_exists(#r)";

        var expressionAttributeNames = new Dictionary<string, string>
        {
            { "#ci", "ClientId" },
            { "#r", "Returned" }
        };

        var expressionAttributeValues = returned ?
            new Dictionary<string, AttributeValue>
            {
                { ":ci", new AttributeValue { S = id } },
                { ":r", new AttributeValue { BOOL = true } }
            } :
            new Dictionary<string, AttributeValue>
            {
                { ":ci", new AttributeValue { S = id } }
            };

        var scanRequest = new ScanRequest
        {
            TableName = dynamoDbTableName,
            FilterExpression = filterExpression,
            ExpressionAttributeNames = expressionAttributeNames,
            ExpressionAttributeValues = expressionAttributeValues
        };

        try
        {
            var scanResponse = dynamoDbClient.ScanAsync(scanRequest).Result;
            var items = scanResponse.Items.Select(item =>
            {
                item["ResourceName"] = GetResourceNameById(item["ResourceId"].S);
                return item.ToDictionary(kv => kv.Key, kv => kv.Value.S);
            }).ToList();

            var response = new { items, count = scanResponse.Count };
            return Ok(response);
        }
        catch (AmazonDynamoDBException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RentingRegistry registry)
    {
        // Validate that the resource exists and it has copies available
        var inventoryApiUrl = $"{inventoryApiEndpoint}/list/{registry.ResourceId}";
        var inventoryApiResponse = HttpClientHelper.GetAsync(inventoryApiUrl).Result;

        var jsonInventoryApiResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(inventoryApiResponse);

        if (Convert.ToInt32(jsonInventoryApiResponse["Count"]) == 0)
        {
            return BadRequest(new { message = "The resource is not available." });
        }

        registry.CopyId = ((List<Dictionary<string, object>>)jsonInventoryApiResponse["Items"])[0]["_id"].ToString();

        try
        {
            var putItemRequest = new PutItemRequest
            {
                TableName = dynamoDbTableName,
                Item = Document.FromJson(JsonConvert.SerializeObject(registry)).ToAttributeMap()
            };

            dynamoDbClient.PutItemAsync(putItemRequest).Wait();
        }
        catch (AmazonDynamoDBException ex)
        {
            return BadRequest(new { message = ex.Message });
        }

        // Set the Copy unavailable
        SetCopyAvailability(registry.CopyId, false);

        return Ok(registry);
    }

    [HttpPut("return/{id}")]
    public IActionResult Return(string id, [FromBody] ReturnInfo returnInfo)
    {
        var copyId = GetCopyIdByRentingId(id);

        var updateItemRequest = new UpdateItemRequest
        {
            TableName = dynamoDbTableName,
            Key = new Dictionary<string, AttributeValue>
            {
                { "_id", new AttributeValue { S = id } }
            },
            UpdateExpression = "SET ReturnDate = :r, Returned = :re",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":r", new AttributeValue { S = returnInfo.ReturnDate } },
                { ":re", new AttributeValue { BOOL = true } }
            }
        };

        try
        {
            dynamoDbClient.UpdateItemAsync(updateItemRequest).Wait();
            SetCopyAvailability(copyId, true);
            return Ok(new { message = $"The copy with ID {copyId} has been returned." });
        }
        catch (AmazonDynamoDBException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    private string GetCopyIdByRentingId(string rentingId)
    {
        try
        {
            var getItemRequest = new GetItemRequest
            {
                TableName = dynamoDbTableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "_id", new AttributeValue { S = rentingId } }
                }
            };

            var result = dynamoDbClient.GetItemAsync(getItemRequest).Result;
            return result.Item["CopyId"].S;
        }
        catch (AmazonDynamoDBException ex)
        {
            return null;
        }
    }

    private void SetCopyAvailability(string copyId, bool available)
    {
        var inventoryApiUrl = $"{inventoryApiEndpoint}/setAvailability/{copyId}";
        var availabilityInfo = new { Available = available };
        var setAvailabilityResponse = HttpClientHelper.PutAsync(inventoryApiUrl, availabilityInfo).Result;
        Console.WriteLine($"Availability response: {setAvailabilityResponse}");
    }

    private string GetResourceNameById(string resourceId)
    {
        var resourcesApiUrl = $"{resourcesApiEndpoint}/get/{resourceId}";
        var resourcesApiResponse = HttpClientHelper.GetAsync(resourcesApiUrl).Result;
        var resource = JsonConvert.DeserializeObject<Dictionary<string, object>>(resourcesApiResponse);
        return resource["Name"].ToString();
    }
}

public class RentingRegistry
{
    public string ResourceId { get; set; }
    public string ClientId { get; set; }
    public string RegistrationDate { get; set; }
    public string ReturnDate { get; set; }
    public string CopyId { get; set; }
}

public class ReturnInfo
{
    public string ReturnDate { get; set; }
}

public static class HttpClientHelper
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task<string> GetAsync(string url)
    {
        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<string> PutAsync(string url, object data)
    {
        var content = new StringContent(JsonConvert.SerializeObject(data));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await client.PutAsync(url, content);
        return await response.Content.ReadAsStringAsync();
    }
}
