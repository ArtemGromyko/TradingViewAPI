namespace TradingView.DAL.Settings;

public class SymbolsCollectionSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string SymbolsCollectionName { get; set; } = null!;
}
