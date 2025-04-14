using StubbingDemo.Services;
using Moq;
using StubbingDemo.Repositories;
using StubbingDemo.Database.Models;
using StubbingDemo.Exceptions;

namespace StubbingDemoTests;

public class ShipperServiceTests
{
    private Mock<IShipperRepository> _mockShipperRepository;
    
    [SetUp]
    public void Setup()
    {
        _mockShipperRepository = new Mock<IShipperRepository>();
    }

    [Test]
    public void Constructor_Test()
    {
        var service = new ShipperService(_mockShipperRepository.Object);
        Assert.That(service, Is.Not.Null);
    }

    [Test]
    public async Task GetShipperByIdAsync_ShouldReturnShipper_WhenShipperExists()
    {
        _mockShipperRepository
            .Setup(repo => repo.GetShipperByIdAsync(1))
            .ReturnsAsync(new Shipper
            {
                ShipperId = 1,
                CompanyName = "Test Company",
                Phone = "123-456-7890"
            });

        var service = new ShipperService(_mockShipperRepository.Object);
        var shipper = await service.GetShipperByIdAsync(1);
        Assert.That(shipper, Is.Not.Null);
        Assert.That(shipper.ShipperId, Is.EqualTo(1));
        Assert.That(shipper.CompanyName, Is.EqualTo("Test Company"));
        Assert.That(shipper.Phone, Is.EqualTo("123-456-7890"));
    }

    [Test]
    public void GetShipperByIdAsync_ShouldThrowException_WhenShipperDoesNotExist()
    {
        _mockShipperRepository
            .Setup(repo => repo.GetShipperByIdAsync(2))
            .ThrowsAsync(new ArgumentException("Shipper not found"));

        var service = new ShipperService(_mockShipperRepository.Object);
        Assert.ThrowsAsync<ShipperNotFoundException>(() => service.GetShipperByIdAsync(2));
    }

    [Test]
    public async Task CreateShipperAsync_ShouldCallRepository()
    {
       _mockShipperRepository
            .Setup(repo => repo.CreateShipperAsync("Test Company", "123-456-7890"))
            .ReturnsAsync(new Shipper { ShipperId = 1, CompanyName = "Test Company", Phone = "123-456-7890" });

        var service = new ShipperService(_mockShipperRepository.Object);
        var result = await service.CreateShipperAsync("Test Company", "123-456-7890");

        _mockShipperRepository.Verify(repo => repo.CreateShipperAsync("Test Company", "123-456-7890"), Times.Once);
    }

    [Test]
    public void GetShippers_ShouldReturnShippers()
    {
        _mockShipperRepository
            .Setup(repo => repo.GetShippers())
            .Returns(new List<Shipper> 
            {
                new Shipper { ShipperId = 1, CompanyName = "Microsoft", Phone = "111-2222"},
                new Shipper { ShipperId = 2, CompanyName = "Google", Phone = "111-3333"},
                new Shipper { ShipperId = 3, CompanyName = "Amazon", Phone = "111-4444"},
                new Shipper { ShipperId = 4, CompanyName = "Apple", Phone = "111-5555"},
            });
        var service = new ShipperService(_mockShipperRepository.Object);
        var results = service.GetShippers();
        Assert.That(results, Is.Not.Null);
        List<Shipper> resultsList = results.ToList();
        Assert.That(resultsList, Has.Count.EqualTo(4));
        Assert.That(resultsList[1].CompanyName, Is.EqualTo("Google"));

    }
}
