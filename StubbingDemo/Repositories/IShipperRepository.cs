using System;
using StubbingDemo.Database.Models;

namespace StubbingDemo.Repositories;

public interface IShipperRepository
{
    Task CreateShipperAsync(int shipperId, string companyName, string phone);
    Task<Shipper> GetShipperByIdAsync(int shipperId);
    IEnumerable<Shipper> GetShippers();
}
