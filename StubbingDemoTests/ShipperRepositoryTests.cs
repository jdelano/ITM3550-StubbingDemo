using NUnit.Framework;
using StubbingDemo;
using StubbingDemo.Database.Models;
using StubbingDemo.Repositories;
using StubbingDemoTests.Stubs;

namespace StubbingDemoTests;

public class ShipperRepositoryTests
{
    private NorthwindContext_Stub _context;
    private ShipperRepository _shipperRepository;


    [SetUp]
    public void Setup()
    {
        _context = new NorthwindContext_Stub();
        _context.Database.EnsureDeleted();
        _shipperRepository = new ShipperRepository(_context);
    }

    [Test]
    public void ShipperRepository_Constructor_IsNotNull()
    {
        Assert.That(_shipperRepository, Is.Not.Null);
    }

    [Test]
    public void ShipperRepository_CreateShipper_ShouldThrowCorrectExceptionOnDatabaseFailure()
    {
        _context.CauseError = true;
        Assert.ThrowsAsync<CouldNotAddToDatabaseException>(() => _shipperRepository.CreateShipperAsync(2, "Test", "111-2222"));
    }

    [Test]
    public async Task ShipperRepository_GetShipperByIDAsync_ShouldReturnCorrectShipper()
    {
        //Arrange
        var shipper = new Shipper
        {
            ShipperId = 1,
            CompanyName = "Test",
            Phone = "111-2222"
        };
        await _context.Shippers.AddAsync(shipper);
        await _context.SaveChangesAsync();


        //Act
        await _shipperRepository.GetShipperByIdAsync(1);

        //Assert
        Assert.That(shipper, Is.Not.Null);
        Assert.That(shipper.CompanyName, Is.EqualTo("Test"));
        Assert.That(shipper.Phone, Is.EqualTo("111-2222"));
        Assert.That(shipper.ShipperId, Is.EqualTo(1));
    }

    [Test]
    public void ShipperRepository_GetShipperByIdAsync_ShouldThrowExceptionWhenShipperIdNotFound()
    {
        //Arrange
        var shipper = new Shipper
        {
            ShipperId = 1,
            CompanyName = "Test",
            Phone = "111-2222"
        };
        _context.Shippers.Add(shipper);
        _context.SaveChanges();
        //Act
        //Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await _shipperRepository.GetShipperByIdAsync(2));
    }

    [Test]
    public void GetShippers_ShouldReturnShippers()
    {
        var shipper1 = new Shipper { ShipperId = 1, CompanyName = "Microsoft", Phone = "111-2222"};
        var shipper2 = new Shipper { ShipperId =2, CompanyName ="Google", Phone = "111-3333"};
        _context.Shippers.Add(shipper1);
        _context.Shippers.Add(shipper2);
        _context.SaveChanges();
        IEnumerable<Shipper> shippers = _shipperRepository.GetShippers();
        Assert.That(shippers.Count(), Is.EqualTo(2));
        var shipperList = shippers.ToList();
        Assert.That(shipperList[1].CompanyName, Is.EqualTo("Google"));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}
