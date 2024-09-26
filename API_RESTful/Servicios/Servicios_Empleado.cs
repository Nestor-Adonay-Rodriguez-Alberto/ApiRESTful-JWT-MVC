using API_RESTful.Modelos;
using Microsoft.EntityFrameworkCore;
using Transferencia_Datos.Empleado_DTO;


namespace API_RESTful.Servicios
{
    public class Servicios_Empleado
    {
        // Representa La DB:
        private readonly MyDBcontext _MyDBcontext;


        // Constructor:
        public Servicios_Empleado(MyDBcontext myDBcontext)
        {
            _MyDBcontext = myDBcontext;
        }



        // **************** METODOS QUE MANDARAN OBJETOS *****************
        // *****************************************************************

        // OBTIENE TODOS LOS REGISTROS DE LA DB:
        public async Task<List<Empleado>> Obtner_Todos()
        {
            List<Empleado> Lista_Empleados = await _MyDBcontext.Empleados.ToListAsync();

            return Lista_Empleados;
        }


        // OBTIENE UN REGISTRO CON EL MISMO ID:
        public async Task<Empleado> Obtener_PorId(int id)
        {
            Empleado? Objeto_Obtenido = await _MyDBcontext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

            return Objeto_Obtenido;
        }


        // OBTIENE TODOS LOS REGISTROS DE Roles DE LA DB:
        public async Task<List<Rol>> Roles_Registrados()
        {
            List<Rol> Lista_Roles = await _MyDBcontext.Roles.ToListAsync();

            return Lista_Roles;
        }





        // *******  METODOS QUE RECIBIRAN OBJETOS Y MODIFICARAN LA DB  *******
        // ********************************************************************

        // RECIBE UN OBJETO Y LO GUARDA EN LA DB:
        public async Task<int> Registrar_Empleado(Crear_Empleado crear_Empleado)
        {
            // Encriptamos Password:
            crear_Empleado.Password = BCrypt.Net.BCrypt.HashPassword(crear_Empleado.Password);

            // Objeto a Mapear:
            Empleado empleado = new Empleado
            {
                Nombre=crear_Empleado.Nombre,
                Salaraio=crear_Empleado.Salaraio,
                Telefono=crear_Empleado.Telefono,
                Email=crear_Empleado.Email,
                Password=crear_Empleado.Password,
                IdRolEnEmpleado=crear_Empleado.IdRolEnEmpleado
            };


            _MyDBcontext.Add(empleado);

            return await _MyDBcontext.SaveChangesAsync();
        }


        // BUSCA UN REGISTRO CON EL MISMO ID EN LA DB Y LO MODIFICA
        public async Task<int> Editar_Empleado(Editar_Empleado editar_Empleado)
        {
            Empleado? Objeto_Obtenido = await _MyDBcontext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == editar_Empleado.IdEmpleado);

            if (Objeto_Obtenido == null)
            {
                return 0;
            }

            // Modificamos:
            Objeto_Obtenido.Nombre = editar_Empleado.Nombre;
            Objeto_Obtenido.Salaraio = editar_Empleado.Salaraio;
            Objeto_Obtenido.Telefono = editar_Empleado.Telefono;
            Objeto_Obtenido.Email=editar_Empleado.Email;
            Objeto_Obtenido.IdRolEnEmpleado = editar_Empleado.IdRolEnEmpleado;

            _MyDBcontext.Update(Objeto_Obtenido);

            return await _MyDBcontext.SaveChangesAsync();
        }


        // BUSCA UN REGISTRO CON EL MISMO ID EN LA DB Y LO ELIMINA:
        public async Task<int> Eliminar_Empleado(int id)
        {
            Empleado? Objeto_Obtenido = await _MyDBcontext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

            if (Objeto_Obtenido == null)
            {
                return 0;
            }

            _MyDBcontext.Remove(Objeto_Obtenido);

            return await _MyDBcontext.SaveChangesAsync();
        }


        // BUSCA UN REGISTRO CON EL MISMO ID EN LA DB Y MODIFICA CONTRASEÑA
        public async Task<int> Editar_Contraseña(Editar_Contraseña editar_Contraseña)
        {
            Empleado? Objeto_Obtenido = await _MyDBcontext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == editar_Contraseña.IdEmpleado);

            if (Objeto_Obtenido == null)
            {
                return 0;
            }

            //Encriptamos Password:
            editar_Contraseña.Password = BCrypt.Net.BCrypt.HashPassword(editar_Contraseña.Password);

            // Modificamos:
            Objeto_Obtenido.Password = editar_Contraseña.Password;

            _MyDBcontext.Update(Objeto_Obtenido);

            return await _MyDBcontext.SaveChangesAsync();
        }

    }
}
