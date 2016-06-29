using System.Web;
using System.Web.Mvc;

namespace Collaborate_lrn_Py
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
