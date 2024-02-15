namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        // A View de Vendedor No sistema Web conterá essas informações

        public Seller Seller { get; set; }
        public ICollection<Department> Department { get; set; }
    }
}
