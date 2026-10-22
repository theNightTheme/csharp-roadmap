using System;

namespace PassManager
{
    public static class Helpers
    {
        // Функции разделены, дабы их было удобно использовать в коде.
        // Функция для проверки валидности ввода (не пустой и не содержит вертикальных черт).
        public static bool IsValid(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && !input.Contains("|");
        }

        // Функция для проверки наличия пробелов в строке.
        public static bool HasNoSpaces(string input)
        {
            return !input.Contains(" ");
        }

        public static string[] ReadAndSplitFile(string filePath)
        {
            return File.ReadAllLines(filePath).SelectMany(x => x.Split("|")).ToArray();
        }

        public static string[] GetApps(string[] fileDataMassive)
        {
            return fileDataMassive.Where((x, index) => index % 3 == 0).Distinct().OrderBy(x => x).ToArray();
        }

        public static void CheckMasterKey()
        {
            string masterkey;
            string actualMasterkey = File.ReadAllText("data/master.txt");

            Console.WriteLine("Для доступа к этому разделу введите мастер-ключ:");
            masterkey = Console.ReadLine();
            while (masterkey != actualMasterkey)
            {
                Console.WriteLine("Мастер-ключ неверный. Попробуйте снова:");
                masterkey = Console.ReadLine();
            }
        }
    }
}
