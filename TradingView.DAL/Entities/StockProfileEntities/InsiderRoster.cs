﻿namespace TradingView.DAL.Entities.StockProfileEntities;
public class InsiderRoster : EntityBase
{
    public string Symbol { get; set; }
    public List<InsiderRosterItem> Items { get; set; }
}
