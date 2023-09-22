using OfficeOpenXml;
using System.Reflection;

namespace web_panel_api.Services
{
    public class ExcelPresenter : IPresenter
    {
        public async Task<byte[]> GenerateFileBytes<T>(List<T> data)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                PropertyInfo[] properties = typeof(T).GetProperties();


                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i].Name;
                }

                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 0; j < properties.Length; j++)
                    {
                        object? value = properties[j].GetValue(data[i]);
                        if (value is DateTime dateValue)
                            worksheet.Cells[i + 2, j + 1].Value = dateValue.ToString("dd.MM.yyyy");
                        else
                            worksheet.Cells[i + 2, j + 1].Value = value;
                    }
                }

                return await package.GetAsByteArrayAsync();
            }
        }
    }
}
