using System;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Case_Target_System_DTO;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("- First Question -");
                FirstQuestion();
                Console.WriteLine("- Second Question -");
                SecondQuestion();
                Console.WriteLine("- Third Question -");
                ThirdQuestion();
                Console.WriteLine("- Fourth Question -");
                FourthQuestion();
                Console.WriteLine("- Fifth Question -");
                FifthQuestion();
            }
            catch (Exception ex) 
            {
               Console.WriteLine(ex.Message);
            }
        }

        #region Questions
        static void FirstQuestion()
        {
            int INDICE = 13;
            int SOMA = 0;
            int K = 0;

            while (K < INDICE)
            {
                K++;
                SOMA += K;
            }
            Console.WriteLine($"Sum = {SOMA}");
        }
        static void SecondQuestion()
        {
            Console.Write("Type a Number: ");
            int number = int.Parse(Console.ReadLine());
            List<int> fibonnaciList = new List<int>() { 0, 1 };

            for (int i = 2; i <= number + 1; i += 1)
            {

                int previous_2 = fibonnaciList[i - 2];
                int previous_1 = fibonnaciList[i - 1];

                int result = previous_1 + previous_2;
                fibonnaciList.Add(result);
            }

            if (fibonnaciList.Contains(number))
            {
                Console.WriteLine($"The number {number} is IN the Fibonnaci Sequence");
            }
            else
            {
                Console.WriteLine($"The number {number} is NOT in the Fibonnaci Sequence");
            }
        }
        static void ThirdQuestion()
        {
            DailyBilling[] monthBilling = GetDataFromJson();
            double lowerBill = monthBilling.Select(dailybill => dailybill.Value).Where(value => value > 0).Min();
            double higherBill = monthBilling.Select(dailybill => dailybill.Value).Max();
            string[] monthAverage = DailyBillingAverage(monthBilling).Split('-');
            double average = double.Parse(monthAverage[0]);
            int days = int.Parse(monthAverage[1]);


            Console.WriteLine($"Lower Billing Value: {lowerBill} \n" +
                $"Higher Billing Value: {higherBill} \n" +
                $"number of days that daily revenue was greater than the average ({average:f2}) monthly revenue: {days}");

        }

        static void FourthQuestion()
        {
            DailyBilling[] monthBilling = GetDataFromJson();
            double billingsSum = monthBilling.Sum(dailybill => dailybill.Value);

            //faturamento mensal de uma distribuidora, detalhado por estado:
            string SP = (67836.43 / billingsSum).ToString("P");
            string RJ = (36678.66 / billingsSum).ToString("P");
            string MG = (29229.88 / billingsSum).ToString("P");
            string ES = (27165.48 / billingsSum).ToString("P");
            string others = (19849.53 / billingsSum).ToString("P");

            Console.WriteLine($"Percent of SP: {SP}\n" +
                $"Percent of RJ: {RJ}\n" +
                $"Percent of MG: {MG}\n" +
                $"Percent of ES: {ES}\n" +
                $"Percent of Others: {others}");
        }
        static void FifthQuestion()
        {
            Console.Write("Type a Word: ");
            string? response = Console.ReadLine();
            char[]? wordChars = response.ToCharArray();

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = wordChars.Length - 1; i >= 0; i--)
            {
                stringBuilder.Append(wordChars[i]);
            }

            Console.WriteLine($"{response} Reversed Word Equals: {stringBuilder}");
        }

        #endregion

        #region Methods
        static DailyBilling[] GetDataFromJson()
        {
            DailyBilling[]? monthBilling = new DailyBilling[30];
            string datapath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dados.json");
            using (StreamReader sr = new StreamReader(datapath))
            {
                string jsonstring = sr.ReadToEnd();
                if (!string.IsNullOrEmpty(jsonstring))
                {
                    monthBilling = JsonSerializer.Deserialize<DailyBilling[]>(jsonstring);
                }
            }
            return monthBilling;
        }
        static string DailyBillingAverage(DailyBilling[] monthBilling)
        {
            double average;
            double sum = 0;
            double count = 0;
            for (int i = 0; i < monthBilling.Length; i++)
            {
                if (monthBilling[i].Value > 0)
                {
                    count += 1;
                    sum += monthBilling[i].Value;
                }
            }
            average = sum / count;

            string result = $"{average}-{count}";
            return result;
        }

        #endregion
    }
}