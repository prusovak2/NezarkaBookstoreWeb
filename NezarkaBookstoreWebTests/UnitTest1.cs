using Microsoft.VisualStudio.TestTools.UnitTesting;
using NezarkaBookstoreWeb;
using JumpingPlatformGame;
using System.Drawing;

namespace NezarkaBookstoreWebTests
{
    [TestClass]
    public class CustomerEntityTests
    {
        [TestMethod]
        public void ColorTest()
        {
            Customer cust1 = new Customer { DateJoined = null, FirstName = "always" };
            CustomerEntity ent1 = new CustomerEntity(cust1);
            Assert.AreEqual(Color.Gold, ent1.Color);

            Customer cust2 = new Customer { DateJoined = new System.DateTime(2020,1,1), FirstName = "less then year" };
            CustomerEntity ent2 = new CustomerEntity(cust2);
            Assert.AreEqual(Color.Black, ent2.Color);

            Customer cust3 = new Customer { DateJoined = new System.DateTime(2019, 1, 1), FirstName = "at least year" };
            CustomerEntity ent3 = new CustomerEntity(cust3);
            Assert.AreEqual(Color.DarkRed, ent3.Color);

            Customer cust4 = new Customer { DateJoined = new System.DateTime(2018, 1, 1), FirstName = "at least two years" };
            CustomerEntity ent4 = new CustomerEntity(cust4);
            Assert.AreEqual(Color.Red, ent4.Color);

            Customer cust5 = new Customer { DateJoined = new System.DateTime(2017, 1, 1), FirstName = "at least three years" };
            CustomerEntity ent5 = new CustomerEntity(cust5);
            Assert.AreEqual(Color.IndianRed, ent5.Color);

            Customer cust6 = new Customer { DateJoined = new System.DateTime(2000, 1, 1), FirstName = "four and more years" };
            CustomerEntity ent6 = new CustomerEntity(cust6);
            Assert.AreEqual(Color.OrangeRed, ent6.Color);

            System.Console.WriteLine(".");
        }
    }
}
