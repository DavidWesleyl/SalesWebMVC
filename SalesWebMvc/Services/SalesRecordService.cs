using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
                       
        }
  


        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate )
        {
            var resultado = from objeto in _context.SalesRecord select objeto;


            if(minDate.HasValue)
            {
                resultado = resultado.Where(x => x.Date >= minDate.Value);
            }

            if(maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.Date <= maxDate.Value);
            }

            return await resultado.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();
               

        }





         









    }
}
