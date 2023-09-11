using Secret_Sharing_Platform.Dto;
using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Services.Interfaces
{
    public interface ITokenService
    {
        TokenDto CreateToken(User user);
        int GetMyId();
    }
}