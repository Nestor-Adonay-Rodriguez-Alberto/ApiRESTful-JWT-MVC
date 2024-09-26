using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transferencia_Datos.Producto_DTO
{
    public class Obtener_Producto
    {
        // ATRIBUTOS:
        public int IdProducto { get; set; }

        public string Nombre { get; set; }

        public byte[]? Fotografia { get; set; }

    }
}
