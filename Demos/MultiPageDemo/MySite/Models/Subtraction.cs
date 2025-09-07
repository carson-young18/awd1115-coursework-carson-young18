namespace MySite.Models
{
    public class Subtraction
    {
        public int NumberOne { get; set; }
        public int NumberTwo { get; set; }
        public int Difference { get; set; }

        public void CalculateDifference()
        {
            Difference = NumberOne - NumberTwo;
        }
    }
}
