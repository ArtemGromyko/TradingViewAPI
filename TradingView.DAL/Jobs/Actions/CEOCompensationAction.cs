using TradingView.DAL.Contracts.Jobs.Action;

namespace TradingView.DAL.Jobs.Actions;
public class CEOCompensationAction : ICEOCompensationAction
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        Console.WriteLine("job--------------------------------------------------------------------------" + DateTime.Now);
        return Task.CompletedTask;
    }
}
