using TradingView.BLL.Contracts.StockProfile;
using TradingView.DAL.Contracts.StockProfile;
using TradingView.DAL.Entities.StockProfileEntities;

namespace TradingView.BLL.Services.StockProfile;
public class СompanyService : IСompanyService
{
    private readonly IСompanyRepository _companyRepository;

    public СompanyService(IСompanyRepository companyRepository)
    {
        _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    }
    public async Task<Сompany> GetCompanyAsync(string symbol, CancellationToken ct = default)
    {
        return await _companyRepository.GetAsync(x => x.Symbol == symbol, ct);
    }
}
