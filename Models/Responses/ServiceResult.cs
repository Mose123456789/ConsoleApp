namespace ConsoleApp.Models.Responses;

using ConsoleApp.Enums;
using ConsoleApp.Interfaces;
using ConsoleApp.Models;

public class ServiceResult
{
    public ServiceStatus Status { get; set; }
    public object Result { get; set; } = null!;
}