﻿using web_panel_api.Models;

namespace web_panel_api.Dto
{
    public class GetReferralDto
    {
        public long Id { get; set; }
        public long? ChatId { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public sbyte? IsReplay { get; set; }
        public sbyte? IsFree { get; set; }
        public sbyte? StatusTariff { get; set; }
        public sbyte? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        /// <summary>
        /// Показывает сколько рефераллов данного ползователя являются активными
        /// </summary>
        public int Active { get; set; }
        /// <summary>
        /// Показывает сколько рефераллов данного ползователя являются неактивными
        /// </summary>
        public int NotActive { get; set; }
        public List<UsersKey> UsersKeys { get; set; } = null!;
    }
    public class GetGodEyeReferralDto
    {
        public long Id { get; set; }
        public long? ChatId { get; set; }
        public string? Username { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool? IsReplay { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int Active { get; set; }
        /// <summary>
        /// Показывает сколько рефераллов данного ползователя являются неактивными
        /// </summary>
        public int NotActive { get; set; }
    }
}
