namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime birthDate { get; set; }
        public double baseSalary { get; set; }
        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
            
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
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
