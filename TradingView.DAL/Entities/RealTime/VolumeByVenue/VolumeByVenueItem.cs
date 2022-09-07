namespace TradingView.DAL.Entities.RealTime.VolumeByVenue;

public class VolumeByVenueItem
{
    public int Volume { get; set; }
    public string? Venue { get; set; }
    public string? VenueName { get; set; }
    public decimal MarketPercent { get; set; }
    public decimal AvgMarketPercent { get; set; }
    public string? Date { get; set; }
}
