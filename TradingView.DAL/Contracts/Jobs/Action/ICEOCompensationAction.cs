namespace TradingView.DAL.Contracts.Jobs.Action;
public interface ICEOCompensationAction
{
    Task SendEmailAsync(string email, string subject, string message);
}
