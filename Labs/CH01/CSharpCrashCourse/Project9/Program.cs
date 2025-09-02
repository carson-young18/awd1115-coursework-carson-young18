using Project9;

Leaf leaf = new Leaf();
Page page = new Page();
Corner corner = new Corner();
Pancake pancake = new Pancake();

List<ITurnable> turnables = new List<ITurnable> { leaf, page, corner, pancake };

static void Turning(List<ITurnable> t)
{
    foreach(ITurnable turn in t)
    {
        Console.WriteLine(turn.Turn());
    }
}

Turning(turnables);