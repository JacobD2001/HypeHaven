﻿using HypeHaven.models;
using System.Composition.Convention;

namespace HypeHaven.ViewModels.ProductViewModels
{
    /// <summary>
    /// View model for viewing products.
    /// </summary>
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string PriceSortOrder { get; set; }
        public string SearchTerm { get; set; }


    }

}
