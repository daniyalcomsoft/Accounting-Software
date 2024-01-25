using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

/// <summary>
/// Summary description for AmountToWords
/// </summary>
public class AmountToWords
{
    public int Total
    {
        get;
        set;
    }

    public string TotalInWords
    {
        get
        {
            return ConvertToWords(Total);
        }
    }

    private static string ConvertToWords(int amount)
    {
        string s = amount + "";
        int g = (s.Length / 3);//groups of three digits
        int a = (s.Length % 3);
        StringBuilder sb = new StringBuilder();

        if (g == 0) //only three or less digits
        {
            sb.Append(ConvertToHunderds(s, false));
        }
        else
        {
            if (a > 0)
            {
                sb.AppendFormat("{0} {1} ", ConvertToHunderds(s.Substring(0, a),
                false), groups[g]);
            }

            int idx = a;

            while (g > 0)
            {
                sb.AppendFormat(" {0} {1} ", ConvertToHunderds(s.Substring(a, 3),
                g == 1), groups[--g]);
                a += 3;
            }
        }

        return sb.ToString();
    }

    private static string ConvertToHunderds(string s, bool useAnd)
    {
        char[] c = new char[s.Length];

        for (int i = s.Length - 1, j = 0; i >= 0 && j < c.Length; j++, i--)
        {
            c[j] = s[i];
        }

        if (c.Length == 3)
        {
            if (c[2] == '0')
                return string.Format("{0}{1}", (useAnd ? "and " : ""), GetTens(c[1],
                c[0]));
            else
                return string.Format("{0} hundred {1} {2}", digits[((int)c[2]) -
                48],
                (useAnd ? "and" : ""),
                GetTens(c[1], c[0]));
        }
        else if (c.Length == 2)
        {
            return useAnd ? "and " : "" + GetTens(c[1], c[0]);
        }
        else
        {
            return digits[((int)c[0]) - 48];
        }
    }

    private static string GetTens(char ten, char one)
    {
        if (one == '0')
            if (ten == '1')
                return eleven2nineteen[((int)ten) - 49];
            else
                return tens[((int)ten) - 49];

        if (ten == '1')
            return eleven2nineteen[((int)one) - 49];

        return tens[((int)ten) - 49] //because tens[0] = 10 so subtract 49 (ascii 1)
        + " "
        + digits[((int)one) - 48];
    }

    private static string[] groups = new[] { ""/*hundreds group*/, "thousand", 
    "million", "billion", "trilliion" };
    private static string[] digits = new[] { "zero", "one", "two", "three", "four",
    "five", "six", "seven", "eight", "nine" };
    private static string[] eleven2nineteen = new[] { "eleven", "twelve", 
    "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", 
    "eighteen", "nineteen" };
    private static string[] tens = new[] { "ten", "twenty", "thirty", "fourty", 
    "fifty", "sixty", "seventy", "eighty", "ninety" };
}
