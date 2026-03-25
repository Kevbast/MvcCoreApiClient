using Microsoft.AspNetCore.Mvc;
using MvcCoreApiClient.Models;
using MvcCoreApiClient.Service;
using System.Threading.Tasks;

namespace MvcCoreApiClient.Controllers
{
    public class HospitalesController : Controller
    {
        private ServiceHospitales service;

        public HospitalesController(ServiceHospitales service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Servidor()
        {
            List<Hospital> hospitales = await this.service.GetHospitalesAsync();
            return View(hospitales);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cliente()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            Hospital hospital = await this.service.FindHospitalById(id);
            return View(hospital);
        }

    }
}
