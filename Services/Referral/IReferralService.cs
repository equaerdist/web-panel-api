using web_panel_api.Dto;

namespace web_panel_api.Services.Referral
{
    public interface IReferralService
    {
        Task<IEnumerable<GetReferralDto>> GetReferralsForUser(string? searchTerm, 
            int page, 
            int pageSize, 
            string sortParam, 
            string sortOrder, 
            string? childNode);
    }
}
