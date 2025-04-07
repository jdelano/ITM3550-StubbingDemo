using System;
using System.Collections.Generic;

namespace StubbingDemo.Database.Models;

public partial class CustomersWhoDidntOrder
{
    public string CustomerId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public DateTime? OrderDate { get; set; }
}
