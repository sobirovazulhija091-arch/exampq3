public interface IPatientService
{
    public void AddPatient(Patient patient);
    public void GetPatient();
    public  string  Update(int patientid,int newage);
    public string Delete(int patientid);
    
     
}
