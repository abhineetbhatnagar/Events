using MongoDB.Bson;  
using MongoDB.Bson.Serialization.Attributes;  

namespace Events.Tenancy.Services.Domain.Entities
{
    public class TenantModel
    {
        [BsonId]  
        [BsonRepresentation(BsonType.ObjectId)]  
        public string _id { get; set; }  
  
        public string Username { get; set; }  
  
        public string Password { get; set; }  
  
        public string Name { get; set; }  
        
        public string Email { get; set; } 
    }
}