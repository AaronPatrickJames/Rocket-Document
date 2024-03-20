using Amazon.Auth.AccessControlPolicy;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

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
        
    }


    /*
   ___   ___   _   _   ___      _   _   ___   ___   ___   ___ 
  / __| | _ \ | | | | |   \    | | | | / __| | __| | _ \ / __|
 | (__  |   / | |_| | | |) |   | |_| | \__ \ | _|  |   / \__ \
  \___| |_|_\  \___/  |___/     \___/  |___/ |___| |_|_\ |___/
      */                                                       
    public void createUser(BsonDocument UserDocument)
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Users");

        col.InsertOne(UserDocument);
    }

    public BsonDocument readOneUser(string userID)
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Users");

        var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(userID));

        var user = col.Find(filter).FirstOrDefault().ToBsonDocument();

        return user;
    }
    public List<BsonDocument> readAllUsers()
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Users");

        var users = col.Find(new BsonDocument()).ToList();

        return users;
    }

    public void updateUser(string userID, BsonDocument userBsonDocument)
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Users");

        var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(userID));

        foreach (var keyvalue in  userBsonDocument)
        {
            var update = Builders<BsonDocument>.Update.Set(keyvalue.Name.ToString(), keyvalue.Value);
            col.UpdateOne(filter, update);
        }
    }
    //Reactiveate User
    public void reactiveate(string userID) //Does not truely delete user - Marks User as Inactive and Hides from displaying when users are called back) 
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Users");
        var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(userID));

        var update = Builders<BsonDocument>.Update.Set("Active_User", true);
        col.UpdateOne(filter, update);
    }

    //Delete and Deactive User
    public void deactiveate(string userID) //Does not truely delete user - Marks User as Inactive and Hides from displaying when users are called back) 
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Users");
        var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(userID));

        var update = Builders<BsonDocument>.Update.Set("Active_User", false);
        col.UpdateOne(filter, update);
    }

    /*
   ___   ___   _   _   ___       ___   ___    ___    _   _   ___   ___ 
  / __| | _ \ | | | | |   \     / __| | _ \  / _ \  | | | | | _ \ / __|
 | (__  |   / | |_| | | |) |   | (_ | |   / | (_) | | |_| | |  _/ \__ \
  \___| |_|_\  \___/  |___/     \___| |_|_\  \___/   \___/  |_|   |___/                                                                
    */
    public void createGroup(BsonDocument GroupDocument)
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Groups");

        col.InsertOne(GroupDocument);
    }
    public BsonDocument readOneGroup(string groupID)
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Groups");

        var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(groupID));

        var group = col.Find(filter).FirstOrDefault().ToBsonDocument();

        return group;
    }
    public List<BsonDocument> readAllGroups()
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Groups");

        var groups = col.Find(new BsonDocument()).ToList();

        return groups;
    }
    public void updateGroup(string groupID, BsonDocument GroupDocument)
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Groups");

        var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(groupID));

        foreach (var keyvalue in GroupDocument)
        {
            var update = Builders<BsonDocument>.Update.Set(keyvalue.Name.ToString(), keyvalue.Value);
            col.UpdateOne(filter, update);
        }
    }
    public void deleteGroup(string groupID)
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var col = db.GetCollection<BsonDocument>("Groups");
        var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(groupID));

        var results = col.DeleteOne(deleteFilter);

    }


    /*
   ___   ___   _   _   ___      ___     ___     ___   _   _   __  __   ___   _  _   _____   ___ 
  / __| | _ \ | | | | |   \    |   \   / _ \   / __| | | | | |  \/  | | __| | \| | |_   _| / __|
 | (__  |   / | |_| | | |) |   | |) | | (_) | | (__  | |_| | | |\/| | | _|  | .` |   | |   \__ \
  \___| |_|_\  \___/  |___/    |___/   \___/   \___|  \___/  |_|  |_| |___| |_|\_|   |_|   |___/                                                                                                     
    */

    public ObjectId createDocument(byte[] document, string documentName, BsonDocument metadata)
    {
        var connection = new MongoDatabaseConnection();
        var client = connection.MongoConnect();
        var db = client.GetDatabase("Rocket_Document");
        var bucket = connection.bucketTable("Repair Orders");
        var options = new GridFSUploadOptions
        {
            ChunkSizeBytes = 1048576, // 1MB
            Metadata = metadata
        };

        var id = bucket.UploadFromBytes(documentName, document, options);
        return id;
    }



}

