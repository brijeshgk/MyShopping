using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping
{
    public class ScreenRefresh
    {


     
        public void BasicScreenRefresh(section s1, section s2, section s3, List<Item> l1 = null)
        {
            Console.Clear();
            for (int i = 0; i < Console.WindowWidth - 1; i++)
            {
                Console.SetCursorPosition(i, s1.top);
                Console.WriteLine("=");
                Console.SetCursorPosition(i, s2.top);
                Console.WriteLine("=");
                Console.SetCursorPosition(i, s3.top);
                Console.WriteLine("=");
                Console.SetCursorPosition(i, s3.bottom - 1);
                Console.WriteLine("=");
            }
            Console.SetCursorPosition(55, (s1.bottom / 2) + 1);
            Console.WriteLine("Shopping List");



            Console.SetCursorPosition(0, s2.top + 1);

            if (l1 != null)
            {
                Console.WriteLine("[ItemCode]  [Price]  [AvailableQuantity]  [ReStock Date]  [Purchased Quantity] [ItemName]");

                var ignoreZerothLine = 0;
                string sampleString;
                using (var streamReader = new StreamReader("C:\\GitRepos\\Shopping\\Spreadsheet.csv"))
                {
                    while (!String.IsNullOrEmpty(sampleString = streamReader.ReadLine()))
                    {
                        if (!(ignoreZerothLine == 0))
                        {
                            //char c = ','; 
                            String[] strSplit = sampleString.Split(new char[] { ',' });
                            Console.WriteLine("{0}         {1} \t     {2} \t   {3}\t\t  {4}\t\t{5}", strSplit[0], float.Parse(strSplit[2]).ToString("0.00"), strSplit[3], strSplit[4], strSplit[5], strSplit[1]);
                        }
                        ignoreZerothLine++;
                    }

                }
            }

        }

    }
}
