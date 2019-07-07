using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookMarket.Models
{
    public class Book
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Опис")]
        public string Description { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Автор")]
        public string Author { get; set; }
        [Required]
        [Display(Name = "Дата публікації")]
        public DateTime DatePublication { get; set; }
        [NotMapped]
        [Display(Name = "Фото")]
        public IFormFile Photo { get; set; }
        [Display(Name = "Адрес фото")]
        public string PhotoUrl { get; set; }
        [Required]
        [Display(Name = "Жанр")]
        public int CategoryId { get; set; }

    }
}
