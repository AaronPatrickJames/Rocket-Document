using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using MongoDB.Driver.GridFS;

public class MongoDatabaseConnection
{
	public string MongoPrefix { get; set; } = "mongodb+srv";
    public string MongoUserName { get; set; } = ActiveAccount.username; //Defaults to current active account upon creation
    public string MongoPassword { get; set; } = ActiveAccount.password; //Defaults to current active account upon creation
    public string MongoCluster { get; set; } = ActiveAccount.repository; //Defaults to current active account upon creation
    public string MongoAppend { get; set; } = "0qydkvz.mongodb.net/?retryWrites=true&w=majority";


	//Public Facing Calls
    public MongoClient MongoConnect()
	{
        //LocalHost Defualt Connection
        //string connectionString = "mongodb://localhost:27017";

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

    //Return All Items In Collection
    public List<string> getMongoCollectionNames(string database)
    {
        var client = MongoConnect();

        //Get This Database
        var dbconnection = client.GetDatabase(database);

        var collectionNames = dbconnection.ListCollectionNames().ToList();

        return collectionNames;
    }

    //Return All Items In Collection with Filter (single Field Search) 
    public List<BsonDocument> getMongoCollectionData(string database, string table, string fieldName, string fieldValue)
    {
        var client = MongoConnect();

        //Get This Database
        var dbconnection = client.GetDatabase(database);
        //Get Table
        var collection = dbconnection.GetCollection<BsonDocument>(table);

        //Create Filter
        var filter = Builders<BsonDocument>.Filter.Eq(fieldName, fieldValue);

        //Query Table
        var queryReturn = collection.Find(filter).ToList();

        return queryReturn;
    }

    //GridFS base items
    public GridFSBucket bucketTable(string bucketName)
    {
        var client = MongoConnect();
        var database = client.GetDatabase("Rocket_Document");

        var bucket = new GridFSBucket(database, new GridFSBucketOptions
        {
            BucketName = bucketName,
            ChunkSizeBytes = 1048576, // 1MB
            WriteConcern = WriteConcern.WMajority,
            ReadPreference = ReadPreference.Secondary
        });

        return bucket;
    }



}
