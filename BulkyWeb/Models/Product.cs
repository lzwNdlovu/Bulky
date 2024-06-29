using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkyWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }

        public string Author { get; set; }
        [Required]
        [Display(Name = "List Price")]
        [Range(1, 1000)]
        public int ListPrice { get; set; }

        [Display(Name ="Price for 1-50")]
        [Range(1, 1000)]
        public int Price { get; set; }

        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public int Price50 { get; set; }

        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public int Price100 { get; set; }

        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }


    }
}
