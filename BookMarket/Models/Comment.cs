﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMarket.Models
{
    public class Comment
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [MaxLength(100)]
        [Display(Name = "Ім\'я")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Опис")]
        public string Content { get; set; }
        [Display(Name = "Час")]
        public DateTime Time { get; set; } = DateTime.Now;
        [Display(Name = "Блог")]
        public int BookId { get; set; }
    }
}
