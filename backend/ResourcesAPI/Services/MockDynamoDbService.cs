using Amazon.DynamoDBv2.DocumentModel;

namespace ResourcesAPI.Services
{
    public class MockDynamoDbService : IDynamoDbService
    {
        private readonly Dictionary<string, Dictionary<Primitive, Document>> mockTable;

        public MockDynamoDbService()
        {
            // Initialize an empty dictionary to store mock data
            mockTable = new Dictionary<string, Dictionary<Primitive, Document>>();

            // Add some mock data
            AddMockData("your_dynamodb_table_name", "1", new Document
            {
                ["_id"] = "1",
                ["Name"] = "Mock Item 1",
                ["DateOfPublication"] = "2023-01-01",
                // Add other attributes based on your DynamoDB table schema
            });

            AddMockData("your_dynamodb_table_name", "2", new Document
            {
                ["_id"] = "2",
                ["Name"] = "Mock Item 2",
                ["DateOfPublication"] = "2023-02-01",
                // Add other attributes based on your DynamoDB table schema
            });
        }

        private void AddMockData(string tableName, string id, Document document)
        {
            if (!mockTable.ContainsKey(tableName))
            {
                mockTable[tableName] = new Dictionary<Primitive, Document>();
            }
            mockTable[tableName][new Primitive(id)] = document;
        }

        public void PutItem(string tableName, Document document)
        {
            // Add or update the item in the mock table
            if (!mockTable.ContainsKey(tableName))
            {
                mockTable[tableName] = new Dictionary<Primitive, Document>();
            }

            var primaryKey = document["_id"] as Primitive;
            if (primaryKey == null)
            {
                // Handle the case where "_id" is not a Primitive; adjust as needed
                throw new ArgumentException("_id must be of type Primitive in the Document.");
            }

            mockTable[tableName][primaryKey] = document;
        }

        public Document GetItem(string tableName, Primitive primaryKey)
        {
            // Retrieve the item from the mock table
            if (mockTable.ContainsKey(tableName) && mockTable[tableName].ContainsKey(primaryKey))
            {
                return mockTable[tableName][primaryKey];
            }
            return null;
        }

        public void UpdateItem(string tableName, Primitive primaryKey, UpdateItemOperationConfig updateConfig)
        {
            // Implement update logic for mock table if needed
            if (!mockTable.ContainsKey(tableName) || !mockTable[tableName].ContainsKey(primaryKey))
            {
                // Item not found, handle accordingly
                return;
            }

            var existingItem = mockTable[tableName][primaryKey];

            // Apply update expression to the existing item
            foreach (var attributeName in updateConfig.ReturnValues)
            {
                var attributeValue = updateConfig.ReturnValues[attributeName];

                if (existingItem.ContainsKey(attributeName))
                {
                    // Update the existing item attribute value based on the update expression
                    existingItem[attributeName] = attributeValue;
                }
                else
                {
                    // Handle the case where the attribute doesn't exist in the existing item
                    // You may choose to ignore or throw an exception based on your needs
                }
            }

            // Update the item in the mock table
            mockTable[tableName][primaryKey] = existingItem;
        }

        public void DeleteItem(string tableName, Primitive primaryKey)
        {
            // Remove the item from the mock table
            if (mockTable.ContainsKey(tableName) && mockTable[tableName].ContainsKey(primaryKey))
            {
                mockTable[tableName].Remove(primaryKey);
            }
        }

        public IEnumerable<Document> Scan(string tableName)
        {
            // Return all items in the mock table for scanning
            return mockTable.ContainsKey(tableName) ? mockTable[tableName].Values : new List<Document>();
        }
    }
}
