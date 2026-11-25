using delegates;
using System;
using System.Collections.Generic;

namespace delegates
{
    internal class Program
    {
        private static void Main()
        {
            
            Console.WriteLine("=== Поиск максимального элемента коллекции ===");

            var people = new List<Person>
            {
                new Person { Name = "Bob",   Salary = 95000 },
                new Person { Name = "boB2",   Salary = 120000 },
                new Person { Name = "Bobster", Salary = 78500 },
                new Person { Name = "notBob", Salary = 150000 },
                new Person { Name = "bObest", Salary = 64000 }
            };

            Person richest = people.GetMax(p => p.Salary);
            Console.WriteLine($"Элемент с максимальным значением поля Salary: {richest?.Name} ({richest?.Salary})");
            Person longestName = people.GetMax(p => p.Name.Length);
            Console.WriteLine($"Элемент с самым длинным именем: {longestName?.Name}");

            Console.WriteLine();

            Console.WriteLine("=== Обход каталога файлов ===");

            string targetPath = AppContext.BaseDirectory;  // для пример взят каталог с .ехе

            var searcher = new FileSearcher();
            int filesFound = 0;

            searcher.FileFound += (sender, args) =>
            {
                filesFound++;
                Console.WriteLine($"Найден файл: {args.FileName}");

                if (filesFound >= 3) // для проверки поменять кол-во файлов
                {
                    args.Cancel = true;
                    Console.WriteLine(" Достигнуто 5 файлов — дальнейший поиск отменён из обработчика.");
                }
            };

            bool cancelled = searcher.Search(targetPath);
            Console.WriteLine();
            Console.WriteLine($"Поиск завершён. Отменён: {cancelled}. Всего файлов обработано: {filesFound}");
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public float Salary { get; set; }
    }
}