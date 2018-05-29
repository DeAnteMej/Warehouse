using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Warehouse.Models
{

    //* ORM    /    DTO
    //  C#          DB
    // Product      Products

    public class Product
    {
        //[Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [UIHint("Currency")]
        [Display(Name = "How much to pay")]
        [Range(1,Int32.MaxValue, ErrorMessage = "The price must be higher than 1 SEK")]
        public int Price { get; set; }

        [Display(Name ="Amount i storage")]
        public int Quantity { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }
    }
}