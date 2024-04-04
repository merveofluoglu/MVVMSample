using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVVMEntityLayer
{
    public class BookSearch
    {
        [Display(Name="Book Title")]
        public string? Name { get; set; }
        
        [Display(Name = "List Price Greater Than or Equal to")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ListPrice { get; set; }

        [Display(Name = "Author")]
        public string? Author { get; set; }
    }
}
