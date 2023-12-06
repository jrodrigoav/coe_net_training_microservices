using Microsoft.AspNetCore.Mvc;
using ResourcesAPI.Services;
using Amazon.DynamoDBv2.DocumentModel;

namespace ResourcesAPI.Controllers
{
    [Route("api/resources")]
    [ApiController]
    public class ResourcesController : Controller
    {
        private readonly IDynamoDbService _dynamoDbClient;
        private const string dynamodb_table = "your_dynamodb_table_name"; // Replace with your DynamoDB table name

        public ResourcesController(IDynamoDbService dynamoDbService)
        {
            _dynamoDbClient = dynamoDbService;

        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] Dictionary<string, object> requestJson)
        {
            // Validate JSON
            (bool isValid, string message) = ValidateJson(requestJson);
            if (!isValid)
            {
                return BadRequest(new { message });
            }

            // Add '_id' to the requestJson
            requestJson["_id"] = Guid.NewGuid().ToString();

            // Put item using IDynamoDbService
            var document = new Document();
            foreach (var entry in requestJson)
            {
                // Add each attribute to the DynamoDB Document
                document[entry.Key] = (DynamoDBEntry)entry.Value;
            }

            _dynamoDbClient.PutItem(dynamodb_table, document);

            return CreatedAtAction(nameof(Create), new { insertedId = requestJson["_id"] });
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(string id, [FromBody] Dictionary<string, object> requestJson)
        {
            // Get the updatable fields
            string[] updatableFields = { "Name", "DateOfPublication", "Type", "Author", "Tags", "Description" };

            // Create update expression components
            var updateExpressionArray = new List<string>();
            var expressionAttributeValues = new Dictionary<string, DynamoDBEntry>();
            var expressionAttributeNames = new Dictionary<string, string>();

            foreach (var field in updatableFields)
            {
                if (requestJson.ContainsKey(field))
                {
                    var fieldExpressionName = "#" + field + "field";
                    updateExpressionArray.Add($"{fieldExpressionName} = :{field}");
                    expressionAttributeValues[$":{field}"] = new Primitive(requestJson[field]);
                    expressionAttributeNames[fieldExpressionName] = field;
                }
            }

            var updateExpression = "SET " + string.Join(", ", updateExpressionArray);

            // Use the mock implementation of your DynamoDB service
            _dynamoDbClient.UpdateItem(dynamodb_table, new Primitive(id), new UpdateItemOperationConfig
            {
                UpdateExpression = updateExpression,
                ExpressionAttributeValues = expressionAttributeValues,
                ExpressionAttributeNames = expressionAttributeNames
            });

            return Ok(new { updatedId = id });
        }


        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            // Use the mock implementation of your DynamoDB service
            var response = _dynamoDbClient.GetItem(dynamodb_table, new Primitive(id));

            if (response == null || response.Count == 0)
            {
                return NotFound(new { message = "The resource was not found." });
            }
            else
            {
                // Use the mock implementation of your DynamoDB service
                _dynamoDbClient.DeleteItem(dynamodb_table, new Primitive(id));
                return Ok(new { message = "The resource was deleted." });
            }
        }


        [HttpGet("get/{id}")]
        public IActionResult Get(string id)
        {
            // Use the mock implementation of your DynamoDB service
            var item = _dynamoDbClient.GetItem(dynamodb_table, new Primitive(id));

            if (item == null || item.Count == 0)
            {
                return NotFound(new { message = "The resource was not found." });
            }
            else
            {
                // Convert DynamoDB item to a dictionary
                var itemDictionary = item.ToDictionary(entry => entry.Key, entry => entry.Value.AsPrimitive());

                return Ok(itemDictionary);
            }
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            // Use the mock implementation of your DynamoDB service
            var scanResult = _dynamoDbClient.Scan(dynamodb_table);

            // Convert scan result to a response object
            var response = new
            {
                count = scanResult,
                data = scanResult.Select(item => item.ToDictionary(entry => entry.Key, entry => entry.Value.AsPrimitive())).ToList()
            };

            return Ok(new { data = response });
        }

        private (bool isValid, string message) ValidateJson(Dictionary<string, object> requestJson)
        {
            // Implement your JSON validation logic here
            // Return a tuple with the validation result and an error message if validation fails
            // Example validation logic:
            if (!requestJson.ContainsKey("your_required_key"))
            {
                return (false, "Missing required key: your_required_key");
            }

            // Add more validation logic as needed

            return (true, "Validation successful");
        }
    }
}
