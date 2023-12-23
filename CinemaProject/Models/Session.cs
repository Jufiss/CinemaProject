using System;
using System.Collections.Generic;

namespace LoginForm.Models;

public partial class Session
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int FilmId { get; set; }

    public int HallId { get; set; }

    public virtual Film Film { get; set; } = null!;

    public virtual Hall Hall { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
