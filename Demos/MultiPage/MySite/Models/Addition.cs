namespace MySite.Models
{
    public class Addition
    {
        public int NumberOne { get; set; }
        public int NumberTWo { get; set; }
        public int Sum { get; set; } 

        public void CalculateSum()
        {
            Sum = NumberOne + NumberTWo;
        }
    }
}
