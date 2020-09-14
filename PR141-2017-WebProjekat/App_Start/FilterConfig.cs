using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
