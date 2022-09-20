using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TradingView.DAL.Jobs.Jobs.StockFundamentals;
public class IncomeStatementJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public IncomeStatementJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("IncomeStatementJob " + DateTime.Now);
    }
}
