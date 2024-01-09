using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context) // Injeção de dependencia
        {
            _context = context;
            
        }

        public List<Seller>FindAll()
        {
            return _context.Seller.ToList();

        }




    }
}
