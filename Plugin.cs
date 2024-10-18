using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using EmployeesImporterPlugin.Domain.DTO;
using EmployeesImporterPlugin.Domain.Wrappers;

namespace EmployeesImporterPlugin
{
    [Author(Name = "Sergey Moshkov")]
    public class Plugin : IPluggable
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            logger.Info("Start importing");

            var employeesResponse = GetUsersAsync().Result;

            logger.Info($"Imported {employeesResponse.Count} employees");

            var employees = employeesResponse.Select(employee =>
            {
                var employeeDTO = new EmployeesDTO
                {
                    Name = $"{employee.firstName} {employee.lastName}"
                };

                if (!string.IsNullOrEmpty(employee.phone))
                {
                    employeeDTO.AddPhone(employee.phone);
                }
                else
                {
                    employeeDTO.AddPhone("-");
                }

                return employeeDTO;

            }).Cast<DataTransferObject>();

            if (args != null ) 
            { 
                return args.Concat(employees);
            }

            return employees;
        }

        private async Task<List<EmployeeResponse>> GetUsersAsync()
        {
            var response = await _httpClient.GetStringAsync("https://dummyjson.com/users");
            var usersData = JsonConvert.DeserializeObject<EmployeesApiResponse>(response);
            return usersData.Users;
        }
    }
}
