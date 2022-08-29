using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Services.StockProfile;
public class CEOCompensationService : ICEOCompensationService
{
    public readonly ICEOCompensationRepository _CEOCompensationRepository;

    public CEOCompensationService(ICEOCompensationRepository CEOCompensationRepository)
    {
        _CEOCompensationRepository = CEOCompensationRepository;
    }
    public Task<CEOCompensation> GetAsync()
    {
        throw new NotImplementedException();
    }
}
