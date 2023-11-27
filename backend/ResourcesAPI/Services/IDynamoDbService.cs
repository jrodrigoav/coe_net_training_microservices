using Amazon.DynamoDBv2.DocumentModel;

namespace ResourcesAPI.Services
{    
        public interface IDynamoDbService
        {
            void PutItem(string tableName, Document document);
            Document GetItem(string tableName, Primitive primaryKey);
            void UpdateItem(string tableName, Primitive primaryKey, UpdateItemOperationConfig updateConfig);
            void DeleteItem(string tableName, Primitive primaryKey);
            IEnumerable<Document> Scan(string tableName);
        }    
}
