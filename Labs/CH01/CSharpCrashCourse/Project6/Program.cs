int[] testScores = [100, 90, 30, 88, 75, 93];

int best = 0, worst = 100, sum = 0;

foreach (int i in testScores)
{
    best = int.Max(best, i);
    worst = int.Min(worst, i);
    sum += i;
}

Console.WriteLine($"Best: {best}");
Console.WriteLine($"Worst: {worst}");
Console.WriteLine($"Sum: {sum}");
Console.WriteLine($"Average: {sum / testScores.Length}");