using GraphQL.Types;
using RealEstateManager.Database.Models;

namespace RealEstateManager.Types
{
    public class PaymentType:ObjectGraphType<Payment>
    {
        public PaymentType()
        {
            Field(x => x.Id);
            Field(x => x.DateCreated);
            Field(x => x.DateOverdue);
            Field(x => x.Value);
            Field(x => x.Paid);
        }
    }
}
