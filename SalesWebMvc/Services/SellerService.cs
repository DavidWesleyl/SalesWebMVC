using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using System.Data;


namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context) // Injeção de dependencia
        {
            _context = context;

        }

        public async Task<List<Seller>> FindAll() // Encontrar e mostrar a lista dos Vendedores do SQLServer
        {
            return await _context.Seller.ToListAsync(); // Retornar em formato de lista

        }
        public async Task InsertAsync(Seller seller) // Adicionar vendedor ao banco de dados
        {

            _context.Add(seller);
            _context.SaveChangesAsync(); // Salvar alterações no banco de dados
        }

        public async Task<Seller> FindByIDAsync(int ID) // Encontrar por ID
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(vendedor => vendedor.Id == ID); // Expressão lambda, para encontrar o "Primeiro ou padrão" vendedor => (que recebe) vendedor.Id seja igual ao ID (Parametro) 
        }

        public async Task RemoveAsync(int ID) // Remover por ID
        {
            var objeto = await _context.Seller.FindAsync(ID); // variável objeto vai encontrar o vendedor pelp ID

            _context.Seller.Remove(objeto); // Vai remover

            await _context.SaveChangesAsync(); // Salvar alterações no SQL

        }



        public async Task UpdateAsync(Seller vendedor) // Método Update | Atualizar informações do Vendedor no Banco de Dados
        {
            var ifAny = await _context.Seller.AnyAsync(x => x.Id == vendedor.Id);

            
            if (!ifAny)
            {

                throw new NotFoundException("Id notfound");

            }

            try
            {
                _context.Update(vendedor);
                await _context.SaveChangesAsync();

            }

            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbConcurrencyException(exception.Message);
            }


        }

















    }

}

