using System.Web;
using System.Web.Mvc;

namespace PR020_2019_Vidak_Grujic_Web_Projekat
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
