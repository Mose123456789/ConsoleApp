using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Models.Responses;
using Newtonsoft.Json;
using System.Diagnostics;
namespace ConsoleApp.Services;

public class CustomerService
{
    private readonly FileService fileService = new FileService();
    private List<Customer> _customers = [];

    // detta är skapat för att tillägga till en ny användare till en text fil med hjälp av JSON.
    public ServiceResult AddCustomerToList(Customer customer)
    {
        ServiceResult response = new ServiceResult();

        try
        {
            if (!_customers.Any(x => x.Email == customer.Email))
            {
                _customers.Add(customer);

                var json = JsonConvert.SerializeObject(_customers);

                fileService.SaveConentToFile(json);
                response.Status = Enums.ServiceStatus.SUCCESSED;
            }
            else
            {
                response.Status = Enums.ServiceStatus.ALREADY_EXISTS;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Result = ex.Message;
        }

        return response;
    }

    // här hämtas specifik kund från filen
    public IEnumerable<Customer> GetCustomerFromList()
    {
        ServiceResult response = new ServiceResult();

        try
        {
            var content = fileService.GetContentFromFile();
            if (!string.IsNullOrEmpty(content))
            {
                _customers = JsonConvert.DeserializeObject<List<Customer>>(content)!;
            }


        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    // hämtar kund från fil
    public IEnumerable<Customer> GetAllFromList()
    {
        try
        {
            var content = fileService.GetContentFromFile();
            if (!string.IsNullOrEmpty(content))
            {
                _customers = JsonConvert.DeserializeObject<List<Customer>>(content)!;
            }

            return _customers;
        }

        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}