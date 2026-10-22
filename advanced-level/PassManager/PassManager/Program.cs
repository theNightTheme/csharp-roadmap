using System.IO;
using System.Threading;
using PassManager;

// Создаем папку "data" и файл "info.txt", если они не существуют.
Directory.CreateDirectory("data");
if (!File.Exists("data/info.txt"))
{
    File.Create("data/info.txt").Close();
}
if (!File.Exists("data/master.txt"))
{
    File.Create("data/master.txt");
    File.WriteAllText("data/master.txt", "defaultpassword");
}

// Главное меню менеджера паролей.
while (true)
{
    Console.WriteLine("Менеджер паролей\n\n1. Добавить.\n2. Просмотреть все логины.\n3. Просмотреть пароль для конкретного логина.\n4. Изменить пароль для логина.");
    string input = Console.ReadLine();

    switch (input)
    {
        case "1": Console.Clear(); MenuSections.AddApp(); break;
        case "2": Console.Clear(); MenuSections.ViewApps(); break;
        case "3": Console.Clear(); MenuSections.ViewPassword(); break;
        case "4": Console.Clear(); MenuSections.EditPassword(); break;
        default: Console.Clear(); Console.WriteLine("Ошибка при навигации в меню. Попробуйте снова:"); continue;
    }
}