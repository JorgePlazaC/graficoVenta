using System;
using System.Collections.Generic;

namespace Proyecto1;

public partial class VentaProducto
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public int VentaId { get; set; }

    public int Cantidad { get; set; }

    public int Precio { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Ventum Venta { get; set; } = null!;
}
