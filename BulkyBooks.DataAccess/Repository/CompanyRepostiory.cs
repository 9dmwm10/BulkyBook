using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBooks.DataAccess.Repository.IRepository;
using System;
using BulkyBook.DataAccess.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BulkyBooks.DataAccess.Repository
{
    public class CompanyRepostiory : Repository<Company>, ICompanyRepostitory
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepostiory(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(Company company)
        {
            Company updating = _db.Companies.Find(company.id);
            updating.City = company.City;
            updating.IsAuthorizedCompany = company.IsAuthorizedCompany;
            updating.Name = company.Name;
            updating.PhoneNumber = company.PhoneNumber;
            updating.PostalCode = company.PostalCode;
            updating.State = company.State;
            updating.StreetAdress = company.StreetAdress;
            _db.Companies.Update(updating);
        }
    }
}
