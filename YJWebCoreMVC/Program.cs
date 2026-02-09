using YJWebCoreMVC.Services;
using AspNetCore.Reporting;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// REQUIRED for session
builder.Services.AddDistributedMemoryCache();   // 🔴 MISSING
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Required for MenuService
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddSingleton<ConnectionProvider>();
builder.Services.AddScoped<GlobalSettingsService>();
builder.Services.AddScoped<HelperService>();
builder.Services.AddScoped<HelperCommonService>();
builder.Services.AddScoped<HelperPhanindraService>();
builder.Services.AddScoped<HelperDharaniService>();
builder.Services.AddScoped<HelperLokeshService>();
builder.Services.AddScoped<HelperManojService>();
builder.Services.AddScoped<HelperSivaService>();
builder.Services.AddScoped<HelperSravanService>();
builder.Services.AddScoped<HelperVenkatService>();
builder.Services.AddScoped<AnalyticsService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<ICryptoService, CryptoService>();
builder.Services.AddScoped<SalesSummaryService>();
builder.Services.AddScoped<RepairService>();
builder.Services.AddScoped<ListOfItemsSoldService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();        // 🔴 MUST be before Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
