using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenerationLogBook.Utility
{
    public class UserMenu
    {
        private List<VMenuMaster>  _objMenu;
        private List<string> _AuthList;
        private string _username;
        public UserMenu(string userid)
        {
            GenerationLogBookEntities _context = new GenerationLogBookEntities();
            _username = userid.Substring(0, userid.IndexOf("@"));
            _objMenu = _context.VMenuMasters.Where( x => x.UserId == userid ).ToList();
            _AuthList = _objMenu.Select(x => x.AuthId).ToList();
            _context.Dispose();
        }

        private string getSitesMenu()
        {
            string html = "";
            List<VMenuMaster> distinctStations, distinctRegisters, distinctAuth;
            
            var stationMenu = _objMenu.Where(u => new[] { "ELOS", "SFT", "TEMP" }.Contains(u.ModuleId)).ToList();
            distinctStations = stationMenu.GroupBy(p => new { p.SiteId, p.SiteName }).Select(g => g.First()).ToList();

            if (distinctStations.Count == 0)
                return "";

            html = "<ul class='nav navbar-nav'>\n" +
                  "<li class='dropdown'>\n" + 
                  "<a id = 'dLabel' role='button' data-toggle='dropdown' data-target='#' href='#'>Sites<span class='caret'></span></a>\n" +
                  "<ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'>\n";

            foreach (var x in distinctStations)
            {
                html += "<li class='dropdown-submenu'>\n" +     //Station LI
                        "<a href='#'>" + x.SiteName + "</a>\n";

                distinctRegisters = stationMenu.Where(a => a.SiteId == x.SiteId).GroupBy(p => new { p.ModuleId, p.ModuleName }).Select(g => g.First()).ToList();

                if (distinctRegisters.Count > 0)
                {
                    html += "<ul class='dropdown-menu'>\n";   //Reg UL

                    foreach (var y in distinctRegisters)
                    {
                        html += "<li class='dropdown-submenu'>\n" +         //Reg LI
                                "<a href='#'>" + y.ModuleName + "</a>\n";

                        distinctAuth = stationMenu.Where(a => a.SiteId == x.SiteId && a.ModuleId == y.ModuleId).GroupBy(p => new { p.ControllerAction, p.AuthTextMenu }).Select(g => g.First()).ToList();
                        if (distinctAuth.Count > 0)
                        {
                            html += "<ul class='dropdown-menu'>\n"; //Auth UL
                            foreach (var z in distinctAuth)
                            {
                                html += "<li>\n" + // class='dropdown-submenu'
                                        "<a href='" + z.ControllerAction + "'>" + z.AuthTextMenu + "</a>\n" +
                                        "</li>\n";
                            }
                            html += "</ul>\n";                //Auth UL
                        }
                        html += "</li>\n";      //Reg LI
                    }
                    html += "</ul>\n";   //Reg UL
                }
                html += "</li>\n";   //Station LI
            }
            html += "</ul>\n";
            html += "</li>";
            html += "</ul>";

            return html;
        }
        private string getMstDataMenu()
        {
            string html = "";

            var mstdatMenu = _objMenu.Where(u => new[] { "MSTDMGMT" }.Contains(u.ModuleId)).ToList();

            if (mstdatMenu.Count == 0)
                return "";

            html = "<ul class='nav navbar-nav'>\n" +
                  "<li class='dropdown'>\n" +
                  "<a id = 'dLabel' role='button' data-toggle='dropdown' data-target='#' href='#'>Master Data<span class='caret'></span></a>\n" +
                  "<ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'>\n";

            foreach (var m in mstdatMenu)
            {
                html += "<li>\n" +//class='dropdown-submenu'
                        "<a href='" + m.ControllerAction + "'>" + m.AuthTextMenu + "</a>\n" +
                        "</li>\n";
            }

            html += "</ul>";
            html += "</li>";
            html += "</ul>";

            return html;
        }
        private string getAdminMenu()
        {
            string html = "";
            
            var adminMenu = _objMenu.Where(u => new[] { "USRMGMT" }.Contains(u.ModuleId)).ToList();

            if (adminMenu.Count == 0)
                return "";

            html = "<ul class='nav navbar-nav'>\n" +
                  "<li class='dropdown'>\n" +
                  "<a id = 'dLabel' role='button' data-toggle='dropdown' data-target='#' href='#'>User Admin<span class='caret'></span></a>\n" +
                  "<ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'>\n";

            foreach (var m in adminMenu)
            {
                html += "<li>\n" +//class='dropdown-submenu'
                        "<a href='" + m.ControllerAction + "'>" + m.AuthTextMenu + "</a>\n" +
                        "</li>\n";
            }

            html += "</ul>";
            html += "</li>";
            html += "</ul>";

            return html;
        }
        private string getReportMenu()
        {
            string html = "";

            var rptMenu = _objMenu.Where(u => new[] { "RPT" }.Contains(u.ModuleId)).ToList();
            
            if (rptMenu.Count == 0)
                return "";

            html = "<ul class='nav navbar-nav'>\n" +
                  "<li class='dropdown'>\n" +
                  "<a id = 'dLabel' role='button' data-toggle='dropdown' data-target='#' href='#'>Reports<span class='caret'></span></a>\n" +
                  "<ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'>\n";

            foreach (var m in rptMenu)
            {
                html += "<li>\n" + // class='dropdown-submenu'
                        "<a href='" + m.ControllerAction + "'>" + m.AuthTextMenu + "</a>\n" +
                        "</li>\n";
            }

            html += "</ul>";
            html += "</li>";
            html += "</ul>";

            return html;
        }

        public List<string> getAuthTokens()
        {
            return _AuthList;
        }
        public string getMenuhtml()
        {
            string html = "<ul class='nav navbar-nav navbar-right'>\n" +
                          //"<li><a href = "+ baseurl+"Login/Logoff> Logoff </a></li>\n" +
                          "<li><a href = '/Login/Logoff'> Logoff " + _username + " </a></li>\n" +

                          "</ul>\n" +
                          "{@SitesMenu@}" +
                          "{@SetupMenu@}" +
                          "{@AdminMenu@}" +
                          "{@ReportMenu@}";
            
            html = html.Replace("{@SitesMenu@}", getSitesMenu());
            html = html.Replace("{@SetupMenu@}", getMstDataMenu());
            html = html.Replace("{@AdminMenu@}", getAdminMenu());
            html = html.Replace("{@ReportMenu@}", getReportMenu());

            _objMenu = null;
            return html;
        }




        public List<VMenuMaster> getobjMenu()
        {
            return _objMenu;
        }



    }
}