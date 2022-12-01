using System;
using System.Collections.Generic;

namespace DL;

public partial class Proovedor
{
    public int IdProovedor { get; set; }

    public string Telefono { get; set; } = null!;

    public string? Nombre { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
