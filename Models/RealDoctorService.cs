namespace ClinicAppointment_System.Models.Entittes;

public class RealDoctorService : IDoctorService
{
    public void DeleteDoctor(int targetDoctorId)
    {
        var doctor = DoctorRepository.Doctors
            .FirstOrDefault(d => d.DoctorInfo.Id == targetDoctorId);

        if (doctor != null)
        {
            DoctorRepository.Doctors.Remove(doctor);
            Console.WriteLine($"Doctor with ID {targetDoctorId} deleted successfully.");
        }
    }

    public void EditDoctor(int targetDoctorId, Doctor newDoctorData)
    {
        var existingDoctor = DoctorRepository.Doctors
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