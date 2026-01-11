using agendamento_recursos.DTOs.User;
using agendamento_recursos.Models;
using agendamento_recursos.Repository.User;

namespace agendamento_recursos.Services.User
{
    public class UserService : IUserService
    {

        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            if(await UserExists(dto.Email))
            {
                throw new Exception("Usuário com esse email já existe.");
            }

            var user = new Models.User
            {
                Name = dto.Name.Trim(),
                Email = dto.Email.ToLowerInvariant(),
                MinutesPerDay = TimeSpan.FromMinutes(240)
            };
            var created = await userRepository.CreateAsync(user);
            return MapToDto(created);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllSync();
            return users.Select(MapToDto);
        }

        public async Task<bool> UserExists(string email)
        {
            var existingEmail = await userRepository.GetByEmailAsync(email);
            return existingEmail != null;
        }
        private static UserDto MapToDto(Models.User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                MinutesPerDay = (int)user.MinutesPerDay.TotalMinutes
            };
        }

        //public Task<UserDto?> GetUserByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        //{

        //}

    }
}
