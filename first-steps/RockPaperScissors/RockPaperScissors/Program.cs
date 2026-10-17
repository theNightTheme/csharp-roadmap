using System.Linq;

Random randomIndex = new Random();
string[] strings = { "К", "Н", "Б" };  // К - Камень, Н - Ножницы, Б - Бумага

int userWins = 0;
int computerWins = 0;

while (true)
{
    if (userWins == 3 || computerWins == 3)
    {
        switch (userWins.CompareTo(computerWins))
        {
            case 1:
                Console.WriteLine($"\nВы победили со счетом {userWins}:{computerWins}!");
                break;
            case -1:
                Console.WriteLine($"\nВы проиграли со счетом {userWins}:{computerWins}!");
                break;
        }

        userWins = 0;
        computerWins = 0;


        while (true)
        {
            Console.WriteLine("\nХотите сыграть еще раз? (Д/Н)\n");
            string playAgain = Console.ReadLine();

            if (playAgain == "Д" || playAgain == "Н")
            {
                switch (playAgain)
                {
                    case "Д":
                        break;
                    case "Н":
                        Console.WriteLine("\nСпасибо за игру!\n");
                        return;
                }
            }
            else
            {
                Console.WriteLine("\nОшибка при вводе. Пожалуйста, введите Д или Н.\n");
                continue;
            }
            break;
        }
    }

    Console.WriteLine($"\nЯ выбрал предмет! Теперь ваша очередь (счет - {userWins}:{computerWins}):\n\nК - Камень\nН - Ножницы\nБ - Бумага\n");

    int itemIndex = randomIndex.Next(0, strings.Length);
    string item = strings[itemIndex];

    string userItem = Console.ReadLine();

    if (strings.Contains(userItem))
    {
        if (userItem == "К")
        {
            switch (item)
            {
                case "Н":
                    Console.WriteLine("\nУ меня были Ножницы. Вы выиграли.\n");
                    userWins++;
                    break;
                case "Б":
                    Console.WriteLine("\nУ меня была Бумага. Вы проиграли.\n");
                    computerWins++;
                    break;
                default:
                    Console.WriteLine("\nНичья.\n");
                    break;
            }
                
        }
        else if (userItem == "Н")
        {
            switch (item)
            {
                case "Б":
                    Console.WriteLine("\nУ меня была Бумага. Вы выиграли!\n");
                    userWins++;
                    break;
                case "К":
                    Console.WriteLine("\nУ меня был Камень. Вы проиграли.\n");
                    computerWins++;
                    break;
                default:
                    Console.WriteLine("\nНичья.\n");
                    break;
            }
                
        }
        else if (userItem == "Б")
        {
            switch (item)
            {
                case "К":
                    Console.WriteLine("\nУ меня был Камень. Вы выиграли!\n");
                    userWins++;
                    break;
                case "Н":
                    Console.WriteLine("\nУ меня были Ножницы. Вы проиграли.\n");
                    computerWins++;
                    break;
                default:
                    Console.WriteLine("\nНичья.\n");
                    break;
            }
                
        }
    }
    else
    {
        Console.WriteLine("\nОшибка при вводе. Пожалуйста, введите К, Н или Б.\n");
        continue;
    }
}