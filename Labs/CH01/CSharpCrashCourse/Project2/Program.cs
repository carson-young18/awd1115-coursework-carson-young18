//Fizz Buzz
Console.WriteLine("Upper Limit:");
int limit = Convert.ToInt32(Console.ReadLine());

for (int i = 1; i <= limit; i++)
{
    if (i % 3 == 0)
    {
        if (i % 5 == 0)
        {
            Console.WriteLine("FizzBuzz");
        }
        else
        {
            Console.WriteLine("Fizz");
        }
    }
    else if (i % 5 == 0)
    {
        Console.WriteLine("Buzz");
    }
    else
    {
        Console.WriteLine(i);
    }
}

