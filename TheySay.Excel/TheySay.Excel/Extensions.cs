using Microsoft.Office.Interop.Excel;

namespace TheySay.Excel
{
    public static class Extensions
    {
        public static Worksheet SheetExists(this Sheets sheets, string sheetName)
        {
            for (int i = 1; i <= sheets.Count; i++)
            {
                if ((sheets[i]).Name == sheetName)
                {
                    return sheets[i];
                }
            }
            return null;
        }
    }
}