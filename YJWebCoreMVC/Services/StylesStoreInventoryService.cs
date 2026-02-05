// Dharani 06/13/2025 created  model
// Dharani 06/17/2025 Added GetInventoryCountByCategory,GetCategoryDetails methods.
// Dharani 06/25/2025 Added SearchVendors method.
// Dharani 06/30/2025 Added GetVendorDetails method.
// Dharani 07/07/2025 Added Vendors property to populate vendor dropdown.
// Dharani 07/08/2025 Updated GetInventoryCountByVendor method.

using System.Data;

namespace YJWebCoreMVC.Services
{
    public class StylesStoreInventoryService
    {

        private readonly ConnectionProvider _connectionProvider;
        private readonly HelperCommonService _helperCommonService;

        public StylesStoreInventoryService(ConnectionProvider connectionProvider, HelperCommonService helperCommonService)
        {
            _connectionProvider = connectionProvider;
            _helperCommonService = helperCommonService;
        }

        public DataTable GetInventoryCountByVendor(string store, bool Vendor, bool IncludeItemsOnMemo, bool isDetails = false, bool grpByCat = false)
        {
            DataTable dtResult = _helperCommonService.getInventoryCountByVendor(store, Vendor, IncludeItemsOnMemo, isDetails);

            if (!dtResult.Columns.Contains("cast_code") || !dtResult.Columns.Contains("In_Stock") || !dtResult.Columns.Contains("Total_Cost"))
                return new DataTable();

            foreach (DataRow row in dtResult.Rows)
            {
                row["In_Stock"] = row["In_Stock"] != DBNull.Value ? row["In_Stock"] : 0;
                row["Total_Cost"] = row["Total_Cost"] != DBNull.Value ? row["Total_Cost"] : 0;
                if (dtResult.Columns.Contains("Category"))
                    row["Category"] = row["Category"] != DBNull.Value ? row["Category"] : "";
            }

            DataTable dtFiltered;

            if (!grpByCat)
            {
                // Group by cast_code only
                var grouped = dtResult.AsEnumerable()
                    .GroupBy(r => r["cast_code"].ToString())
                    .Select(g =>
                    {
                        var row = dtResult.NewRow();
                        row["cast_code"] = g.Key;
                        row["In_Stock"] = g.Sum(r => r.Field<decimal?>("In_Stock") ?? 0);
                        row["Total_Cost"] = g.Sum(r => r.Field<decimal?>("Total_Cost") ?? 0);
                        return row;
                    })
                    .Where(r => Convert.ToDecimal(r["In_Stock"]) != 0);

                dtFiltered = grouped.Any() ? grouped.CopyToDataTable() : dtResult.Clone();
                dtFiltered.DefaultView.Sort = "cast_code ASC";
                dtFiltered = dtFiltered.DefaultView.ToTable();
            }
            else
            {
                // Group by cast_code and Category
                var grouped = dtResult.AsEnumerable()
                    .GroupBy(r => new { cast_code = r["cast_code"].ToString(), Category = r["Category"].ToString() })
                    .Select(g =>
                    {
                        var row = dtResult.NewRow();
                        row["cast_code"] = g.Key.cast_code;
                        row["Category"] = g.Key.Category;
                        row["In_Stock"] = g.Sum(r => r.Field<decimal?>("In_Stock") ?? 0);
                        row["Total_Cost"] = g.Sum(r => r.Field<decimal?>("Total_Cost") ?? 0);
                        return row;
                    })
                    .Where(r => Convert.ToDecimal(r["In_Stock"]) != 0);

                dtFiltered = grouped.Any() ? grouped.CopyToDataTable() : dtResult.Clone();

                if (dtFiltered.Columns.Contains("cast_code") && dtFiltered.Columns.Contains("Category"))
                {
                    dtFiltered.Columns["cast_code"].SetOrdinal(0);
                    dtFiltered.Columns["Category"].SetOrdinal(1);
                }

                dtFiltered.DefaultView.Sort = "cast_code ASC, Category ASC";
                dtFiltered = dtFiltered.DefaultView.ToTable();
            }

            return dtFiltered;
        }


        public DataTable GetInventoryCountByCategory(string store, bool subCatGroup, bool includeItemsOnMemo, bool noNegativeCheck, bool isGroup)
        {
            //if (isGroup)
            //    subCatGroup = true;
            DataTable dtResult = _helperCommonService.getInventoryCountByCategory(store, subCatGroup, includeItemsOnMemo, noNegativeCheck);

            if (!dtResult.Columns.Contains("cast_code") || !dtResult.Columns.Contains("In_Stock") || !dtResult.Columns.Contains("Total_Cost"))
            {
                return new DataTable();
            }
            DataTable dtFiltered;

            if (!subCatGroup && !isGroup)
            {

                string[] columnsToRemove = { "style", "subcat", "group", "cast_code", "store_no", "Cost", "is_memo" };
                foreach (var col in columnsToRemove)
                    if (dtResult.Columns.Contains(col)) dtResult.Columns.Remove(col);
                dtFiltered = dtResult.AsEnumerable()
                    .GroupBy(r => new { Category = r["Category"].ToString() })
                    .Select(g =>
                    {
                        var row = dtResult.NewRow();
                        row["Category"] = g.Key.Category;
                        row["In_Stock"] = g.Sum(r => r.Field<decimal?>("In_Stock") ?? 0);
                        row["Wt_Stock"] = g.Sum(r => r.Field<decimal?>("Wt_Stock") ?? 0);
                        row["Layaway"] = g.Sum(r => r.Field<int?>("Layaway") ?? 0);
                        row["Alloc"] = g.Sum(r => r.Field<decimal?>("Alloc") ?? 0);
                        row["On_Memo"] = g.Sum(r => r.Field<decimal?>("On_Memo") ?? 0);
                        row["In_Transit"] = g.Sum(r => r.Field<decimal?>("In_Transit") ?? 0);
                        row["Total_Cost"] = g.Sum(r => r.Field<decimal?>("Total_Cost") ?? 0);
                        row["TOT_GOLDWT"] = g.Sum(r => r.Field<decimal?>("TOT_GOLDWT") ?? 0);
                        row["TOT_DIAMONDWT"] = g.Sum(r => r.Field<decimal?>("TOT_DIAMONDWT") ?? 0);
                        return row;
                    })
                    .Where(r => noNegativeCheck ? (r.Field<decimal?>("In_Stock") != 0 || r.Field<decimal?>("Wt_Stock") != 0 || r.Field<int?>("Layaway") != 0 || r.Field<decimal?>("Alloc") != 0 || r.Field<decimal?>("On_Memo") != 0 || r.Field<decimal?>("In_Transit") != 0) : true)
                    .OrderBy(r => r["Category"].ToString())
                    .CopyToDataTable();
            }
            else if (subCatGroup) // Group by Subcategory (optSubcat)
            {

                string[] columnsToRemove = { "style", "group", "cast_code", "store_no", "Cost", "is_memo" };
                foreach (var col in columnsToRemove)
                    if (dtResult.Columns.Contains(col)) dtResult.Columns.Remove(col);
                dtFiltered = dtResult.AsEnumerable()
                    .GroupBy(r => new { Category = r["Category"].ToString(), Subcat = r["subcat"].ToString() })
                    .Select(g =>
                    {
                        var row = dtResult.NewRow();
                        row["Category"] = g.Key.Category;
                        row["Subcat"] = g.Key.Subcat;
                        row["In_Stock"] = g.Sum(r => r.Field<decimal?>("In_Stock") ?? 0);
                        row["Wt_Stock"] = g.Sum(r => r.Field<decimal?>("Wt_Stock") ?? 0);
                        row["Layaway"] = g.Sum(r => r.Field<int?>("Layaway") ?? 0);
                        row["Alloc"] = g.Sum(r => r.Field<decimal?>("Alloc") ?? 0);
                        row["On_Memo"] = g.Sum(r => r.Field<decimal?>("On_Memo") ?? 0);
                        row["In_Transit"] = g.Sum(r => r.Field<decimal?>("In_Transit") ?? 0);
                        row["Total_Cost"] = g.Sum(r => r.Field<decimal?>("Total_Cost") ?? 0);
                        row["TOT_GOLDWT"] = g.Sum(r => r.Field<decimal?>("TOT_GOLDWT") ?? 0);
                        row["TOT_DIAMONDWT"] = g.Sum(r => r.Field<decimal?>("TOT_DIAMONDWT") ?? 0);
                        return row;
                    })
                    .Where(r => noNegativeCheck ? (r.Field<decimal?>("In_Stock") != 0 || r.Field<decimal?>("Wt_Stock") != 0 || r.Field<int?>("Layaway") != 0 || r.Field<decimal?>("Alloc") != 0 || r.Field<decimal?>("On_Memo") != 0 || r.Field<decimal?>("In_Transit") != 0) : true)
                    .OrderBy(r => r["Category"].ToString())
                    .ThenBy(r => r["Subcat"].ToString())
                    .CopyToDataTable();
                dtFiltered.Columns["Category"].SetOrdinal(0);
                dtFiltered.Columns["Subcat"].SetOrdinal(1);
            }
            else // Group by Group (optGroup)
            {

                string[] columnsToRemove = { "style", "cast_code", "store_no", "Cost", "is_memo" };
                foreach (var col in columnsToRemove)
                    if (dtResult.Columns.Contains(col)) dtResult.Columns.Remove(col);
                dtFiltered = dtResult.AsEnumerable().GroupBy(r => new { Category = r["Category"].ToString(), Subcat = r["subcat"].ToString(), Group = r["group"].ToString() })
                    .Select(g =>
                    {
                        var row = dtResult.NewRow();
                        row["Category"] = g.Key.Category;
                        row["Subcat"] = g.Key.Subcat;
                        row["Group"] = g.Key.Group;
                        row["In_Stock"] = g.Sum(r => r.Field<decimal?>("In_Stock") ?? 0);
                        row["Wt_Stock"] = g.Sum(r => r.Field<decimal?>("Wt_Stock") ?? 0);
                        row["Layaway"] = g.Sum(r => r.Field<int?>("Layaway") ?? 0);
                        row["Alloc"] = g.Sum(r => r.Field<decimal?>("Alloc") ?? 0);
                        row["On_Memo"] = g.Sum(r => r.Field<decimal?>("On_Memo") ?? 0);
                        row["In_Transit"] = g.Sum(r => r.Field<decimal?>("In_Transit") ?? 0);
                        row["Total_Cost"] = g.Sum(r => r.Field<decimal?>("Total_Cost") ?? 0);
                        row["TOT_GOLDWT"] = g.Sum(r => r.Field<decimal?>("TOT_GOLDWT") ?? 0);
                        row["TOT_DIAMONDWT"] = g.Sum(r => r.Field<decimal?>("TOT_DIAMONDWT") ?? 0);
                        return row;
                    })
                    .Where(r => noNegativeCheck ? (r.Field<decimal?>("In_Stock") != 0 || r.Field<decimal?>("Wt_Stock") != 0 || r.Field<int?>("Layaway") != 0 || r.Field<decimal?>("Alloc") != 0 || r.Field<decimal?>("On_Memo") != 0 || r.Field<decimal?>("In_Transit") != 0) : true)
                    .OrderBy(r => r["Category"].ToString())
                    .ThenBy(r => r["Subcat"].ToString())
                    .ThenBy(r => r["Group"].ToString())
                    .CopyToDataTable();
                dtFiltered.Columns["Category"].SetOrdinal(0);
                dtFiltered.Columns["Subcat"].SetOrdinal(1);
                dtFiltered.Columns["Group"].SetOrdinal(2);
            }
            // Create dtFinal with the correct structure
            DataTable dtFinal = new DataTable();
            dtFinal.Columns.Add("Vendor", typeof(string));
            dtFinal.Columns.Add("Category", typeof(string));
            if (subCatGroup || isGroup)
                dtFinal.Columns.Add("Subcat", typeof(string));
            if (isGroup)
                dtFinal.Columns.Add("Group", typeof(string));
            dtFinal.Columns.Add("IN_STOCK", typeof(string));
            dtFinal.Columns.Add("In_Stock", typeof(decimal));
            dtFinal.Columns.Add("Wt_Stock", typeof(decimal));
            dtFinal.Columns.Add("Layaway", typeof(int));
            dtFinal.Columns.Add("Alloc", typeof(decimal));
            dtFinal.Columns.Add("On_Memo", typeof(decimal));
            dtFinal.Columns.Add("In_Transit", typeof(decimal));
            dtFinal.Columns.Add("Total_Cost", typeof(decimal));
            if (dtResult.Columns.Contains("TOT_GOLDWT"))
                dtFinal.Columns.Add("TOT_GOLDWT", typeof(decimal));
            if (dtResult.Columns.Contains("TOT_DIAMONDWT"))
                dtFinal.Columns.Add("TOT_DIAMONDWT", typeof(decimal));
            foreach (DataRow row in dtFiltered.Rows)
            {
                DataRow newRow = dtFinal.NewRow();
                newRow["Category"] = row["Category"].ToString();
                if (subCatGroup || isGroup)
                    newRow["Subcat"] = row.Field<string>("Subcat") ?? "";
                if (isGroup)
                    newRow["Group"] = row.Field<string>("Group") ?? "";
                newRow["IN_STOCK"] = row["Category"].ToString();
                newRow["In_Stock"] = row.Field<decimal?>("In_Stock") ?? 0;
                newRow["Wt_Stock"] = row.Field<decimal?>("Wt_Stock") ?? 0;
                newRow["Layaway"] = row.Field<int?>("Layaway") ?? 0;
                newRow["Alloc"] = row.Field<decimal?>("Alloc") ?? 0;
                newRow["On_Memo"] = row.Field<decimal?>("On_Memo") ?? 0;
                newRow["In_Transit"] = row.Field<decimal?>("In_Transit") ?? 0;
                newRow["Total_Cost"] = row.Field<decimal?>("Total_Cost") ?? 0;
                if (dtResult.Columns.Contains("TOT_GOLDWT"))
                    newRow["TOT_GOLDWT"] = row.Field<decimal?>("TOT_GOLDWT") ?? 0;
                if (dtResult.Columns.Contains("TOT_DIAMONDWT"))
                    newRow["TOT_DIAMONDWT"] = row.Field<decimal?>("TOT_DIAMONDWT") ?? 0;
                dtFinal.Rows.Add(newRow);
            }
            return dtFinal;
        }


        public DataTable GetCategoryDetails(string store, bool subCatGroup, bool includeItemsOnMemo, bool noNegativeCheck)
        {
            try
            {
                DataTable dtCatDetails = _helperCommonService.getInventoryCountByCategory(store, subCatGroup, includeItemsOnMemo, noNegativeCheck);

                if (!_helperCommonService.DataTableOK(dtCatDetails))
                {
                    return new DataTable();
                }

                return dtCatDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCategoryDetails: {ex.Message}");
                return new DataTable();
            }
        }

        public DataTable SearchVendors(bool isCreditCard = false, string attribFilter = "1=1", bool chkCCrd = false)
        {
            return _helperCommonService.GetSqlData(@"SELECT ACC,NAME,try_cast(TEL as Nvarchar(30)) as TEL,EMAIL,ADDR11,STATE1,ZIP1,CITY1,TERM,GL_CODE FROM VENDORS with (nolock) WHERE  1 = 1 order by acc");
        }

        public DataTable GetVendorDetails(string store, bool Vendor, bool IncludeItemsOnMemo, bool isDetails = false)
        {
            try
            {
                DataTable dtCatDetails = _helperCommonService.getInventoryCountByVendor(store, Vendor, IncludeItemsOnMemo, isDetails);

                if (!_helperCommonService.DataTableOK(dtCatDetails))
                {
                    return new DataTable();
                }

                return dtCatDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCategoryDetails: {ex.Message}");
                return new DataTable();
            }
        }

    }
}
