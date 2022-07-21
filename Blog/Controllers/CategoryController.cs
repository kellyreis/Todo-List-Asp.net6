using Microsoft.AspNetCore.Mvc;
using Blog.Data;
using Microsoft.EntityFrameworkCore;
using Blog.Models;
using Blog.ViewModels;
namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context
            )
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("05X04 - Falha interna no servidor"));
            }
        }


        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext context
            )
        {         
            try
            {
                var categorie = await context.
                    Categories.
                    FirstOrDefaultAsync(x => x.Id == id);

                if (categorie is null)
                    return NotFound(new ResultViewModel<Category>("Contéudo não encontrado"));

                return Ok(new ResultViewModel<Category>(categorie));

            }
            catch (Exception)
            {

                return StatusCode(500, new ResultViewModel<List<Category>>("05X02 - Falha interna no servidor"));

            }

        }



        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
            [FromRoute] EditorCategoryViewModel model,
            [FromServices] BlogDataContext context
            )
        {

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErros()));

            try
            {
                var categorie = new Category
                {

                    Id = 0,
                    Name = model.Name,
                    Slug = model.slug.ToLower(),
                };

                await context.Categories.AddAsync(categorie);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{categorie.Id}", new ResultViewModel<Category>(Category));

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("Não foi possivel incluir a categoria"));
         
            }
            catch 
            {
                return  StatusCode(500, new ResultViewModel<Category>("05X02 - Falha interna no servidor"));
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
                  [FromRoute] int id,
          [FromRoute] EditorCategoryViewModel model,
          [FromServices] BlogDataContext context
          )
        {
            try
            {
                var categorie = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (categorie is null)
                    return NotFound(new ResultViewModel<Category>("Contéudo não encontrado"));


                var categorie_ = new Category
                {

                    Id = 0,
                    Name = model.Name,
                    Slug = model.slug.ToLower(),
                };

                context.Categories.Update(categorie_);
                await context.SaveChangesAsync();

                return Ok(categorie_);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("Não foi possivel incluir a categoria"));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<Category>("05X02 - Falha interna no servidor"));

            }

        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
                [FromRoute] int id,
        [FromRoute] EditorCategoryViewModel model,
        [FromServices] BlogDataContext context
        )
        {
            var categorie = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (categorie is null)
                return NotFound(new ResultViewModel<Category>("Contéudo não encontrado"));


            context.Categories.Remove(categorie);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<EditorCategoryViewModel>(model));
        }


    }
}
