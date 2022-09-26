using TradingView.BLL.Contracts;
using TradingView.BLL.Contracts.StockProfile;
using TradingView.BLL.Models.Response;

namespace TradingView.BLL.Services
{
    public class StockProfileService : IStockProfileService
    {
        private readonly ILogoService _logoService;
        private readonly IСompanyService _companyService;
        private readonly ICEOCompensationService _ceoCompensationUrlService;
        private readonly IInsiderRosterService _insiderRosterService;
        private readonly IInsiderTransactionsService _insiderTransactionsService;
        private readonly IInsiderSummaryService _insiderSummaryService;
        private readonly IPeerGroupService _peerGroupService;

        public StockProfileService(ILogoService logoService,
            IСompanyService companyService,
            ICEOCompensationService ceoCompensationUrlService,
            IInsiderRosterService insiderRosterService,
            IInsiderTransactionsService insiderTransactionsService,
            IInsiderSummaryService insiderSummaryService,
            IPeerGroupService peerGroupService)
        {
            _logoService = logoService ?? throw new ArgumentNullException(nameof(logoService));
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _ceoCompensationUrlService = ceoCompensationUrlService ?? throw new ArgumentNullException(nameof(ceoCompensationUrlService));
            _insiderRosterService = insiderRosterService ?? throw new ArgumentNullException(nameof(insiderRosterService));
            _insiderTransactionsService = insiderTransactionsService ?? throw new ArgumentNullException(nameof(insiderTransactionsService));
            _insiderSummaryService = insiderSummaryService ?? throw new ArgumentNullException(nameof(insiderSummaryService));
            _peerGroupService = peerGroupService ?? throw new ArgumentNullException(nameof(peerGroupService));
        }

        public async Task<StockProfileDto> GetAsync(string symbol, CancellationToken ct = default)
        {
            var logo = await _logoService.GetAsync(symbol, ct);
            var insiderRoster = await _insiderRosterService.GetAsync(symbol, ct);
            var peerGroup = await _peerGroupService.GetAsync(symbol, ct);

            var stockProfile = new StockProfileDto()
            {
                CEOCompensation = await _ceoCompensationUrlService.GetAsync(symbol, ct),
                Company = await _companyService.GetAsync(symbol, ct),
                Logo = logo.Url,
                InsiderRoster = insiderRoster.Items,
                InsiderSummary = await _insiderSummaryService.GetAsync(symbol, ct),
                InsiderTransactions = await _insiderTransactionsService.GetAsync(symbol, ct),
                PeerGroup = peerGroup.Items
            };

            return stockProfile;
        }
    }
}
