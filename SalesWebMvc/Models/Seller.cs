using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SalesWebMvc.Models
{
    public class Seller // Model Seller | Vendedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} Required")] // Vamos colocar a requisição onde os campos forem obrigatórios
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho inválido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Required")]
        [EmailAddress(ErrorMessage = "Enter a Valid Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} Required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime birthDate { get; set; }


        [Required(ErrorMessage = "{0} Required")]
        [Range(100.00, 50000.00, ErrorMessage = "Salary must be min {1} and  max {2}")]
        [Display (Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double baseSalary { get; set; }
        
        
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
       

        public Seller()
        {
            
        }

        public Seller(string name, string email, DateTime birthDate, double baseSalary, Department department )
        {
           
            Name = name;
            Email = email;
            this.birthDate = birthDate;
            this.baseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord addSales) // Método adicionar vendas
        {
            Sales.Add(addSales);
        }

        public void removeSales(SalesRecord removeSales) // Método remover vendas
        {
            Sales.Remove(removeSales);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(x => x.Date >= initial && x.Date <= final).Sum(x => x.Amount);
        }
















    }
}
