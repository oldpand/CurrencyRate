﻿namespace CurrencyRate.Model
{
    public class CurrencyRateRow
    {
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;
        public decimal Currency { get; set; }
        public DateTime DateTime { get; set; }
    }
}