namespace DemoStuff
{
    public class TaskList
    {

        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public List<ToDoItem> Tasks { get; set; }
    }




    public class ToDoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TaskQueue Queue { get; set; }
    }


    public class TaskQueue
    {
        public int QueueId { get; set; }
        public string QueueName { get; set; }
    }

}