namespace IST.WebApi2.Models
{
    /// <summary>
    /// Menu Right
    /// </summary>
    public class MenuRight
    {
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
        public bool IsSelected { get; set; }

        public bool IsParent { get; set; }
        public int? ParentId { get; set; }
    }
}
