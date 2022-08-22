namespace TradingView.DAL.Settings;

public class HistoricalPricesCollectionSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string HistoricalPricesCollectionName { get; set; } = null!;
}
