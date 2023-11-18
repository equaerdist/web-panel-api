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
            Wallets = new HashSet<Wallet>();
        }

        public long Id { get; set; }
        public long? ChatId { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsReplay { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Balance Balance { get; set; } = null!;
        public virtual Referral ReferralChild { get; set; } = null!;
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Pay> Pays { get; set; }
        public virtual ICollection<Referral> ReferralParents { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
