namespace ClinicAppointment_System.Models.Entittes;

public class DoctorBuilder : IdoctorBuilder
{
    private Doctor _doctor = new Doctor();
     public IdoctorBuilder BuilDoctorInfo(User user)
     {
        _doctor.DoctorInfo = user;
        return this;
     } 
   public IdoctorBuilder BuildSpecialist(DoctorSpecialist specialist){
        _doctor.DoctorSpecialist = specialist;
        return this;
    }
   public IdoctorBuilder BuildSchedule(List<DoctorSchedule> schedules){
        _doctor.DoctorSchedule = schedules;
        return this;
   }
    public Doctor Build(){
        return _doctor;
    }
}