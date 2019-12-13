using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace RSAProject
{
    class BigInt
    {
        static bool neg = false;    //O(1)
        public static string Add(string FirstNum, string SecondNum) //O(N)
        {
            if (FirstNum.Length > SecondNum.Length) //O(1)
            {
                string temp = FirstNum; //O(1)
                FirstNum = SecondNum;   //O(1)
                SecondNum = temp;       //O(1)
            }
            string ResultNum = "";      //O(1)
            int n1 = FirstNum.Length, n2 = SecondNum.Length;    //O(1)
            int difference = n2 - n1;   //O(1)
            int carry = 0;      //O(1)

            for (int i = n1 - 1; i >= 0; i--)   //O(N) * O(body) 
            {
                int sum = ((int)(FirstNum[i] - '0') +
                        (int)(SecondNum[i + difference] - '0') + carry);    //O(1)
                ResultNum += (char)(sum % 10 + '0');    //O(1)
                carry = sum / 10;   //O(1)
            }
            for (int i = n2 - n1 - 1; i >= 0; i--)  //O(N)* O(body)
            {
                int sum = ((int)(SecondNum[i] - '0') + carry);  //O(1)
                ResultNum += (char)(sum % 10 + '0');    //O(1)
                carry = sum / 10;   //O(1)
            }
            if (carry > 0)  //O(1)
            {
                ResultNum += (char)(carry + '0');   //O(1)
            }
            char[] result = ResultNum.ToCharArray();    //O(1)
            Array.Reverse(result);  //O(N)
            return new string(result);  //O(1)
        }
        public static bool negative(string num1 , string num2)  //O(N)
        {
            char[] N1 = num1.ToCharArray(); //O(1)
            char[] N2 = num2.ToCharArray(); //O(1)
            for (int i = 0; i < num1.Length;i++)    //O(N) * O(body)
            {
                if(N1[i] > N2[i])   //O(1)
                {
                    return false;   //O(1)
                }
                else if(N1[i] < N2[i])  //O(1)
                {
                    return true;    //O(1)
                }
            }
            return true;    //O(1)
        }
        public static string SUB(string f , string s) //O(N)
        {
            long diff = Math.Abs(f.Length - s.Length);  //O(1)
            int len = Equalize(ref f, ref s);   //O(N)
            string answer = "";                //O(1)
            bool borrow = false;        //O(1)
            neg = negative(f , s);      //O(N)
            if (neg)        //O(1)
            {
                string temp = f;        //O(1)
                f = s;          //O(1)
                s = temp;       //O(1)
            }
            char[] first = f.ToCharArray();     //O(1)
            char[] second = s.ToCharArray();    //O(1)
            if (f == s)     //O(1)
            {
                return "0"; //O(1)
            }   
            else
            {
                for (int i = len - 1; i >= 0; i--)  //O(N) * O(body)
                {
                    if (borrow && first[i] - '0' > 0)   //O(1)
                    {
                        int rkm = first[i] - '0';   //O(1)
                        first[i]--;     //O(1)
                        borrow = false;     //O(1)
                    }
                    else if (borrow && first[i] - '0' == 0) //O(1)
                    {
                        first[i] = '9';     //O(1)
                        borrow = true;      //O(1)
                    }
                    if (first[i] - '0' >= second[i] - '0')  //O(1)
                    {
                        answer += first[i] - second[i]; //O(1)
                    }
                    else
                    {
                        borrow = true;  //O(1)
                        int rkm = first[i] - '0';   //O(1)
                        rkm += 10;      //O(1)
                        rkm -= (second[i] - '0');       //O(1)
                        answer += rkm.ToString();       //O(1)
                    }   
                }
                if(neg) //O(1)
                {   
                    answer += '-';      //O(1)
                }                              
                char[] result = answer.ToCharArray();   //O(1)
                Array.Reverse(result);  //O(N)
                string Modify = LeftZeros(result);  //O(N)
                return Modify;  //O(1)
            }
        }
        public static string LeftZeros(char[] res)  //O(N)
        {
            long idx = 0;   //O(1)
            string modAnswer = (neg) ? "-" : "";    //O(1)
            for (long i = (neg) ? 1 : 0; i < res.Length;i++)    //O(N) * O(body)
            {
                if(res[i] != '0')   //O(1)      
                {
                    idx = i;        //O(1)
                    break;      //O(1)
                }
            }
            for(long i = idx; i < res.Length; i++)  //O(N) * O(body)
            {
                modAnswer += res[i];    //O(1)
            }
            return modAnswer;       //O(1)
        }
        public static int Equalize(ref string Num1, ref string Num2)    //O(N)
        {
            char[] N1 = Num1.ToCharArray(); //O(1)
            char[] N2 = Num2.ToCharArray(); //O(1)
            int difference = Math.Abs(Num1.Length - Num2.Length);   //O(1)
            if (difference == 0)        //O(1)
            {
                return Num1.Length;     //O(1)
            }
            if (Num1.Length < Num2.Length)  //O(1)
            {
                char[] temp = new char[Num2.Length];    //
                for (int i = 0; i < Num2.Length; i++)   //O(N) * O(body)
                {
                    temp[i] = '0';  //O(1)
                }
                for (int j = Num1.Length - 1; j >= 0; j--) //O(N) * O(body)
                {
                    temp[j + difference] = N1[j];   //O(1)
                }
                Num1 = "";      //O(1)
                for (int i = 0; i < temp.Length; i++)   //O(N) * O(body)
                {
                    Num1 += temp[i];        //O(1)
                }
                return Num2.Length;     //O(1)
            }
            else
            {
                char[] temp = new char[Num1.Length]; //O(1)
                for (int i = 0; i < Num1.Length; i++)   //O(N) * O(body)
                {
                    temp[i] = '0';      //O(1)
                }
                for(int j = N2.Length - 1; j >= 0; j--)  //O(N) * O(body)
                {
                    temp[j + difference] = N2[j];       //O(1)
                }
                Num2 = "";      //O(1)
                for (int i = 0; i < temp.Length; i++)   //O(N) * O(body)
                {
                    Num2 += temp[i];        //O(1)
                }
                return Num1.Length;     //O(1)
            }
        }
        public static string MUL(string f, string s) //T(N) = 3T(N/2) + O(N)    Using Master Method Case 1  
        {                                           //F(n) = O(n)    G(n) = N ^ Log(3) base 2  -->  T(N) = O(N^1.59)
            int idx = 0;                            //O(1)
            int length = Equalize(ref f, ref s);    //O(N)
            if (length == 1)                        //O(1)
            {
                return singleDigit(f, s);           //O(1)
            }
            int firsthalf = (length / 2), secondhalf = length - firsthalf;  //O(1)

            string A = f.Substring(0, firsthalf);       //O(N/2)
            string B = f.Substring(firsthalf, secondhalf);  //O(N/2)
            string C = s.Substring(0, firsthalf);           //O(N/2)
            string D = s.Substring(firsthalf, secondhalf);  //O(N/2)

            string P1 = MUL(A, C);              
            string P2 = MUL(B, D);
            string P3 = MUL(Add(A, B), Add(C, D));

            string res = calculate(P1, P2, P3, B.Length + D.Length);  //O(N)
            char[] result = res.ToCharArray();                       //O(1)
            string modAnswer = "";                      //O(1)
            for (int i = 0; i < res.Length; i++)  //O(N) * O(body)
            {
                if(result[i] != '0')        //O(1)
                {
                    idx = i;               //O(1)
                    break;                //O(1)
                }
            }
            for(int i = idx; i < res.Length; i++)   //O(N) * O(body)
            {
                modAnswer += result[i];     //O(1)
            }
            return modAnswer;   //O(1)
        }
        public static string calculate(string AC , string BD , string ABCD , int l) //O(N)
        {
            string T0 = SUB(SUB(ABCD, AC), BD);      //O(N)
            string T1 = T0.PadRight(T0.Length + l / 2, '0');   //O(l)
            string T2 = AC.PadRight(AC.Length + l , '0');      //O(l)
            return Add(Add(T1 , T2) , BD);          //O(N)
        }
        public static string singleDigit(string First, string Second) //O(1)
        {
            long res = long.Parse(First) * long.Parse(Second); //O(1)
            return res.ToString(); //O(1)
        }
    }
}

/////////**** Total Complexity Of Code Is O(N^1.59) ****///////////