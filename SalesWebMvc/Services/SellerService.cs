using Microsoft.EntityFrameworkCore;
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

        public List<Seller>FindAll() // Encontrar e mostrar a lista dos Vendedores do SQLServer
        {
            return _context.Seller.ToList(); // Retornar em formato de lista

        }
        public void Insert(Seller seller) // Adicionar vendedor ao banco de dados
        {
            
            _context.Add(seller);
            _context.SaveChanges(); // Salvar alterações no banco de dados
        }

        public Seller FindByID(int ID) // Encontrar por ID
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(vendedor => vendedor.Id == ID); // Expressão lambda, para encontrar o "Primeiro ou padrão" vendedor => (que recebe) vendedor.Id seja igual ao ID (Parametro) 
        }

        public void Remove(int ID) // Remover por ID
        {
            var objeto = _context.Seller.Find(ID); // variável objeto vai encontrar o vendedor pelp ID

            _context.Seller.Remove(objeto); // Vai remover

            _context.SaveChanges(); // Salvar alterações no SQL

        }



         
    }
}
