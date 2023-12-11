using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index() // Index | Razor da nossa aplicação
        {
            List<Department> lista = new List<Department>(); // Instanciamos uma lista e adicionamos dados

            lista.Add(new Department { Id = 1, Name = "Eletronicos"});
            lista.Add(new Department { Id = 2, Name = "Fashion" });

            return View(lista); // Para mandar nossa Controler para a view, basta colocar entre parenteses
        }
    }
}
