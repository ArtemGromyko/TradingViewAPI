using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Services.StockProfile;
public class CEOCompensationService : ICEOCompensationService
{
    public CEOCompensationService()
    {

    }

    public Task<CEOCompensation> GetAsync()
    {
        throw new NotImplementedException();
    }
}
