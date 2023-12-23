using System;
using System.Collections.Generic;

namespace LoginForm.Models;

public partial class Seat
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int HallId { get; set; }

    public int Row { get; set; }

    public int Number { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Hall Hall { get; set; } = null!;

    public virtual ICollection<OrderSeat> OrderSeats { get; set; } = new List<OrderSeat>();
}
