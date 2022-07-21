using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.ViewModels
{
    public static class ModelStateExtension
    {
        public static List<string> GetErros(this ModelStateDictionary modelState)
        {
            var results = new List<string>();

            foreach (var item in modelState.Values)
            {
                results.AddRange(item.Errors.Select(erros => erros.ErrorMessage));
            }

            return results;
        }
    }
}
