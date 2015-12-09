using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using WebApi3.Models;

namespace WebApi3.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        [FromServices]
        public ITodoRepository TodoItems { get; set; } 

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return TodoItems.GetAll();
        }

        [HttpGet("{key}", Name = "Find")]
        public IActionResult Find(string key)
        {
            TodoItem item = TodoItems.Find(key);
            if (item == null) return HttpNotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null) return HttpBadRequest();

            TodoItems.Add(item);
            return CreatedAtRoute("Find", new { controller = "Todo", key = item.Key }, item);
        }

        [HttpPut("{key}")]
        public IActionResult Update(string key, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != key) return HttpBadRequest();

            TodoItem existingItem = TodoItems.Find(key);
            if (existingItem == null) return HttpBadRequest();

            TodoItems.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{key}")]
        public void Delete(string key)
        {
            TodoItems.Remove(key);
        }
    }
}
