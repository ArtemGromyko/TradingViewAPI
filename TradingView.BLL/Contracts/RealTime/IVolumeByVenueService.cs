using TradingView.DAL.Entities.RealTime.VolumeByVenue;

namespace TradingView.BLL.Contracts.RealTime;

public interface IVolumeByVenueService
{
    Task<List<VolumeByVenueItem>> GetVolumesByVenueAsync(string symbol);
}
