using System;
using System.Collections.Generic;

namespace LoginForm.Models;

public partial class Promocode
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Discount { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
