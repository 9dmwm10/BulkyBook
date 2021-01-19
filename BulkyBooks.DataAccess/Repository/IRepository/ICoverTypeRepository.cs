using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BulkyBook.DataAccess.Repository.IRepository;


namespace BulkyBooks.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType covertype);
    }
}
