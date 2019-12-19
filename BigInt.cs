using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSAProject
{
    class BigInt
    {
        //static bool neg = false;    //O(1)

        public static string Add(string FirstNum, string SecondNum) //O(N)
        {
            StringBuilder f = new StringBuilder(FirstNum);
            if (FirstNum.Length > SecondNum.Length) //O(1)
            {
                string temp = f.ToString();    //O(N)
                FirstNum = SecondNum;           //O(N)
                SecondNum = temp;               //O(N)
            }
            string ResultNum = "";      //O(1)
            int n1 = FirstNum.Length, n2 = SecondNum.Length;    //O(1)
            int difference = n2 - n1;   //O(1)
            int carry = 0;      //O(1)
            for (int i = n1 - 1; i >= 0; i--)   //O(N) * O(body) 
            {
                int sum = ((int)(FirstNum[i] - '0') +
                        (int)(SecondNum[i + difference] - '0') + carry);    //O(N)
                ResultNum += (char)(sum % 10 + '0');    //O(N)
                carry = sum / 10;   //O(1)
            }
            for (int i = n2 - n1 - 1; i >= 0; i--)  //O(N)* O(body)
            {
                int sum = ((int)(SecondNum[i] - '0') + carry);  //O(N)
                ResultNum += (char)(sum % 10 + '0');    //O(N)
                carry = sum / 10;   //O(1)
            }
            if (carry > 0)  //O(1)
            {
                ResultNum += (char)(carry + '0');   //O(N)
            }
            char[] result = ResultNum.ToCharArray();    //O(N)
            Array.Reverse(result);  //O(N)
            return new string(result);  //O(N)
        }
        public static string SUB(string f, string s) //O(N)
        {
            long diff = Math.Abs(f.Length - s.Length);  //O(1)
            int len = Equalize(ref f, ref s);   //O(N)
            string answer = "";                //O(1)
            bool borrow = false;        //O(1)
            //neg = Compare(f, s);      //O(N)
            //if (!neg)        //O(1)
            //{
            //    string temp = f;        //O(1)
            //    f = s;          //O(1)
            //    s = temp;       //O(1)
            //}
            char[] first = f.ToCharArray();     //O(N)
            char[] second = s.ToCharArray();    //O(N)
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
                        int rkm = first[i] - '0';   //O(M)
                        first[i]--;     //O(1)
                        borrow = false;     //O(1)
                    }
                    else if (borrow && first[i] - '0' == 0) //O(N)
                    {
                        first[i] = '9';     //O(1)
                        borrow = true;      //O(1)
                    }
                    if (first[i] - '0' >= second[i] - '0')  //O(N)
                    {
                        answer += first[i] - second[i]; //O(N)
                    }
                    else
                    {
                        borrow = true;  //O(1)
                        int rkm = first[i] - '0';   //O(N)
                        rkm += 10;      //O(1)
                        rkm -= (second[i] - '0');       //O(N)
                        answer += rkm.ToString();       //O(N)
                    }
                }
                //if (!neg) //O(1)
                //{
                //    answer += '-';      //O(1)
                //}
                char[] result = answer.ToCharArray();   //O(N)
                Array.Reverse(result);  //O(N)
                string Modify = LeftZeros(result);  //O(N^2)
                return Modify;  //O(1)
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

            string A = f.Substring(0, firsthalf);       //O(N)
            string B = f.Substring(firsthalf, secondhalf);  //O(N)
            string C = s.Substring(0, firsthalf);           //O(N)
            string D = s.Substring(firsthalf, secondhalf);  //O(N)

            string P1 = MUL(A, C);
            string P2 = MUL(B, D);
            string P3 = MUL(Add(A, B), Add(C, D));

            string res = calculate(P1, P2, P3, B.Length + D.Length);  //O(N)
            char[] result = res.ToCharArray();                       //O(N)
            string modAnswer = "";                      //O(1)
            for (int i = 0; i < res.Length; i++)  //O(N) * O(body)
            {
                if (result[i] != '0')        //O(1)
                {
                    idx = i;               //O(1)
                    break;                //O(1)
                }
            }
            for (int i = idx; i < res.Length; i++)   //O(N) * O(body)
            {
                modAnswer += result[i];     //O(N)
            }
            return modAnswer;   //O(1)
        }
        public static Tuple<string, string> DIV(string first, string second)  //O(N)
        {
            string Q, R;    //O(1)
            Tuple<string, string> QR;   //O(1)
            if (!Compare(first, second)) //O(N)  
            {
                return new Tuple<string, string>("0", first); //O(N)
            }
            QR = DIV(first, MUL("2", second));  //O(N)
            Q = QR.Item1;   //O(N)
            Q = MUL("2", Q);   //O(N)
            R = QR.Item2;   //O(N)
            if (!Compare(R, second)) //O(N)
            {
                return new Tuple<string, string>(Q, R);   //O(N)
            }
            else
            {
                return new Tuple<string, string>(Add(Q, "1"), SUB(R, second)); //O(N)
            }
        }
        static Tuple<string, string> Modules;
        static Tuple<string, string> NewPower;
        static Tuple<string, string> NewAns;
        public static string ModOfPower(string Base , string Power , string Mod) //O(N^1.59 LogN) Must Calc The Analsyis
        {                                                               // T(N) = T(N/2) + O(N^1.59)
            if(Base == "0") //O(1)
            {
                return "0"; 
            }
            if(Power == "0")  //O(1)
            {
                return "1"; 
            }           
            string Answer = ""; //O(1)
            if ((Power[Power.Length - 1] - '0') % 2 == 0)
            {
                NewPower = DIV(Power, "2"); //O(N)
                string New = NewPower.Item1;   //O(N) 
                Answer = ModOfPower(Base, New, Mod); 
                Modules = DIV(MUL(Answer, Answer), Mod); //O(N^1.59)   
                string NewMod = Modules.Item2;  //O(N);
                Answer = NewMod; //O(N)
            }
            else
            {
                NewAns = DIV(Base , Mod);  //O(N)
                Answer = NewAns.Item2;  //O(N)
                Modules = DIV(MUL(Answer, ModOfPower(Base, SUB(Power, "1"), Mod)), Mod); //O(N^1.59)
                string X = Modules.Item2;   //O(N)
                Modules = DIV(X , Mod); //O(N)
                Answer = Modules.Item2; //O(N)
            }
            return Answer; //O(1)
        }
        public static bool Compare(string num1, string num2)  //O(N)
        {
            Equalize(ref num1, ref num2);
            char[] N1 = num1.ToCharArray(); //O(N)
            char[] N2 = num2.ToCharArray(); //O(N)
            for (int i = 0; i < num1.Length; i++)    //O(N) * O(body)
            {
                if (N1[i] > N2[i])   //O(1)
                {
                    return true;   //O(1)
                }
                else if (N1[i] < N2[i])  //O(1)
                {
                    return false;    //O(1)
                }
            }
            return true;    //O(1)
        }      
        public static string Reverse(string Ans)
        {
            return Ans;
        }
        public static string LeftZeros(char[] res)  //O(N)
        {
            long idx = 0;   //O(1)
            string modAnswer = "";//(neg) ? "" : "-";    //O(1)
            for (long i = 0/*(neg) ? 0 : 1*/; i < res.Length; i++)    //O(N) * O(body)
            {
                if (res[i] != '0')   //O(1)      
                {
                    idx = i;        //O(1)
                    break;      //O(1)
                }
            }
            for (long i = idx; i < res.Length; i++)  //O(N) * O(body)
            {
                modAnswer += res[i];    //O(N)
            }
            return modAnswer;       //O(1)
        }
        public static int Equalize(ref string Num1, ref string Num2)    //O(N)
        {
            char[] N1 = Num1.ToCharArray(); //O(N)
            char[] N2 = Num2.ToCharArray(); //O(N)
            int difference = Math.Abs(Num1.Length - Num2.Length);   //O(1)
            if (difference == 0)        //O(1)
            {
                return Num1.Length;     //O(1)
            }
            if (Num1.Length < Num2.Length)  //O(1)
            {
                char[] temp = new char[Num2.Length];    //O(N)
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
                    Num1 += temp[i];        //O(N)
                }
                return Num2.Length;     //O(1)
            }
            else
            {
                char[] temp = new char[Num1.Length]; //O(N)
                for (int i = 0; i < Num1.Length; i++)   //O(N) * O(body)
                {
                    temp[i] = '0';      //O(1)
                }
                for (int j = N2.Length - 1; j >= 0; j--)  //O(N) * O(body)
                {
                    temp[j + difference] = N2[j];       //O(1)
                }
                Num2 = "";      //O(1)
                for (int i = 0; i < temp.Length; i++)   //O(N) * O(body)
                {
                    Num2 += temp[i];        //O(N)
                }
                return Num1.Length;     //O(1)
            }
        }      
        public static string calculate(string AC, string BD, string ABCD, int l) //O(N)
        {
            string T0 = SUB(SUB(ABCD, AC), BD);      //O(N)
            string T1 = T0.PadRight(T0.Length + l / 2, '0');   //O(N)
            string T2 = AC.PadRight(AC.Length + l, '0');      //O(N)
            return Add(Add(T1, T2), BD);          //O(N)
        }
        public static string singleDigit(string First, string Second) //O(1)
        {
            long res = long.Parse(First) * long.Parse(Second); //O(1)
            return res.ToString(); //O(1)
        }        
    }
}
/////////**** Total Complexity Of Code SHOULD BE  O(N^1.59) ****///////////
////////////**** CURRENT CCOMPLEXITY  O(N^2) ****///////////
