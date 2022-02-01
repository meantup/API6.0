using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ASP.NET_CORE_API.Model
{
    public class AccountModel : Credential
    {
        [JsonPropertyName("Lastname"),Required, MinLength(8),MaxLength(9)]
        public string? lName { get; set; }
        [JsonPropertyName("Firstname")]
        public string? fName { get; set; }
        [JsonPropertyName("Middlename")]
        public string? mName { get; set; }
        
    }
    public class Credential
    {
        [JsonPropertyName("Username"),Required, MinLength(4), MaxLength(20)]
        public string? username { get; set; }
        [JsonPropertyName("Password"), Required, MinLength(4), MaxLength(8)]
        public string? password { get; set; }
    }
    public class ModelStateError
    {
        public string? exception { get; set; }
        public string? errorMessage { get; set; }
    }
}
