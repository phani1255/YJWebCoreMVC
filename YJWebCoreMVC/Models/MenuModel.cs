namespace YJWebCoreMVC.Models
{
    public class MenuModel
    {
        public int MenuId { get; set; }
        public int? ParentMenuId { get; set; }
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string weblink { get; set; }
        public string ShowExcept { get; set; }
        public string ShowOnlyFor { get; set; }
        public string OnClick { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string ImageIcon { get; set; } = string.Empty;
        public bool AskPassword { get; set; }

    }
}
