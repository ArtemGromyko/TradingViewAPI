namespace TradingView.DAL.Settings;

public class FundamentalsStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string FundamentalsCollectionName { get; set; } = null!;
}
