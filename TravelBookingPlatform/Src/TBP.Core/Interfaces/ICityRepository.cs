using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBP.Domain.Entites;

namespace TBP.Core.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City> GetCityByIdAsync(int id);
        Task<City> AddCityAsync(City city);
        Task<City> UpdateCityAsync(int id, City city);
        Task DeleteCityAsync(int id);
    }
}
