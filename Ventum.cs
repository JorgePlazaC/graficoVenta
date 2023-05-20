using System;
using System.Collections.Generic;

namespace Proyecto1;

public partial class Ventum
{
    public int Id { get; set; }

    public int Correlativo { get; set; }

    public DateTime Fecha { get; set; }

    public TimeSpan Hora { get; set; }

    public string RutCliente { get; set; } = null!;

    public virtual ICollection<VentaProducto> VentaProductos { get; set; } = new List<VentaProducto>();
}
