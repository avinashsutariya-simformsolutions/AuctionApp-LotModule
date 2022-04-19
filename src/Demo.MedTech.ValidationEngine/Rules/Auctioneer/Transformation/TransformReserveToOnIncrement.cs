using Demo.MedTech.DataModel.Shared;
using Demo.MedTech.Utility.Helper;
using Demo.MedTech.ValidationEngine.Model;
using System;
using System.Linq;

namespace Demo.MedTech.ValidationEngine.Rules.Auctioneer.Transformation
{
    public class TransformReserveToOnIncrement : ITransform
    {
        private const int TransformReserveToOnIncrementWarningCode = 303;

        public RuleValidationMessage Transform(AuctioneerContext auctioneerContext)
        {
            RuleValidationMessage ruleValidationMessage = new RuleValidationMessage() { IsValid = true };

            if (!auctioneerContext.LotDetail.ReservePrice.HasValue)
            {
                return ruleValidationMessage;
            }

            decimal reserveAmount = auctioneerContext.LotDetail.ReservePrice.Value;
            decimal relevantIncrement = IncrementHelper.GetIncrementFromRange(auctioneerContext.LotDetail.Increment, reserveAmount);

            if (relevantIncrement > 0 && reserveAmount % relevantIncrement != 0)
            {
                decimal quotient = reserveAmount / relevantIncrement;

                auctioneerContext.LotDetail.ReservePrice = Math.Ceiling(quotient) * relevantIncrement;

                ruleValidationMessage.ValidationResults.AddRange(
                    Response.ValidationResults.Where(x => x.Code == TransformReserveToOnIncrementWarningCode));
            }

            return ruleValidationMessage;
        }
    }
}