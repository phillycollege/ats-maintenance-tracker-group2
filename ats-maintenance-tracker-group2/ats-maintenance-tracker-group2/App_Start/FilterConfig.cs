using System.Web;
using System.Web.Mvc;

namespace ats_maintenance_tracker_group2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
