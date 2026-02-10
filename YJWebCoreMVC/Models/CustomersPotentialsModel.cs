using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace YJWebCoreMVC.Models
{
    public class CustomersPotentialsModel
    {
        public List<CustomerEventVM> Events { get; set; }
        public List<string> MainEventList { get; set; }

        public string CustomerCode { get; set; }
        public string SalesRep { get; set; }
        public List<SelectListItem> AllSalesReps { get; set; }
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public string FollowUp { get; set; }
        public bool Completed { get; set; }
        public bool Reminder { get; set; }
        public List<CustomersPotentialsModel> Notes { get; set; }
        public List<string> FollowUpTypes { get; set; }


        public string ACC { get; set; }
        public string NAME { get; set; }
        public string ADDR1 { get; set; } = string.Empty;
        public string ADDR12 { get; set; } = string.Empty;
        public string CITY1 { get; set; } = string.Empty;
        public string STATE1 { get; set; } = string.Empty;
        public string ZIP1 { get; set; } = string.Empty;
        public string COUNTRY { get; set; } = string.Empty;
        public string EMAIL { get; set; } = string.Empty;
        public string NOTE1 { get; set; } = string.Empty;
        public string TEL { get; set; } = "0";
        public string FAX { get; set; } = "0";
        public DateTime? EST_DATE { get; set; } = null;
        public string SALESMAN { get; set; } = string.Empty;
        public string DNB { get; set; } = string.Empty;
        public bool IsEditMode { get; set; }
        public string OriginalACC { get; set; } = string.Empty;

        public string message { get; set; }
        public DataTable PotentialCustomerTemplate { get; set; }

        public bool IsImportFromExcel { get; set; }

        public string TemplateName { get; set; }
        public string CopyFromTemplate { get; set; }

        public int? NoOfRowsSkip { get; set; }
        public string ExcelFilePath { get; set; }

        public IEnumerable<string> TemplateList { get; set; }

        public PotentialcustomerImportModel PotentialcustomerImport { get; set; }
        public string Mode { get; set; }

    }

    public class CustomerEventVM
    {
        public string MainEvent { get; set; }
        public string SubEvent { get; set; }
    }
    public class CustomerEventSaveModel
    {
        public string Acc { get; set; }
        public List<CustomerEventVM> Events { get; set; }
    }
    public class NotesRowVM
    {
        public string User { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public string FollowUp { get; set; }
        public bool Completed { get; set; }

    }


    public class PotentialcustomerModel
    {
        public string ACC { get; set; }

        public string NAME { get; set; } = string.Empty;

        public string ADDR1 { get; set; } = string.Empty;

        public string ADDR12 { get; set; } = string.Empty;

        public string CITY1 { get; set; } = string.Empty;

        public string STATE1 { get; set; } = string.Empty;

        public string ZIP1 { get; set; } = string.Empty;

        public string BUYER { get; set; } = string.Empty;

        public string TEL { get; set; } = string.Empty;

        public DateTime? EST_DATE { get; set; } = null;

        public string FAX { get; set; } = string.Empty;

        public string DNB { get; set; } = string.Empty;

        public string JBT { get; set; } = string.Empty;

        public bool? CHANGED { get; set; } = false;

        public string STORE { get; set; } = string.Empty;

        public string SOURCE { get; set; } = string.Empty;

        public string NOTE1 { get; set; } = string.Empty;

        public string NOTE2 { get; set; } = string.Empty;

        public string SALESMAN { get; set; } = string.Empty;

        public string COUNTRY { get; set; } = string.Empty;

        public string EMAIL { get; set; } = string.Empty;

        public string WWW { get; set; } = string.Empty;

    }
    public class PotentialcustomerImportModel
    {
        public string AddEdit { get; set; }

        public string Template_Name { get; set; }

        public string ACC { get; set; } = string.Empty;


        public string NAME { get; set; } = string.Empty;


        public string ADDR1 { get; set; } = string.Empty;


        public string ADDR12 { get; set; } = string.Empty;


        public string CITY1 { get; set; } = string.Empty;


        public string STATE1 { get; set; } = string.Empty;


        public string ZIP1 { get; set; } = string.Empty;


        public string BUYER { get; set; } = string.Empty;


        public string TEL { get; set; } = string.Empty;


        public string EST_DATE { get; set; } = string.Empty;


        public string FAX { get; set; } = string.Empty;


        public string DNB { get; set; } = string.Empty;


        public string JBT { get; set; } = string.Empty;


        public string CHANGED { get; set; } = string.Empty;


        public string STORES { get; set; } = string.Empty;


        public string SOURCE { get; set; } = string.Empty;


        public string NOTE1 { get; set; } = string.Empty;


        public string NOTE2 { get; set; } = string.Empty;


        public string SALESMAN { get; set; } = string.Empty;


        public string COUNTRY { get; set; } = string.Empty;


        public string EMAIL { get; set; } = string.Empty;


        public string WWW { get; set; } = string.Empty;

    }


}
