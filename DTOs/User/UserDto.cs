namespace agendamento_recursos.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int MaxMinutesPerDay { get; set; }
    }

    public class CreateUserDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    // login / cadastro "dinamico"

    public class LoginDto
    {
        public string Email { get; set; } = null!;
    }

    //public class LoginResponseDto
    //{
    //    public UserDto User { get; set; } = null!;
    //    public bool IsNewUser { get; set; }
    //}
}
