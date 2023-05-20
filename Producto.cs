using System;
using System.Collections.Generic;

namespace Proyecto1;

public partial class Producto
{
    public int Id { get; set; }

    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Precio { get; set; }

    public virtual ICollection<VentaProducto> VentaProductos { get; set; } = new List<VentaProducto>();
}
