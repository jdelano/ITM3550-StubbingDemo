using System;
using StubbingDemo.Database.Models;

namespace StubbingDemo.Repositories;

public interface IShipperRepository
{
    Task<Shipper> CreateShipperAsync(string companyName, string phone);
    Task<bool> DeleteShipperAsync(Shipper shipper);
    Task<Shipper> GetShipperByIdAsync(int shipperId);
    IEnumerable<Shipper> GetShippers();
    Task SaveChangesAsync();
}
