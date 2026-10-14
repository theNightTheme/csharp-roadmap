try
{
    Console.WriteLine("Введите первое число:");
    float firstNumber = float.Parse(Console.ReadLine());
    Console.WriteLine("Введите операцию (+, -, *, /):");
    string operation = Console.ReadLine();
    Console.WriteLine("Введите второе число:");
    float secondNumber = float.Parse(Console.ReadLine());

    Console.WriteLine("Результат: " + Calculate(firstNumber, operation, secondNumber));
}
catch (FormatException)
{
    Console.WriteLine("Ошибка при вводе чисел.");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Ошибка: деление на ноль.");
}
catch (ArgumentException)
{
    Console.WriteLine("Ошибка: неизвестная операция.");
}


static float Calculate(float firstNumber, string operation, float secondNumber)
{
    switch (operation)
    {
        case "+": return firstNumber + secondNumber;
        case "-": return firstNumber - secondNumber;
        case "*": return firstNumber * secondNumber;
        case "/": if (secondNumber.Equals(0)) throw new DivideByZeroException(); else return firstNumber / secondNumber;
        default: throw new ArgumentException();
    }
}