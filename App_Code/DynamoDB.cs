using System;
using System.Collections.Generic;
using Amazon;
using Amazon.DynamoDB;
using Amazon.DynamoDB.Model;
using Amazon.DynamoDB.DocumentModel;
using Amazon.SecurityToken;
using Amazon.Runtime;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

/// <summary>
/// Summary description for DynamoDB
/// </summary>
public class DynamoDB
{
    public DynamoDB()
    {
    }
    public void SetupClient(Hashtable State)
    {
        //Creating AmazonSecurityTokenServiceClient, using Access and Secret keys from web.config
        AmazonSecurityTokenServiceClient stsClient = new AmazonSecurityTokenServiceClient();
        //Creating RefreshingSessionAWSCredentials and initializing AmazonDynamoDBClient
        RefreshingSessionAWSCredentials sessionCredentials = new RefreshingSessionAWSCredentials(stsClient);
        State["DynamoDBClient"] = new AmazonDynamoDBClient(sessionCredentials);
    }
    public List<string> GetTableNames(Hashtable State)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
        List<string> currentTables = client.ListTables().ListTablesResult.TableNames;
        return currentTables;
    }
    public void CreateTable(Hashtable State, string tableName, string IDField)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
        List<string> currentTables = client.ListTables().ListTablesResult.TableNames;
        if (!currentTables.Contains(tableName))
        {
            client.CreateTable(new CreateTableRequest
            {
                TableName = tableName,
                ProvisionedThroughput = new ProvisionedThroughput { ReadCapacityUnits = 10, WriteCapacityUnits = 10 },
                KeySchema = new KeySchema
                {
                    HashKeyElement = new KeySchemaElement { AttributeName = IDField, AttributeType = "S" },
                }
            });
        }
    }
    public void DeleteTable(Hashtable State, string tableName)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
        client.DeleteTable(new DeleteTableRequest { TableName = tableName });
    }
    public void EmptyTable(Hashtable State, string tableName, string primaryKey)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
        var request = new ScanRequest
        {
            TableName = tableName,
            AttributesToGet = new List<string> { primaryKey }
        };
        var response = client.Scan(request);

        Table table = Table.LoadTable(client, tableName);
        DocumentBatchWrite batch = table.CreateBatchWrite();
        Document doc = new Document();

        DateTime start = DateTime.Now;
        foreach (Dictionary<string, AttributeValue> item
          in response.ScanResult.Items)
        {
            foreach (KeyValuePair<string, AttributeValue> kvp in item)
            {
                string attributeName = kvp.Key;
                AttributeValue value = kvp.Value;

                Console.WriteLine(
                    "Deleting " + primaryKey + "= " + value.ToString()
                );
                DeleteItem(State, tableName, value.S);
            }

        }
        // batch.AddDocumentToPut(doc);
        //batch.Execute();
        DateTime end = DateTime.Now;
        TimeSpan duration = end - start;
    }
    public Hashtable GetItem(Hashtable State, string tableName, string PrimaryKeyValue)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
        Table table = Table.LoadTable(client, tableName);
        Document doc = table.GetItem(PrimaryKeyValue);
        Hashtable item = new Hashtable();
        foreach (var attribute in doc.GetAttributeNames())
        {
            string stringValue = null;
            var value = doc[attribute];
            if (value is Primitive)
                stringValue = value.AsPrimitive().Value;
            else if (value is PrimitiveList)
                stringValue = string.Join(",", (from primitive
                                                  in value.AsPrimitiveList().Entries
                                                select primitive.Value).ToArray());
            item[attribute] = stringValue;
        }
        return item;
    }
    public void PutItem(Hashtable State, string TableName, Document item)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
        Table table = Table.LoadTable(client, TableName);
        table.PutItem(item);
    }
    public void PutItems(Hashtable State, string TableName, ArrayList items)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
        Table table = Table.LoadTable(client, TableName);
        DocumentBatchWrite batch = table.CreateBatchWrite();
        foreach (Document item in items)
        {
            batch.AddDocumentToPut(item);
        }
        batch.Execute();
    }
    public void UpdateItem(Hashtable State, string TableName, Document item)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
        Table table = Table.LoadTable(client, TableName);
        table.UpdateItem(item);
    }
    public void DeleteItem(Hashtable State, string TableName, string primaryKeyValue)
    {
        try
        {
            if (State["DynamoDBClient"] == null)
                SetupClient(State);
            AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
            Table table = Table.LoadTable(client, TableName);
            DeleteItemOperationConfig config = new DeleteItemOperationConfig
            {
                // Return the deleted item.
                ReturnValues = ReturnValues.AllOldAttributes
            };
            Document document = table.DeleteItem(primaryKeyValue, config);
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }
}