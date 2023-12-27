namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); // Coleção de seller


        public Department()
        {
            
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void addSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double totalSales(DateTime initial, DateTime final) // calcular o total de vendas do departamento
        {
            return Sellers.Sum(x => x.TotalSales(initial, final));

        }



    }
}
