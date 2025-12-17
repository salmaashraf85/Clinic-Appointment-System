namespace ClinicAppointment_System.Models.Entittes;

public interface IDoctorService
{
    void DeleteDoctor(Guid targetDoctorId);
    void EditDoctor(Guid targetDoctorId, Doctor newDoctorData);
}