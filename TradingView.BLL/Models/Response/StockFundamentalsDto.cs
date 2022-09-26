using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Models.Response
{
    public class StockFundamentalsDto
    {
        public List<BalanceSheetItem>? BalanceSheet { get; set; }
        public List<CashFlowItem>? CashFlow { get; set; }
        public List<Dividend>? Dividend { get; set; }
        public List<EarningsItem>? Earnings { get; set; }
        public List<string>? Options { get; set; }
        public List<Expiration>? Expiration { get; set; }
        public List<FinancialsItem>? Financials { get; set; }
        public List<Income>? IncomeStatement { get; set; }
        public List<ReportedFinancials>? ReportedFinancials { get; set; }
        public List<SplitEntity>? SplitEntity { get; set; }
    }
}
