using AutoMapper;
using Demo.MedTech.DataModel.Request;
using Demo.MedTech.DataModel.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Playground.Services.IServices;
using RestSharp;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Playground.Controllers
{
    public class MarketplaceController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRestClientApiCall _restClientApiCall;
        private readonly IConfiguration _configuration;

        public MarketplaceController(IRestClientApiCall restClientApiCall, IMapper mapper, IMediator mediator, IConfiguration configuration)
        {
            _restClientApiCall = restClientApiCall;
            _mapper = mapper;
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PlaceBid bid)
        {
            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonSerializer.Serialize(bid), ParameterType.RequestBody);

            IRestResponse response = _restClientApiCall.Execute(request, _configuration["CORE_BIDDING_API"] + _configuration["PLACE_BID_ENDPOINT"]);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNameCaseInsensitive = true
            };
            
            var bidResponse = JsonSerializer.Deserialize<BidMinimumAndStandardResponse>(response.Content, options);

            if (bidResponse?.IsValid == true)
            {
                // send message to service bus for sbs egress
                var lastBiddingStates = bidResponse.BiddingStates?.LastOrDefault()?.State;

                dynamic result = new ExpandoObject();
                result.timeStamp = bidResponse.TimeStamp.ToString(CultureInfo.InvariantCulture);
                result.bidderId = lastBiddingStates?.BidderId;

                return Ok(JsonSerializer.Serialize(result));
            }
            else
            {
                return Ok(JsonSerializer.Serialize(new { bidResponse.TimeStamp, bidResponse.ValidationResults }));
            }
        }

        [HttpPost]
        public ActionResult VerifyBid(PlaceBid bid)
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonSerializer.Serialize(bid), ParameterType.RequestBody);

            IRestResponse response = _restClientApiCall.Execute(request, _configuration["CORE_BIDDING_API"] + _configuration["VERIFY_BID_ENDPOINT"]);

            return Ok(response.Content);
        }

        [HttpPost]
        public ActionResult LotDetails(long auctionId, long lotId)
        {
            string url = _configuration["INGRESS_API"] + _configuration["LOT_LATEST_DETAILS_ENDPOINT"] + $"?auctionId={auctionId}&lotId={lotId}";
            var request = new RestRequest(Method.GET);
          
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-correlation-id", "1");
            IRestResponse response = _restClientApiCall.Execute(request, url);
            
            return Ok(response.Content);
        }
    }
}
