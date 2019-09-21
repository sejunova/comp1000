namespace Assignment1
{
    public class BigNumberCalculator
    {
        public BigNumberCalculator(int bitCount, EMode mode)
        {
        }

        public static string GetOnesComplement(string num)
        {
            if (!isBinaryNumber(num))
            {
                return null;
            }

            char[] numToChar = num.ToCharArray();
            for (int i = numToChar.Length; i >= 2; i--)
            {
                if (numToChar[i] == '0')
                {
                    numToChar[i] = '1';
                }
                else
                {
                    numToChar[i] = '0';
                }
            }
            return numToChar.ToString();
        }

        public static string GetTwosComplement(string num)
        {
            return null;
        }

        public static string ToBinary(string num)
        {
            return null;
        }

        public static string ToHex(string num)
        {
            return null;
        }

        public static string ToDecimal(string num)
        {
            return null;
        }

        public string AddOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;
            return null;
        }

        public string SubtractOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;
            return null;
        }

        private static bool isBinaryNumber(string num)
        {
            for (int i = 0; i < num.Length; i++)
            {
                if (i == 0 && num[i] != '0')
                {
                    return false;
                }
                if (i == 1 && num[i] != 'b')
                {
                    return false;
                }
                if (num[i] != '0' || num[i] != '1')
                {
                    return false;
                }
            }
            return true;
        }
    }
}