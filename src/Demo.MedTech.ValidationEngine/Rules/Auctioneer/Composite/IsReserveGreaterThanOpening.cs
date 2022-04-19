using Demo.MedTech.DataModel.Shared;
using Demo.MedTech.Utility.Helper;
using Demo.MedTech.ValidationEngine.Model;
using System.Linq;

namespace Demo.MedTech.ValidationEngine.Rules.Auctioneer.Composite
{
    public class IsReserveGreaterThanOpening : IRule
    {
        private const int IsReserveGreaterThanOpeningErrorCode = 152;

        public RuleValidationMessage Execute(AuctioneerContext auctioneerContext)
        {
            RuleValidationMessage ruleValidationMessage = new RuleValidationMessage() { IsValid = true };
            if (auctioneerContext?.LotDetail?.OpeningPrice > auctioneerContext?.LotDetail?.ReservePrice)
            {
                ruleValidationMessage.IsValid = false;
                ruleValidationMessage.ValidationResults.AddRange(Response.ValidationResults.Where(x => x.Code == IsReserveGreaterThanOpeningErrorCode));
            }

            return ruleValidationMessage;
        }
    }
}
