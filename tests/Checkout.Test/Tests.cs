using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckoutKata.Implementation;
using System.Collections.Generic;
using CheckoutKata.Domain.Exceptions;

namespace CheckoutKata.Test
{

    [TestClass]
    public class Tests
    {
        private Checkout _initialiseCheckout()
        {
            var _priceList = new List<ShopItem>();
            _priceList.Add(new ShopItem("A", 50, 3, 30));
            _priceList.Add(new ShopItem("B", 30, 2, 15));
            _priceList.Add(new ShopItem("C", 60));
            _priceList.Add(new ShopItem("D", 99));

            return new Checkout(_priceList);
        }


        [TestMethod]
        public void Scan_SKU_A()
        {
            var checkout = _initialiseCheckout();
            checkout.Scan("A");

            Assert.AreEqual(50, checkout.Total());
        }


        [TestMethod]
        public void Scan_3_SKU_A()
        {
            var checkout = _initialiseCheckout();
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            Assert.AreEqual(120, checkout.Total());
        }

        [TestMethod]
        public void Scan_4_SKU_A_and_1_SKU_B()
        {
            var checkout = _initialiseCheckout();
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");

            Assert.AreEqual(200, checkout.Total());
        }

        [TestMethod]
        public void ExceptionHandling()
        {
            var checkout = _initialiseCheckout();
            try
            {
                checkout.Scan("E");
            }
            catch (UnknownItemException e)
            {
                Assert.AreEqual("Unknown item scanned: E", e.Message);
            }
        }

    }
}
