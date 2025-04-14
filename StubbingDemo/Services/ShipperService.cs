using System;
using StubbingDemo.Database.Models;
using StubbingDemo.Database.Models.Dtos;
using StubbingDemo.Exceptions;
using StubbingDemo.Repositories;

namespace StubbingDemo.Services;

public interface IShipperService
{
    Task<ShipperDto> CreateShipperAsync(string companyName, string phone);
    Task<bool> DeleteShipperAsync(int id);
    Task<ShipperDto> GetShipperByIdAsync(int shipperId);
    IEnumerable<Shipper> GetShippers();
    Task<bool> UpdateShipperAsync(ShipperDto dto);
}

public class ShipperService : IShipperService
{
    private readonly IShipperRepository _shipperRepository;
    public ShipperService(IShipperRepository shipperRepository)
    {
        _shipperRepository = shipperRepository;
    }

    public async Task<ShipperDto> CreateShipperAsync(string companyName, string phone)
    {
        var shipper = await _shipperRepository.CreateShipperAsync(companyName, phone);
        return new ShipperDto
        {
            ShipperId = shipper.ShipperId,
            CompanyName = shipper.CompanyName,
            Phone = shipper.Phone ?? ""
        };
    }

    public async Task<bool> DeleteShipperAsync(int id)
    {
        var shipper = await _shipperRepository.GetShipperByIdAsync(id);
        if (shipper == null) return false;

        _shipperRepository.DeleteShipperAsync(shipper);
        await _shipperRepository.SaveChangesAsync();

        return true;
    }

    public async Task<ShipperDto> GetShipperByIdAsync(int shipperId)
    {
        try
        {
            var shipper = await _shipperRepository.GetShipperByIdAsync(shipperId);
            return new ShipperDto
            {
                ShipperId = shipper.ShipperId,
                CompanyName = shipper.CompanyName,
                Phone = shipper.Phone ?? ""
            };
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

    public async Task<bool> UpdateShipperAsync(ShipperDto dto)
    {
        var shipper = await _shipperRepository.GetShipperByIdAsync(dto.ShipperId);
        if (shipper == null) return false;

        shipper.CompanyName = dto.CompanyName;
        shipper.Phone = dto.Phone;

        await _shipperRepository.SaveChangesAsync();
        return true;
    }
}
