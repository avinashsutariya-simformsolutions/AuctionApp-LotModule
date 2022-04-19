using Demo.MedTech.DataModel.Shared;
using Demo.MedTech.Utility.Helper;
using Demo.MedTech.ValidationEngine.Model;
using System.Linq;

namespace Demo.MedTech.ValidationEngine.Rules.Auctioneer.Transformation
{
    public class TransformReserveToNull : ITransform
    {
        private const int TransformReserveToNullWarningCode = 308;

        public RuleValidationMessage Transform(AuctioneerContext auctioneerContext)
        {
            RuleValidationMessage ruleValidationMessage = new RuleValidationMessage() { IsValid = true };

            if (auctioneerContext.LotDetail.ReservePrice.HasValue && auctioneerContext.LotDetail.ReservePrice <= 0)
            {
                auctioneerContext.LotDetail.ReservePrice = null;
                ruleValidationMessage.ValidationResults.AddRange(
                    Response.ValidationResults.Where(x => x.Code == TransformReserveToNullWarningCode));
            }

            return ruleValidationMessage;
        }
    }
}
