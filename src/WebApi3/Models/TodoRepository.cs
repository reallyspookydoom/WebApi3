using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace WebApi3.Models
{
    public class TodoRepository : ITodoRepository
    {
        private static ConcurrentDictionary<string, TodoItem> data = new ConcurrentDictionary<string, TodoItem>();

        public TodoRepository()
        {
            Add(new TodoItem() { Name = "Item 1" });
            Add(new TodoItem() { Name = "Item 2" });
            Add(new TodoItem() { Name = "Item 3" });

            AddSubItem(data.Values.First(), new TodoItem() { Name = "Sub item 1" });
            AddSubItem(data.Values.First(), new TodoItem() { Name = "Sub item 2" });
        }

        public void Add(TodoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            data[item.Key] = item;
        }

        public void AddSubItem(TodoItem parent, TodoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            parent.SubItems.Add(item);
        }

        public TodoItem Find(string key)
        {
            TodoItem item;
            data.TryGetValue(key, out item);
            return item;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return data.Values;
        }

        public TodoItem Remove(string key)
        {
            TodoItem item;
            data.TryGetValue(key, out item);
            data.TryRemove(key, out item);
            return item;
        }

        public void Update(TodoItem item)
        {
            data[item.Key] = item;
        }
    }
}
