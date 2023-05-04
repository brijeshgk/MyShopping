using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping
{
    public interface InterfaceItemAddition
    {
        Item ItemToBeAdded(string str, int it);
    }

    public class ItemAddition : InterfaceItemAddition
    {
        public Item ItemToBeAdded(string str, int i)
        {
            var item = new Item();
            item.ItemName = str;
            Console.WriteLine("Enter Selling Price:");
            item.price = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter Quantity:");
            item.availableQuantity = Int32.Parse(Console.ReadLine());
            item.reStockDate = $"{DateTime.Now.AddDays(4):d}";
            item.itemCode = i;

            return item;

        }
    }
}
