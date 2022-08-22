namespace TradingView.DAL.Settings;

public class ExchangesCollectionSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ExchangesCollectionName { get; set; } = null!;
}
