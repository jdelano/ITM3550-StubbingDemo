using System;
using StubbingDemo.Database.Models;
using StubbingDemo.Exceptions;
using StubbingDemo.Repositories;

namespace StubbingDemo.Services;

public interface IShipperService
{
    Task<bool> CreateShipperAsync(int shipperId, string companyName, string phone);
    Task<Shipper> GetShipperByIdAsync(int shipperId);
    IEnumerable<Shipper> GetShippers();
}

public class ShipperService : IShipperService
{
    private readonly IShipperRepository _shipperRepository;
    public ShipperService(IShipperRepository shipperRepository)
    {
        _shipperRepository = shipperRepository;
    }

    public async Task<bool> CreateShipperAsync(int shipperId, string companyName, string phone)
    {
        await _shipperRepository.CreateShipperAsync(shipperId, companyName, phone);
        return true;
    }

    public async Task<Shipper> GetShipperByIdAsync(int shipperId)
    {
        try
        {
            return await _shipperRepository.GetShipperByIdAsync(shipperId);

        }
        catch (ArgumentException ex)
        {
            throw new ShipperNotFoundException("Shipper not found", ex);
        }
    }

    public IEnumerable<Shipper> GetShippers()
    {
        foreach (var shipper in _shipperRepository.GetShippers())
        {
            yield return shipper;
        }
    }
}
