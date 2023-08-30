﻿using System;
using System.Collections.Generic;

namespace ex1.Models;

public partial class Fabricante
{
    public int Codigo { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
}
