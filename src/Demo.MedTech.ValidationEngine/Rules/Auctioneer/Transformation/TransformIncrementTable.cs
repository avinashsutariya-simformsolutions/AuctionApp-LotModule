using Demo.MedTech.DataModel.Shared;
using Demo.MedTech.Utility.Helper;
using Demo.MedTech.ValidationEngine.Model;
using System.Linq;

namespace Demo.MedTech.ValidationEngine.Rules.Auctioneer.Transformation
{
    public class TransformIncrementTable : ITransform
    {
        private const int IncrementTableLastRowAddedWarningCode = 300;
        private const int IncrementTableLastRowAmendedWarningCode = 301;

        public RuleValidationMessage Transform(AuctioneerContext auctioneerContext)
        {
            RuleValidationMessage ruleValidationMessage = new RuleValidationMessage() { IsValid = true };
            int incrementListCount = auctioneerContext.LotDetail.Increment.Count;

            var lastIncrementRange = auctioneerContext.LotDetail.Increment[incrementListCount - 1];

            if (!lastIncrementRange.IncrementValue.HasValue && incrementListCount > 1)
            {
                var secondLastIncrementValue = auctioneerContext.LotDetail.Increment[incrementListCount - 2].IncrementValue;

                // Amend row
                auctioneerContext.LotDetail.Increment[incrementListCount - 1].IncrementValue = secondLastIncrementValue;
                ruleValidationMessage.ValidationResults.AddRange(Response.ValidationResults.Where(x => x.Code == IncrementTableLastRowAmendedWarningCode));
            }

            if (lastIncrementRange.High.HasValue && lastIncrementRange.IncrementValue.HasValue)
            {
                // Add new row
                auctioneerContext.LotDetail.Increment.Add(
                    new Increment()
                    {
                        Low = lastIncrementRange.High.Value,
                        IncrementValue = lastIncrementRange.IncrementValue.Value
                    });
                ruleValidationMessage.ValidationResults.AddRange(Response.ValidationResults.Where(x => x.Code == IncrementTableLastRowAddedWarningCode));
            }

            return ruleValidationMessage;
        }
    }
}
