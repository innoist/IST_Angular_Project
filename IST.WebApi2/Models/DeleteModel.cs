namespace IST.WebApi2.Models
{
    public class DeleteModel : BaseModel
    {
        public int Id { get; set; }
        public bool DeleteType { get; set; }
    }
}