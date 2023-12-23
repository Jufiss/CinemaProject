using System;
using System.Collections.Generic;

namespace LoginForm.Models;

public partial class Hall
{
    public int Id { get; set; }

    public int Number { get; set; }

    public int NumberSeats { get; set; }

    public int NumberRows { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
