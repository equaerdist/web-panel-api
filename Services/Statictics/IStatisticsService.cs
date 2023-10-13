using web_panel_api.Dto;

namespace web_panel_api.Services.Statictics
{
    public interface IStatisticsService
    {
        Task<GetStaticticsDto> GetStats(DateDto dates, string group, string offset, string project);
    }
}
