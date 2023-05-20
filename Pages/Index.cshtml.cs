﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;

namespace Proyecto1.Pages;

public class IndexModel : PageModel
{

    public List<VentaProducto> ventasDias;
    public List<Producto> productos;
    public List<Ventum> ventum;
    public List<Producto> productosVendidos;
    public List<Decimal> sumaTotalPorFecha; 
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        VentasContext context = new VentasContext();
        ventasDias = context.VentaProductos.ToList();
        productos = context.Productos.ToList();
        ventum = context.Venta.ToList();

        DateTime fechaDeterminada = new DateTime(2023, 5, 20); // Fecha determinada que deseas consultar
        DateTime fechaInicio = new DateTime(2023, 5, 20);
        DateTime fechaFin = new DateTime(2023, 5, 31);

        productosVendidos = context.VentaProductos
            .Where(vp => vp.Venta.Fecha.Date == fechaDeterminada.Date)
            .Select(vp => vp.Producto)
            .ToList();

        
        sumaTotalPorFecha = context.VentaProductos
            .Where(vp => vp.Venta.Fecha >= fechaInicio && vp.Venta.Fecha <= fechaFin)
            .GroupBy(vp => vp.Venta.Fecha.Date)
            .Select(g => g.Sum(vp => vp.Precio))
            .ToList();
        
    }
}
