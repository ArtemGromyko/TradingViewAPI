using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;
using TradingView.DAL.Repositories.StockProfile;

namespace TradingView.BLL.Services.StockProfile;
public class CEOCompensationService : ICEOCompensationService
{
    private readonly ICEOCompensationRepository _CEOCompensationRepository;

    public CEOCompensationService(ICEOCompensationRepository CEOCompensationRepository)
    {
        _CEOCompensationRepository = CEOCompensationRepository ?? throw new ArgumentNullException(nameof(CEOCompensationRepository));
    }
    public Task<CEOCompensation> GetAsync()
    {
        throw new NotImplementedException();
    }
}
