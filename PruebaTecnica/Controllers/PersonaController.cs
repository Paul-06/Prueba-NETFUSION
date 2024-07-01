using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Data;
using PruebaTecnica.Models;
using PruebaTecnica.Models.DTO;

namespace PruebaTecnica.Controllers
{
    public class PersonaController : Controller
    {
        private readonly PersonaService _service = new();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var persona = new Persona();

            if (id is null)
            {
                return View(persona);
            }

            persona = await _service.GetById((int) id);

            if (persona is null)
            {
                return NotFound();
            }

            return View(persona);
        }


        [HttpGet]
        public async Task<JsonResult> ListarPersonas()
        {
            var cargos = await _service.GetAll();
            var resultado = cargos.Select(c => c.ToDto());

            return new JsonResult(new { data = resultado });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Sirve para evitar solicitudes externas
        public async Task<IActionResult> Upsert(Persona persona)
        {
            if (ModelState.IsValid) // Si el modelo es válido
            {
                if (persona.Id == 0) // Significa un nuevo registro
                {
                    await _service.Add(persona);
                    TempData["Mensaje"] = "Persona agregada exitosamente"; // Será usado para las notificaciones
                }
                else
                {
                    await _service.Update(persona);
                    TempData["Mensaje"] = "Persona actualizada exitosamente"; // Será usado para las notificaciones
                }

                return RedirectToAction(nameof(Index)); // Redirigir al Index
            }

            // Si no se hace nada
            TempData["Mensaje"] = "Error al guardar los cambios"; // Notificación de error
            return View(persona);
        }

        [HttpDelete]
        public async Task<JsonResult> Eliminar(int id)
        {
            // Buscamos el registro a eliminar
            var registro = await _service.GetById(id);

            if (registro is null)
            {
                return new JsonResult(new { success = false, message = "Error al borrar a la persona" });
            }

            // En caso se encuentre el registro
            await _service.Delete(registro.Id);

            // Enviamos el mensaje de éxito
            return new JsonResult(new { success = true, message = "Persona eliminada exitosamente" });
        }
    }
}
