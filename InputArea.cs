using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping
{
    public interface InterfaceInputArea
    {
        void DisplayCustomerList(List<Item> l);
        string EnterItemsToTheList();
        void ScreenSectionSplit();
        string[] SeekCustomerInput(List<Item> l);
        void WritingToFile(List<Item> listToFile);
    }

    public class InputArea : InterfaceInputArea
    {
        section section1, section2, section3;
        ScreenRefresh sr;
        public void ScreenSectionSplit()
        {
            section1 = new section { top = 1, bottom = Console.WindowHeight / 15, };
            section2 = new section { top = (Console.WindowHeight / 15) + 1, bottom = Console.WindowHeight / 2, };
            section3 = new section { top = (Console.WindowHeight / 2) + 1, bottom = Console.WindowHeight, };
            sr = new ScreenRefresh();
        }

        public string EnterItemsToTheList()
        {
            sr.BasicScreenRefresh(section1, section2, section3);
            Console.SetCursorPosition(0, section2.top + 1);
            Console.WriteLine("Enter the list of items needed in the Shopping cart.Quit to quit the list");

            return Console.ReadLine();
        }

        public string[] SeekCustomerInput(List<Item> l)
        {
            string[] custString = new string[2];
            sr.BasicScreenRefresh(section1, section2, section3, l);
            Console.SetCursorPosition(0, section3.top + 1);
            Console.WriteLine("Enter the item's code to buy from above list:");
            custString[0]= Console.ReadLine();
            if (custString[0] != "quit")
            {
                Console.WriteLine("Enter Quantity required");
                custString[1] = Console.ReadLine();
                
            }
            return custString;
        }

        public void DisplayCustomerList(List<Item> purchasel)
        {
            Console.WriteLine("Item Code  \t Price \t Quantity \t AmtDue\t Item Name");
            Console.WriteLine("========================================================");
            float total = 0;
            foreach (Item item in purchasel)
            {
                Console.WriteLine("{0} \t\t  {1} \t    {2} \t\t  {3} \t {4}", item.itemCode, item.price.ToString("0.00"), item.purchaseQuantity, (item.price * item.purchaseQuantity).ToString("000.00"), item.ItemName);
                total += item.price * item.purchaseQuantity;

            }
            Console.WriteLine("\t\tTotal:" + total.ToString("0.00"));

        }

        public void WritingToFile(List<Item> listToFile)
        {
            using (var streamWriter = new StreamWriter("C:\\GitRepos\\Shopping\\Spreadsheet.csv", true))
            {
                streamWriter.WriteLine("Item Code,Item Name, Selling Price, Available Quantity,ReStock Date,Purchased Quantity");
                foreach (var i in listToFile)
                    streamWriter.WriteLine(i.itemCode + "," + i.ItemName + "," + i.price + "," + i.availableQuantity + "," + i.reStockDate + "," + i.purchaseQuantity);
            }

        }

    }
}
