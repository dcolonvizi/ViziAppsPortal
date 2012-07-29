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
    public Hashtable SelectFromIDKey(Hashtable State, string TableName, string ID)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
        Table table = Table.LoadTable(client, TableName);
        Document doc = table.GetItem(ID);
        List<String> fields = doc.GetAttributeNames();
        Hashtable row = new Hashtable();
        foreach (String field in fields)
        {
            row[field] = doc[field];
        }
        return row;
    }
    public void InsertRow(Hashtable State, string TableName, Hashtable fields)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
        Table table = Table.LoadTable(client, TableName);
        Document doc = new Document();
        foreach (string key in fields.Keys)
        {
            doc[key] = fields[key].ToString();
        }
        table.PutItem(doc);
    }
    public void InsertRows(Hashtable State, string TableName, ArrayList rows)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
        Table table = Table.LoadTable(client, TableName);
        DocumentBatchWrite batch = table.CreateBatchWrite();
        foreach (Hashtable fields in rows)
        {
            Document doc = new Document();
            foreach (string key in fields.Keys)
            {
                doc[key] = fields[key].ToString();
            }
            batch.AddDocumentToPut(doc);
        }
        DateTime start = DateTime.Now;
        batch.Execute();
        DateTime end = DateTime.Now;
        TimeSpan duration = end - start;
    }
    public void UpdateRow(Hashtable State, string TableName, Hashtable fields)
    {
        if (State["DynamoDBClient"] == null)
            SetupClient(State);
        AmazonDynamoDB client = (AmazonDynamoDB)State["DynamoDBClient"];
        Table table = Table.LoadTable(client, TableName);
        if (fields.ContainsKey(table.HashKeyName))
            throw new Exception("DynamoDB update does not contain Hash Key Name");

        Document doc = new Document();
        foreach (string key in fields.Keys)
        {
            doc[key] = (DynamoDBEntry)fields[key]; //field value of null will delete field
        }
        table.UpdateItem(doc);
    }
    public void CreateViziAppsDatabase(Hashtable State)
    {
        DB db = new DB();
        String query = "SHOW TABLES";
        DataRow[] tables = db.ViziAppsExecuteSql(State, query);
        foreach (DataRow table in tables)
        {
            query = "SHOW COLUMNS in " + table.ItemArray[0].ToString();
            DataRow[] fields = db.ViziAppsExecuteSql(State, query);
            CreateTable(State, table.ItemArray[0].ToString(), fields[0].ItemArray[0].ToString());
            query = "SELECT * FROM " + table.ItemArray[0].ToString() + " LIMIT 0,50";
            DataRow[] dataRows = db.ViziAppsExecuteSql(State, query);
            ArrayList rows = new ArrayList();
            foreach (DataRow dataRow in dataRows)
            {
                Hashtable fieldMap = new Hashtable();
                int index = 0;
                foreach (object o in dataRow.ItemArray)
                {
                    fieldMap[fields[index].ItemArray[0].ToString()] = o;
                    index++;
                }
                rows.Add(fieldMap);
            }
            InsertRows(State, table.ItemArray[0].ToString(), rows);
        }
    }
}