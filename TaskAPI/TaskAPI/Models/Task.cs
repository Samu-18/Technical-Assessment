namespace TaskAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = ""; 
        public string? Description { get; set; }
        public int? AssigneeUserId { get; set; }        // FK 
        public DateTime? DueDate { get; set; }
    }
}
