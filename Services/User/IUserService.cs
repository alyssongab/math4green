using agendamento_recursos.DTOs.User;

namespace agendamento_recursos.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> UserExists(string email);
        Task<UserDto> CreateUserAsync(CreateUserDto dto);

        //Task<UserDto?> GetUserByIdAsync(int id);
        //Task<LoginResponseDto> LoginAsync(LoginDto dto);
    }
}
