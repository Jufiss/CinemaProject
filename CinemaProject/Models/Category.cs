using System;
using System.Collections.Generic;

namespace LoginForm.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Cost { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
