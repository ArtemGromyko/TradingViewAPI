namespace TradingView.DAL.Entities.RealTime;


public class LargestTrade : EntityBase
{
    public float Price { get; set; }
    public int Size { get; set; }
    public long Time { get; set; }
    public string? TimeLabel { get; set; }
    public string? VenueName { get; set; }
    public string? Venue { get; set; }
}

