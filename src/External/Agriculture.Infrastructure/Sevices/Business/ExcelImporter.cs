using Agriculture.Application.Services.Business;
using MiniExcelLibs;

namespace Agriculture.Infrastructure.Sevices.Business
{
    internal class ExcelImporter : IDataImporter
    {
        private readonly string _filePath;

        public ExcelImporter()
        {
            _filePath = Path.Combine(
                AppContext.BaseDirectory,
                "Data",
                "GardenData.xlsx");
        }

        public IReadOnlyList<T> Read<T>(string sheetName)
            where T : class, new()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException(_filePath);

            return MiniExcel
                .Query<T>(_filePath, sheetName)
                .ToList();
        }
    }
}
