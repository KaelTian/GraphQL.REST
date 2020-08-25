using RealEstateManager.Database.Models;
using System.Collections.Generic;

namespace RealEstateManager.DataAccess.Repositories
{
    public interface IPropertyRepository
    {
        IEnumerable<Property> GetAll();

        Property GetById(int id);

        Property Add(Property property);
    }
}
