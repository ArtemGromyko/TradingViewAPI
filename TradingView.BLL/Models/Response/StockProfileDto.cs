using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Models.Response
{
    public class StockProfileDto
    {
        public CEOCompensation? CEOCompensation { get; set; }
        public Company? Company { get; set; }
        public List<InsiderRosterItem>? InsiderRoster { get; set; }
        public List<InsiderSummaryItem>? InsiderSummary { get; set; }
        public List<InsiderTransactionsItem>? InsiderTransactions { get; set; }
        public string? Logo { get; set; }
        public List<string>? PeerGroup { get; set; }
    }
}
