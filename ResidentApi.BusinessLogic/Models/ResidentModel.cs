using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ResidentApi.BusinessLogic.Models
{
    public class ResidentModel
    {
        [BsonId]
        public string ResidentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double TotalBalance { get; set; }

        public string ApartmentNumber { get; set; }

        public string Email { get; set; }
    }
}
