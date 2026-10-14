Random ints = new Random();
int userNumber;
int hidden = ints.Next(1, 101);

Console.WriteLine("Введите число в диапазоне от 1 до 100:");

while (true)
{
    bool isNumber = int.TryParse(Console.ReadLine(), out userNumber);

    if (isNumber)
    {
        if (userNumber >= 1 && userNumber <= 100)
        {
            if (hidden > userNumber)
            {
                Console.WriteLine("Загаданное число больше! Попробуйте снова:");

                continue;
            }
            else if (hidden < userNumber)
            {
                Console.WriteLine("Загаданное число меньше! Попробуйте снова:");

                continue;
            }
            if (hidden == userNumber)
            {
                Console.WriteLine("Вы угадали число!");

                break;
            }
        }
        else
        {
            Console.WriteLine("Вы ввели число не в диапазоне от 1 до 100! Попробуйте снова:");

            continue;
        }
    }
    else
    {
        Console.WriteLine("Ошибка при вводе числа! Попробуйте снова:");

        continue;
    }
}