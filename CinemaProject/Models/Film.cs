using System;
using System.Collections.Generic;

namespace LoginForm.Models;

public partial class Film
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int GenreId { get; set; }

    public int Duration { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public byte[]? Poster { get; set; }

    public string? Director { get; set; }

    public string? Plot { get; set; }

    public string? Country { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
