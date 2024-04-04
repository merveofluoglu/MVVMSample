using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVVMEntityLayer
{
    [Table("Book")]
    public class Book
    {
        public long Id { get; set; }

        [Display(Name = "Book Title")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Name { get; set; }

        [Display(Name = "Author")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Author { get; set; }

        [Display(Name = "Year Of Puplication")]
        [DataType(DataType.Date)]
        public DateTime? DateOfIssue { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [Required]
        [Range(0.01, 9999)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cost { get; set; }

        [Display(Name = "Start Selling Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime SellStartDate { get; set; }

        [Display(Name = "End Selling Date")]
        [DataType(DataType.Date)]
        public DateTime? SellEndDate { get; set; }

        [Display(Name = "Paper Type")]
        [StringLength(50)]
        public string PaperType { get; set; }

        [Display(Name = "Hardcover")]
        public bool IsHardcover { get; set; }
    }
}
