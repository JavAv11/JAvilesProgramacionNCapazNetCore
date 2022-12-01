﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal PrecioUnitario { get; set; }

    public int Stock { get; set; }

    public int? IdProovedor { get; set; }

    public int? IdDepartamento { get; set; }

    public string? Descripcion { get; set; }

    public string? Imagen { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual Proovedor? IdProovedorNavigation { get; set; }

    //Agregadas

    public string NombreProovedor { get; set; }
    public string NombreDepartamento { get; set; }

    public int? IdArea { get; set; }
    public string NombreArea { get; set; }
}
