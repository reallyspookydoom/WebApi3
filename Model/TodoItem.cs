using System.Collections.Generic;

namespace Model
{
    public class TodoItem
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public List<TodoItem> SubItems { get; set; }

        public TodoItem()
        {
            SubItems = new List<TodoItem>();
        }
    }
}
