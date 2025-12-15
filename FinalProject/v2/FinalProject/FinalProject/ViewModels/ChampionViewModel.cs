using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class ChampionViewModel
    {
        public IEnumerable<Champion>? Champions { get; set; }

        public Champion? Champion { get; set; }

        public string? Message { get; set; }
    }
}