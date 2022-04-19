using Demo.MedTech.DataModel.Shared;
using Demo.MedTech.Utility.Helper;
using Demo.MedTech.ValidationEngine.Model;
using System;
using System.Linq;

namespace Demo.MedTech.ValidationEngine.Rules.Auctioneer.Transformation
{
    public class TransformOpeningToOnIncrement : ITransform
    {
        private const int TransformOpeningToOnIncrementWarningCode = 302;

        public RuleValidationMessage Transform(AuctioneerContext auctioneerContext)
        {
            RuleValidationMessage ruleValidationMessage = new RuleValidationMessage() { IsValid = true };

            if (!auctioneerContext.LotDetail.OpeningPrice.HasValue || auctioneerContext.LotDetail.OpeningPrice <= 0)
            {
                return ruleValidationMessage;
            }

            decimal openingAmount = auctioneerContext.LotDetail.OpeningPrice.Value;
            decimal relevantIncrement = IncrementHelper.GetIncrementFromRange(auctioneerContext.LotDetail.Increment, openingAmount);

            if (relevantIncrement > 0 && openingAmount % relevantIncrement != 0)
            {
                decimal quotient = openingAmount / relevantIncrement;

                auctioneerContext.LotDetail.OpeningPrice = Math.Ceiling(quotient) * relevantIncrement;

                ruleValidationMessage.ValidationResults.AddRange(
                    Response.ValidationResults.Where(x => x.Code == TransformOpeningToOnIncrementWarningCode));
            }

            return ruleValidationMessage;
        }
    }
}
