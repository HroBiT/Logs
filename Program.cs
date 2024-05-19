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
            Console.WriteLine("2. Operacje na ciągach");
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
        string[] parts = expression.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
            return false;

        if (double.TryParse(parts[1], out double number))
        {
            if (parts[0].StartsWith("log_"))
            {
                if (double.TryParse(parts[0].Substring(4), out double baseNumber))
                {
                    result = Math.Log(number, baseNumber);
                    return true;
                }
            }
            else if (parts[0].Equals("log", StringComparison.OrdinalIgnoreCase))
            {
                result = Math.Log(number);
                return true;
            }
        }

        return false;
    }

    static bool TryParseLogarithmOperation(string expression, out double result)
    {
        result = 0.0;
        string[] parts = expression.Split(new[] { '+', '-', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
            return false;

        if (TryParseLogarithm(parts[0].Trim(), out double result1) &&
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
        Console.WriteLine("Podaj długośc ciągu:");

        if (!int.TryParse(Console.ReadLine(), out int length) || length <= 0)
        {
            Console.WriteLine("Niepoprawna długośc ciągu. Sprobuj ponownie.");
            Console.ReadLine();
            return;
        }
        Console.WriteLine("Podaj elementy ciągu:");
        Dictionary<int, double> elements = new Dictionary<int, double>();

        for (int i = 1; i <= length; i++)
        {
            Console.Write($"{i} = ");
            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                Console.WriteLine("Niepoprawna wartośc. Sprobuj ponownie.");
                i--;
                continue;
            }
            elements.Add(i, value);
        }

        CalculateAndPrintSequence(elements);
        Console.ReadLine();
    }

    static void CalculateAndPrintSequence(Dictionary<int, double> elements)
    {
        string sequenceType = GetSequenceType(elements);
        string monotonicity = GetMonotonicity(elements);

        Console.WriteLine($"Typ ciągu: {sequenceType}");
        Console.WriteLine($"Monotonicznośc ciągu: {monotonicity}");

        Console.WriteLine("Podaj numer indeksu (n) elementu ktorego wartośc chcesz poznac:");
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1)
        {
            Console.WriteLine("Niepoprawny numer indeksu. Sprobuj ponownie.");
            return;
        }

        double value = CalculateElementValue(elements, index, sequenceType);
        Console.WriteLine($"Wartośc elementu indeksu {index} wynosi {value}");
    }

    static double CalculateElementValue(Dictionary<int, double> elements, int index, string sequenceType)
    {
        double firstTerm = elements[1];

        if (sequenceType == "arytmetyczny")
        {
            double commonDifference = elements[2] - elements[1];
            return CalculateArithmetic(firstTerm, commonDifference, index);
        }
        else if (sequenceType == "geometryczny")
        {
            double commonRatio = elements[2] / elements[1];
            return CalculateGeometric(firstTerm, commonRatio, index);
        }
        else
        {
            return elements.ContainsKey(index) ? elements[index] : double.NaN;
        }
    }

    static double CalculateCommonDifference(Dictionary<int, double> elements)
    {
        if (elements.Count < 2)
            return 0.0;

        return elements[2] - elements[1];
    }

    static double CalculateCommonRatio(Dictionary<int, double> elements)
    {
        if (elements.Count < 2)
            return 0.0;

        return elements[2] / elements[1];
    }

    static string GetSequenceType(Dictionary<int, double> elements)
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

    static bool CheckArithmeticSequence(Dictionary<int, double> elements)
    {
        double commonDifference = CalculateCommonDifference(elements);

        for (int i = 2; i <= elements.Count; i++)
        {
            if (elements[i] != CalculateArithmetic(elements[1], commonDifference, i))
                return false;
        }

        return true;
    }

    static bool CheckGeometricSequence(Dictionary<int, double> elements)
    {
        double commonRatio = CalculateCommonRatio(elements);

        for (int i = 2; i <= elements.Count; i++)
        {
            if (elements[i] != CalculateGeometric(elements[1], commonRatio, i))
                return false;
        }

        return true;
    }

    static string GetMonotonicity(Dictionary<int, double> elements)
    {
        bool isIncreasing = true;
        bool isDecreasing = true;

        for (int i = 2; i <= elements.Count; i++)
        {
            if (elements[i] > elements[i - 1])
                isDecreasing = false;
            else if (elements[i] < elements[i - 1])
                isIncreasing = false;
        }

        if (isIncreasing)
            return "rosnący";
        else if (isDecreasing)
            return "malejący";
        else
            return "niemonotoniczny";
    }
}
