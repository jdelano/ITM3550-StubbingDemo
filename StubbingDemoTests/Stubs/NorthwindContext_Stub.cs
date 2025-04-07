using System;
using Microsoft.EntityFrameworkCore;
using StubbingDemo.Database.Models;

namespace StubbingDemoTests.Stubs;

public class NorthwindContext_Stub : NorthwindContext
{
    public bool CauseError { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Northwind");
    }

    public override int SaveChanges()
    {
        if (CauseError)
        {
            throw new Exception("Stubbed exception");
        }

        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (CauseError)
        {
            throw new Exception("Stubbed exception");
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
