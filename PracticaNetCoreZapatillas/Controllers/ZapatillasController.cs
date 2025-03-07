using Microsoft.AspNetCore.Mvc;
using PracticaNetCoreZapatillas.Models;
using PracticaNetCoreZapatillas.Repositories;

namespace PracticaNetCoreZapatillas.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Zapatilla> zapas = await this.repo.GetAllZapatillasAsync();

            return View(zapas);
        }

        public async Task<IActionResult> Details(int id)
        {
            Zapatilla zapa = await this.repo.FindZapatillaByIdAsync(id);

            return View(zapa);
        }

        public async Task<IActionResult> ImagenesZapatilla(int id, int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            ModelImagenes model = await this.repo.GetImagenesZapatillaAsync(posicion.Value, id);

            int numReg = model.Registros;

            int siguiente = posicion.Value + 1;

            if (siguiente > numReg)
            {
                siguiente = numReg;
            }

            int anterior = posicion.Value - 1;

            if (anterior < 1)
            {
                anterior = 1;
            }

            ViewData["ultimo"] = numReg;
            ViewData["siguiente"] = siguiente;
            ViewData["anterior"] = anterior;
            ViewData["posicion"] = posicion;

            return View(model.Imagen);
        }

        public async Task<IActionResult> DetailsPartial(int id, int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            ModelImagenes model = await this.repo.GetImagenesZapatillaAsync(posicion.Value, id);

            int numReg = model.Registros;

            int siguiente = posicion.Value + 1;

            if (siguiente > numReg)
            {
                siguiente = numReg;
            }

            int anterior = posicion.Value - 1;

            if (anterior < 1)
            {
                anterior = 1;
            }

            ViewData["ultimo"] = numReg;
            ViewData["siguiente"] = siguiente;
            ViewData["anterior"] = anterior;
            ViewData["posicion"] = posicion;

            Zapatilla zapa = await this.repo.FindZapatillaByIdAsync(id);

            return View(zapa);
        }

        public async Task<IActionResult> _PartialImagenes(int id, int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            ModelImagenes model = await this.repo.GetImagenesZapatillaAsync(posicion.Value, id);

            int numReg = model.Registros;

            int siguiente = posicion.Value + 1;

            if (siguiente > numReg)
            {
                siguiente = numReg;
            }

            int anterior = posicion.Value - 1;

            if (anterior < 1)
            {
                anterior = 1;
            }

            ViewData["ultimo"] = numReg;
            ViewData["siguiente"] = siguiente;
            ViewData["anterior"] = anterior;
            ViewData["posicion"] = posicion;

            return PartialView("_PartialImagenes", model.Imagen);
        }
    }
}
