﻿using System.ComponentModel.DataAnnotations;

namespace GenericStore.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
