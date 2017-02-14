namespace IST.WebApi2.Models
{
    public class EmailModel
    {
        public int SolutionId { get; set; }
        public string RecieverEmail { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }
}