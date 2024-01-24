using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Data;

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
            if (ID == null)
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

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var vendedorDB = _sellerService.FindByID(Id.Value);

            if (vendedorDB == null)
            {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll(); // Vai retornar uma lista com todos os departamentos do DB

            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = vendedorDB, Department = departments }; // A view de Edit irá retornar as informações dos vendedores e do Departamento

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }


    }















    }






















