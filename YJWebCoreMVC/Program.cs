// Chakri 02/05/2026  Added EmailSettingsService and EmployeeService.
// venkat 02/05/2026  RapnetService added
// Dharani 02/05/2026 Added ReportService, VendorPoWoCustomerPoService, CustomerReturnsService and MemoServices.
// Dharani 02/06/2026 Added JsonSerializerOptions to keep the propery names to pascal case
// Chakri  02/06/2026 added SalesQuotesWishlistService.
// Sravan  02/06/2026 Added BankAccService,AccountsPayableService,CustomerService,EmployeeService,CheckOneVendorService,CustomerNewService,APCreditViewService
// Manoj   02/06/2026 Added CustomerService,PhysicalInventoryService,GLAcctService,SalesmenService
// Dharani 02/05/2025 Added ReportService, VendorPoWoCustomerPoService, CustomerReturnsService and MemoServices.
// venkat  02/06/2026 Registered ShopifyService , StylesService
// Neetha  02/06/2026 Added AdminMessagesService, AdminMiscService.
// Dharani 02/09/2026 Added CustomerService
// Phanindra 02/09/2026 Added AddRazorRuntimeCompilation, RepairService, ListOfItemsSoldService, SalesmenService, ImageService, RegisterProvider
// Dharani 02/09/2026 Added BankingDepositsService, BankAccService
// Phanindra 02/11/2026 added SalesPaymentsCreditsService, CommonService, SalesLayAwaysService, OrderRepairService

using YJWebCoreMVC.ReportEngine;
using YJWebCoreMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    // This keeps property names exactly as they are in C# (PascalCase)
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
//    .AddRazorRuntimeCompilation();

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
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmailSettingsService>();
builder.Services.AddScoped<SalesmenService>();
builder.Services.AddScoped<SalesPOSReportsService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddScoped<RapnetService, RapnetService>();
builder.Services.AddScoped<YJWebCoreMVC.ReportEngine.ReportService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<VendorPoWoCustomerPoService>();
builder.Services.AddScoped<CustomerReturnsService>();
builder.Services.AddScoped<MemoService>();
builder.Services.AddScoped<GLAcctService>();
builder.Services.AddScoped<PhysicalInventoryService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<BankAccService>();
builder.Services.AddScoped<AccountsPayableService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CashRegisterService>();
builder.Services.AddScoped<AppraisalsService>();
builder.Services.AddScoped<SalesQuotesWishlistService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<CheckOneVendorService>();
builder.Services.AddScoped<CustomerNewService>();
builder.Services.AddScoped<APCreditViewService>();
builder.Services.AddScoped<ShopifyService>();
builder.Services.AddScoped<StylesService>();
builder.Services.AddScoped<AdminMessagesService>();
builder.Services.AddScoped<AdminMiscService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<RepairService>();
builder.Services.AddScoped<ListOfItemsSoldService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<BankingDepositsService>();
builder.Services.AddScoped<BankAccService>();
builder.Services.AddScoped<BankingBankAccountsService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<DefaultAccountsService>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<SalesPaymentsCreditsService>();
builder.Services.AddScoped<CommonService>();
builder.Services.AddScoped<SalesLayAwaysService>();
builder.Services.AddScoped<OrderRepairService>();
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
