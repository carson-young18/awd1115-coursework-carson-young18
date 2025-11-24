namespace FinalProject.Models
{
    public class BuildItem
    {
        public int BuildId { get; set; }
        public Build Build { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
