namespace ToDoList.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Asignate_to { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
