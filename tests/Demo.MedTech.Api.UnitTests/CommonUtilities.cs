using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Demo.MedTech.DataModel.Request;
using Demo.MedTech.DataModel.Shared;
using Demo.MedTech.Utility.Helper;

namespace Demo.MedTech.Api.UnitTests
{
    public static class CommonUtilities
    {
        public static string HouseBidderRef = "House";
        public static Guid AuctionHouseId = Guid.NewGuid();

        public static LotModel CreateLot()
        {
            return new LotModel
            {
                LotDetail = new LotDetail
                {
                    AuctionId = 1,
                    LotId = 1,
                    OpeningPrice = 10,
                    ReservePrice = 100,
                    Quantity = 5,
                    Increment = new List<Increment>()
                    {
                        new Increment()
                        {
                            Low = 0,
                            High = 100,
                            IncrementValue = 10
                        },
                        new Increment()
                        {
                            Low = 100,
                            High = 500,
                            IncrementValue = 50
                        },
                        new Increment()
                        {
                            Low = 500
                        }
                    }
                },
                BiddingStates = new List<BiddingState>
                {
                    new BiddingState
                    {
                        Id = "445939884",
                        Action = new Demo.MedTech.DataModel.Shared.Action
                        {
                            ActorType = ActorTypes.Bidder,
                            
                            ActionType = ActionTypes.CreateLot,
                            ActionResult = ActionResults.LotCreated,
                            TimeStamp = DateTime.UtcNow
                        },
                        SequenceNumber = 0,
                        State = new State
                        {
                            MaxBid = 0,
                            BidderId = "PBX54566",
                            CurrentBid = 0,
                            MinimumBid = 10,
                        }
                    }
                }
            };
        }

        public static LotModel CreateLot(decimal opening = 35, decimal? reserve = 50, decimal? minBid = null, long AuctionId = 1, long LotId = 13, decimal? buyItNow = null, int extensionTimeInSeconds = 600, int remainingTimeInSeconds = 18000, List<Increment> increments = null, string reserveType = null, DateTime? startTime = null, string currency = "GBP",int quantity = 5, string incrementType = "Auction", bool isPiecemeal = false, DateTime? endTime = null)
        {
            return new LotModel
            {
                LotDetail = new LotDetail
                {
                    AuctionId = AuctionId,
                    LotId = LotId,
                    OpeningPrice = opening,
                    ReservePrice = reserve,
                    Quantity = quantity,
                    Increment = increments ?? new List<Increment>
                    {
                        new Increment {Low = 0,High = 50,IncrementValue = 5},
                        new Increment {Low = 50,High = 100,IncrementValue = 10},
                        new Increment {Low = 100,High = 500,IncrementValue = 25},
                        new Increment {Low = 500,High = 1000,IncrementValue = 50},
                        new Increment {Low = 1000,High = 5000,IncrementValue = 100},
                        new Increment {Low = 5000,High=null,IncrementValue = 100}
                    }
                },
                BiddingStates = new List<BiddingState>
                {
                    new BiddingState
                    {
                        Id = "123",
                        Action = new Demo.MedTech.DataModel.Shared.Action
                        {
                            ActorType = ActorTypes.Bidder,
                            ActionType = ActionTypes.CreateLot,
                            ActionResult = ActionResults.LotCreated,
                            TimeStamp = DateTime.UtcNow
                        },

                        SequenceNumber = 0,
                        State = new State
                        {
                            MaxBid = 0,
                            BidderId = null,
                            CurrentBid = 0,
                            MinimumBid = minBid ?? opening
                        }
                    }
                }
            };
        }

        
        public static PlaceBid CreatePlaceBidRequest(decimal amount, string bidderId = "Bidder1", long AuctionId = 11,
            long LotId = 11, string MarketplaceCode = "1", string PlatformCode = "10", string bidderRef = "UnitTest")
        {
            return new PlaceBid
            {
                Amount = amount,
                AuctionId = AuctionId,
                BidderId = bidderId,
                LotId = LotId,
                MarketplaceCode = MarketplaceCode,
                PlatformCode = PlatformCode,
                BidderRef = bidderRef
            };
        }

        public static string CreateLotDetailString(long AuctionId, long LotId, decimal? openingPrice, decimal? buyItNow, decimal? quantity, string timeZone, int? extensionTimeInSeconds, decimal? reservePrice = null, List<Increment> increments = null, bool? pieceMeal = false, string startTime = "2021-07-25T19:20:30+05:30", string endsFrom = "2022-07-27T19:20:30+05:30", string biddingType = "TimedBidding", string currencyType = "GBP", string incrementType = null, string reserveType = null,Guid AuctionHouseId = new Guid())
        {
            var userInput = new
            {
                AuctionId = AuctionId,
                LotId = LotId,
                BiddingType = biddingType,
                OpeningPrice = openingPrice,
                ReservePrice = reservePrice,
                BuyItNow = buyItNow,
                Currency = currencyType,
                IncrementType = incrementType,
                Increment = increments ?? new List<Increment> { new Increment { Low = 0, High = 50, IncrementValue = 5 }, new Increment { Low = 50, High = null, IncrementValue = 100 } },
                Quantity = quantity,
                IsPiecemeal = pieceMeal,
                StartTime = startTime,
                EndsFrom = endsFrom,
                TimeZone = timeZone,
                ExtensionTimeInSeconds = extensionTimeInSeconds,
                BiddingSuspended = false,
                ReserveType = reserveType,
                AuctionHouseId = AuctionHouseId
            };

            return JsonSerializer.Serialize(userInput);
        }

        public static LotRequest CreateEditLotDetailRequest(long AuctionId, long LotId, DateTime? startTime, DateTime? endsFrom, decimal? openingPrice, decimal? reservePrice = null, List<Increment> increments = null, string reserveType = null, Guid AuctionHouseId = new Guid(), int extensionTimeInSeconds=600, string currency = "GBP", int quantity = 5, string incrementType = "Auction", bool isPiecemeal = false)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeConverter());

            var lotDetail = new LotDetail
            {
                AuctionId = AuctionId,
                LotId = LotId,
                OpeningPrice = openingPrice,
                ReservePrice = reservePrice,
                Increment = increments ?? new List<Increment>
                {
                    new Increment {Low = 0,High = 50,IncrementValue = 5},
                    new Increment {Low = 50,High = 100,IncrementValue = 10},
                    new Increment {Low = 100,High = 500,IncrementValue = 25},
                    new Increment {Low = 500,High = 1000,IncrementValue = 50},
                    new Increment {Low = 1000,High = 5000,IncrementValue = 100},
                    new Increment {Low = 5000}
                },
                Quantity = quantity
            };

            var userInput = new LotRequest
            {
                AuctioneerId = "Test",
                LotDetail = JsonSerializer.Serialize(lotDetail, options),
                PlatformCode = "10"
            };

            return userInput;
        }

        public static decimal ConvertOffIncrementToOnIncrement(List<Increment> increments, decimal amount)
        {
            var incrementValue = IncrementHelper.GetIncrementFromRange(increments, amount);
            if (amount % incrementValue != 0)
            {
                amount = (Math.Floor(amount / incrementValue) * incrementValue);
            }

            return amount;
        }
        public static decimal GetIncrementedValue(List<Increment> increments, decimal amount)
        {
            amount = ConvertOffIncrementToOnIncrement(increments, amount);
            return amount + IncrementHelper.GetIncrementFromRange(increments, amount);
        }

        public static LotDetail CreateLotDetail(long AuctionId,
            long LotId,
            decimal openingPrice,
            decimal? buyItNow,
            int quantity,
            string timeZone,
            int extensionTimeInSeconds,
            decimal? reservePrice = null,
            List<Increment> increments = null,
            DateTime startTime = default,
            DateTime endsFrom = default
        )
        {
            return new LotDetail()
            {
                AuctionId = AuctionId,
                LotId = LotId,
                OpeningPrice = openingPrice,
                ReservePrice = reservePrice,
                Increment = increments ?? new List<Increment>
                {
                    new Increment {Low = 0,High = 50,IncrementValue = 5},
                    new Increment {Low = 50,High = 100,IncrementValue = 10},
                    new Increment {Low = 100,High = 500,IncrementValue = 25},
                    new Increment {Low = 500,High = 1000,IncrementValue = 50},
                    new Increment {Low = 1000,High = 5000,IncrementValue = 100},
                    new Increment {Low = 5000,High=null,IncrementValue = 100}
                },
                Quantity = quantity
            };
        }

        public static CreateLotRequestModelTest CreateRequestModel(LotDetail lotDetail)
        {
            return new CreateLotRequestModelTest
            {
                LotDetail = lotDetail
            };
        }

        public static bool CheckIncrementEquality(this List<Increment> initialIncrement, List<Increment> changedIncrement)
        {
            if (initialIncrement.Count != changedIncrement.Count)
            {
                return false;
            }

            int rowCount = initialIncrement.Count - 1;

            while (rowCount >= 0)
            {
                if (initialIncrement[rowCount].Low != changedIncrement[rowCount].Low ||
                    initialIncrement[rowCount].High != changedIncrement[rowCount].High ||
                    initialIncrement[rowCount].IncrementValue != changedIncrement[rowCount].IncrementValue)
                {
                    return false;
                }
                rowCount--;
            }
            return true;
        }
    }

    public class CreateLotRequestModelTest
    {
        public LotDetail LotDetail { get; set; }
    }
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
        }
    }

}
