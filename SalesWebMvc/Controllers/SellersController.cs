using Microsoft.AspNetCore.Mvc;
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





    }
}
