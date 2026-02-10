/*
 *  Created By Phanindra on 26-Mar-2025
 *  Chakri 02/05/2026 The code has been separated into the Employee model and service layers.
 *  
 */
using Microsoft.AspNetCore.Mvc.Rendering;

namespace YJWebCoreMVC.Models
{
    public class EmployeeModel
    {
        public string Employee_Code { get; set; }
        public DateTime Date { get; set; }
        public string Vacation_Type { get; set; }
        public string Note { get; set; }

        public string SelectedStore { get; set; }
        public string empCode { get; set; }
        public IEnumerable<SelectListItem> AllStores { get; set; }
        public List<EmployeeModel> updatedVacations { get; set; }

        public string EmployeeCode { get; set; }
        public DateTime PunchDate { get; set; }

        // Grid
        public List<EmployeeModel> PunchRecords { get; set; }

        public string Acc { get; set; }

        public string InTime { get; set; }
        public string OutTime { get; set; }



    }
}