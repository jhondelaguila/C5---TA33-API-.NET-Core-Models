using System;
using System.Collections.Generic;

namespace ex3.Models;

public partial class Almacene
{
    public int Codigo { get; set; }

    public string? Lugar { get; set; }

    public int? Capacidad { get; set; }

    public virtual ICollection<Caja> Cajas { get; set; } = new List<Caja>();
}
