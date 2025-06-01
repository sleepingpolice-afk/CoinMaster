public static class NumberFormatter
{
    private static readonly string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc" };

    public static string FormatNumber(double number)
    {
        if (number == 0) return "0";
        
        bool isNegative = number < 0;
        number = System.Math.Abs(number);
        
        int suffixIndex = 0;
        
        while (number >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            number /= 1000;
            suffixIndex++;
        }
        
        string result;
        if (number >= 100)
        {
            result = number.ToString("F0") + suffixes[suffixIndex];
        }
        else if (number >= 10)
        {
            result = number.ToString("F1") + suffixes[suffixIndex];
        }
        else
        {
            result = number.ToString("F2") + suffixes[suffixIndex];
        }
        
        return isNegative ? "-" + result : result;
    }
}