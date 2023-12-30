using Castle.Core.Resource;
using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Services;


public class MenuService
{
    private readonly CustomerService _customerService = new CustomerService();


    // visar meny så du ser olika val angående kunder
    public void ShowMainMenu()
    {
        while (true)
        {
            DisplayMenuTitle("Menu Options");
            Console.WriteLine($"{"1.",-4} Add New Customer");
            Console.WriteLine($"{"2.",-4} View Customer List");
            Console.WriteLine($"{"0.",-4} Exit Application");
            Console.WriteLine();
            Console.Write("Enter Menu Option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ShowAddCustomerOption();
                    break;
                case "2":
                    ShowViewCustomerListOption();
                    break;
                case "0":
                    ShowExitApplicationOption();
                    break;

                default:
                    Console.WriteLine("\nInvalid Option Selected. Press any key to try again.");
                    break;
            }

            Console.ReadKey();
        }
    }

    // detta är hur du lämnar konsolen 
    private void ShowExitApplicationOption()
    {
        Console.Clear();
        Console.Write("Are you sure you want to exit? (y/n): ");
        var option = Console.ReadLine() ?? "";

        if (option.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            Environment.Exit(0);
        }
    }

    // lägga till detaljer som en kund
    private void ShowAddCustomerOption()
    {
        Customer customer = new Customer();

        DisplayMenuTitle("Add New Customer");

        Console.WriteLine("First Name: ");
        customer.FirstName = Console.ReadLine()!;

        Console.WriteLine("Last Name: ");
        customer.LastName = Console.ReadLine()!;

        Console.WriteLine("Email: ");
        customer.Email = Console.ReadLine()!;

        Console.WriteLine("StreetName: ");
        customer.StreetName = Console.ReadLine()!;

        Console.WriteLine("PostalCode: ");
        customer.PostalCode = Console.ReadLine()!;

        Console.WriteLine("City: ");
        customer.City = Console.ReadLine()!;

        var res = _customerService.AddCustomerToList(customer);

        switch (res.Status)
        {
            case Enums.ServiceStatus.SUCCESSED:
                Console.WriteLine("The customer has been added.");
                break;

            case Enums.ServiceStatus.ALREADY_EXISTS:
                Console.WriteLine("The customer already exist.");
                break;

            case Enums.ServiceStatus.FAILED:
                Console.WriteLine("Failed trying to add customer to list.");
                Console.WriteLine("See error message ::" + res.Result.ToString());
                break;
        }

        DisplayPressAnyKey();
    }

    // titta på kund detailjerna
    private void ShowViewCustomerListOption()
    {
        DisplayMenuTitle("Customer List");
        var list = _customerService.GetAllFromList();

        if (!list.Any())
        {
            Console.WriteLine("No customer found.");
        }
        else
        {
            foreach (var customer in list)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName} <{customer.Email}>");
                Console.WriteLine();
            }

            Console.WriteLine($"{"1.",-4} Show Customer Details");
            Console.WriteLine($"{"2.",-4} Return To Main Menu");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    foreach (var customer in list)
                    {
                        Console.WriteLine($"{customer.FirstName} {customer.LastName} <{customer.Email}> {customer.StreetName} {customer.PostalCode} {customer.City}");
                    }
                    break;

                case "2":
                    DisplayPressAnyKey();
                    break;

                default:
                    Console.WriteLine("\nInvalid Option Selected. Press any key to try again.");
                    break;
            }
        }
    }

    // rubrik på meny
    private void DisplayMenuTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"#### {title} ####");
        Console.WriteLine();
    }

    // hjälper att gå tillbaka
    private void DisplayPressAnyKey()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
        ShowMainMenu();
    }
}