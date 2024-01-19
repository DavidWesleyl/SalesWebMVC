using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService; // Injeção de dependencia do Seller Service

        private readonly DepartmentService _departmentService; // Injeção de dependencia do DepartmentSerice
  
        public SellersController(SellerService sellerservice, DepartmentService departmentService)
        {
            _sellerService = sellerservice;
            _departmentService = departmentService;
            
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();

            return View(list);
        }


        [HttpGet]
        public IActionResult Create() 
        {
            var departments = _departmentService.FindAll(); // Busca do DB todos os departamentos

            var viewModel = new SellerFormViewModel { Department = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));


           
        }

        
        [HttpGet]
        public IActionResult Delete(int? ID) // O ponto de interrogação significa que é opcional
        {
            if (ID == null) 
            {
                return NotFound();
            }

            var objeto = _sellerService.FindByID(ID.Value);

            if (objeto == null) 
            {
                return NotFound();
            }

            return View(objeto);
                 
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // vai evitar que seja implementado um malware no cadastro
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));

        }


        public IActionResult Details(int? ID)
        {
            if(ID == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindByID(ID.Value);

            if (obj == null) 
            {
                return NotFound();
            }

            return View(obj);
        }




    }
}
