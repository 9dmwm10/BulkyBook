using BulkyBook.Models;
using BulkyBooks.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using BulkyBook.DataAccess.Repository.IRepository;


namespace BulkyBooks.DataAccess.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);

    }
}
