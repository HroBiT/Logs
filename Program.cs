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
                Console.WriteLine("Niepoprawny wybor sprobuj ponownie.");
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
                    Console.WriteLine("Niepoprawny wybor sprobuj ponownie.");
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

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Niepoprawny wybor, sprobuj ponownie.");
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
            default:
                Console.WriteLine("Niepoprawny wybor, sprobuj ponownie.");
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
            Console.WriteLine("Niepoprawne wyrażenie logarytmiczne. Sprobuj ponownie.");
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
            Console.WriteLine("Niepoprawne wyrażenie logarytmiczne. Sprobuj ponownie.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine($"Wynik obliczeń: {result}");
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
       Console.WriteLine("Podaj długosc ciagu:");

        if (!int.TryParse(Console.ReadLine(), out int length) || length <= 0)
        {
            Console.WriteLine("Niepoprawna długosc ciagu. Sprobuj ponownie");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Podaj elementy ciagu:");
       List<double> elementy = new List<double>();

        for (int i = 1; i <= length; i++)
        {
            Console.Write($"{i} = ");
            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                Console.WriteLine("Niepoprawna wartosc. Sprobuj ponownie");
                i--; // powrot do ciagu zeby nie bylo nie poprawnosci
                continue; // jezeli git to dalej
            }
            elementy.Add(value);
        }
       CalculateAndPrintSequence(elementy); //wyswietalnie po wszystkim 
    }


    static void CalculateAndPrintSequence(List<double> elements)
    {
        string sequenceType = GetSequenceType(elements); // wynik
        string monotonicity = GetMonotonicity(elements);//wynik 
       Console.WriteLine($"Typ ciagu: {sequenceType}");
       Console.WriteLine($"Monotonicznosc ciagu: {monotonicity}");
        
      Console.WriteLine("Podaj numer indeksu (n) elementu, ktorego wartosc chcesz poznac:");
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > elements.Count)
        {
            Console.WriteLine("Niepoprawny numer indeksu. Sprobuj ponownie.");
            return;
        }

        double value = CalculateElementValue(elements, index, sequenceType);
        Console.WriteLine($"Wartosc elementu o indeksie {index} wynosi {value}");
    }

    static double CalculateElementValue(List<double> elements, int index, string sequenceType)
    {
       double firstTerm = elements[0];
       if (sequenceType == "arytmetyczny")
       {
            double commonDifference = elements[1] - elements[0];
            return CalculateArithmetic(firstTerm, commonDifference, index);
       }
        else if (sequenceType == "geometryczny")
        {
            double commonRatio = elements[1] / elements[0]; 
            return CalculateGeometric(firstTerm, commonRatio, index);
        }
        else
        {
            return elements[index - 1]; 
        }
    }

    static double CalculateCommonDifference(List<double> elements)
    {
        if (elements.Count < 2)
            return 0.0;

        return elements[1] - elements[0];
    }

    static double CalculateCommonRatio(List<double> elements)
    {
        if (elements.Count < 2 || elements[0] == 0)
            return 0.0;

        return elements[1] / elements[0];
    }

    static string GetSequenceType(List<double> elements)
    {
        bool isArithmetic = CheckArithmeticSequence(elements);
        bool isGeometric = CheckGeometricSequence(elements);

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

    static double CalculateArithmetic(double firstTerm, double commonDifference, int n)
    {
        return firstTerm + (n - 1) * commonDifference;
    }

    static bool CheckArithmeticSequence(List<double> elements)
    {
        double commonDifference = CalculateCommonDifference(elements);

        for (int i = 2; i <= elements.Count; i++)
        {
            if (elements[i - 1] != CalculateArithmetic(elements[0], commonDifference, i))
                return false;
        }

        return true;
    }

    static bool CheckGeometricSequence(List<double> elements)
    {
        double commonRatio = CalculateCommonRatio(elements);

        for (int i = 2; i <= elements.Count; i++)
        {
            if (elements[i - 1] != CalculateGeometric(elements[0], commonRatio, i))
                return false;
        }

        return true;
    }

    static string GetMonotonicity(List<double> elements)
    {
        bool isIncreasing = true;
        bool isDecreasing = true;

        for (int i = 1; i < elements.Count; i++)
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
