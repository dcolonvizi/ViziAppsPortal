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
    public Hashtable GetItem(Hashtable State, string tableName, string PrimaryKeyValue)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];

        var request = new GetItemRequest
        {
            TableName = tableName,
            Key = new Key { HashKeyElement = new AttributeValue { S = PrimaryKeyValue } },
            ConsistentRead = true
        };
        var response = client.GetItem(request);

        // Check the response.
        var result = response.GetItemResult;
        var attributeList = result.Item; // attribute list in the response.
        Hashtable map = new Hashtable();
        if (attributeList != null)
        {
            foreach (KeyValuePair<string, AttributeValue> kvp in attributeList)
            {
                string attributeName = kvp.Key;
                AttributeValue value = kvp.Value;

                map[attributeName] = value.S;
            }
        }
        return map;
    }
    public Hashtable GetItem(Hashtable State, string tableName, string PrimaryKeyValue, string RangeKeyValue)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];

        var request = new QueryRequest
        {
            TableName = tableName,
            HashKeyValue = new AttributeValue { S = PrimaryKeyValue },
            // Optional parameters.
            RangeKeyCondition = new Condition
            {
                ComparisonOperator = "EQ",
                AttributeValueList = new List<AttributeValue>()
                    {
                        new AttributeValue { S = RangeKeyValue }
                    }
            },
            ConsistentRead = true
        };

        var response = client.Query(request);

        foreach (Dictionary<string, AttributeValue> item
          in response.QueryResult.Items)
        {
            Hashtable map = new Hashtable();
            foreach (string key in item.Keys)
            {
                AttributeValue value = item[key];
                map[key] = value.S;
            }
            return map;
        }
        return null;
    }

    public ArrayList GetItems(Hashtable State, string tableName, string PrimaryKeyValue)
    {
        ArrayList output = new ArrayList();
        try
        {
            if (State["DynamoDBClient"] == null)
                SetupClient(State);
            AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];

            var request = new QueryRequest
            {
                TableName = tableName,
                HashKeyValue = new AttributeValue { S = PrimaryKeyValue }
            };

            var response = client.Query(request);

            foreach (Dictionary<string, AttributeValue> item in response.QueryResult.Items)
            {
                Hashtable map = new Hashtable();
                foreach (string key in item.Keys)
                {
                    AttributeValue value = item[key];
                    map[key] = value.S;
                }
                output.Add(map);
            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
            return null;
        }
        return output;
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
    public void DeleteItem(Hashtable State, string TableName, string primaryKeyValue,string RangeKeyValue)
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
            Document document = table.DeleteItem(primaryKeyValue, RangeKeyValue,config);
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }
}