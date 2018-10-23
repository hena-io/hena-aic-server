using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Hena;
using Hena.DB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HenaWebsite
{
	public class Startup
	{
		private const string enUSCulture = "en-US";

		private static CultureInfo[] SupportedCultures = new CultureInfo[]
		{
				new CultureInfo("en"),
				new CultureInfo("en-US"),
				new CultureInfo("ko"),
				new CultureInfo("ko-KR")
		};

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			#region Localization
			services.AddLocalization(options => options.ResourcesPath = "Resources");
			services.AddMvc()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
				.AddDataAnnotationsLocalization();
			#endregion   // Localization

			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services
				.AddResponseCaching()
				.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/User/Login/";
				});

			services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
			services.AddResponseCompression(options =>
			{
				options.MimeTypes = new[]
				{
                    // Default
                    "text/plain",
					"text/css",
					"application/javascript",
					"text/html",
					"application/xml",
					"text/xml",
					"application/json",
					"text/json",
                    // Custom
                    "image/svg+xml"
				};
			});


			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			#region Localization
			var localizationOptions = new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en-US"),
				SupportedCultures = SupportedCultures,
				SupportedUICultures = SupportedCultures,
			};

			// Adding our UrlRequestCultureProvider as first object in the list
			localizationOptions.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider
			{
				Options = localizationOptions
			});

			app.UseRequestLocalization(localizationOptions);
			#endregion   // Localization


			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseAuthentication();

			//Accept All HTTP Request Methods from all origins
			app.UseCors(builder =>
				builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

			app.UseMvc(routes =>
			{
				routes.MapRoute(
				name: "cultureRoute",
				template: "{culture}/{controller}/{action}/{id?}",
				defaults: new { controller = "Home", action = "Index" },
				constraints: new
				{
					culture = new RegexRouteConstraint("^[a-z]{2}(?:-[A-Z]{2})?$")
				});

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			// 데이터 경로 설정
			SetupDataDirectory(app, env);

			// 서비스 설정
			SetupService();
		}

		private void Configure_Localization_(IApplicationBuilder app, IHostingEnvironment env)
		{

		}

		private void Configure_Localization_First(IApplicationBuilder app, IHostingEnvironment env)
		{

		}

		private void SetupDataDirectory(IApplicationBuilder app, IHostingEnvironment env)
		{
			string dataRoot = env.ContentRootPath + "_Config";
			if (Directory.Exists(dataRoot))
			{
				AppDomain.CurrentDomain.SetData("DataDirectory", dataRoot);
			}
			else
			{
				var dataDirectory = System.IO.Path.Combine(env.WebRootPath, "App_Data");
				AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
			}
		}

		private bool SetupService()
		{

			NLog.LogManager.GetCurrentClassLogger().Error("Setup Service");
			if (DBThread.Instance.IsActiveService)
				return false;

			string databaseFilePath = WebConfiguration.Instance.DatabaseConfigFilePath;

			if (DBThread.Instance.LoadFromFile(databaseFilePath) == false)
				return false;

			DBThread.Instance.StartService();
			AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

			SetupMachineID();

			VerifyDataManager.Instance.StartService();

			return true;
		}

		private void SetupMachineID()
		{
			Task.WaitAll(Task.Run(async () =>
			{
				var query = new DBQuery_Machine_Select();
				//string port = Configuration["PORT"];
				query.IN.MacAddress = SystemInfomation.MacAddress;
				query.IN.Port = 80;
				bool result = await DBThread.Instance.ReqQueryAsync(query);
				if (result)
				{
					IDGenerator.SetMachineId(query.OUT.MachineId);
				}
			}));
		}

		private void OnProcessExit(object sender, EventArgs e)
		{
			if (DBThread.Instanced != null)
			{
				DBThread.Instanced.StopService();
			}
		}
	}

	public class LanguageRouteConstraint : IRouteConstraint
	{
		public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
		{

			if (!values.ContainsKey("culture"))
				return false;

			var culture = values["culture"].ToString();
			return culture == "en" || culture == "tr";
		}
	}
}
