namespace ClinicAppointment_System.Models;
using ClinicAppointment_System.Models.Entittes;

public class Doctor : User
{
     public User DoctorInfo { get; set; }
     public List<DoctorSchedule> DoctorSchedule { get; set; }
     public DoctorSpecialist DoctorSpecialist { get; set; }
     public decimal Price { get; set; }
      public override string ToString(){
    return DoctorInfo.ToString();
  }
}