public class ScheduleInput
    { 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
public class DoctorViewModel
{
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }

    public string Specialization { get; set; }
    public string Department { get; set; }
    public int YearsOfExperience { get; set; }
    public decimal Price { get; set; }

    public List<ScheduleInput> Schedules { get; set; } = new List<ScheduleInput>();
}