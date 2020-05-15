﻿using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;


namespace priv_BMI_zpliku
{
    // nazwa klasy informuje o funkcjonalności, interface to nazwa zastrzeżona
    // ctrl + k i ctrl+u - komentowanie
    // ctr + r ctrl +r - nazwa klasy/metody/zmiennej/prop
    // ctrl + shift +s - zapisz wszystkie pliki
    // ctrl + . - proponowane naprawienie problemu
    // F5 - star debugera
    // shift + F5 zatrzymaj debuggera
    // ctrl+ shit + a dodaj nowy plik



    public class BmiCalculation
    {
        //nazwy metod bez dużej litery I na poczatku - uważać na sugestywność nazw!

        public void ChooseTheSource(string[] args)
        {

            if (ValidateIfCommandArgsAreCorrect(args))
            {
                Console.WriteLine(SharedResources.CorrectValuesInArgs);
                CalculateFromArgs(args);
            }
            else
            {
                Console.WriteLine("Brak poprawnie wpisanych wartości w wierszu poleceń");

                int answer = GetUserAnswerOfCalculationMethod();

                if (answer == 1)
                {
                    CalculateFromConsole();
                }
                else
                {
                    CalculateFromFile();
                }
            }



        }

        private int GetUserAnswerOfCalculationMethod()
        {
            int answer = 0;

            do
            {
                Console.WriteLine(
                    "Proszę wybrać sposób pobrania danych: wpisz 1 jeżeli chcesz wprowadzić dane ręcznie lub 2 jeżeli chcesz je wczytać z pliku 'PoliczMojeBmi.txt' ");

                Int32.TryParse(Console.ReadLine(), out int result);

                if (result != 1 && result != 2)
                    Console.WriteLine("Proszę wpisać cyfrę 1 lub 2!");
                else
                    answer = result;
            } while (answer == 0);

            return answer;
        }

        private bool ValidateIfCommandArgsAreCorrect(string[] args)
        {
            return Validator.DoesItContainsThreeArguments(args) && 
                   Validator.DoesFirstParamIsManOrWoman(args) &&
                   Validator.DoesThisParamContainsDoubleValue(args[1]) &&
                   Validator.DoesThisParamContainsDoubleValue(args[2]);
        }

        public void CalculateFromConsole()
        {
            UserTemplate user = new UserTemplate();
            double number;

            Console.WriteLine(
                "Cześć! Wpisz, proszę poniższe wartości. Pamiętaj, żeby używać przecinka zamiast kropki :)");

            do
            {
                Console.WriteLine(
                    "Określ płeć. Wpisz 1 jeżeli jesteś mężczyzną lub 2 jeżeli jesteś kobietą i zatwierdź klawiszem Enter");
                Double.TryParse(Console.ReadLine(), out number);
                if (number != 1 & number != 2)
                    Console.WriteLine("Proszę wpisać cyfrę 1 lub 2!");
                else
                    user.Sex = number;
            } while (user.Sex == 0);


            do
            {
                Console.WriteLine("Proszę podać wagę w kilogramach [kg] i zatwierdzić klawiszem Enter");
                Double.TryParse(Console.ReadLine(), out number);
                if (number <= 0)
                    Console.WriteLine("Proszę wpisać wartość dodatnią i stosować ',' zamiast '.'!");
                else
                    user.Weight = number;
            } while (user.Weight == 0);


            do
            {
                Console.WriteLine("Proszę podać wzrost w metrach [m] i zatwierdzić klawiszem Enter");
                Double.TryParse(Console.ReadLine(), out number);
                if (!(number > 0))
                    Console.WriteLine("Proszę wpisać wartość dodatnią i stosować ',' zamiast '.'!");
                else
                    user.Height = number;
            } while (user.Height == 0);







            Console.WriteLine($"Twój wskaźnik BMI wynosi: {user.Bmi} ");
            Console.WriteLine($"{user.ShowInformation()}");
            Console.WriteLine("Źródło : https://pl.wikipedia.org/wiki/Wska%C5%BAnik_masy_cia%C5%82a");

            FileGenerator.GenerateTheFile(user);



        }

        public void CalculateFromFile()
        {
            UserTemplate user = new UserTemplate();

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Tytus\Desktop\PoliczMojeBmi.txt");

            if (lines.Length < 4)
            {
                Console.WriteLine("Nie udało się wczytać pliku - sprawdź poprawność formatu lub uzupełnij dane!");
                return;
            }

            Double.TryParse(lines[1], out double result1);
            Double.TryParse(lines[2], out double result2);
            Double.TryParse(lines[3], out double result3);

            if (result1 == 0 || result2 == 0 || result3 == 0)
            {

                Console.WriteLine("Nie udało się wczytać pliku - sprawdź poprawność formatu lub uzupełnij dane!");
                return;
            }


            user.Sex = Convert.ToDouble(lines[1]);
            user.Weight = Convert.ToDouble(lines[3]);
            user.Height = Convert.ToDouble(lines[2]);

            FileGenerator.GenerateTheFile(user);

        }

        public void CalculateFromArgs(string[] args)
        {

            UserTemplate user = new UserTemplate();

            if (args[0] == "M")
                user.Sex = 1;
            else if (args[0] == "K")
                user.Sex = 2;

            user.Weight = Convert.ToDouble(args[2]);
            user.Height = Convert.ToDouble(args[1]);



            Console.WriteLine($"Twój wskaźnik BMI wynosi: {user.Bmi} ");
            Console.WriteLine($"{user.ShowInformation()}");
            Console.WriteLine("Źródło : https://pl.wikipedia.org/wiki/Wska%C5%BAnik_masy_cia%C5%82a");

            



        }
    }
}