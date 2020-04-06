using System;

namespace ConsoleAppPM
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hell to World!");
            
            Console.WriteLine("Выберете операцию: 1 - проверка INN кода; 2 - проверка ЕДРПОУ; 3 - проверка банкоской карты");
            int numChoice = Convert.ToInt32(Console.ReadLine());
            switch (numChoice)
            {
                case 1:
                    Console.WriteLine("Выберете операцию: 1 - INN с 10 символами; 2 - INN с 14 символами, 3 - выход");
                    numChoice = Convert.ToInt32(Console.ReadLine());
                    switch (numChoice)
                    {
                        case 1:
                            Console.WriteLine("Напишите ваш INN");
                            int numSymOfArrayINN = 10;
                            int[] inn = DeclarArray(numSymOfArrayINN);                            
                            bool checkAnswerINN;
                            int year;
                            int day;
                            int month;
                            string nameOperationINN = "INN1";
                            CheckINN(inn, out checkAnswerINN);
                            CheckBirthday(inn, out year, out month, out day);
                            WriteAnswer(checkAnswerINN, nameOperationINN, CheckSex(inn), year, month, day);
                            break;
                        case 2:
                            Console.WriteLine("Данная функция пока не реализована");
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    Console.WriteLine("Напишите ваш код ЕДПРОУ");
                    int numSymOfArrayEDRPOU = 8;
                    string nameOperationEDRPOU = "EDRPOU";
                    int[] kodEDPROU = DeclarArray(numSymOfArrayEDRPOU);
                    bool checkAnswerEDRPOU = CheckEDPROU(kodEDPROU);
                    WriteAnswer(checkAnswerEDRPOU, nameOperationEDRPOU);
                    break;
                case 3:
                    Console.WriteLine("Напишите номер карты");
                    int numSymOfArrayCard = 16;
                    string nameOperationCard = "Card";
                    int[] cardArr = DeclarArray(numSymOfArrayCard);
                    bool checkAnswerCard = CheckCard(cardArr);
                    WriteAnswer(checkAnswerCard, nameOperationCard);
                    break;
            }
        }
        static int[] DeclarArray(int num)          // Записываем INN в виде массива, где каждый элемент массива является одним символом   
        {
            char[] y;
            do
            {
                long notX = Convert.ToInt64(Console.ReadLine());
                y = notX.ToString().ToCharArray();
                if (y.Length > num)
                {
                    Console.WriteLine($"Количество введённых знаков больше {num}. Повторите попытку.");
                }
                else
                {
                    if (y.Length < num)
                    {
                        Console.WriteLine($"Количество введённых знаков меньше {num}. Повторите попытку.");
                    }
                }
            } while (y.Length < num | y.Length > num);
            int[] smt = new int[num];
            for (int i = 0; i < smt.Length; i++)
            {
                smt[i] = Convert.ToInt32(y[i]) - '0';
            }
            return smt;
        }
        static void CheckINN(int[] inn, out bool checkAnswer)
        {
            int[] koef = new int[9] { -1, 5, 7, 9, 4, 6, 10, 5, 7 }; // коэфиценты
            int sum = 0;       // далее операция перемножения коэфицентов и inn кода. Так как нам нужна сумма, будем сразу её присваивать переменной sum  
            for (int i = 0; i < inn.Length - 1; i++)
            {
                sum += inn[i] * koef[i];
            }
            //sum = sum / 11;
            if (sum % 11 == inn[9])   // проверка контрольного числа
                checkAnswer = true;
            else
                checkAnswer = false;

        }
        static bool CheckEDPROU(int[] arr)
        {
            int arrInt = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                arrInt += arr[i] * Convert.ToInt32(Math.Pow(10, arr.Length - i - 1));
            }
            if (arrInt < 30000000 | arrInt > 60000000)
            {
                int[] koef = new int[7] { 1, 2, 3, 4, 5, 6, 7};
                int sum = 0;
                for (int i = 0; i < arr.Length-1; i++)
                {
                    sum += arr[i] * koef[i];
                }
                if (sum % 11 == arr[arr.Length - 1])
                    return true;
                else
                    return false;
            }
            else
            {
                int[] koef = new int[7] { 7, 2, 3, 4, 5, 6, 1};
                int sum = 0;
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    sum += arr[i] * koef[i];
                }
                if (sum % 11 == arr[arr.Length - 1])
                    return true;
                else
                    return false;
            }
        }
        static bool CheckCard(int[] arr)
        {
            int sum = 0;
            int num = 0;
            char[] y;
            for (int i = 0; i < arr.Length; i+=2)
            {
                num = arr[i] * 2;
                if (num > 9)
                {
                    y = num.ToString().ToCharArray();
                    num = (y[0] - '0') + (y[1] - '0');
                    sum += num;
                }
                else
                    sum += num;
            }
            for(int i = 1; i < arr.Length; i += 2)
            {
                sum += arr[i];
            }
            if (sum % 10 == 0)
                return true;
            else
                return false;
        }
        static void CheckBirthday(int[] inn, out int year, out int month, out int day)
        {
            year = 1900;
            day = 01;
            month = 01;
            int num = inn[0] * 10000 + inn[1] * 1000 + inn[2] * 100 + inn[3] * 10 + inn[4];
            while (num > 365)
            {
                if (year % 4 == 0)
                {
                    num -= 366;
                    year++;
                }
                else
                {
                    num -= 365;
                    year++;
                }
            }
            while (num > 30)
            {
                if (month == 2)
                {
                    num -= 28;
                    month++;
                }
                else
                {
                    if (month % 2 == 0)
                    {
                        num -= 30;
                        month++;
                    }
                    else
                    {
                        num -= 31;
                        month++;
                    }
                }
            }            
            day += num;           
        }
        static string CheckSex(int[] inn)
        {
            if (inn[8] % 2 == 0)
                return "Female";
            else
                return "Male";
        }
        static void WriteAnswer(bool x, string nameOperation, string answerSex = "Male", int year = 1900, int month = 01, int day = 01)
        {
            
            switch (nameOperation)
            {
                case "INN1":
                    if (x == true)
                    {
                        Console.WriteLine("INN верный");
                        Console.WriteLine("Пол - " + answerSex);
                        Console.WriteLine($"День вашего рождения: {year}.{month}.{day}");
                    }
                    else
                        Console.WriteLine("INN не верный");
                    break;
                case "INN2":
                    break;
                case "EDRPOU":
                    if (x == true)
                        Console.WriteLine("ЕДРПОУ верный");
                    else
                        Console.WriteLine("ЕДРПОУ не верный");                  
                    break;
                case "Card":
                    if (x == true)
                        Console.WriteLine("Номер карты верный");
                    else
                        Console.WriteLine("Номер карты не верный");
                    break;
            }
        }
    }
}
