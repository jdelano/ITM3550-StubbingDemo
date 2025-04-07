using System;
using System.Collections.Generic;

namespace StubbingDemo.Database.Models;

public partial class CustomerSale
{
    public string? CustomerId { get; set; }

    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? OrderTotal { get; set; }
}
