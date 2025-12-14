namespace ClinicAppointment_System.Models.Entittes;

public class DoctorServiceProxy : IDoctorService
{
    private RealDoctorService _realService;
    private User _currentUser; 

    public DoctorServiceProxy(User currentUser)
    {
        _realService = new RealDoctorService();
        _currentUser = currentUser;
    }

    public void DeleteDoctor(int targetDoctorId)
    {
        if (CanAccess(targetDoctorId))
        {
            _realService.DeleteDoctor(targetDoctorId);
        }
        else
        {
            throw new UnauthorizedAccessException("Access Denied: You cannot delete this profile.");
        }
    }

    public void EditDoctor(int targetDoctorId, Doctor newDoctorData)
    {
        if (CanAccess(targetDoctorId))
        {
            _realService.EditDoctor(targetDoctorId, newDoctorData);
        }
        else
        {
            throw new UnauthorizedAccessException("Access Denied: You cannot edit this profile.");
        }
    }

    public bool CanAccess(int targetId)
    {
        Console.WriteLine("targetId: " + targetId);
        Console.WriteLine("_currentUser.Id: " + _currentUser.Id);
        if (_currentUser.Role == "Admin")
        {
            return true;
        }

        if (_currentUser.Role == "Doctor" && _currentUser.Id == targetId)
        {
            return true;
        }

        return false;
    }
}