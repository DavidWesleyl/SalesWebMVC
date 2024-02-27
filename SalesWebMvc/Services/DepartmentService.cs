using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;
        public DepartmentService(SalesWebMvcContext context) // Injeção de dependencia
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync() // otimizando a operação FindAll sincrona em Assincrona 
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); // mudamos o to list que era uma operação sincrona para ToListAsync(), e adicionamos o Await informando que é uma operação assíncrona
            

        }
    }
}
    