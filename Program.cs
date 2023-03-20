using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Services;
//using System.ComponentModel.Design;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Collections.Specialized.BitVector32;

namespace Shopping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileaccess = new FileAccess();
            fileaccess.FileReadAccess();
            
            Console.WriteLine("Input <Enter key> if satisfied with above Listings");

            var newItem = Console.ReadLine();
            if (newItem.Trim()!= null)

            {

                Console.WriteLine("successess");
            }

            
            

            ScreenSetup();
        }

        public static void ScreenSetup()
        {


            section section1 = new section { top = 1, bottom = Console.WindowHeight / 15, };
            section section2 = new section { top = (Console.WindowHeight / 15) + 1, bottom = Console.WindowHeight / 2, };
            section section3 = new section { top = (Console.WindowHeight /2 ) + 1, bottom = Console.WindowHeight, };

            List<Item> listOfItems = new List<Item>();

            string str=null;
            int numForItem=1001;
            do
            {
                BasicScreenRefresh(section1, section2, section3);
                Console.SetCursorPosition(0, section2.top + 1);
                Console.WriteLine("Enter the list of items needed in the Shopping cart.Quit to quit the list");

                str = Console.ReadLine();

                if (!(String.IsNullOrWhiteSpace(str) || String.IsNullOrEmpty(str))&& str!="quit")
                {
                    var item = new Item();
                    item.ItemName = str;
                    Console.WriteLine("Enter Selling Price:");
                    item.price =float.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Quantity:");
                    item.availableQuantity= Int32.Parse(Console.ReadLine());
                    item.reStockDate = DateTime.Now.AddDays(4).ToString("dd/mm/yyyy");
                    item.itemCode = numForItem;
                    
                    //list.Add(new Item  { itemCode = numForItem,ItemName = str,price=30, availableQuantity=50, reStockDate=DateTime.Now.AddDays(4)});
                    listOfItems.Add(item);
                    numForItem++;
                }
            } while (str != "quit");

            using (var streamWriter = new StreamWriter("C:\\Test\\Shopping\\Spreadsheet.csv", true))
            {
                streamWriter.WriteLine("Item Code,Item Name, Selling Price, Available Quantity,ReStock Date,Purchased Quantity");
                foreach (var i in listOfItems)
                    streamWriter.WriteLine(i.itemCode + "," + i.ItemName + "," + i.price + "," + i.availableQuantity + "," + i.reStockDate + "," + i.purchaseQuantity);
            }

            //using FileStream fs = new FileStream();

            

            List<Item> purchaselist = new List<Item>();
            string purchaseItemCode = null;
            do
            {
                //BasicScreenRefresh(section s1, section s2, section s3)
                BasicScreenRefresh(section1, section2, section3, listOfItems);
                Console.SetCursorPosition(0, section3.top + 1);
                Console.WriteLine("Enter the item's code to buy from above list:");

                purchaseItemCode= Console.ReadLine();
                
                if (Int32.TryParse(purchaseItemCode, out int code))
                {
                   
                    foreach (Item item in listOfItems)
                    {
                        if (code==item.itemCode)
                        {
                            Console.WriteLine("Enter Quantity required");
                            if (Int32.TryParse(Console.ReadLine(), out int qty))
                                purchaselist.Add(new Item { itemCode = item.itemCode, ItemName=item.ItemName, price=item.price, purchaseQuantity = qty,});

                        }                           

                    }
                }
                
            }while (purchaseItemCode != "quit");

            Console.WriteLine("Item Code  \t Price \t Quantity \t AmtDue\t Item Name");
            Console.WriteLine("========================================================");
            float total = 0;
            foreach (Item item in purchaselist)
            {
                Console.WriteLine("{0} \t\t  {1} \t    {2} \t\t  {3} \t {4}", item.itemCode, item.price, item.purchaseQuantity,item.price * item.purchaseQuantity, item.ItemName);
                total += item.price * item.purchaseQuantity;
            }
            Console.WriteLine("\t\tTotal:"+total);

            //using (var streamWriter1 = new StreamWriter("C:\\Test\\Shopping\\Spreadsheet.csv", true))
            //{
            //    streamWriter1.WriteLine("New values being written, this is another, this what \n next line check?");
            //}

            //string sampleString;
            //using (var streamReader = new StreamReader("C:\\Test\\Shopping\\Spreadsheet.csv"))
            //{
                
            //    while (!String.IsNullOrEmpty (sampleString = streamReader.ReadLine()))
            //    {
                 
            //        Console.WriteLine(sampleString);
            //    }                                      

            //}          
        }

        public static void BasicScreenRefresh(section s1, section s2, section s3, List<Item> l1 = null)
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

            if(l1 !=null)
            {
                Console.WriteLine("[ItemCode]  [Price]  [AvailableQuantity]  [ReStock Date]  [Purchased Quantity] [ItemName]");
                //for (int i = 0; i < l1.Count; i++)
                //    Console.WriteLine("[{0}] \t\t [{1}] \t [{2}] \t\t\t [{3}]\t \t [{4}]", l1[i].itemCode, l1[i].ItemName, l1[i].price, l1[i].availableQuantity, l1[i].reStockDate, l1[i].purchaseQuantity);
                var ignoreZerothLine = 0;
                string sampleString;
                 using (var streamReader=new StreamReader("C:\\Test\\Shopping\\Spreadsheet.csv"))
                {
                    while (!String.IsNullOrEmpty(sampleString = streamReader.ReadLine()))
                    {
                        if (!(ignoreZerothLine == 0))
                        {
                            //char c = ','; 
                            String[] strSplit =sampleString.Split(new char[] {','});
                            Console.WriteLine("{0}         {1} \t     {2} \t\t   {3}\t\t{4}\t\t{5}",strSplit[0],float.Parse(strSplit[2]).ToString("0.00"),strSplit[3],strSplit[4],strSplit[5], strSplit[1]);
                        }
                        ignoreZerothLine++;
                    }             

                }
            }

        }
        
    }

    public class FileAccess
    {
        public void FileReadAccess()
        {
            if (File.Exists("C:\\Test\\Shopping\\Spreadsheet.csv"))
            {

                Console.Clear();

                Console.WriteLine("[ItemCode]  [Price]  [AvailableQuantity]  [ReStock Date]  [Purchased Quantity] [ItemName]");
                //for (int i = 0; i < l1.Count; i++)
                //    Console.WriteLine("[{0}] \t\t [{1}] \t [{2}] \t\t\t [{3}]\t \t [{4}]", l1[i].itemCode, l1[i].ItemName, l1[i].price, l1[i].availableQuantity, l1[i].reStockDate, l1[i].purchaseQuantity);
                var ignoreZerothLine = 0;
                string sampleString;
                using (var streamReader = new StreamReader("C:\\Test\\Shopping\\Spreadsheet.csv"))
                {
                    while (!String.IsNullOrEmpty(sampleString = streamReader.ReadLine()))
                    {
                        if (!(ignoreZerothLine == 0))
                        {
                            //char c = ','; 
                            String[] strSplit = sampleString.Split(new char[] { ',' });
                            Console.WriteLine("{0}         {1} \t     {2} \t   {3}\t\t   {4}\t\t{5}", strSplit[0], float.Parse(strSplit[2]).ToString("0.00"), strSplit[3], strSplit[4], strSplit[5], strSplit[1]);
                        }
                        ignoreZerothLine++;
                    }

                }

            }
        }
    }

}
