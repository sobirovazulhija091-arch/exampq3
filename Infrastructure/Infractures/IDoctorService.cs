public interface IDoctorService
{
     public void AddDoctor(Doctor doctor);
     public void GetDoctor();
     public string Update(int doctorid,string fullname);
     public string Delete(int doctorid);
   
}