namespace HomeWork_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // --------------- Задча 1 - Приложение по управлению фильмами -----------------
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задача 1 - Приложение по управлению фильмами");
            Console.ForegroundColor = ConsoleColor.White;
            FilmManager films = new FilmManager();
            films.AddFilm(new Film { Name = "Кровавый спорт", Genre = "Боевик", Regisseur = "Вася", Year = 1987 });
            films.AddFilm(new Film { Name = "Однажды в Вегасе", Genre = "Комедия", Regisseur = "Маша", Year = 2007 });
            films.AddFilm(new Film { Name = "Аватар", Genre = "Фантастика", Regisseur = "Кэмерон", Year = 2009 });
            films.SaveXML("Films.xml");
            Console.WriteLine("\nВыгруженный из xml-файла список фильмов:");
            films.Clear(); // Чистим исходную коллекцию фильмов для проверки корректного считывания инф-ии из файла
            // Цикл заполнения пустой коллекции данными из файла и выводы фильмов в консоль
            foreach (IFilm el in films.DownloadXML("Films.xml")) 
            {
                films.AddFilm(el);
                el.Print();
            }
            films.SaveJSON("Films.json");                        
            Console.WriteLine("\nВыгруженный из json-файла список фильмов:");
            films.Clear();
            foreach (var el in films.DownloadJSON("Films.json"))
            {
                films.AddFilm(el);
                el.Print();
            }
            // Добавление нового фильма в коллекцию
            Film film = new Film();            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nДобавление нового фильма пользователем:\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Введите название фильма -> ");
            film.Name = Console.ReadLine();
            Console.Write("Введите жанр фильма -> ");
            film.Genre = Console.ReadLine();
            Console.Write("Введите режиссёра фильма -> ");
            film.Regisseur = Console.ReadLine();
            Console.Write("Введите год премьеры фильма -> ");
            try
            {
                if (Int32.TryParse(Console.ReadLine(), out int num) && num >= 1895 && num <= 2024)
                {                    
                    film.Year = num;
                    films.AddFilm(film);
                }
                else // Исключительная ситуация
                {                   
                    throw new InvalidValueException("Некорректно введён год премьеры!");
                }
            }
            catch (InvalidValueException ex) // Обработка исключения
            {
                Console.WriteLine($"\nОшибка добавления нового фильма: {ex.Message}");
            }                
            // Удаление фильма из коллекции по наименованию фильма
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nУдаление фильма из коллекции пользователем:\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Введите название фильма -> ");
            films.RemoveFilm(Console.ReadLine());
            films.SaveXML("Films.xml");
            Console.WriteLine("\nВыгруженный из xml-файла обновлённый список фильмов:");
            foreach (IFilm el in films.DownloadXML("Films.xml"))
                el.Print();
            films.SaveJSON("Films.json");
            Console.WriteLine("\nВыгруженный из json-файла обновлённый список фильмов:");
            foreach (var el in films.DownloadJSON("Films.json"))
                el.Print();
            // Поиск фильмов по названию
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nПоиск фильмов в коллекции по названию:\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Введите название фильма -> ");
            foreach(var el in films.FindFilmName(Console.ReadLine()))
                el.Print();
            // Поиск фильмов по жанру
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nПоиск фильмов в коллекции по жанру:\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Введите жанр -> ");
            foreach (var el in films.FindFilmGenre(Console.ReadLine()))
                el.Print();
            // Сортировка фильмов по возрастанию года премьеры
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nСортировка фильмов по возрастанию года премьеры:\n");
            Console.ForegroundColor = ConsoleColor.White;            
            foreach (var el in films.SortYear())
                el.Print();

            // --------------- Задча 2 - Система управления контактами -----------------
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задача 2 - Система управления контактами");
            Console.ForegroundColor = ConsoleColor.White;
            ContactManager<IContact> book = new ContactManager<IContact>();
            book.AddContact(new Contact { Name = "Вася", PhoneNumber = "789456123321" });
            book.AddContact(new Contact { Name = "Петя", PhoneNumber = "8343444843" });
            book.AddContact(new Contact { Name = "Маша", PhoneNumber = "321654974513" });
            book.AddContact(new Contact { Name = "Маша", PhoneNumber = "472318947243" });
            book.AddContact(new Contact { Name = "Саша", PhoneNumber = "55-55-55" });
            book.SaveXML("PhoneBook.xml"); // Сохраняем данные в xml-файл
            book.Clear(); // Чистим объект "Список контактов" для проверки корректного считывания данных из файла
            // В объект "Список контактов" переносим данных, считанные из xml-файла
            Console.WriteLine("\nСписок контактов,выгруженный из xml-файла:");
            foreach (var el in book.DownloadXML("PhoneBook.xml")) 
                book.AddContact(el);
            book.Print(); // Выводим в консоль данные из объекта "Список контактов" (перезаписанным данными из xml-файла)
            book.SaveJSON("PhoneBook.json");
            book.Clear();
            Console.WriteLine("\nСписок контактов,выгруженный из json-файла:");
            foreach (var el in book.DownloadJSON("PhoneBook.json"))
                book.AddContact(el);
            book.Print();
            int choice = 0; // Номер пункта меню телефонной книги, которые выбрал пользователь
            while(choice != 6) // Цикл взаимодействия пользователя с меню
            {
                bool flag = false; // (если true, то список контактов изменён и его надо перезаписать)
                choice = Menu();
                switch (choice)
                {
                    case 1:
                        // Добавление нового контакта в телефонную книгу
                        IContact contact = new Contact();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nДобавление нового контакта пользователем:");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Введите имя -> ");
                        contact.Name = Console.ReadLine();
                        Console.Write("Введите номер телефона -> ");
                        contact.PhoneNumber = Console.ReadLine();
                        book.AddContact(contact);                        
                        flag = true;
                        break;
                    case 2:
                        // Удаление контакта из телефонной книги            
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nУдаление контакта пользователем:");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Введите номер телефона -> ");
                        book.RemoveContact(Console.ReadLine());
                        flag = true;
                        break;
                    case 3:
                        // Поиск контакта по имени
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nПоиск контакта по имени:");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Введите имя -> ");  
                        string name = Console.ReadLine();
                        if (book.FindName(name).Count() > 0)
                        {
                            Console.WriteLine("\nУспешно найдено:");
                            foreach (var el in book.FindName(name))
                                Console.WriteLine($"Имя: {el.Value}, номер телефона: {el.Key}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nКонтакта с таким именем в телефонной книге нет!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write("Для продолжения нажмите любую клавишу");
                        Console.ReadKey(true);
                        break;
                    case 4:
                        // Поиск контакта по номеру телефона
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nПоиск контакта по номеру телефона:");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Введите номер телефона -> ");
                        string numPhone = Console.ReadLine();
                        if (book.FindPhoneNumber(numPhone).PhoneNumber == "-1")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nКонтакта с таким номером телефона в телефонной книге нет!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                            Console.WriteLine($"\nУспешно найдено:\nИмя: {book.FindPhoneNumber(numPhone).Name}, номер телефона: " +
                                $"{book.FindPhoneNumber(numPhone).PhoneNumber}");
                        Console.Write("Для продолжения нажмите любую клавишу");
                        Console.ReadKey(true);
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nСортировка контактов в алфавитном порядке:");
                        Console.ForegroundColor = ConsoleColor.White;
                        book.SortName();
                        flag = true;
                        break;
                    default:
                        break;
                }
                // Сохранение и вывод изменнённых данных
                if (flag)
                {
                    book.SaveXML("PhoneBook.xml"); // Сохраняем данные в xml-файл
                    book.SaveJSON("PhoneBook.json"); // Сохраняем данные в json-файл
                    book.Clear();
                    foreach (var el in book.DownloadXML("PhoneBook.xml"))
                        book.AddContact(el);
                    Console.WriteLine("\nВывод изменнённой телефонной книги из xml-файла:");
                    book.Print();
                    book.Clear();
                    foreach (var el in book.DownloadJSON("PhoneBook.json"))
                        book.AddContact(el);
                    Console.WriteLine("\nВывод изменнённой телефонной книги из json-файла:");
                    book.Print();
                }                
            }           
        }
        static int Menu()
        {            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nМеню телефонной книги:\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("1. Добавить новый контакт\n2. Удалить контакт\n3. Найти контакт по имени\n" +
                "4. Найти контакт по номеру телефона\n5. Отсортировать телефонную книгу по алфавитному порядку\n" +
                "6. Выход\n\nВыберите номер пункта меню -> ");
            try
            {
                if (Int32.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 6)
                {
                    return choice;
                }
                else // Исключительная ситуация
                {
                    throw new InvalidValueException("Некорректно выбран пункт меню!");
                }
            }
            catch (InvalidValueException ex) // Обработка исключения
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nОшибка: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                return 0;
            }            
        }
    }
}