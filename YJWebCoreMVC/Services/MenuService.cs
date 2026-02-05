//-- Neetha    05/06/2025 Added OnClick property.
// venkat 01/07/2026 added AskPassword

using System.Xml;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using YJWebCoreMVC.Models;

public interface IMenuService
{
    List<MenuModel> GenerateMenu();
}

namespace YJWebCoreMVC.Services
{
    public class MenuService : IMenuService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly List<MenuModel> menuModels = new();

        public MenuService(
            IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<MenuModel> GenerateMenu()
        {
            menuModels.Clear();

            var xmlPath = Path.Combine(_env.ContentRootPath, "Resources", "menu.xml");

            var doc = new XmlDocument();
            doc.Load(xmlPath);

            var root = doc.DocumentElement;

            foreach (XmlNode node in root.ChildNodes)
            {
                if (!CheckNodeModuleAccess(node))
                    continue;

                AddMenuItem(node, 0);

                if (node.HasChildNodes)
                    GenerateRibbonBarGroups(node, Convert.ToInt32(node.Attributes["Name"].Value));
            }

            return menuModels;
        }
        private void AddMenuItem(XmlNode node, int parentId)
        {
            menuModels.Add(new MenuModel
            {
                MenuId = Convert.ToInt32(GetAttribute(node, "Name")),
                ParentMenuId = parentId,
                Title = GetAttribute(node, "Text").Replace(":", "<br>"),
                weblink = GetAttribute(node, "weblink"),
                OnClick = GetAttribute(node, "OnClick"),
                ImageIcon = GetAttribute(node, "icon"),
                ImageName = GetImage(node)
            });
        }

        private void GenerateRibbonBarGroups(XmlNode rootNode, int parentId)
        {
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (!CheckNodeModuleAccess(node))
                    continue;

                AddMenuItem(node, parentId);

                if (node.HasChildNodes)
                    GenerateRibbonBarGroupsItems(node, Convert.ToInt32(node.Attributes["Name"].Value));
            }
        }

        private void GenerateRibbonBarGroupsItems(XmlNode rootNode, int parentId)
        {
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (!CheckNodeModuleAccess(node))
                    continue;

                AddMenuItem(node, parentId);

                if (node.HasChildNodes)
                    AddRibbonbarGrpItems(node, Convert.ToInt32(node.Attributes["Name"].Value));
            }
        }

        private void AddRibbonbarGrpItems(XmlNode rootNode, int parentId)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var companyName = session?.GetString("COMPANYNAME");

            if (string.IsNullOrEmpty(companyName))
                return;

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (!CheckNodeModuleAccess(node))
                    continue;

                menuModels.Add(new MenuModel
                {
                    MenuId = Convert.ToInt32(node.Attributes["Name"]?.Value ?? "0"),
                    ParentMenuId = parentId,
                    Title = GetAttribute(node, "Text"),
                    weblink = GetAttribute(node, "weblink"),
                    OnClick = node.Attributes["OnClick"]?.Value ?? "#",
                    AskPassword = string.Equals(
                        node.Attributes["askpassword"]?.Value,
                        "yes",
                        StringComparison.OrdinalIgnoreCase)
                });
            }
        }

        private string GetImage(XmlNode node)
        {
            var fileName = node.Attributes["image"]?.Value + ".png";
            var path = Path.Combine(_env.WebRootPath, "images", fileName);

            return File.Exists(path) ? fileName : "";
        }

        private bool CheckNodeModuleAccess(XmlNode node)
        {

            //if (node.Attributes["UserLevel"] != null)
            //{
            //    int menuLevel;
            //    if (int.TryParse(node.Attributes["UserLevel"].Value, out menuLevel))
            //    {
            //        if (Helper.LoggedUserLevel < menuLevel)
            //            return false;
            //    }
            //}

            //if (node.Attributes["ShowOnlyFor"] != null)
            //{
            //    string value = node.Attributes["ShowOnlyFor"].Value;
            //    object sessionValue = null;

            //    if (HttpContext.Current != null && HttpContext.Current.Session != null)
            //        sessionValue = HttpContext.Current.Session[value];

            //    if (sessionValue is bool)
            //    {
            //        if (!(bool)sessionValue)
            //            return false;
            //    }
            //    else if (Enum.IsDefined(typeof(Helper.Modules), value))
            //    {
            //        Helper.Modules module =
            //            (Helper.Modules)Enum.Parse(typeof(Helper.Modules), value, true);

            //        if (!Helper.CheckModuleEnabled(module))
            //            return false;
            //    }
            //    else if (!Helper.GetVariableValue(value))
            //    {
            //        return false;
            //    }
            //}

            //if (node.Attributes["ShowExcept"] != null)
            //{
            //    string value = node.Attributes["ShowExcept"].Value;
            //    object sessionValue = null;

            //    if (HttpContext.Current != null && HttpContext.Current.Session != null)
            //        sessionValue = HttpContext.Current.Session[value];

            //    if (sessionValue is bool)
            //    {
            //        if ((bool)sessionValue)
            //            return false;
            //    }
            //    else if (Enum.IsDefined(typeof(Helper.Modules), value))
            //    {
            //        Helper.Modules module =
            //            (Helper.Modules)Enum.Parse(typeof(Helper.Modules), value, true);

            //        if (Helper.CheckModuleEnabled(module))
            //            return false;
            //    }
            //    else if (Helper.GetVariableValue(value))
            //    {
            //        return false;
            //    }
            //}

            return true;
        }

        private static string GetAttribute(XmlNode node, string name)
        {
            return node.Attributes?
                .Cast<XmlAttribute>()
                .FirstOrDefault(a =>
                    string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase))
                ?.Value ?? "";
        }


    }

}


