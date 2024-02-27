using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Data;
using System.Diagnostics;

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
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAll();

            return View(list);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync(); // Busca do DB todos os departamentos

            var viewModel = new SellerFormViewModel { Department = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if(ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Department = departments };

                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));



        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? ID) // O ponto de interrogação significa que é opcional
        {
            if (ID == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Id not provider"});
            }

            var objeto = await _sellerService.FindByIDAsync(ID.Value);

            if (objeto == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(objeto);

        }

        [HttpPost]
        [ValidateAntiForgeryToken] // vai evitar que seja implementado um malware no cadastro
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Details(int? ID)
        {
            if (ID == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provider" });
            }

            var obj = await _sellerService.FindByIDAsync(ID.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provider" });
            }

            var vendedorDB = await _sellerService.FindByIDAsync(Id.Value);

            if (vendedorDB == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync(); // Vai retornar uma lista com todos os departamentos do DB

            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = vendedorDB, Department = departments }; // A view de Edit irá retornar as informações dos vendedores e do Departamento

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {

            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Department = departments };

                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id is diferent" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            
            
            
            catch (NotFoundException NotFoundMessage)
            {
                return RedirectToAction(nameof(Error), new {message = NotFoundMessage.Message}); ;
            }
            catch (DbConcurrencyException DbErrorMessage)
            {
                return RedirectToAction(nameof(Error), new { message = DbErrorMessage.Message});
            }

        }


        public IActionResult Error(string message)
        {
            var ErrorviewModel = new ErrorViewModel { Mensagem = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            return View(ErrorviewModel);
        }





























    }















    }






















