using System;
using StubbingDemo.Database.Models;
using StubbingDemo.Exceptions;
using StubbingDemo.Repositories;

namespace StubbingDemo.Services;

public class ShipperService
{
    private readonly IShipperRepository _shipperRepository;
    public ShipperService(IShipperRepository shipperRepository)
    {
        _shipperRepository = shipperRepository;
    }

    public async Task<bool> CreateShipperAsync(int v1, string v2, string v3)
    {
        await _shipperRepository.CreateShipperAsync(v1, v2, v3);
        return true;
    }

    public async Task<Shipper> GetShipperByIdAsync(int shipperId)
    {
        try
        {
            return await _shipperRepository.GetShipperByIdAsync(shipperId);

        } catch (ArgumentException ex)
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
