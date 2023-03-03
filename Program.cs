using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Shopping
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ScreenSetup();
        }

        public static void ScreenSetup()
        {
             
            //while(Console.ReadKey().Key != ConsoleKey.Escape)
            

            section section1 = new section { top = 1, bottom = Console.WindowHeight / 15, };
            section section2 = new section { top = (Console.WindowHeight / 15) + 1, bottom = Console.WindowHeight / 2, };
            section section3 = new section { top = (Console.WindowHeight /2 ) + 1, bottom = Console.WindowHeight, };

            List<Item> list = new List<Item>();

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
                    list.Add(new Item  { itemCode = numForItem,ItemName = str,price=30, availableQuantity=50, reStockDate=DateTime.Now.AddDays(4)});
                    numForItem++;
                }
            } while (str != "quit");

            

            List<Item> purchaselist = new List<Item>();
            string purchaseItemCode = null;
            do
            {
                //BasicScreenRefresh(section s1, section s2, section s3)
                BasicScreenRefresh(section1, section2, section3, list);
                Console.SetCursorPosition(0, section3.top + 1);
                Console.WriteLine("Enter the item's code to buy from above list:");
                purchaseItemCode= Console.ReadLine();
                
                if (Int32.TryParse(purchaseItemCode, out int code))
                {
                   
                    foreach (Item item in list)
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
            
            foreach (Item item in purchaselist)
                Console.WriteLine("Item Code:{0} \t Item Name:{1} \t Price:{2} \t Quantity:{3} \t AmtDue:{4}", item.itemCode,item.ItemName, item.price, item.purchaseQuantity, item.price* item.purchaseQuantity);

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
                Console.WriteLine("[ItemCode] \t  [ItemName] \t [Price] \t [AvailableQuantity] \t [ReStock Date]");
                for (int i = 0; i < l1.Count; i++)
                    Console.WriteLine("[{0}] \t\t [{1}] \t [{2}] \t\t\t [{3}]\t \t [{4}]", l1[i].itemCode, l1[i].ItemName, l1[i].price, l1[i].availableQuantity, l1[i].reStockDate);

            }

        }
        
    }

    public class section
    {
        public int top { get; set; }
        public int bottom { get; set; }
    }

    public class Item
    {
        public int itemCode;
        public string ItemName;
        public float price;
        public int availableQuantity;
        public DateTime reStockDate;
        public int purchaseQuantity=0;

    }

}
