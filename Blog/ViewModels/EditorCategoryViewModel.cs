using System.ComponentModel.DataAnnotations;
namespace Blog.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage ="O nome é Obrigatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O slug é obrigatorio")]
        public string slug { get; set; }
    }
}
