using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Core.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetRoomsAsync();
        Task<Room> GetRoomByIdAsync(int id);
        Task<Room> AddRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(int id, Room room);
        Task DeleteRoomAsync(int id);
        Task<IEnumerable<Room>> SearchAsync(SearchRoomDto searchRoomDto);

    }
}