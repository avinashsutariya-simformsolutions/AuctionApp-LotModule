using Demo.MedTech.DataModel.Shared;
using Demo.MedTech.Utility.Helper;
using Demo.MedTech.ValidationEngine.Model;
using System.Linq;

namespace Demo.MedTech.ValidationEngine.Rules.Auctioneer.Transformation
{
    public class TransformQuantity : ITransform
    {
        private const int TransformQuantityCode = 309;

        /// <summary>
        /// Transforms 
        /// set quantity to 1 if it is equal to zero or less than zero 
        /// </summary>
        /// <param name="auctioneerContext"></param>
        /// <returns></returns>
        public RuleValidationMessage Transform(AuctioneerContext auctioneerContext)
        {
            RuleValidationMessage ruleValidationMessage = new RuleValidationMessage() { IsValid = true };

            if (!auctioneerContext.LotDetail.Quantity.HasValue || auctioneerContext.LotDetail.Quantity.Value <= 0)
            {
                auctioneerContext.LotDetail.Quantity = 1;
                ruleValidationMessage.ValidationResults.AddRange(Response.ValidationResults.Where(x => x.Code == TransformQuantityCode));
            }

            return ruleValidationMessage;
        }
    }
}
