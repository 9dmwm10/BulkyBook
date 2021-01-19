using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BulkyBook.DataAccess.Repository.IRepository;
namespace BulkyBooks.DataAccess.Repository.IRepository
{
    public interface ICompanyRepostitory : IRepository<Company>
    {
        void Update(Company company);
    }
}
