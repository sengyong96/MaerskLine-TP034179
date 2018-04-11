using System.Web;
using System.Web.Mvc;

namespace MaerskLine_TP034179
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
