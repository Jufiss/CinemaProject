using System;
using System.Collections.Generic;

namespace LoginForm.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int CategoryId { get; set; }

    public byte[]? Image { get; set; }

    public virtual UserCategory Category { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
