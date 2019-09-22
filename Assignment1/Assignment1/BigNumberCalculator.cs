using System;
using System.Collections.Generic;

namespace Assignment1
{
    public class BigNumberCalculator
    {
        private int mBitcount;
        private EMode mMode;
        public BigNumberCalculator(int bitCount, EMode mode)
        {
            mBitcount = bitCount;
            mMode = mode;
        }

        public static string GetOnesComplement(string num)
        {
            if (!isBinaryNumber(num))
            {
                return null;
            }

            char[] numToChar = num.ToCharArray();
            for (int i = numToChar.Length - 1; i >= 2; i--)
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
            return new string(numToChar);
        }

        public static string GetTwosComplement(string num)
        {
            if (!isBinaryNumber(num))
            {
                return null;
            }

            char[] numToChar = num.ToCharArray();
            for (int i = numToChar.Length - 1; i >= 2; i--)
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

            for (int i = numToChar.Length - 1; i >= 2; i--)
            {
                if (numToChar[i] == '0')
                {
                    numToChar[i] = '1';
                    break;
                }
                numToChar[i] = '0';
            }
            return new string(numToChar);
        }

        public static string ToBinary(string num)
        {
            if (num == "")
            {
                return null;
            }
            if (num.StartsWith("0b") && isBinaryNumber(num))
            {
                return num;
            }
            else if (num.StartsWith("0x") && isHexNumber(num))
            {
                return convertHexToBinary(num);
            }
            else if (isDecimalNumber(num))
            {
                return convertDecimalToBinary(num);
            }
            else
            {
                return null;
            }
        }

        public static string ToHex(string num)
        {
            if (num == "")
            {
                return null;
            }
            if (num.StartsWith("0b") && isBinaryNumber(num))
            {
                return convertBinaryToHex(num);
            }
            else if (num.StartsWith("0x") && isHexNumber(num))
            {
                return num;
            }
            else if (isDecimalNumber(num))
            {
                string binaryNum = convertDecimalToBinary(num);
                return convertBinaryToHex(binaryNum);
            }
            else
            {
                return null;
            }
        }

        public static string ToDecimal(string num)
        {
            if (num == "")
            {
                return null;
            }
            if (num.StartsWith("0b") && isBinaryNumber(num))
            {
                return convertBinaryToDecimal(num);
            }
            else if (num.StartsWith("0x") && isHexNumber(num))
            {
                string binaryNum = convertHexToBinary(num);
                return convertBinaryToDecimal(binaryNum);
            }
            else if (isDecimalNumber(num))
            {
                return num;
            }
            else
            {
                return null;
            }
        }

        public string AddOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;
            string num1Binary = ToBinary(num1);
            string num2Binary = ToBinary(num2);
            if (num1Binary == null || num2Binary == null || num1Binary.Length - 2 > mBitcount || num2Binary.Length - 2 > mBitcount)
            {
                return null;
            }

            string num1BinaryWithout0B = num1Binary.Substring(2, num1Binary.Length - 2);
            string num2BinaryWithout0B = num2Binary.Substring(2, num2Binary.Length - 2);

            if (mBitcount > num1BinaryWithout0B.Length)
            {
                int bufferLength = mBitcount - num1BinaryWithout0B.Length;
                num1BinaryWithout0B = addBufferToBinaryNumberWithout0B(num1BinaryWithout0B, bufferLength);
            }
            if (mBitcount > num2BinaryWithout0B.Length)
            {
                int bufferLength = mBitcount - num2BinaryWithout0B.Length;
                num2BinaryWithout0B = addBufferToBinaryNumberWithout0B(num2BinaryWithout0B, bufferLength);
            }

            string binaryAddedWithout0B = addTwoBinaryNumberWithout0B(num1BinaryWithout0B, num2BinaryWithout0B, out bOverflow);
            if (mMode == EMode.Binary)
            {
                if (bOverflow)
                {
                    return null;
                }
                return "0b" + binaryAddedWithout0B;
            }
            else
            {
                string decimalValue = convertBinaryToDecimal("0b" + binaryAddedWithout0B);
                return decimalValue;
            }
        }

        public string SubtractOrNull(string num1, string num2, out bool bOverflow)
        {
            bOverflow = false;
            string num1Binary = ToBinary(num1);
            string num2Binary = ToBinary(num2);
            if (num1Binary == null || num2Binary == null || num1Binary.Length - 2 > mBitcount || num2Binary.Length - 2 > mBitcount)
            {
                return null;
            }

            num2Binary = GetTwosComplement(num2Binary);
            string num1BinaryWithout0B = num1Binary.Substring(2, num1Binary.Length - 2);
            string num2BinaryWithout0B = num2Binary.Substring(2, num2Binary.Length - 2);

            if (mBitcount > num1BinaryWithout0B.Length)
            {
                int bufferLength = mBitcount - num1BinaryWithout0B.Length;
                num1BinaryWithout0B = addBufferToBinaryNumberWithout0B(num1BinaryWithout0B, bufferLength);
            }
            if (mBitcount > num2BinaryWithout0B.Length)
            {
                int bufferLength = mBitcount - num2BinaryWithout0B.Length;
                num2BinaryWithout0B = addBufferToBinaryNumberWithout0B(num2BinaryWithout0B, bufferLength);
            }

            string binaryAddedWithout0B = addTwoBinaryNumberWithout0B(num1BinaryWithout0B, num2BinaryWithout0B, out bOverflow);
            if (mMode == EMode.Binary)
            {
                return "0b" + binaryAddedWithout0B;
            }
            else
            {
                string decimalValue = convertBinaryToDecimal("0b" + binaryAddedWithout0B);
                return decimalValue;
            }
        }

        private static bool isBinaryNumber(string num)
        {
            if (num.Length <= 2 || !num.StartsWith("0b"))
            {
                return false;
            }

            for (int i = 2; i < num.Length; i++)
            {
                if (num[i] != '0' && num[i] != '1')
                {
                    return false;
                }
            }
            return true;
        }
        private static bool isHexNumber(string num)
        {
            bool bNotZeroAppeared = false;
            if (num.Length <= 2 || !num.StartsWith("0x"))
            {
                return false;
            }

            for (int i = 2; i < num.Length; i++)
            {
                if (!((65 <= num[i] && num[i] <= 70) || (48 <= num[i] && num[i] <= 57)))
                {
                    return false;
                }
                if (!bNotZeroAppeared)
                {
                    if (num[i] != '0')
                    {
                        bNotZeroAppeared = true;
                    }
                }
            }
            if (!bNotZeroAppeared && num.Length != 3)
            {
                return false;
            }
            return true;
        }
        private static bool isDecimalNumber(string num)
        {
            if (num.Length == 0)
            {
                return false;
            }

            // status = 0 -> 0 ~ 9 까지 그리고 - 받을 수 있음. (처음 시작상태)
            // status = 1 -> 1 ~ 9 까지 받을 수 있음. 맨 처음에 - 뽑은 상태
            // status = 2 -> 0 ~ 9 까지 받을 수 있음. (일반적인 상태)
            // status = 3 -> 더 이상 받을 게 잇으면 안되는 상태 (초장에 0 뽑음)

            int status = 0;
            for (int i = 0; i < num.Length; i++)
            {
                if (status == 0)
                {
                    if (num[i] == '-')
                    {
                        status = 1;
                    }
                    else if (49 <= num[i] && num[i] <= 57)
                    {
                        status = 2;
                    }
                    else if (num[i] == '0')
                    {
                        status = 3;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (status == 1)
                {
                    if (49 <= num[i] && num[i] <= 57)
                    {
                        status = 2;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (status == 2)
                {
                    if (!(48 <= num[i] && num[i] <= 57))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            if (status == 1)
            {
                return false;
            }
            return true;
        }
        private static string convertHexToBinary(string hexNum)
        {
            int i = 2;
            int binaryNumberLength = (hexNum.Length - 2) * 4 + 2;
            char[] binaryNumber = new char[binaryNumberLength];
            binaryNumber[0] = '0';
            binaryNumber[1] = 'b';

            for (int j = 2; j < hexNum.Length; j++)
            {
                char c = hexNum[j];
                if (c == '0')
                {
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '0';
                }
                else if (c == '1')
                {
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '1';
                }
                else if (c == '2')
                {
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '0';
                }
                else if (c == '3')
                {
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '1';
                }
                else if (c == '4')
                {
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '0';
                }
                else if (c == '5')
                {
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '1';
                }
                else if (c == '6')
                {
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '0';
                }
                else if (c == '7')
                {
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '1';
                }
                else if (c == '8')
                {
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '0';
                }
                else if (c == '9')
                {
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '1';
                }
                else if (c == 'A')
                {
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '0';
                }
                else if (c == 'B')
                {
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '1';
                }
                else if (c == 'C')
                {
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '0';
                }
                else if (c == 'D')
                {
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '0';
                    binaryNumber[i++] = '1';
                }
                else if (c == 'E')
                {
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '0';
                }
                else
                {
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '1';
                    binaryNumber[i++] = '1';
                }
            }
            return new string(binaryNumber);
        }

        private static string convertBinaryToHex(string binaryNum)
        {
            string binaryNumWithout0B = binaryNum.Substring(2, binaryNum.Length - 2);
            int bufferLength;
            if (binaryNumWithout0B.Length % 4 == 1)
            {
                bufferLength = 3;
            }
            else if (binaryNumWithout0B.Length % 4 == 2)
            {
                bufferLength = 2;
            }
            else if (binaryNumWithout0B.Length % 4 == 3)
            {
                bufferLength = 1;
            }
            else
            {
                bufferLength = 0;
            }

            if (bufferLength != 0)
            {
                binaryNumWithout0B = addBufferToBinaryNumberWithout0B(binaryNumWithout0B, bufferLength);
            }

            binaryNum = "0b" + binaryNumWithout0B;
            char[] hexNumberArray = new char[binaryNum.Length / 4 + 2];
            hexNumberArray[0] = '0';
            hexNumberArray[1] = 'x';

            int i = 2;
            int j = 2;
            string fourDigits;
            while (i + 4 <= binaryNum.Length)
            {
                fourDigits = binaryNum.Substring(i, 4);
                if (fourDigits == "0000")
                {
                    hexNumberArray[j++] = '0';
                }
                else if (fourDigits == "0001")
                {
                    hexNumberArray[j++] = '1';
                }
                else if (fourDigits == "0010")
                {
                    hexNumberArray[j++] = '2';
                }
                else if (fourDigits == "0011")
                {
                    hexNumberArray[j++] = '3';
                }
                else if (fourDigits == "0100")
                {
                    hexNumberArray[j++] = '4';
                }
                else if (fourDigits == "0101")
                {
                    hexNumberArray[j++] = '5';
                }
                else if (fourDigits == "0110")
                {
                    hexNumberArray[j++] = '6';
                }
                else if (fourDigits == "0111")
                {
                    hexNumberArray[j++] = '7';
                }
                else if (fourDigits == "1000")
                {
                    hexNumberArray[j++] = '8';
                }
                else if (fourDigits == "1001")
                {
                    hexNumberArray[j++] = '9';
                }
                else if (fourDigits == "1010")
                {
                    hexNumberArray[j++] = 'A';
                }
                else if (fourDigits == "1011")
                {
                    hexNumberArray[j++] = 'B';
                }
                else if (fourDigits == "1100")
                {
                    hexNumberArray[j++] = 'C';
                }
                else if (fourDigits == "1101")
                {
                    hexNumberArray[j++] = 'D';
                }
                else if (fourDigits == "1110")
                {
                    hexNumberArray[j++] = 'E';
                }
                else
                {
                    hexNumberArray[j++] = 'F';
                }
                i += 4;
            }
            // 비트 수 줄이기
            int shrinkCount = 0;
            char initialChar = hexNumberArray[2];
            if (initialChar == 'F' || initialChar == '0')
            {
                for (int k = 3; k < hexNumberArray.Length; k++)
                {
                    if (hexNumberArray[k] == initialChar)
                    {
                        shrinkCount++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            string hexNumber = new string(hexNumberArray);
            if (shrinkCount != 0)
            {
                hexNumber = "0x" + hexNumber.Substring(shrinkCount + 2, hexNumber.Length - (shrinkCount + 2));
            }
            return hexNumber;
        }

        private static string convertDecimalToBinary(string decimalNumber)
        {
            if (decimalNumber == "0")
            {
                return "0b0";
            }

            Stack<char> binaryNumberStack = new Stack<char>();

            bool bNumMinus = false;
            if (decimalNumber[0] == '-')
            {
                bNumMinus = true;
                decimalNumber = decimalNumber.Substring(1, decimalNumber.Length - 1);
            }

            while (true)
            {
                if (decimalNumber == "1")
                {
                    binaryNumberStack.Push('1');
                    break;
                }
                if ((decimalNumber[decimalNumber.Length - 1] - '0') % 2 == 1)
                {
                    binaryNumberStack.Push('1');
                }
                else
                {
                    binaryNumberStack.Push('0');
                }


                int add = 0;
                int digitNumber;
                char[] decimalNumberDividedByTwo = new char[decimalNumber.Length];
                for (int i = 0; i < decimalNumber.Length; i++)
                {
                    char c = decimalNumber[i];
                    digitNumber = ((c - '0') / 2 + add);
                    decimalNumberDividedByTwo[i] = convertOneDigitIntlToChar(digitNumber);
                    if ((c - '0') % 2 == 1)
                    {
                        add = 5;
                    }
                    else
                    {
                        add = 0;
                    }
                }
                decimalNumber = new string(decimalNumberDividedByTwo);
                if (decimalNumber[0] == '0')
                {
                    decimalNumber = decimalNumber.Substring(1, decimalNumber.Length - 1);
                }
            }

            binaryNumberStack.Push('0');
            binaryNumberStack.Push('b');
            binaryNumberStack.Push('0');

            string binaryNumber = new string(binaryNumberStack.ToArray());
            if (bNumMinus)
            {
                binaryNumber = GetTwosComplement(binaryNumber);
            }

            // 비트 수 줄이기
            int shrinkCount = 0;
            char initialChar = binaryNumber[2];
            for (int i = 3; i < binaryNumber.Length; i++)
            {
                if (binaryNumber[i] == initialChar)
                {
                    shrinkCount++;
                }
                else
                {
                    break;
                }
            }
            if (shrinkCount != 0)
            {
                binaryNumber = "0b" + binaryNumber.Substring(shrinkCount + 2, binaryNumber.Length - (shrinkCount + 2));
            }
            return binaryNumber;
        }
        private static string convertBinaryToDecimal(string binaryNumber)
        {
            bool bMinus = false;
            if (binaryNumber[2] == '1')
            {
                binaryNumber = GetTwosComplement(binaryNumber);
                bMinus = true;
            }

            binaryNumber = binaryNumber.Substring(2, binaryNumber.Length - 2);

            // 보수개념 무시한 unsigned 처럼 취급하기
            int trailingZeroCount = 0;
            for (int i = 0; i < binaryNumber.Length - 1; i++)
            {
                if (binaryNumber[i] == '0')
                {
                    trailingZeroCount++;
                }
                else
                {
                    break;
                }
            }
            if (trailingZeroCount != 0)
            {
                binaryNumber = binaryNumber.Substring(trailingZeroCount, binaryNumber.Length - trailingZeroCount);
            }

            // 이진수 값이 0이면 그대로 0 반환
            if (binaryNumber == "0")
            {
                return "0";
            }

            // 10진수 배열 준비
            int decimalNumArrayLength = (int)Math.Ceiling(binaryNumber.Length * Math.Log10(2));
            char[] decimalNumArray = new char[decimalNumArrayLength];
            for (int i = 0; i < decimalNumArrayLength; i++)
            {
                decimalNumArray[i] = '0';
            }

            int add;
            int digitIntVal;
            foreach (char c in binaryNumber)
            {
                add = 0;
                for (int i = 0; i < decimalNumArrayLength; i++)
                {
                    digitIntVal = (decimalNumArray[i] - '0') * 2 + add;
                    add = digitIntVal / 10;
                    decimalNumArray[i] = convertOneDigitIntlToChar(digitIntVal % 10);
                }

                if (c == '1')
                {
                    int j = 0;
                    digitIntVal = (decimalNumArray[j] - '0') + 1 + add;
                    add = digitIntVal / 10;
                    decimalNumArray[j] = convertOneDigitIntlToChar(digitIntVal % 10);

                    while (add != 0)
                    {
                        j++;
                        digitIntVal = (decimalNumArray[j] - '0') + add;
                        add = digitIntVal / 10;
                        decimalNumArray[j] = convertOneDigitIntlToChar(digitIntVal % 10);
                    }
                }
            }

            Array.Reverse(decimalNumArray); // 역순으로 들어가 있으므로 뒤집어주기
            string decimalNum = new string(decimalNumArray);
            if (decimalNum[0] == '0')
            {
                decimalNum = decimalNum.Substring(1, decimalNum.Length - 1);
            }

            if (bMinus)
            {
                decimalNum = "-" + decimalNum;
            }
            return decimalNum;
        }
        private static char convertOneDigitIntlToChar(int oneDigitInt)
        {
            char convertedValue;
            switch (oneDigitInt)
            {
                case 0:
                    convertedValue = '0';
                    break;
                case 1:
                    convertedValue = '1';
                    break;
                case 2:
                    convertedValue = '2';
                    break;
                case 3:
                    convertedValue = '3';
                    break;
                case 4:
                    convertedValue = '4';
                    break;
                case 5:
                    convertedValue = '5';
                    break;
                case 6:
                    convertedValue = '6';
                    break;
                case 7:
                    convertedValue = '7';
                    break;
                case 8:
                    convertedValue = '8';
                    break;
                case 9:
                    convertedValue = '9';
                    break;
                default:
                    convertedValue = '0';
                    break;
            }
            return convertedValue;
        }
        private static string addBufferToBinaryNumberWithout0B(string binaryNumWithout0B, int bufferLength)
        {
            char[] buffer = new char[bufferLength];
            if (binaryNumWithout0B[0] == '1') // 음수
            {
                for (int i = 0; i < bufferLength; i++)
                {
                    buffer[i] = '1';
                }
            }
            else
            {
                for (int i = 0; i < bufferLength; i++)
                {
                    buffer[i] = '0';
                }
            }
            return new string(buffer) + binaryNumWithout0B;
        }
        private static string addTwoBinaryNumberWithout0B(string binaryNum1, string binaryNum2, out bool bOverflow)
        {
            int add = 0;
            int binaryDigit;
            bOverflow = false;
            char[] binaryNumAddedArray = new char[binaryNum1.Length];

            for (int i = binaryNum1.Length - 1; i >= 0; i--)
            {
                binaryDigit = (binaryNum1[i] - '0') + (binaryNum2[i] - '0') + add;
                add = binaryDigit / 2;
                binaryNumAddedArray[i] = convertOneDigitIntlToChar(binaryDigit % 2);
            }
            if (binaryNum1[0] == binaryNum2[0] && binaryNum1[0] != binaryNumAddedArray[0])
            {
                bOverflow = true;
            }
            string binaryNumAdded = new string(binaryNumAddedArray);
            return binaryNumAdded;
        }
    }
}