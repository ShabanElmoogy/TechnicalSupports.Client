using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using TechnicalSupport.Client.Core.Services.Dashboard;
using TechnicalSupport.Client.Core.Services.ExportFilesService;
using TechnicalSupport.Client.Core.Services.LocalStorageService;
using TechnicalSupport.Client.Core.StateManagement.StateContainer;

namespace TechnicalSupport.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddRadzenComponents();

        builder.Services.AddCascadingAuthenticationState();

        builder.Services.AddLocalization();
        string[] supportedLangusges = ["en", "ar"];


        //Must Singleton To Be Shared Between All Components
        builder.Services.AddSingleton<StateContainer>();
        builder.Services.AddScoped<ExportListService>();
        builder.Services.AddScoped<ExportHelper>();
        builder.Services.AddScoped<CultureService>();
        builder.Services.AddScoped<PhotoService>();
        builder.Services.AddScoped<RadzenGridFunction>();

        builder.Services.AddScoped<IDashboardDataService, DashboardDataService>();
        builder.Services.AddScoped<IDashboardSignalRService, DashboardSignalRService>();
        builder.Services.AddScoped<IDashboardNavigationService, DashboardNavigationService>();
        builder.Services.AddScoped<IDashboardNotificationService, DashboardNotificationService>();

        builder.Services.AddPWAUpdater();
        builder.Services.AddHotKeys2();

        builder.Services.AddTransient<AuthenticationHandler>();

        builder.Services.AddHttpClient("ApiClient", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiSettings:ApiAddress").Value!);
            client.DefaultRequestHeaders.Add("clientCulture", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

        }).AddHttpMessageHandler<AuthenticationHandler>();

        builder.Services.AddHttpClient("ReportClient", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiSettings:ReportApiAddress").Value!);
            client.DefaultRequestHeaders.Add("clientCulture", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

        });

        //builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("ApiSettings:ReportApiAddress").Value!) });

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddScoped(typeof(MainService<>));

        builder.Services.AddScoped(typeof(IMainService<>), typeof(MainService<>));


        builder.Services.AddScoped<AppAuthenticationStateProviderFactory>();
        builder.Services.AddScoped<AuthenticationStateProvider, AppAuthenticationStateProviderFactory>();
        builder.Services.AddScoped<IAuthenticationServiceFactory, AuthenticationServiceFactory>();

        builder.Services.AddScoped<UserInfoService>();

        builder.Services.AddScoped<ILocalizationService, LocalizationService>();
        builder.Services.AddScoped<IStorageService, StorageService>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<IAccountService,AccountService>();
        builder.Services.AddScoped<IExportFilesService, ExportFilesService>();
        builder.Services.AddTransient<GlobalFunctions>();

        static void RegisterPermissionClaims(AuthorizationOptions options)
        {
            // Get all public static fields of the Permissions class
            var fields = typeof(Permissions).GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var field in fields)
            {
                var propertyValue = field.GetValue(null);
                if (propertyValue is not null)
                {
                    options.AddPolicy(propertyValue.ToString(), policy => policy.RequireClaim(Permissions.Type, propertyValue.ToString()));
                }
            }
        }

        builder.Services.AddAuthorizationCore(options =>
        {
            RegisterPermissionClaims(options);
        });

        builder.Services.AddI18nText();
        builder.Services.AddBlazoredLocalStorage();

        builder.Services.AddRadzenCookieThemeService(options =>
        {
            options.Name = "MyApplicationTheme"; // The name of the cookie
            options.Duration = TimeSpan.FromDays(365); // The duration of the cookie
        });

        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        builder.Services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        var host = builder.Build();
        
        await host.SetDefaultCulture();
        
        await host.RunAsync();

        await builder.Build().RunAsync();
    }
}