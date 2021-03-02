﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_3 {
    public class Stock {
        public event EventHandler<StockNotification> StockEvent;
        private readonly Thread _thread;
        public string StockName { get; set; }
        public int InitialValue { get; set; }
        public int CurrentValue { get; set; }
        public int MaxChange { get; set; }
        public int Threshold { get; set; }
        public int NumChanges { get; set; }
        /// <summary>
        /// Stock class that contains all the information and changes of the stock
        /// </summary>
        /// <param name="name">Stock name</param>
        /// <param name="startingValue">Starting stock value</param>
        /// <param name="maxChange">The max value change of the stock</param>
        /// <param name="threshold">The range for the stock</param>
        public Stock(string name, int startingValue, int maxChange, int threshold) {
            StockName = name;
            InitialValue = startingValue;
            CurrentValue = startingValue;
            MaxChange = maxChange;
            Threshold = threshold;
            //Activate();
            //ActivateTask();
            _thread = new Thread(() => Activate());
            _thread.Start();
        }
        /// <summary>
        /// Activates the threads synchronizations
        /// </summary>
        public void Activate() {
            for (int i = 0; i < 25; i++) {
                Thread.Sleep(500); // 1/2 second
                //Task changeStock = Task.Run( () => ChangeStockValue());
                //await changeStock;
                ChangeStockValue();
            }
        }
        /// <summary>
        /// Changes the stock value and also raising the event of stock value changes
        /// </summary>
        public void ChangeStockValue() {
            var rand = new Random();
            CurrentValue += rand.Next((-1 * MaxChange), MaxChange);
            NumChanges++;
            if (Math.Abs((CurrentValue - InitialValue)) > Threshold) {
                StockEvent?.Invoke(this, new StockNotification(StockName, CurrentValue, NumChanges));
            }
        }

        //public async void ActivateTask() {
        //    Console.WriteLine("in async method");
        //    Task changeStock = Task.Run(() => Activate());
        //    await changeStock;
        //}

    }
}
