using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Core.Interfaces
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int id);
        Task<Hotel> AddHotelAsync(Hotel hotel);
        Task<Hotel> UpdateHotelAsync(int id, Hotel hotel);
        Task DeleteHotelAsync(int id);
        Task<IEnumerable<Hotel>> SearchAsync(SearchHotelDto searchHotelDto);
    }
}
