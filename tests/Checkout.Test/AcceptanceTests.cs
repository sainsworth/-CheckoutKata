using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CheckoutKata.Domain.Exceptions;
using CheckoutKata.Models;

namespace CheckoutKata.Tests
{

    [TestClass]
    public class AcceptanceTests
    {
        private Checkout _initialiseCheckout()
        {
            var _shopItemList = new List<ShopItem>();
            _shopItemList.Add(new ShopItem("A", 50, 3, 30));
            _shopItemList.Add(new ShopItem("B", 30, 2, 15));
            _shopItemList.Add(new ShopItem("C", 60));
            _shopItemList.Add(new ShopItem("D", 99));

            return new Checkout(new PriceList(_shopItemList));
        }

        /// <summary>
        /// When I scan one item A then the total is 50
        /// </summary>
        [TestMethod]
        public void Acceptance__Scan_A_total_50()
        {
            var checkout = _initialiseCheckout();
            checkout.Scan("A");

            Assert.AreEqual(50, checkout.Total());
        }

        /// <summary>
        /// When I scan 3 item A then the total is 120
        /// </summary>
        [TestMethod]
        public void Acceptance__Scan_3_A_total_120()
        {
            var checkout = _initialiseCheckout();
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            Assert.AreEqual(120, checkout.Total());
        }

        /// <summary>
        /// When I scan one item B then the total is 30
        /// </summary>
        [TestMethod]
        public void Acceptance__Scan_B_total_30()
        {
            var checkout = _initialiseCheckout();
            checkout.Scan("B");

            Assert.AreEqual(30, checkout.Total());
        }

        /// <summary>
        /// When I scan 2 item B then the total is 45
        /// </summary>
        [TestMethod]
        public void Acceptance__Scan_2_B_total_45()
        {
            var checkout = _initialiseCheckout();
            checkout.Scan("B");

            Assert.AreEqual(30, checkout.Total());
        }

        /// <summary>
        /// When I scan one item C then the total is 60
        /// </summary>
        [TestMethod]
        public void Acceptance__Scan_C_total_60()
        {
            var checkout = _initialiseCheckout();
            checkout.Scan("C");

            Assert.AreEqual(60, checkout.Total());
        }

        /// <summary>
        /// When I scan one item C then the total is 60
        /// </summary>
        [TestMethod]
        public void Acceptance__Scan_D_total_99()
        {
            var checkout = _initialiseCheckout();
            checkout.Scan("D");

            Assert.AreEqual(99, checkout.Total());
        }

    }
}
