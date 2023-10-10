using System;
using System.Collections.Generic;

namespace web_panel_api.Models.god_eyes
{
    public partial class User
    {
        public User()
        {
            Clients = new HashSet<Client>();
            Pays = new HashSet<Pay>();
            ReferralParents = new HashSet<Referral>();
        }

        public long Id { get; set; }
        public long? ChatId { get; set; }
        public string? Username { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool? IsReplay { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Referral ReferralChild { get; set; } = null!;
        public virtual Wallet Wallet { get; set; } = null!;
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Pay> Pays { get; set; }
        public virtual ICollection<Referral> ReferralParents { get; set; }
    }
}
