using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;

public class MongoDatabaseConnection
{
	public string MongoPrefix { get; set; } = "mongodb+srv";
    public string MongoUserName { get; set; } = string.Empty;
    public string MongoPassword { get; set; } = string.Empty;
    public string MongoCluster { get; set; } = string.Empty;
    public string MongoAppend { get; set; } = "0qydkvz.mongodb.net/?retryWrites=true&w=majority";


	//Public Facing Calls
    public MongoClient MongoConnect()
	{

        //Create Connection String
        string connectionString = MongoPrefix + "://" + MongoUserName + ":" + MongoPassword + "@" + MongoCluster + "." + MongoAppend;

        //Create Connect
        MongoClient client = new MongoClient(connectionString);
 
        return client;
	}

    //Return All Databases in Cluster
    public List<string> getMongoDatabaseList()
    {
        var client = MongoConnect();

        //Validate Connection
        var databaseNames = client.ListDatabaseNames().ToList();

        return databaseNames;
    }

    //Return All Collections In Database
    public List<string> getMongoCollectionList(string database)
    {
        var client = MongoConnect();

        //Get This Database
        var dbconnection = client.GetDatabase(database);

        var collectionNames = dbconnection.ListCollectionNames().ToList();

        return collectionNames;
    }


}
