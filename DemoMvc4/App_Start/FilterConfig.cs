using System.Web.Mvc;

// ReSharper disable CheckNamespace
namespace DemoMvc4
// ReSharper restore CheckNamespace
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}