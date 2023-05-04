using Moq;
using NUnit.Framework;
using Shopping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Threading;
//using Xunit;

namespace UnitTestForShopping
{
    [TestFixture]
    public class UnitTest1
    {
        private Mock<InterfaceItemAddition> _itemadd;
        private ScreenSetUp _ss;
        private Mock<InterfaceInputArea> _intInputArea;

        [SetUp]
        public void SetUp()
        {
            
            _itemadd = new Mock<InterfaceItemAddition>();
            
            _intInputArea = new Mock<InterfaceInputArea>();
            
        }
        [Test]
        public void TestMethod1()
        {
            //var ss = new ScreenSetUp();
            //var item1 = new Item();
            //Program.ScreenSetup();
            _ss = new ScreenSetUp(_itemadd.Object, _intInputArea.Object);
            Assert.That(_ss.numForItem, Is.EqualTo(1001));

        }

        [Test]
        public void TestMethod2()
        {

            //var T1 = ss.listOfItems.GetType();
            //var item1 = new Item();
            //Program.ScreenSetup();
            _ss = new ScreenSetUp(_itemadd.Object, _intInputArea.Object);
            Assert.That(_ss.listOfItems, Is.EqualTo(new System.Collections.Generic.List<Item>()));

        }

        [Test]
        public void TestMethod3()
        {

            var item1 = new Item()
            {
                ItemName = "CampaCola",
                itemCode = 001,
                availableQuantity = 100,
                price = 1.2f,
                purchaseQuantity = 50,
                reStockDate = DateTime.Now.AddDays(4).ToString("dd/mm/yyyy"),
            };
            _ss = new ScreenSetUp(_itemadd.Object, _intInputArea.Object);
            _itemadd.Setup(ia => ia.ItemToBeAdded(It.IsAny<string>(), It.IsAny<int>())).Returns(item1);
            //_intInputArea.Setup(iia => iia.EnterItemsToTheList()).Returns("2");
            //_intInputArea.Setup(iic => iic.EnterItemsToTheList()).Returns("quit");
            _intInputArea.Setup(iia => iia.EnterItemsToTheList()).
                Returns(new Queue<string>(new[] { "2", "quit" }).Dequeue);
            var stringarray = new String[2];
            stringarray[0] = "quit";
            _intInputArea.Setup(iib => iib.SeekCustomerInput(_ss.listOfItems)).Returns(stringarray);

            
            
            var l1=_ss.refreshingscreenandPrinting();
            System.Threading.Thread.Sleep(1000);
            //var item1 = new Item();
            //Program.ScreenSetup();
            Assert.That(_ss.listOfItems[0], Is.EqualTo(item1));

        }


    }
}
