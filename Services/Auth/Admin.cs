using ClinicAppointment_System.Models;

public class Admin : IUser
{
    public User RegisterUser(User data)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            Password = data.Password,
            CreatedAt = DateTime.Now,
        };
    }


}