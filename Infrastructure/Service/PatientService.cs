using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Npgsql;
using NpgsqlTypes;

namespace Infrastructure;

public class PatientService : IPatientService
{
    private string connString = "Host=localhost;Port=5432;Database=newlesson2;Username=postgres;Password=1234";
    public void AddPatient(Patient patient)
    {
        var conn= new NpgsqlConnection(connString);
        conn.Open();
        string query = $"insert into patients(fullname,gender,age,phone) values('{patient.fullname}','{patient.gender}',{patient.age},{patient.phone})";
         using var cmd = new NpgsqlCommand(query,conn);
         cmd.ExecuteNonQuery();
    }
    public string Delete(int patientid)
    {
        var conn= new NpgsqlConnection(connString);
        conn.Open();
        string query = $"delete from patients where id={patientid}";
        using var cmd = new NpgsqlCommand(query,conn);
        var result=cmd.ExecuteNonQuery();  
        return result ==0? "Can not delete" : "Deleted!";
    }
     public void GetPatient()
    {
       var conn= new NpgsqlConnection(connString);
        conn.Open();
        string query = $"select * from  patients";
        using var cmd = new NpgsqlCommand(query,conn);
        var result=cmd.ExecuteReader();  
       while (result.Read())
       {
          Console.WriteLine($"'{result["fullname"]}',{result["age"]},'{result["gender"]}',{result["phone"]}");
       }
    }
    public string Update(int patientid,int newage)
    {
        var conn= new NpgsqlConnection(connString);
        conn.Open();
        string query = $"update patients set age={newage} where id={patientid}";
        using var cmd = new NpgsqlCommand(query,conn);
        var result=cmd.ExecuteNonQuery();  
        return result ==0? "Can not update" : "Update!";
    }
    public void GetName()
    {
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        string query= $"select fullname from patients where id>3";
        using var cmd= new NpgsqlCommand(query,conn);
        var show= cmd.ExecuteReader();
        while (show.Read())
        {
             Console.WriteLine($"'{show["fullname"]}'");
        }
    }
    public void GetSortName()
    {
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        string query = $"select * from patients order by fullname asc";
        using var cmd = new NpgsqlCommand(query,conn);
       var show= cmd.ExecuteReader();
        while (show.Read())
        {
             Console.WriteLine($"'{show["fullname"]}'");
        }
    }
   public string UpadatePhone(int patientid,int phone)
    {
          var conn = new NpgsqlConnection(connString);
          conn.Open();
          string query = $"update patients set phone={phone} where id={patientid}";
          using var cmd = new NpgsqlCommand(query,conn);
          var take = cmd.ExecuteNonQuery();
          return take==0? "Cannot update" : "Updated!";
    }
   public void GetNumOfPatients()
    {
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        string query = $@"select count(*) from patients ";
        using var cmd = new NpgsqlCommand(query,conn);
        int count = Convert.ToInt16(cmd.ExecuteScalar());
        Console.WriteLine($"Number of patients {count}");
    }
}
