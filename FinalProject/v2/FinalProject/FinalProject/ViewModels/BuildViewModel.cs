using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class BuildViewModel
    {
        public Build Build { get; set; }

        public List<Build>? Builds { get; set; }

        public List<Item>? Items { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Champion>? Champions { get; set; }

        public string? Message { get; set; }

        public int[] ItemIds { get; set; } = Array.Empty<int>();

        public int? SelectedCategoryId { get; set; }
        public int? SelectedChampionId { get; set; }
    }
}