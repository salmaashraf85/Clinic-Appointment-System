namespace ClinicAppointment_System.Models.Entittes;

public interface IDoctorService
{
    void DeleteDoctor(int targetDoctorId);
    void EditDoctor(int targetDoctorId, Doctor newDoctorData);
}