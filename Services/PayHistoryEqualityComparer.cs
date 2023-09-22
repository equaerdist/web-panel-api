using System.Diagnostics.CodeAnalysis;
using web_panel_api.Models;

namespace web_panel_api.Services
{
    public class PayHistoryEqualityComparer : IEqualityComparer<PayHistory>
    {
        public bool Equals(PayHistory? x, PayHistory? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;
            return x.UserId == y.UserId;
        }

        public int GetHashCode([DisallowNull] PayHistory obj)
        {
            return obj.Id;
            
        }
    }
}
