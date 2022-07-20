using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;
namespace Todo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]

        public IActionResult Get([FromServices] AppDbContext context)
         => Ok(context.Todos.ToList());

        [HttpGet("/{id:int}")]

        public IActionResult GetById(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);

            if (todo is null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPost("/")]

        public IActionResult Post(
          [FromBody] TodoModel todos,
            [FromServices] AppDbContext context)
        {

            context.Todos.Add(todos);
            context.SaveChanges();
            return Created($"/{todos.Id}", todos);
        }
        [HttpPut("/{id:int}")]
        public IActionResult Put(
                       [FromRoute] int id,
                     [FromBody] TodoModel todos,
                       [FromServices] AppDbContext context)
        {

            var model = context.Todos.FirstOrDefault(x => x.Id == id);

            if (model is null)
                return NotFound();

            model.Title = todos.Title;
            model.Done = todos.Done;

            context.Todos.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete(
              [FromRoute] int id,
              [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return NotFound();

            context.Todos.Remove(model);
            context.SaveChanges();

            return Ok(model);
        }
    }
}