﻿using System;
using System.Collections.Generic;

namespace ex4.Models;

public partial class Sala
{
    public int Codigo { get; set; }

    public string? Nombre { get; set; }

    public int? Pelicula { get; set; }

    public virtual Pelicula? PeliculaNavigation { get; set; }
}
