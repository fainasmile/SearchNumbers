using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace ProjectSearchNumbers
{
    public class SearchNumbersOfCars
    {
        //запись из файла
        public static string[] ReadFromFile(string nameFile)
        {
            try
            {
                return File.ReadAllLines(nameFile);
            }
            catch
            {
                return null;
            }
        }

        //добавление в коллекцию
        public static List<string> AddFileToList(string[] numbers)
        {
            List<string> numbersList = new List<string>();
            foreach (string x in numbers)
            {
                numbersList.Add(x);
            }
            return numbersList;

        }

        //проверка на ввод пустой строки
        public static bool InputNumberIsEmpty(string numberInput)
        {
            return String.IsNullOrWhiteSpace(numberInput);
        }

        //проверка на содержание пробелов в вводимой строки
        public static bool InputNumberContainsSpace(string numberInput)
        {
            return numberInput.Contains(" ");
        }

        public static bool InputNumberHasBigLength(string numberInput)
        {
            return numberInput.Length > 9;

        }

        //проверка на формат
        public static bool InputNumberHasCorrectFormat(string numberInput)
        {
            Regex oneSymbol = new Regex(@"^[а-яА-Я]{1}$");
            Regex fourSymbols = new Regex(@"^[а-яА-Я]{1}\d{1,3}$");
            Regex sixSymbols = new Regex(@"^[а-яА-Я]{1}\d{3}[а-яА-Я]{1,2}$");
            Regex eightSymbols = new Regex(@"^[а-яА-Я]{1}\d{3}[а-яА-Я]{2}\d{2,3}$");
          
            if (numberInput.Length == 1)
            {
                return oneSymbol.IsMatch(numberInput);
            }
            else if (numberInput.Length > 1 && numberInput.Length < 5)
            {
                return fourSymbols.IsMatch(numberInput);
            }
            else if (numberInput.Length > 4 && numberInput.Length < 7)
            {
                return sixSymbols.IsMatch(numberInput);
            }

            else if (numberInput.Length > 6 && numberInput.Length < 10)
            {
                return eightSymbols.IsMatch(numberInput);
            }
            else
            {
                return false;
            }
        }

        public static List<string> GetResultsOfSearch(string numberInput, List<string> numbersList)
        {
            //поиск результатов при вводе полного номера
            List<string> results = numbersList.FindAll(a => a == numberInput);

            if (results.Count == 0 && numberInput.Length != 9)
            {
                //поиск результатов при вводе части номера
                results = numbersList.FindAll(a => a.Substring(0, numberInput.Length) == numberInput);
            }

            return results;
        }

        public static void PrintResults(string numberInput, List<string> results)
        {
            Console.Write("***************************************");
            Console.WriteLine($"\nРезультаты поиска по номеру \"{numberInput}\":");
            Console.WriteLine("Количество найденных номеров: {0} ", results.Count);
            Console.WriteLine("Список номеров:");
            foreach (string x in results)
            {
                Console.WriteLine(x);
            }
            Console.Write("***************************************");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Программа для поиска автомобильных номеров");

            string[] numbers = ReadFromFile("NumbersOfCars.txt");
            if (numbers != null)
            {
                List<string> numbersList = AddFileToList(numbers);
                do
                {
                    string numberInput;
                    Console.WriteLine("\nВведите полный номер автомобиля или его часть(затем нажмите Enter):");

                    while (true)
                    {
                        numberInput = Console.ReadLine();
                        if (InputNumberIsEmpty(numberInput))
                        {
                            Console.WriteLine("Вы ввели пустую строку. Попробуйте ввести еще раз:");
                        }
                        else if (InputNumberContainsSpace(numberInput))
                        {
                            Console.WriteLine("Введенная строка содержит проблемы. Попробуйте ввести еще раз:");
                        }

                        else if (InputNumberHasBigLength(numberInput))
                        {
                            Console.WriteLine("Введенная строка содержит больше 9 символов. " +
                                "\nПопробуйте ввести еще раз:");
                        }
                        else if (InputNumberHasCorrectFormat(numberInput) == false)
                        {

                            Console.WriteLine("Введенная строка не соответствует формату номера автомобиля. " +
                                "\nФормат: {(1рус.б.)(3цифры)(2рус.б.)(2или3цифры)}" +
                                "\nПопробуйте ввести еще раз:");
                        }
                        else
                            break;
                 

                    }

                    List<string> results = GetResultsOfSearch(numberInput, numbersList);
                    if (results.Count == 0)
                    {
                        Console.WriteLine("Поиск не дал результатов");
                    }
                    else
                    {
                        PrintResults(numberInput, results);
                    }

                    Console.WriteLine("\nДля продолжения поиска нажмите ENTER, для выхода Esc");


                } while (Console.ReadKey(true).Key == ConsoleKey.Enter);
            }
            else Console.WriteLine("Хранилеще номеров пусто или путь до файла не найден");
        }
    }
}


