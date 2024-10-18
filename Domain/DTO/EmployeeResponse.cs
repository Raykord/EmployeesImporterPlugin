using Newtonsoft.Json;

namespace EmployeesImporterPlugin.Domain.DTO
{
    public class EmployeeResponse
    {
        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("lastName")]
        public string lastName { get; set; }

        [JsonProperty("phone")]
        public string phone { get; set; }
    }
}
