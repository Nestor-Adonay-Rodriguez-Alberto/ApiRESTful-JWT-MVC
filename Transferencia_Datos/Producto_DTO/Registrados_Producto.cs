using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Transferencia_Datos.Rol_DTO.Registrados_Rol;

namespace Transferencia_Datos.Producto_DTO
{
    internal class Registrados_Producto
    {
        // CLASE:
        public class Producto
        {
            // ATRIBUTOS:
            public int IdProducto { get; set; }

            public string Nombre { get; set; }

            public byte[]? Fotografia { get; set; }

        }

        // ALMACENA TODOS LOS PRODUCTOS DE LA DB:
        public List<Producto> Lista_Productos { get; set; }


        // CONSTRUCTOR:
        public Registrados_Producto()
        {
            Lista_Productos = new List<Producto>();
        }

    }
}
