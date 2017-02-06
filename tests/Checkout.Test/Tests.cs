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
        public void Add_New_PriceItem_and_Scan()
        {
            var checkout = _initialiseCheckout();
            checkout.UpsertShopItem(new ShopItem("E", 60, 2, 40));
            checkout.Scan("E");
            
            Assert.AreEqual(60, checkout.Total());
        }

        [TestMethod]
        public void Change_PriceItem_and_Scan()
        {
            var checkout = _initialiseCheckout();
            checkout.UpsertShopItem(new ShopItem("A", 60, 2, 40));
            checkout.Scan("A");
            checkout.Scan("A");
            Assert.AreEqual(80, checkout.Total());
        }

        [TestMethod]
        public void ExceptionHandling_ScannedUnknownSKU()
        {
            var checkout = _initialiseCheckout();
            var exceptionMessage = "No Exception Caught";
            try
            {
                checkout.Scan("E");
            }
            catch (UnknownItemException e)
            {
                exceptionMessage = e.Message;
            }

            Assert.AreEqual("Unknown item scanned: E", exceptionMessage);
        }

        [TestMethod]
        public void ExceptionHandling_ExcessiveDiscount()
        {
            var checkout = new Checkout();
            var exceptionMessage = "No Exception Caught";
            try
            {
                checkout.UpsertShopItem(new ShopItem("A", 10, 2, 20));
            }
            catch (ExcessiveDiscountException e)
            {
                exceptionMessage = e.Message;
            }
            Assert.AreEqual("Cannot add a discount of 20 when buying 2 items for SKU A as the discount per item is greater than or equal to the price 10", exceptionMessage);
        }

    }
}
