using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Npgsql;

public class AppointmentService : IAppointmentService
{
    private string connString = "Host=localhost;Port=5432;Database=newlesson2;Username=postgres;Password=1234";
    public void AddApp(Appointment appointment)
    {
         var conn = new NpgsqlConnection(connString);
         conn.Open();
          string query = $"insert into appointments(status,appointment) values('{appointment.status}','{appointment.appointment}')";
          using var cmd = new NpgsqlCommand(query,conn);
            cmd.ExecuteNonQuery();
    }
        public string Delete(int appid)
    {
        var conn = new NpgsqlConnection(connString);
         conn.Open();
         string query =$"delete  from appointmets where id{appid}";
         using var cmd = new NpgsqlCommand(query,conn);
         var  reuslt = cmd.ExecuteNonQuery();
         return reuslt==0? "Cannnot delete" : "Deleted!";
    }
    public void GetApp()
    {
        var conn= new NpgsqlConnection(connString);
        conn.Open();
        string query=$"select * from appointments";
        using var cmd= new NpgsqlCommand(query,conn);
        var takeinfo=cmd.ExecuteReader();
        while (takeinfo.Read())
        {
            Console.WriteLine($"'{takeinfo["status"]}',{takeinfo["appointment"]}");
        } 
    }
    public string Uppdate(int appid,string status)
    {
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        string query=$"update appointments set status={status} where id={appid}";
        using var cmd= new NpgsqlCommand(query,conn);
        var update = cmd.ExecuteNonQuery();
        return update == 0? "Can not update" : "Updated";
    }
    public  string CheckPatientHasApp(int patientid)
    {
              using var conn = new NpgsqlConnection(connString);
    conn.Open();
    string query = "SELECT COUNT(*) FROM appointments WHERE patientid = @id";
    using var cmd = new NpgsqlCommand(query, conn);
    cmd.Parameters.AddWithValue("@id", patientid);
    int count = Convert.ToInt32(cmd.ExecuteScalar());
     if(count>0){ return"Patient has appointmet to doctor";}
      else{ return"Patient has not appointmet to doctor";}
    }
    public void CheckDoctors(int doctorid)
    {
         var conn = new NpgsqlConnection(connString);
         conn.Open();
         string query =$"select count(*) from appointments where doctorid =@id";
         using var cmd = new NpgsqlCommand(query,conn);
         cmd.Parameters.AddWithValue("@id",doctorid);
          int countdoctors = Convert.ToInt16(cmd.ExecuteScalar());
 if (countdoctors > 0)
{
    Console.WriteLine("Doctor has appointments");
}
else
{
    Console.WriteLine("Doctor has no appointments");
}
 }
    public void ShowAllpersons()
    {
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        string query = $@"select d.fullname as doctor_name,d.age as doctor_age ,d.specialization as doctor_job,d.phone as doctor_phone,
p.fullname as patient_name,p.age as patient_age ,p.gender as patient_gender ,p.phone as patient_phone,
a.status as app_status,a.appointment as app_appointment
from doctors d left join appointments a  on a.doctorid=d.id left join patients p on p.id=a.patientid";
        using var cmd = new NpgsqlCommand(query,conn);
        var take = cmd.ExecuteReader();
        while (take.Read())
        {
             Console.WriteLine($@"{take["doctor_name"]}-{take["doctor_age"]}-{take["doctor_job"]}-{take["doctor_phone"]}
                                  {take["patient_name"]}-{take["patient_age"]}-{take["patient_gender"]}-{take["patient_phone"]}
                                 {take["app_status"]}-{take["app_appointment"]}");
        }
    }
    public void NumAppIN()
    {
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        string query = $@"select count(*) from appointments where extract(year from appointment)>2000";
        using var cmd = new NpgsqlCommand(query,conn);
        int num = Convert.ToInt32(cmd.ExecuteScalar());
        Console.WriteLine($"Number of appointments after 2000 year: {num}");
    }
}