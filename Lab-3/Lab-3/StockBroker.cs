﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Lab_3 {
    public class StockBroker {
        public string BrokerName { get; set; }
        public List<Stock> stocks = new List<Stock>();
        public static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
        readonly string docPath = @"/Users/raymanan11/CECS475Lab3/Lab3_output.txt";
        public string titles = "Broker".PadRight(10) + "Stock".PadRight(15) +
       "Value".PadRight(10) + "Changes".PadRight(10) + "Date and Time";
        /// <summary>
        /// The stockbroker object
        /// </summary>
        /// <param name="brokerName">The stockbroker's name</param>
        public StockBroker(string brokerName) {
            BrokerName = brokerName;
        }
        /// <summary>
        /// Adds stock objects to the stock list
        /// </summary>
        /// <param name="stock">Stock object</param>
        public void AddStock(Stock stock) {
            stocks.Add(stock);
            stock.StockEvent += EventHandler;
        }
        /// <summary>
        /// The eventhandler that raises the event of a change
        /// </summary>
        /// <param name="sender">The sender that indicated a change</param>
        /// <param name="e">Event arguments</param>
        void EventHandler(Object sender, EventArgs e) {
            try {
                myLock.EnterWriteLock();
                Stock newStock = (Stock)sender;
                Console.WriteLine(BrokerName + " " + ((StockNotification) e).ToString());
                DateTime currentDate = DateTime.Now;
                string stockInfo = currentDate + "  " + newStock.StockName + "  Initial Value: " + newStock.InitialValue + "  Current Value: " + newStock.CurrentValue;
                using (StreamWriter file = new StreamWriter(docPath, true)) {
                    file.WriteLine(stockInfo);
                }
                myLock.ExitWriteLock();
            }
            catch {}
        }
    }
}