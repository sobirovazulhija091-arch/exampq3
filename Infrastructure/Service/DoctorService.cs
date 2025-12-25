using Infrastructure;
using Npgsql;

public class DoctorService : IDoctorService
{
private string connString = "Host=localhost;Port=5432;Database=newlesson2;Username=postgres;Password=1234";
    public void AddDoctor(Doctor doctor)
    {
          var conn = new NpgsqlConnection(connString);
          conn.Open();
          string query = $"insert  into doctors(fullname,specialization,phone,age) values('{doctor.fullname}','{doctor.specialization}',{doctor.phone},{doctor.age})";
         using  var  cmd = new NpgsqlCommand(query,conn);
         cmd.ExecuteNonQuery();
    }
    public void GetDoctor()
    {
       var conn = new NpgsqlConnection(connString);
       conn.Open();
       string query = @$"select d.fullname as fullname_doctors ,p.fullname as fullname_patients ,a.appointment as appointment_time from patients p  left join  appointments a  on   a.patientid=p.id left join doctors d
            on  a.doctorid=d.id";
        using var cmd = new NpgsqlCommand(query,conn);
        var getinfo=cmd.ExecuteReader();
        while (getinfo.Read())
        {
            Console.WriteLine($"{getinfo["fullname_doctors"]},{getinfo["fullname_patients"]},{getinfo["appointment_time"]}");
        }
    }
    public string Update(int doctorid,string fullname)
    {
         var conn = new NpgsqlConnection(connString);
         conn.Open();
          string query = $"upadate dactor set fullname={fullname} where id={doctorid}";
          using var cmd = new NpgsqlCommand(query,conn);
          var result = cmd.ExecuteNonQuery();
          return result ==0? "Update cannot" : "Updated!"; 
         
    }
    public string Delete(int doctorid)
    {
        var conn= new NpgsqlConnection(connString);
        conn.Open();
        string query = $"delete from doctors where id = {doctorid}";
         using var cmd = new NpgsqlCommand(query,conn);
         var result=cmd.ExecuteNonQuery();
         return result==0? "Cannot delete" : "Deleted!";
    }
    public void GetDoctorSpecialization()
    {
         var conn = new NpgsqlConnection(connString);
         conn.Open();
         string query = $" select  specialization from doctors ";
         using var cmd = new NpgsqlCommand(query,conn);
         var r = cmd.ExecuteReader();
         while (r.Read())
         {
             Console.WriteLine($"{r["specialization"]}");
         }
    }
     public void GetAll()
    {
         var  conn = new NpgsqlConnection(connString);
         conn.Open();
         string query = $@"select * from doctors";
         using var cmd= new NpgsqlCommand(query,conn);
         var take = cmd.ExecuteReader();
         while (take.Read())
         {
            Console.WriteLine($"'{take["fullname"]}','{take["specialization"]}',{take["age"]},{take["phone"]}");
         }
    }
    public string Updatephone(int doctorid,int newphone)
    {
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        string query = @$"update doctors set phone={newphone} where id={doctorid}";
        using var cmd = new NpgsqlCommand(query,conn);
        var m = cmd.ExecuteNonQuery();
          return m==0? "NOT UPDATE PHONE"  : "UPDATED PHONE";
    }
     public void GetNumOfDoctors()
    {
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        string query = $@"select count(*) from doctors";
        using var cmd = new NpgsqlCommand(query,conn);
        int num=Convert.ToInt32(cmd.ExecuteScalar());
         Console.WriteLine($"(Numbers of doctors: {num})");
    }
}