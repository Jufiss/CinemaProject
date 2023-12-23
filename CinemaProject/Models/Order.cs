using System;
using System.Collections.Generic;

namespace LoginForm.Models;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SessionId { get; set; }

    public long Number { get; set; }

    public int? PromocodeId { get; set; }

    public double Sum { get; set; }
    public DateTime OrderDate { get; set; }

    public virtual ICollection<OrderSeat> OrderSeats { get; set; } = new List<OrderSeat>();

    public virtual Promocode? Promocode { get; set; }

    public virtual Session Session { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
