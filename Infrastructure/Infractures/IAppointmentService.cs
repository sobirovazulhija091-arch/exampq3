public  interface IAppointmentService
{
     public void AddApp(Appointment appointment);
     public void GetApp();
     public  string Uppdate(int appid,string status);
     public string Delete (int appid);
    
      
}