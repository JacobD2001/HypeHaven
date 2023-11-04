﻿using HypeHaven.models;

namespace HypeHaven.ViewModels.ProductViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string PriceSortOrder { get; set; }

    }

}
