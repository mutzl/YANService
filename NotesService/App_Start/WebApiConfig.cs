using System.Web.Http;

namespace NotesService
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{tenantId}/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
