using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class User
    {
        public User()
        {
            PayHistories = new HashSet<PayHistory>();
            Promocodes = new HashSet<Promocode>();
            ReferralsTreeChildren = new HashSet<ReferralsTree>();
            ReferralsTreeParents = new HashSet<ReferralsTree>();
            UsersTariffs = new HashSet<UsersTariff>();
        }

        public long Id { get; set; }
        public long? ChatId { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public sbyte? IsReplay { get; set; }
        public sbyte? IsFree { get; set; }
        public sbyte? StatusTariff { get; set; }
        public sbyte? Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<PayHistory> PayHistories { get; set; }
        public virtual ICollection<Promocode> Promocodes { get; set; }
        public virtual ICollection<ReferralsTree> ReferralsTreeChildren { get; set; }
        public virtual ICollection<ReferralsTree> ReferralsTreeParents { get; set; }
        public virtual UsersKey UsersKeys { get; set; }
        public virtual ICollection<UsersTariff> UsersTariffs { get; set; }
    }
}
