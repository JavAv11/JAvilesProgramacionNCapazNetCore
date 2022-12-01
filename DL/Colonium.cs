using System;
using System.Collections.Generic;

namespace DL;

public partial class Colonium
{
    public string Nombre { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public int? IdMunicipio { get; set; }

    public int IdColonia { get; set; }

    public virtual ICollection<Direccion> Direccions { get; } = new List<Direccion>();

    public virtual Municipio? IdMunicipioNavigation { get; set; }
}
