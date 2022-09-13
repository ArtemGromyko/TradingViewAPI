using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingView.DAL.Contracts;
using TradingView.DAL.Contracts.ApiServices;
using TradingView.DAL.Contracts.RealTime;

namespace TradingView.DAL.ApiServices;
public class RealTimeApiService : IRealTimeApiService
{
    private readonly IDividendsRepository _dividendsRepository;
    private readonly IExchangesRepository _exchangesRepository;
    private readonly IHistoricalPricesRepository _historicalPricesRepository;
    private readonly IQuotesRepository _quotesRepository;
    private readonly IIntradayPricesRepository _intradayPricesRepository;
    private readonly ILargestTradesRepository _largestTradesRepository;
    private readonly IOHLCRepository _ioHLCRepository;
    private readonly IPreviousDayPriceRepository _previousDayPriceRepository;
    private readonly IVolumeByVenueRepository _volumeByVenueRepository;

    public RealTimeApiService(IDividendsRepository dividendsRepository, 
        IExchangesRepository exchangesRepository,
        IHistoricalPricesRepository historicalPricesRepository, 
        IQuotesRepository quotesRepository, 
        IIntradayPricesRepository intradayPricesRepository,
        ILargestTradesRepository largestTradesRepository, 
        IOHLCRepository ioHLCRepository,
        IPreviousDayPriceRepository previousDayPriceRepository, 
        IVolumeByVenueRepository volumeByVenueRepository)
    {
        _dividendsRepository = dividendsRepository ?? throw new ArgumentNullException(nameof(dividendsRepository));
        _exchangesRepository = exchangesRepository ?? throw new ArgumentNullException(nameof(exchangesRepository));
        _historicalPricesRepository = historicalPricesRepository ?? throw new ArgumentNullException(nameof(historicalPricesRepository));
        _quotesRepository = quotesRepository ?? throw new ArgumentNullException(nameof(quotesRepository));
        _intradayPricesRepository = intradayPricesRepository ?? throw new ArgumentNullException(nameof(intradayPricesRepository));
        _largestTradesRepository = largestTradesRepository ?? throw new ArgumentNullException(nameof(largestTradesRepository));
        _ioHLCRepository = ioHLCRepository ?? throw new ArgumentNullException(nameof(ioHLCRepository));
        _previousDayPriceRepository = previousDayPriceRepository ?? throw new ArgumentNullException(nameof(previousDayPriceRepository));
        _volumeByVenueRepository = volumeByVenueRepository ?? throw new ArgumentNullException(nameof(volumeByVenueRepository));
    }
}
