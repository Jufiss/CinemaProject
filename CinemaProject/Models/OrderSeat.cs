using System;
using System.Collections.Generic;

namespace LoginForm.Models;

public partial class OrderSeat
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int SeatId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Seat Seat { get; set; } = null!;
}
