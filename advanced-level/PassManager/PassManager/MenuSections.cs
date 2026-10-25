using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PassManager
{
    public static class MenuSections
    {
        // Функция для записи данных в файл info.txt. Проверяет валидность ввода и наличие уже существующих пользователей для приложения.
        public static void AddApp()
        {
            string app, username, password, toAppend;

            // Читаем данные из файла info.txt и разделяем их на массив строк.
            string[] fileData = Helpers.ReadAndSplitFile("data/info.txt");

            // Обрабатываем название приложения. Удаляем пробелы в начале и конце, проверяем валидность и делаем первую букву заглавной.
            Console.WriteLine("Введите название приложения:");
            app = Helpers.ReadInputOrException().Trim();
            if (Helpers.IsValid(app))
            {
                app = char.ToUpper(app[0]) + app.Substring(1);
            }
            while (!Helpers.IsValid(app))
            {
                Console.WriteLine("Название приложения не может быть пустым или содержать вертикальную черту.\nВведите название приложения снова:");
                app = Helpers.ReadInputOrException().Trim();
                if (Helpers.IsValid(app))
                {
                    app = char.ToUpper(app[0]) + app.Substring(1);
                }
            }

            // Обрабатываем имя пользователя. Удаляем пробелы в начале и конце, проверяем валидность и делаем все буквы строчными.
            Console.WriteLine("Введите имя пользователя:");
            username = Helpers.ReadInputOrException().Trim();
            if (Helpers.IsValid(username) && Helpers.HasNoSpaces(username))
            {
                username = username.ToLower();
            }

            while (true)
            {
                bool isValid = true;

                for (int i = 0; i < fileData.Length; i += 3)
                {
                    if (fileData[i] == app && fileData[i + 1] == username)
                    {
                        Console.WriteLine($"Пользователь {username} уже существует для приложения {app}.\nВведите имя пользователя снова:");
                        username = Helpers.ReadInputOrException().Trim();
                        if (Helpers.IsValid(username) && Helpers.HasNoSpaces(username))
                        {
                            username = username.ToLower();
                        }

                        isValid = false;
                        break;
                    }
                }

                if (!isValid)
                {
                    continue;
                }

                if (!Helpers.IsValid(username) || !Helpers.HasNoSpaces(username))
                {
                    Console.WriteLine("Имя пользователя не может быть пустым, содержать вертикальную черту или пробелы.\nВведите имя пользователя снова:");
                    username = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(username) && Helpers.HasNoSpaces(username))
                    {
                        username = username.ToLower();
                    }
                    continue;
                }
                break;
            }

            // Обрабатываем пароль для пользователя. Проверяем валидность и наличие пробелов внутри.
            Console.WriteLine($"Введите пароль для пользователя {username}:");
            password = Helpers.ReadInputOrException();
            while (!Helpers.IsValid(password) || !Helpers.HasNoSpaces(password))
            {
                Console.WriteLine($"Пароль не может быть пустым, содержать вертикальную черту или пробелы.\nВведите пароль для пользователя {username} снова:");
                password = Helpers.ReadInputOrException();
            }

            toAppend = app + "|" + username + "|" + password;

            File.AppendAllText("data/info.txt", toAppend + Environment.NewLine);

            Console.WriteLine($"Данные успешно записаны. Нажмите любую клавишу...");
            Console.ReadKey();

            Console.Clear();
        }

        // Функция для просмотра данных из файла info.txt. Позволяет просмотреть приложения и учетные записи в них, а также пароли (по запросу).
        public static void ViewApps()
        {
            string username, toWrite = "";
            int passwordLength;

            string[] fileData = Helpers.ReadAndSplitFile("data/info.txt");
            string[] apps = Helpers.GetApps(fileData);

            foreach (string appName in apps)
            {
                toWrite += $"Приложение: {appName}\n";
                toWrite += "--------------------\n";
                for (int i = 0; i < fileData.Length; i += 3)
                {
                    if (fileData[i] == appName)
                    {
                        username = fileData[i + 1];
                        passwordLength = fileData[i + 2].Length;
                        toWrite += $"Пользователь: {username}, Пароль: {new string('*', passwordLength)}\n";
                    }
                }
                toWrite += "\n";
            }

            Console.WriteLine(toWrite);

            Console.WriteLine($"Нажмите любую клавишу...");
            Console.ReadKey();

            Console.Clear();
        }

        // Функция для просмотра пароля конкретного пользователя в конкретном приложении.
        public static void ViewPassword()
        {
            string readApp, username, password = "";

            string[] fileData = Helpers.ReadAndSplitFile("data/info.txt");
            string[] apps = Helpers.GetApps(fileData);

            Helpers.CheckMasterKey();

            Console.WriteLine("Мастер-ключ подтвержден. Введите название приложения:");
            readApp = Helpers.ReadInputOrException().Trim();
            if (Helpers.IsValid(readApp))
            {
                readApp = char.ToUpper(readApp[0]) + readApp.Substring(1);
            }

            while (!apps.Contains(readApp))
            {
                if (!Helpers.IsValid(readApp))
                {
                    Console.WriteLine("Название приложения не может быть пустым или содержать вертикальную черту.\nВведите название приложения снова: ");
                    readApp = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(readApp))
                    {
                        readApp = char.ToUpper(readApp[0]) + readApp.Substring(1);
                    }
                }
                else
                {
                    Console.WriteLine("Такого приложения нет в менеджере. Попробуйте ввести название приложения снова:");
                    readApp = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(readApp))
                    {
                        readApp = char.ToUpper(readApp[0]) + readApp.Substring(1);
                    }
                }
            }

            Console.WriteLine("Введите имя пользователя:");
            username = Helpers.ReadInputOrException().Trim();
            if (Helpers.IsValid(username) && Helpers.HasNoSpaces(username))
            {
                username = username.ToLower();
            }

            while (true)
            {

                if (!Helpers.IsValid(username) || !Helpers.HasNoSpaces(username))
                {
                    Console.WriteLine("Имя пользователя не может быть пустым, содержать вертикальную черту или пробелы.\nВведите имя пользователя снова:");
                    username = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(username) && Helpers.HasNoSpaces(username))
                    {
                        username = username.ToLower();
                    }
                    continue;
                }
                break;
            }

            while (true)
            {
                bool found = false;

                for (int i = 0; i < fileData.Length; i += 3)
                {
                    if (fileData[i] == readApp && fileData[i + 1] == username)
                    {
                        password = fileData[i + 2];

                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Console.WriteLine($"Пользователь {username} не найден в приложении {readApp}. Попробуйте ввести имя пользователя снова:");
                    username = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(username) && Helpers.HasNoSpaces(username))
                    {
                        username = username.ToLower();
                    }
                    continue;
                }
                break;
            }

            Console.WriteLine($"Пароль для пользователя {username} в приложении {readApp}: {password}");

            Console.WriteLine($"Нажмите любую клавишу...");
            Console.ReadKey();

            Console.Clear();
        }

        // Функция для изменения пароля пользователя в приложении. 
        public static void EditPassword()
        {
            string readApp, username, newPassword = "", confirmNewPassword = "";

            string[] fileData = Helpers.ReadAndSplitFile("data/info.txt");
            string[] apps = Helpers.GetApps(fileData);

            Helpers.CheckMasterKey();
            Console.WriteLine("Мастер-ключ подтвержден. Введите название приложения:");
            readApp = Helpers.ReadInputOrException().Trim();

            if (Helpers.IsValid(readApp))
            {
                readApp = char.ToUpper(readApp[0]) + readApp.Substring(1);
            }
            while (!apps.Contains(readApp))
            {
                if (!Helpers.IsValid(readApp))
                {
                    Console.WriteLine("Название приложения не может быть пустым или содержать вертикальную черту.\nВведите название приложения снова: ");
                    readApp = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(readApp))
                    {
                        readApp = char.ToUpper(readApp[0]) + readApp.Substring(1);
                    }
                }
                else
                {
                    Console.WriteLine("Такого приложения нет в менеджере. Попробуйте ввести название приложения снова:");
                    readApp = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(readApp))
                    {
                        readApp = char.ToUpper(readApp[0]) + readApp.Substring(1);
                    }
                }
            }

            Console.WriteLine("Введите имя пользователя:");
            username = Helpers.ReadInputOrException().Trim();
            if (Helpers.IsValid(username) && Helpers.HasNoSpaces(username))
            {
                username = username.ToLower();
            }

            while (true)
            {

                if (!Helpers.IsValid(username) || !Helpers.HasNoSpaces(username))
                {
                    Console.WriteLine("Имя пользователя не может быть пустым, содержать вертикальную черту или пробелы.\nВведите имя пользователя снова:");
                    username = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(username) && Helpers.HasNoSpaces(username))
                    {
                        username = username.ToLower();
                    }
                    continue;
                }
                break;
            }

            while (true)
            {
                bool found = false;

                for (int i = 0; i < fileData.Length; i += 3)
                {
                    if (fileData[i] == readApp && fileData[i + 1] == username)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Console.WriteLine($"Пользователь {username} не найден в приложении {readApp}. Попробуйте ввести имя пользователя снова:");
                    username = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(username) && Helpers.HasNoSpaces(username))
                    {
                        username = username.ToLower();
                    }
                    continue;
                }
                break;
            }


            while (true)
            {
                Console.WriteLine($"Введите новый пароль для пользователя {username}:");
                newPassword = Helpers.ReadInputOrException();

                if (!Helpers.IsValid(newPassword) || !Helpers.HasNoSpaces(newPassword))
                {
                    Console.WriteLine($"Пароль не может быть пустым, содержать вертикальную черту или пробелы.\nВведите пароль для пользователя {username} снова:");
                    continue;
                }

                Console.WriteLine($"Подтвердите новый пароль для пользователя {username}:");
                confirmNewPassword = Helpers.ReadInputOrException();

                if (newPassword != confirmNewPassword)
                {
                    Console.WriteLine("Пароли не совпадают. Введите новый пароль снова, а затем подтвердите его:");
                    continue;
                }

                break;
            }

            List<string> newLines = new List<string>();

            for (int i = 0; i < fileData.Length; i += 3)
            {
                if (fileData[i] == readApp && fileData[i + 1] == username)
                {
                    fileData[i + 2] = newPassword;
                    break;
                }
            }

            for (int i = 0; i < fileData.Length; i += 3)
            {
                string line = $"{fileData[i]}|{fileData[i + 1]}|{fileData[i + 2]}";
                newLines.Add(line);
            }

            File.WriteAllLines("data/info.txt", newLines);

            Console.WriteLine($"Пароль для пользователя {username} в приложении {readApp} успешно изменен. Нажмите любую клавишу...");
            Console.ReadKey();

            Console.Clear();
        }

        // Функция для удаления учетной записи.
        public static void DeleteAccount()
        {
            string readApp, username, enteredUsername;

            string[] fileData = Helpers.ReadAndSplitFile("data/info.txt");
            string[] apps = Helpers.GetApps(fileData);

            Helpers.CheckMasterKey();
            Console.WriteLine("Мастер-ключ подтвержден. Введите название приложения:");
            readApp = Helpers.ReadInputOrException().Trim();

            if (Helpers.IsValid(readApp))
            {
                readApp = char.ToUpper(readApp[0]) + readApp.Substring(1);
            }

            while (!apps.Contains(readApp))
            {
                if (!Helpers.IsValid(readApp))
                {
                    Console.WriteLine("Название приложения не может быть пустым или содержать вертикальную черту.\nВведите название приложения снова: ");
                    readApp = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(readApp))
                    {
                        readApp = char.ToUpper(readApp[0]) + readApp.Substring(1);
                    }
                }
                else
                {
                    Console.WriteLine("Такого приложения нет в менеджере. Попробуйте ввести название приложения снова:");
                    readApp = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(readApp))
                    {
                        readApp = char.ToUpper(readApp[0]) + readApp.Substring(1);
                    }
                }
            }

            Console.WriteLine("Введите имя пользователя:");
            username = Helpers.ReadInputOrException().Trim();
            if (Helpers.IsValid(username) && Helpers.HasNoSpaces(username))
            {
                username = username.ToLower();
            }

            while (true)
            {

                if (!Helpers.IsValid(username) || !Helpers.HasNoSpaces(username))
                {
                    Console.WriteLine("Имя пользователя не может быть пустым, содержать вертикальную черту или пробелы.\nВведите имя пользователя снова:");
                    username = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(username) && Helpers.HasNoSpaces(username))
                    {
                        username = username.ToLower();
                    }
                    continue;
                }
                break;
            }

            while (true)
            {
                bool found = false;

                for (int i = 0; i < fileData.Length; i += 3)
                {
                    if (fileData[i] == readApp && fileData[i + 1] == username)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Console.WriteLine($"Пользователь {username} не найден в приложении {readApp}. Попробуйте ввести имя пользователя снова:");
                    username = Helpers.ReadInputOrException().Trim();
                    if (Helpers.IsValid(username) && Helpers.HasNoSpaces(username))
                    {
                        username = username.ToLower();
                    }
                    continue;
                }
                break;
            }

            Console.WriteLine($"Чтобы удалить пользователя {username} из приложения {readApp}, введите \"{readApp}:{username}\" с учетом регистра:");
            
            while (true)
            {
                enteredUsername = Helpers.ReadInputOrException();

                if (enteredUsername == $"{readApp}:{username}")
                {
                    List<string> newLines = new List<string>();

                    for (int i = 0; i < fileData.Length; i += 3)
                    {
                        if (fileData[i] == readApp && fileData[i + 1] == username)
                        {
                            continue;
                        }

                        string line = $"{fileData[i]}|{fileData[i + 1]}|{fileData[i + 2]}";
                        newLines.Add(line);
                    }

                    File.WriteAllLines("data/info.txt", newLines);

                    Console.WriteLine($"Пользователь {username} удален из приложения {readApp}. Нажмите любую клавишу...");
                    Console.ReadKey();

                    Console.Clear();

                    break;
                }
                else
                {
                    Console.WriteLine($"Неправильный ввод. Попробуйте снова:");
                }
            }
        }
    }
}