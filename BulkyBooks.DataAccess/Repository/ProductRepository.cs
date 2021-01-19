using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using System;
using BulkyBook.DataAccess.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBooks.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(a => a.Id == product.Id);

            if(product.ImageUrl != null)
            {
                objFromDb.ImageUrl = product.ImageUrl;
            }

            if (objFromDb != null)
            {
                objFromDb.Author = product.Author;
                objFromDb.ISBN = product.ISBN;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.CoverTypeId = product.CoverTypeId;
                objFromDb.Description = product.Description;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Price = product.Price;
                objFromDb.Price50 = product.Price50;
                objFromDb.Price100 = product.Price100;
                objFromDb.Title = product.Title;
            }
        }
    }
}
