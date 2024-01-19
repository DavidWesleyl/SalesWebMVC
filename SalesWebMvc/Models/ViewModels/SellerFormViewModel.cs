namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        // Tela de Cadastro do Vendedor na Aplicação MVC

        public Seller Seller { get; set; }
        public ICollection<Department> Department { get; set; }
    }
}
