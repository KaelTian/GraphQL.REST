using RealEstateManager.Database;
using RealEstateManager.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateManager.DataAccess.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly RealEstateContext _dbContext;
        public PropertyRepository(RealEstateContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Property Add(Property property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("Add propperty is null");
            }
            //property.Id = _dbContext.Properties.Max(x => x.Id) + 1;
            _dbContext.Properties.Add(property);
            _dbContext.SaveChanges();
            return property;
        }

        public IEnumerable<Property> GetAll()
        {
            return _dbContext.Properties;
        }

        public Property GetById(int id)
        {
            return _dbContext.Properties.SingleOrDefault(x => x.Id == id);
        }
    }
}
