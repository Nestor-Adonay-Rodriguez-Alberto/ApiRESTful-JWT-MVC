﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Transferencia_Datos.Empleado_DTO;
using Transferencia_Datos.Rol_DTO;

namespace UI_MVC.Controllers
{
    public class EmpleadoController : Controller
    {
        // Para Hacer Solicitudes Al Servidor:
        private readonly HttpClient _HttpClient;


        // Constructor:
        public EmpleadoController(IHttpClientFactory httpClientFactory)
        {
            _HttpClient = httpClientFactory.CreateClient("API_RESTful");
        }




        // **************** METODOS QUE MANDARAN OBJETOS *****************
        // *****************************************************************

        // OBTIENE TODOS LOS REGISTROS DE LA DB:
        public async Task<IActionResult> Empleados_Registrados()
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenidos = await _HttpClient.GetAsync("/api/Empleado");

            // OBJETO:
            Registrados_Empleado Lista_Empleados = new Registrados_Empleado();

            // True=200-299
            if (JSON_Obtenidos.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Lista_Empleados = await JSON_Obtenidos.Content.ReadFromJsonAsync<Registrados_Empleado>();
            }

            return View(Lista_Empleados);
        }


        // OBTIENE UN REGISTRO CON EL MISMO ID:
        public async Task<IActionResult> Detalle_Empleado(int id)
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenido = await _HttpClient.GetAsync("/api/Empleado/" + id);

            // OBJETO:
            Obtener_Empleado Objeto_Obtenido = new Obtener_Empleado();

            // True=200-299
            if (JSON_Obtenido.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Objeto_Obtenido = await JSON_Obtenido.Content.ReadFromJsonAsync<Obtener_Empleado>();
            }

            return View(Objeto_Obtenido);
        }


        // OBTIENE TODOS LOS REGISTROS Rol DE LA DB Para ViewData:
        private async Task<Registrados_Rol> Lista_Roles()
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenidos = await _HttpClient.GetAsync("/api/Empleado/Roles_Registrados");

            // OBJETO:
            Registrados_Rol Lista_Roles = new Registrados_Rol();

            // True=200-299
            if (JSON_Obtenidos.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Lista_Roles = await JSON_Obtenidos.Content.ReadFromJsonAsync<Registrados_Rol>();
            }


            return Lista_Roles;
        }





        // *******  METODOS QUE RECIBIRAN OBJETOS Y MODIFICARAN LA DB  *******
        // ********************************************************************

        // OBTIENE LOS ROLES Y LOS MANDA EN UN VIEWDATA:
        public async Task<ActionResult> Registrar_Empleado()
        {
            Registrados_Rol Objeto_Obtenido = await Lista_Roles();
            ViewData["Lista_Roles"] = new SelectList(Objeto_Obtenido.Lista_Roles, "IdRol", "Nombre");

            return View();
        }


        // RECIBE UN OBJETO Y LO GUARDA EN LA DB:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar_Empleado(Crear_Empleado crear_Empleado)
        {
            // Solicitud POST al Endpoint de la API:
            HttpResponseMessage Respuesta = await _HttpClient.PostAsJsonAsync("/api/Empleado", crear_Empleado);

            // True=200-299
            if (Respuesta.IsSuccessStatusCode)
            {
                // Volemos a Vista Principal:
                return RedirectToAction("Empleados_Registrados", "Empleado");
            }

            return View();
        }


        // BUSCA UN REGISTRO CON EL MISMO ID EN LA DB Y LO MANDA A VISTA
        public async Task<IActionResult> Editar_Empleado(int id)
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenido = await _HttpClient.GetAsync("/api/Empleado/" + id);

            // OBJETO:
            Obtener_Empleado Objeto_Obtenido = new Obtener_Empleado();

            // True=200-299
            if (JSON_Obtenido.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Objeto_Obtenido = await JSON_Obtenido.Content.ReadFromJsonAsync<Obtener_Empleado>();
            }

            Editar_Empleado Objeto_Editar = new Editar_Empleado
            {
                IdEmpleado = Objeto_Obtenido.IdEmpleado,
                Nombre = Objeto_Obtenido.Nombre,
                Salaraio = Objeto_Obtenido.Salaraio,
                Telefono = Objeto_Obtenido.Telefono,
                Email = Objeto_Obtenido.Email,
                IdRolEnEmpleado = Objeto_Obtenido.IdRolEnEmpleado
            };

            // Para Seleccionar Roles:
            Registrados_Rol Objeto_Rol = await Lista_Roles();
            ViewData["Lista_Roles"] = new SelectList(Objeto_Rol.Lista_Roles, "IdRol", "Nombre", Objeto_Editar.IdRolEnEmpleado);

            return View(Objeto_Editar);
        }


        // RECIBE EL OBJETO MODIFICADO Y LO MODIFICA EN DB:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar_Empleado(Editar_Empleado editar_Empleado)
        {
            // Solicitud PUT al Endpoint de la API:
            HttpResponseMessage Respuesta = await _HttpClient.PutAsJsonAsync("/api/Empleado", editar_Empleado);

            // True=200-299
            if (Respuesta.IsSuccessStatusCode)
            {
                // Volemos a Vista Principal:
                return RedirectToAction("Empleados_Registrados", "Empleado");
            }

            return View();
        }


        // BUSCA UN REGISTRO CON EL MISMO ID EN LA DB Y LO MANDA A VISTA:
        public async Task<IActionResult> Eliminar_Empleado(int id)
        {
            // Solicitud GET al Endpoint de la API:
            HttpResponseMessage JSON_Obtenido = await _HttpClient.GetAsync("/api/Empleado/" + id);

            // OBJETO:
            Obtener_Empleado Objeto_Obtenido = new Obtener_Empleado();

            // True=200-299
            if (JSON_Obtenido.IsSuccessStatusCode)
            {
                // Deserializamos el Json:
                Objeto_Obtenido = await JSON_Obtenido.Content.ReadFromJsonAsync<Obtener_Empleado>();
            }

            return View(Objeto_Obtenido);
        }


        // BUSCA UN REGISTRO CON EL MISMO ID EN LA DB Y LO ELIMINA:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar_Empleado(Obtener_Empleado obtener_Empleado)
        {
            // Solicitud DELETE al Endpoint de la API:
            HttpResponseMessage Respuesta = await _HttpClient.DeleteAsync("/api/Empleado/" + obtener_Empleado.IdEmpleado);

            // True=200-299
            if (Respuesta.IsSuccessStatusCode)
            {
                // Volemos a Vista Principal:
                return RedirectToAction("Empleados_Registrados", "Empleado");
            }

            return View();
        }


    }
}
