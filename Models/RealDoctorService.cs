namespace ClinicAppointment_System.Models.Entittes;
using ClinicAppointment_System.Data;
public class RealDoctorService : IDoctorService
{
    public void DeleteDoctor(Guid targetDoctorId)
    {
        var doctor = DataSeed.Doctors
            .FirstOrDefault(d => d.DoctorInfo.Id == targetDoctorId);

        if (doctor != null)
        {
            DataSeed.Doctors.Remove(doctor);
            Console.WriteLine($"Doctor with ID {targetDoctorId} deleted successfully.");
        }
    }

    public void EditDoctor(Guid targetDoctorId, Doctor newDoctorData)
    {
        var existingDoctor = DataSeed.Doctors
            .FirstOrDefault(d => d.DoctorInfo.Id == targetDoctorId);

        if (existingDoctor != null)
        {
            existingDoctor.DoctorInfo.FirstName = newDoctorData.DoctorInfo.FirstName;
            existingDoctor.DoctorInfo.Email = newDoctorData.DoctorInfo.Email;
            existingDoctor.DoctorInfo.LastName = newDoctorData.DoctorInfo.LastName;
            existingDoctor.DoctorSpecialist.Specialization = newDoctorData.DoctorSpecialist.Specialization;
            existingDoctor.DoctorSpecialist.Department = newDoctorData.DoctorSpecialist.Department;
            existingDoctor.DoctorSpecialist.YearsOfExperience = newDoctorData.DoctorSpecialist.YearsOfExperience;
            existingDoctor.DoctorSchedule = newDoctorData.DoctorSchedule;
            Console.WriteLine($"Doctor with ID {targetDoctorId} updated.");
        }
    }
}