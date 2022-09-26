using TradingView.BLL.Contracts;
using TradingView.BLL.Contracts.StockFundamentals;
using TradingView.BLL.Models.Response;
using TradingView.DAL.Entities.StockFundamentals;

namespace TradingView.BLL.Services
{
    public class StockFundamentalsService : IStockFundamentalsService
    {
        private readonly IBalanceSheetService _balanceSheetService;
        private readonly ICashFlowService _cashFlowService;
        private readonly IFinancialsService _financialsService;
        private readonly IReportedFinancialsService _reportedFinancialsService;
        private readonly IIncomeStatementService _incomeStatementService;
        private readonly ISplitService _splitService;
        private readonly IOptionService _optionService;
        private readonly IEarningsService _earningsService;
        private readonly IDividendService _idividendService;

        public StockFundamentalsService(IBalanceSheetService balanceSheetService,
            ICashFlowService cashFlowService,
            IFinancialsService financialsService,
            IReportedFinancialsService reportedFinancialsService,
            IIncomeStatementService incomeStatementService,
            ISplitService splitService,
            IOptionService optionService,
            IEarningsService earningsService,
            IDividendService idividendService)
        {
            _balanceSheetService = balanceSheetService ?? throw new ArgumentNullException(nameof(balanceSheetService));
            _cashFlowService = cashFlowService ?? throw new ArgumentNullException(nameof(cashFlowService));
            _financialsService = financialsService ?? throw new ArgumentNullException(nameof(financialsService));
            _reportedFinancialsService = reportedFinancialsService ?? throw new ArgumentNullException(nameof(reportedFinancialsService));
            _incomeStatementService = incomeStatementService ?? throw new ArgumentNullException(nameof(incomeStatementService));
            _splitService = splitService ?? throw new ArgumentNullException(nameof(splitService));
            _optionService = optionService ?? throw new ArgumentNullException(nameof(optionService));
            _earningsService = earningsService ?? throw new ArgumentNullException(nameof(earningsService));
            _idividendService = idividendService ?? throw new ArgumentNullException(nameof(idividendService));
        }

        public async Task<StockFundamentalsDto> GetAsync(string symbol, CancellationToken ct = default)
        {
            List<Expiration>? expiration = null;
            List<SplitEntity>? splitEntity;
            List<ReportedFinancials> reportedFinancials;
            FinancialsEntity financials;
            IncomeStatement incomeStatement;
            OptionEntity options;
            EarningsEntity earnings;
            List<Dividend> dividends;
            BalanceSheetEntity balanceSheet;
            CashFlowEntity cashFlow;

            try
            {
                splitEntity = await _splitService.GetAsync(symbol, ct);
            }
            catch
            {
                splitEntity = null;
            }

            try
            {
                reportedFinancials = await _reportedFinancialsService.GetAsync(symbol, ct);
            }
            catch
            {
                reportedFinancials = null;
            }

            try
            {
                incomeStatement = await _incomeStatementService.GetAsync(symbol, ct);
            }
            catch
            {
                incomeStatement = null;
            }

            try
            {
                financials = await _financialsService.GetAsync(symbol, ct);
            }
            catch
            {
                financials = null;
            }

            try
            {
                expiration = await _optionService.GetExpirationAsync(symbol, ct);
            }
            catch
            {
                expiration = null;
            }

            try
            {
                options = await _optionService.GetOptionAsync(symbol, ct);
            }
            catch
            {
                options = null;
            }

            try
            {
                earnings = await _earningsService.GetAsync(symbol, ct);
            }
            catch
            {
                earnings = null;
            }

            try
            {
                dividends = await _idividendService.GetAsync(symbol, null, ct);
            }
            catch
            {
                dividends = null;
            }

            try
            {
                balanceSheet = await _balanceSheetService.GetAsync(symbol, ct);
            }
            catch
            {
                balanceSheet = null;
            }

            try
            {
                cashFlow = await _cashFlowService.GetAsync(symbol, ct);
            }
            catch
            {
                cashFlow = null;
            }

            var stockFundamentalsDto = new StockFundamentalsDto()
            {
                BalanceSheet = balanceSheet?.BalanceSheet ?? null,
                CashFlow = cashFlow?.CashFlow ?? null,
                Dividend = dividends ?? null,
                Earnings = earnings?.Earnings ?? null,
                Options = options?.Options ?? null,
                Expiration = expiration ?? null,
                Financials = financials?.Financials ?? null,
                IncomeStatement = incomeStatement?.Income ?? null,
                ReportedFinancials = reportedFinancials ?? null,
                SplitEntity = splitEntity ?? null
            };

            return stockFundamentalsDto;
        }
    }
}
