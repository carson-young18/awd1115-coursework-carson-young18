string num;

do
{
    Console.WriteLine("Enter a credit card number:");
    num = Console.ReadLine();
}
while (String.IsNullOrEmpty(num));

string maskedNum = String.Empty;

for (int i = 0; i < num.Length; i++)
{
    if (num[i] == '-' || Char.IsWhiteSpace(num[i]) || i >= num.Length - 4)
    {
        maskedNum += num[i];
    }
    else
    {
        maskedNum += 'X';
    }
}

Console.WriteLine(maskedNum);