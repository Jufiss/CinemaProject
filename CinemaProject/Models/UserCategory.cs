using System;
using System.Collections.Generic;

namespace LoginForm.Models;

public partial class UserCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
