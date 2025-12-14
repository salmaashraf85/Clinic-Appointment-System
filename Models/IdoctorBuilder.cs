namespace ClinicAppointment_System.Models.Entittes;

public interface IdoctorBuilder {
    IdoctorBuilder BuilDoctorInfo(User user); 
    IdoctorBuilder BuildSpecialist(DoctorSpecialist specialist);
    IdoctorBuilder BuildSchedule(List<DoctorSchedule> schedules);
    Doctor Build();
}