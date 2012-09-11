﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Data.OData.IntegrationTests
{
    using Xunit;

    public class InsertTest : TestBase
    {
        [Fact]
        public void Insert()
        {
            var product = _db.Products.Insert(ProductID: 1001, ProductName: "Test", UnitPrice: 18m);

            Assert.Equal("Test", product.ProductName);
        }

        [Fact]
        public void InsertAutogeneratedID()
        {
            var product = _db.Products.Insert(ProductName: "Test", UnitPrice: 18m);

            Assert.True(product.ProductID > 0);
        }

        [Fact]
        public void InsertProductWithCategoryByID()
        {
            var category = _db.Categories.Insert(CategoryID: 1001, CategoryName: "Test");
            var product = _db.Products.Insert(ProductID: 1001, ProductName: "Test", UnitPrice: 18m, CategoryID: category.CategoryID);

            Assert.Equal("Test", product.ProductName);
            Assert.Equal(1001, product.CategoryID);
            category = _db.Category.WithProducts().FindByCategoryName("Test");
            Assert.True(category.Products.Count == 1);
        }

        [Fact]
        public void InsertProductWithCategoryByAssociation()
        {
            var category = _db.Categories.Insert(CategoryID: 1001, CategoryName: "Test");
            var product = _db.Products.Insert(ProductID: 1001, ProductName: "Test", UnitPrice: 18m, Category: category);

            Assert.Equal("Test", product.ProductName);
            Assert.Equal(1001, product.CategoryID);
            category = _db.Category.WithProducts().FindByCategoryName("Test");
            Assert.True(category.Products.Count == 1);
        }
    }
}
