
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Principal;

namespace SalesWebMvc.Models
{
    public class Department // Classe Department | Dados que serão alimentados
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
