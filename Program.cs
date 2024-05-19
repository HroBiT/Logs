using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Wybierz co chcesz zrobic:");
            Console.WriteLine("1. Obliczenia logarytmiczne");
            Console.WriteLine("2. Operacje na ciagach");
            Console.WriteLine("3. Wyjdz z programu");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("cos nie tak");
                continue;
            }

            switch (choice)
            {
                case 1:
                    LogarithmMenu();
                    break;
                case 2:
                    CalculateSequence();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("cos nie tak");
                    break;
            }
        }
    }

    static void LogarithmMenu()
    {
        Console.Clear();
        Console.WriteLine("Wybierz rodzaj obliczeń logarytmicznych:");
        Console.WriteLine("1. Proste logarytmy");
        Console.WriteLine("2. Działania na logarytmach");
        Console.WriteLine("3. Znajdz np. log_2(x)=4");
        Console.WriteLine("4. Znajdz np. log_x(16)=4");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("cos nie tak");
            return;
        }

        switch (choice)
        {
            case 1:
                CalculateSimpleLogarithms();
                break;
            case 2:
                CalculateLogarithmOperations();
                break;
            case 3:
                FindXFromLogEquation();
                break;
            case 4:
                FindXFromLogBase();
                break;
            default:
                Console.WriteLine("cos nie tak");
                break;
        }
    }

    static void CalculateSimpleLogarithms()
    {
        Console.Clear();
        Console.WriteLine("Podaj działanie logarytmiczne do obliczenia (np. log_2(16)): ");
        string input = Console.ReadLine().Trim();

        if (!TryParseLogarithm(input, out double result))
        {
            Console.WriteLine("cos nie tak");
            Console.ReadLine();
            return;
        }

        Console.WriteLine($"Wynik obliczeń: {result}");
        Console.ReadLine();
    }

    static void CalculateLogarithmOperations()
    {
        Console.Clear();
        Console.WriteLine("Podaj działanie na logarytmach do obliczenia (np. log_2(16) + log_2(16)): ");
        string input = Console.ReadLine().Trim();

        if (!TryParseLogarithmOperation(input, out double result))
        {
            Console.WriteLine("cos nie tak");
            Console.ReadLine();
            return;
        }

        Console.WriteLine($"Wynik obliczeń: {result}");
        Console.ReadLine();
    }

    static void FindXFromLogEquation()
    {
        Console.Clear();
        Console.WriteLine("Podaj równanie w formacie log_b(x)=a (np. log_2(x)=4): ");
        string input = Console.ReadLine().Trim();

        string[] parts = input.Split(new[] { "log_", "(", ")", "=" }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 3 && double.TryParse(parts[0], out double baseNumber) && double.TryParse(parts[2], out double result)) // musi byc 3 
        {
            double x = Math.Pow(baseNumber, result); 
            Console.WriteLine($"x = {x}");
        }
        else
        {
            Console.WriteLine("cos nie tak");
        }
        Console.ReadLine();
    }

    static void FindXFromLogBase()
    {
        Console.Clear();
        Console.WriteLine("Podaj równanie w formacie log_x(b)=a (np. log_x(16)=4): ");
        string input = Console.ReadLine().Trim();

        string[] parts = input.Split(new[] { "log_", "(", ")", "=" }, StringSplitOptions.RemoveEmptyEntries);  //dzieli wzgeledem log itd

        if (parts.Length == 3 && double.TryParse(parts[1], out double number) && double.TryParse(parts[2], out double result))
        {
            double x = Math.Pow(number, 1 / result); // logika do sprawdzania
            Console.WriteLine($"x = {x}");
        }
        else
        {
            Console.WriteLine("cos nie tak");
        }
        Console.ReadLine();
    }

    static bool TryParseLogarithm(string expression, out double result)
    {
        result = 0.0;
        string[] parts = expression.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries); //dzielimy wyrazenie zeby wyszedl logarytm  

        if (parts.Length != 2) // poprawnosc logarytmu
            return false;

        if (double.TryParse(parts[1], out double number)) // sprawdzenie czy 2 czesc log moze byc number
        {
            if (parts[0].StartsWith("log_")) // czy pierwsza to log_
            {
                if (double.TryParse(parts[0].Substring(4), out double baseNumber))  // i tutaj jak cos przy log to dajemy w badsenumber
                {
                    result = Math.Log(number, baseNumber);
                    return true; // logarytm
                }
            }
            else if (parts[0].Equals("log", StringComparison.OrdinalIgnoreCase))
            {
                result = Math.Log(number);
                return true; //logarytm ale jezeli jest tylko log
            }
        }

        return false;
    }

    static bool TryParseLogarithmOperation(string expression, out double result)
    {
        result = 0.0;
        string[] parts = expression.Split(new[] { '+', '-', '*', '/' }, StringSplitOptions.RemoveEmptyEntries); //dzieli na czesci

        if (parts.Length != 2)
            return false; // obsluga bledu 

        if (TryParseLogarithm(parts[0].Trim(), out double result1) && // jezeli sie wszystko powiedzie w tryprasach to wtedy wyniki wchodza w resulty
            TryParseLogarithm(parts[1].Trim(), out double result2))
        {
            if (expression.Contains("+"))
            {
                result = result1 + result2;
            }
            else if (expression.Contains("-"))
            {
                result = result1 - result2;
            }
            else if (expression.Contains("*"))
            {
                result = result1 * result2;
            }
            else if (expression.Contains("/"))
            {
                result = result1 / result2;
            }
            return true;
        }

        return false;
    }

    static void CalculateSequence()
    {
        Console.Clear();
        Console.WriteLine("Podaj długosc ciągu:");

        if (!int.TryParse(Console.ReadLine(), out int length) || length <= 0)
        {
            Console.WriteLine("cos nie tak");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Podaj elementy ciągu:");
        List<double> elementy = new List<double>();

        for (int i = 1; i <= length; i++)
        {
            Console.Write($"{i} = ");
            if (!double.TryParse(Console.ReadLine(), out double v))
            {
                Console.WriteLine("cos nie tak");
                i--; // powrot do ciagu zeby nie bylo nie poprawnosci
                continue; // jezeli git to dalej
            }
            elementy.Add(v);
        }
        SequencePrint(elementy); //wyswietalnie po wszystkim 
        Console.ReadLine();
    }

    static void SequencePrint(List<double> elements)
    {
        string sequenceType = GetSequenceType(elements);
        string monotonicity = GetMonotonicity(elements);
        Console.WriteLine($"Typ ciagu: {sequenceType}");
        Console.WriteLine($"Monotonicznosc ciagu: {monotonicity}");

        Console.WriteLine("Podaj numer indeksu (n) elementu, ktorego wartosc chcesz poznac:");
        if (!int.TryParse(Console.ReadLine(), out int i) || i < 1 || i > elements.Count)
        {
            Console.WriteLine("Niepoprawny numer indeksu. Sprobuj ponownie.");
            return;
        }

        double v = Elements(elements, i, sequenceType);
        Console.WriteLine($"Wartosc elementu o indeksie {i} wynosi {v}");
    }

    static double Elements(List<double> elements, int i, string sequenceType)
    {
        double firstTerm = elements[0];
        if (sequenceType == "arytmetyczny")
        {
            double commonDiff = elements[1] - elements[0];
            return CalculateArithmetic(firstTerm, commonDiff, i);
        }
        else if (sequenceType == "geometryczny")
        {
            double commonRatio = elements[1] / elements[0];
            return CalculateGeometric(firstTerm, commonRatio, i);
        }
        else
        {
            return elements[i - 1];
        }
    }

    static double CommonDiff(List<double> elements)
    {
        if (elements.Count < 2)
            return 0.0;

        return elements[1] - elements[0];  // roznica z ciagu
    }

    static double CommonRatio(List<double> elements)
    {
        if (elements.Count < 2 || elements[0] == 0)
            return 0.0;

        return elements[1] / elements[0];
    }

    static string GetSequenceType(List<double> elements)
    {
        bool isArithmetic = CheckArithmSeq(elements);
        bool isGeometric = CheckGeom(elements);

        if (isArithmetic)
            return "arytmetyczny";
        else if (isGeometric)
            return "geometryczny";
        else
            return "inny";
    }

    static double CalculateGeometric(double firstTerm, double commonRatio, int n)
    {
        return firstTerm * Math.Pow(commonRatio, n - 1);
    }

    static double CalculateArithmetic(double firstTerm, double commonDiff, int n) // pierwsza liczba , roznica , index 
    {
        return firstTerm + (n - 1) * commonDiff;
    }

    static bool CheckArithmSeq(List<double> elements)
    {
        double commonDiff = CommonDiff(elements);

        for (int i = 2; i <= elements.Count; i++)
        {
            if (elements[i - 1] != CalculateArithmetic(elements[0], commonDiff, i)) // porownanie wszystkiego z artmetycznoscia
                return false;
        }

        return true;
    }

    static bool CheckGeom(List<double> elements)
    {
        double commonRatio = CommonRatio(elements);

        for (int i = 2; i <= elements.Count; i++)
        { 
            if (elements[i - 1] != CalculateGeometric(elements[0], commonRatio, i)) // porownanie wszystkiego z geometrycznoscia
                return false; // czy jest staly zeby gemetrycznosc sprawdzic
        }

        return true;
    }

    static string GetMonotonicity(List<double> elements)
    {
        bool isIncreasing = true;
        bool isDecreasing = true;

        for (int i = 1; i < elements.Count; i++) // Sprawdza monotonicznosc ciągu
        {
            if (elements[i] > elements[i - 1])
                isDecreasing = false;
            else if (elements[i] < elements[i - 1])
                isIncreasing = false;
        }

        if (isIncreasing)
            return "rosnacy";
        else if (isDecreasing)
            return "malejacy";
        else
            return "nie monotoniczny";
    }
}
