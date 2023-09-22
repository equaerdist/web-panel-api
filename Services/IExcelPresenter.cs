using OfficeOpenXml;
using System.Reflection;

namespace web_panel_api.Services
{
    public interface IPresenter
    {
        Task<byte[]> GenerateFileBytes<T>(List<T> data);
    }
}
