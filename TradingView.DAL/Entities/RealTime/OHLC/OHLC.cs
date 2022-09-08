namespace TradingView.DAL.Entities.RealTime.OHLC;

public class OHLC
{
    public Open? Open { get; set; }
    public Close? Close { get; set; }
    public double? High { get; set; }
    public double? Low { get; set; }
}