namespace BlogDataLibrary.Models
{
    public class ListPostModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public object UserName { get; set; }
        public object Body { get; set; }
        public object FirstName { get; set; }
        public object LastName { get; set; }
    }
}
