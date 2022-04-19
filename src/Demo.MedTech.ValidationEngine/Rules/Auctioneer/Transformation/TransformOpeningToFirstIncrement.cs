using Demo.MedTech.DataModel.Shared;
using Demo.MedTech.Utility.Helper;
using Demo.MedTech.ValidationEngine.Model;
using System.Linq;

namespace Demo.MedTech.ValidationEngine.Rules.Auctioneer.Transformation
{
    public class TransformOpeningToFirstIncrement : ITransform
    {
        private const int TransformOpeningToFirstIncrementWarningCode = 302;

        public RuleValidationMessage Transform(AuctioneerContext auctioneerContext)
        {
            RuleValidationMessage ruleValidationMessage = new RuleValidationMessage() { IsValid = true };

            if (!auctioneerContext.LotDetail.OpeningPrice.HasValue || auctioneerContext.LotDetail.OpeningPrice <= 0)
            {
                auctioneerContext.LotDetail.OpeningPrice = auctioneerContext.LotDetail.Increment[0].IncrementValue;

                ruleValidationMessage.ValidationResults.AddRange(
                    Response.ValidationResults.Where(x => x.Code == TransformOpeningToFirstIncrementWarningCode));
            }

            return ruleValidationMessage;
        }
    }
}
