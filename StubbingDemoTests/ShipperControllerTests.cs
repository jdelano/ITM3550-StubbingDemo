using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Moq;
using NUnit.Framework;
using StubbingDemo.Controllers;
using StubbingDemo.Database.Models;
using StubbingDemo.Database.Models.Dtos;
using StubbingDemo.Services;

namespace StubbingDemoTests;

public class ShipperControllerTests
{
    private Mock<IShipperService> _mockShipperService;

    [SetUp]
    public void Setup()
    {
        _mockShipperService = new Mock<IShipperService>();
    }

    [Test]
    public void GetShippers_ShouldReturnShippers()
    {
        _mockShipperService
            .Setup(svc => svc.GetShippers())
            .Returns(new List<Shipper>
            {
                new Shipper { ShipperId = 1, CompanyName = "Microsoft", Phone = "111-2222"},
                new Shipper { ShipperId = 2, CompanyName = "Google", Phone = "111-3333"},
                new Shipper { ShipperId = 3, CompanyName = "Amazon", Phone = "111-4444"},
                new Shipper { ShipperId = 4, CompanyName = "Apple", Phone = "111-5555"},
            });

        var controller = new ShipperController(_mockShipperService.Object);
        var result = controller.Get();
        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        var value = okResult.Value as IEnumerable<Shipper>;
        Assert.That(value, Is.Not.Null);
        var list = value.ToList();
        Assert.That(list.Count(), Is.EqualTo(4));
    }

    [Test]
    public async Task Get_ShouldReturnShipper_OnSucces()
    {
        _mockShipperService
            .Setup(svc => svc.GetShipperByIdAsync(1))
            .ReturnsAsync(new  ShipperDto { ShipperId = 1, CompanyName = "Microsoft", Phone = "111-2222"});

        var controller = new ShipperController(_mockShipperService.Object);
        var actionResult = await controller.Get(1);
        var resultObject = actionResult as OkObjectResult;
        Assert.That(resultObject, Is.Not.Null);
        var shipper = resultObject.Value as ShipperDto;
        Assert.That(shipper, Is.Not.Null);
        Assert.That(shipper.CompanyName, Is.EqualTo("Microsoft"));
    }

    [Test]
    public async Task Post_ShouldReturnCreatedShipper()
    {
        var shipperDto = new ShipperDto { ShipperId = 1, CompanyName = "Microsoft", Phone = "111-2222" };
        _mockShipperService
            .Setup(svc => svc.CreateShipperAsync(shipperDto.CompanyName, shipperDto.Phone))
            .ReturnsAsync(shipperDto);

        var controller = new ShipperController(_mockShipperService.Object);
        var actionResult = await controller.Post(shipperDto);
        var result = actionResult as CreatedAtActionResult;
        
    }
}
