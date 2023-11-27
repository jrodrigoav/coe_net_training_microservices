using InventoryAPI.DTO;
using InventoryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace InventoryAPI.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService =  inventoryService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] InventoryRegistryModel inventoryRegistryJson)
        {
            // TODO: Validate inventoryRegistryJson

            string resourceApiUrl = $"{_resourceApiEndpoint}/get/{inventoryRegistryJson.ResourceId}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(resourceApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    if (inventoryRegistryJson.Available == null)
                    {
                        inventoryRegistryJson.Available = true;
                    }

                    inventoryRegistryJson._id = Guid.NewGuid().ToString();

                    var putItem = new PutItemOperationConfig();
                    var table = Table.LoadTable(_dynamoDbClient, _dynamoDbTable);
                    var document = Document.FromJson(JsonConvert.SerializeObject(inventoryRegistryJson));

                    await table.PutItemAsync(document, putItem);

                    return StatusCode(201, new { insertedId = inventoryRegistryJson._id });
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound(new { message = "The resource wasn't found." });
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return StatusCode((int)response.StatusCode);
                }
            }
        }

        [HttpGet("list/{resourceId}")]
        public async Task<IActionResult> GetResourceList(string resourceId, [FromQuery] bool? available)
        {
            bool isAvailable = available ?? true;

            var scanFilter = new ScanFilter();
            scanFilter.AddCondition("ResourceId", ScanOperator.Equal, resourceId);
            scanFilter.AddCondition("Available", ScanOperator.Equal, isAvailable);

            var scanOperationConfig = new ScanOperationConfig
            {
                Filter = scanFilter
            };

            var table = Table.LoadTable(_dynamoDbClient, _dynamoDbTable);
            var search = table.Scan(scanOperationConfig);

            var documentList = await search.GetNextSetAsync();

            // You can further process the documentList or map it to a custom model as needed
            return Ok(documentList);
        }

        [HttpPut("setAvailability/{id}")]
        public async Task<IActionResult> SetAvailability(string id, [FromBody] AvailabilityUpdateModel availabilityUpdateModel)
        {
            bool isAvailable = availabilityUpdateModel.Available;

            var updateExpression = new UpdateExpression();
            updateExpression.SetExpression("#available = :a");

            var expressionAttributeValues = new Expression();
            expressionAttributeValues.ExpressionAttributeValues[":a"] = isAvailable;

            var expressionAttributeNames = new Expression();
            expressionAttributeNames.ExpressionAttributeNames["#available"] = "Available";

            var updateItemOperationConfig = new UpdateItemOperationConfig
            {
                UpdateExpression = updateExpression,
                ExpressionAttributeValues = expressionAttributeValues,
                ExpressionAttributeNames = expressionAttributeNames
            };

            var table = Table.LoadTable(_dynamoDbClient, _dynamoDbTable);
            var document = new Document();
            document["_id"] = id;

            await table.UpdateItemAsync(document, updateItemOperationConfig);

            return Ok(new { message = "Updated successfully" });
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            string resourceApiListUrl = $"{_resourceApiEndpoint}/list";
            Console.WriteLine(resourceApiListUrl);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(resourceApiListUrl);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(data);

                    // You can further process the data or return it as needed
                    return Ok(data);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return StatusCode((int)response.StatusCode);
                }
            }
        }
    }
}
