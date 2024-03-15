using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Diagnostics;

public class MongoCommandExector
{
    /*
   ___   ___   _   _   ___       ___     _     ___   ___   _  _   ___   _____   ___ 
  / __| | _ \ | | | | |   \     / __|   /_\   | _ ) |_ _| | \| | | __| |_   _| / __|
 | (__  |   / | |_| | | |) |   | (__   / _ \  | _ \  | |  | .` | | _|    | |   \__ \
  \___| |_|_\  \___/  |___/     \___| /_/ \_\ |___/ |___| |_|\_| |___|   |_|   |___/

    */                                                                                  
    //CRUD actions for Cabinet
    //CREATE
    public void addCabinet(BsonDocument cabinetBsonDocument)
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Cabinets");

        col.InsertOne(cabinetBsonDocument);
    }

    //READ
    public BsonDocument readOneCabinet(string cabinetID)
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Cabinets");

        var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(cabinetID));

        var cabinet = col.Find(filter).FirstOrDefault().ToBsonDocument();

        return cabinet;
    }
    public List<BsonDocument> readAllCabinets()
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Cabinets");

        var cabinets = col.Find(new BsonDocument()).ToList();

        return cabinets;
    }

    //UPDATE
    public void updateCabinet(string cabinetID, BsonArray updatedFieldBsonDoc)
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Cabinets");

        var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(cabinetID));

        var update = Builders<BsonDocument>.Update.Set("Cabinet_Fields", updatedFieldBsonDoc);
        col.UpdateOne(filter, update);
    }

    //DELETE
    public void deleteCabinet(string cabinetID)
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Cabinets");
        var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(cabinetID));

        var results = col.DeleteOne(deleteFilter);
        Debug.WriteLine(results);
    }


    /*
   ___   ___   _   _   ___      _   _   ___   ___   ___   ___ 
  / __| | _ \ | | | | |   \    | | | | / __| | __| | _ \ / __|
 | (__  |   / | |_| | | |) |   | |_| | \__ \ | _|  |   / \__ \
  \___| |_|_\  \___/  |___/     \___/  |___/ |___| |_|_\ |___/
      */                                                       
    public void createUser(string usernamme)
    {

    }

    public void readOneUser(string usernamme)
    {

    }
    public void readAllUsers(string usernamme)
    {

    }

    public void updateUser(string usernamme)
    {

    }
    public void deleteUser(string usernamme)
    {

    }


}

