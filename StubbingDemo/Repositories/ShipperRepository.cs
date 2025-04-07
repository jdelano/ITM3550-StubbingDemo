using System;
using StubbingDemo.Database.Models;

namespace StubbingDemo.Repositories;

public class ShipperRepository : IShipperRepository
{
    private readonly NorthwindContext _context;
    public ShipperRepository(NorthwindContext context) 
    {
        _context = context;
    }

    public async Task CreateShipperAsync(int shipperId, string companyName, string phone)
    {
        var shipper = new Shipper
        {
            ShipperId = shipperId,
            CompanyName = companyName,
            Phone = phone
        };
        try
        {
            await _context.Shippers.AddAsync(shipper);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new CouldNotAddToDatabaseException("Could not add shipper to database", ex);
        }
    }

    public async Task<Shipper> GetShipperByIdAsync(int shipperId)
    {
        return await _context.Shippers.FindAsync(shipperId) ?? throw new ArgumentException("Shipper not found");
        // var shipper = await _context.Shippers.FindAsync(shipperId);

        // if (shipper == null)
        // {
        //     throw new ArgumentException("Shipper not found");
        // }
        // else
        // {
        //     return shipper;
        // }
    }

    public IEnumerable<Shipper> GetShippers()
    {
        // return _context.Shippers;
        foreach (var shipper in _context.Shippers)
        {
            yield return shipper;
        }
    }
}
