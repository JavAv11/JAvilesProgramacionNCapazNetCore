using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ML;

public partial class Rol
{
    [DisplayName("Rol")]
    public int IdRol { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();

    public List<object> NombreRol { get; set; }
}
