using System.ComponentModel.DataAnnotations;

namespace RSI_Pharmacy.Data.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}