using Shopping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Shopping
{
    public class ScreenSetUp
    {

        string str;
        public int numForItem;
        private readonly InterfaceItemAddition _iIA;
        private readonly InterfaceInputArea _intIArea;
        

        public ScreenSetUp(InterfaceItemAddition IIA, InterfaceInputArea intIArea)
        {
            str = null;
            numForItem = 1001;
            _iIA = IIA;
            _intIArea = intIArea;
        }

        
        public  List<Item> listOfItems = new List<Item>();
        

        public List<Item> refreshingscreenandPrinting()
        {
               
            _intIArea.ScreenSectionSplit();

        do
        {
            str= _intIArea.EnterItemsToTheList();

            if (!(String.IsNullOrWhiteSpace(str) || String.IsNullOrEmpty(str))&& str!="quit")
            {

                listOfItems.Add(_iIA.ItemToBeAdded(str, numForItem));

                numForItem++;
            }
        } while (str != "quit") ;

        _intIArea.WritingToFile(listOfItems);

        List<Item> purchaselist = new List<Item>();
        string[] custString = new String[2];
        do
        {
                
            custString = _intIArea.SeekCustomerInput(listOfItems);

            if (Int32.TryParse(custString[0], out int code))
            {

                foreach (Item item in listOfItems)
                {
                    if (code == item.itemCode)
                    {
                        Console.WriteLine("Enter Quantity required");
                        if (Int32.TryParse(custString[1], out int qty))
                            purchaselist.Add(new Item { itemCode = item.itemCode, ItemName = item.ItemName, price = item.price, purchaseQuantity = qty, });

                    }

                }
            }

        } while (custString[0] != "quit");

            _intIArea.DisplayCustomerList(purchaselist);
        
            return listOfItems;
        }

    }
}
