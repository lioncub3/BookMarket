using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMarket.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Назва")]
        public string Name { get; set; }
    }
}
