using EmployeesImporterPlugin.Domain.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EmployeesImporterPlugin.Domain.Wrappers
{
    internal class EmployeesApiResponse
    {
        [JsonProperty("users")]
        public List<EmployeeResponse> Users { get; set; }
    }
}
