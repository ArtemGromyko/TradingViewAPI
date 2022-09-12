namespace TradingView.DAL.Entities.RealTime.Book;

public class Book : EntityBase
{
    public string? Symbol { get; set; }
    public BookItem? BookItem { get; set; }
}
