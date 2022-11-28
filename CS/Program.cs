using DevExpress.DataAccess.Sql;
using DevExpress.AspNetCore;
using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

IFileProvider? fileProvider = builder.Environment.ContentRootFileProvider;
IConfiguration? configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

builder.Services.AddDevExpressControls();
builder.Services.AddScoped<DashboardConfigurator>((IServiceProvider serviceProvider) => {
    DashboardConfigurator configurator = new DashboardConfigurator();

    configurator.SetConnectionStringsProvider(new DashboardConnectionStringsProvider(configuration));
    configurator.SetDashboardStorage(new DashboardFileStorage(fileProvider.GetFileInfo("Data/Dashboards").PhysicalPath));

    DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();

    var dataSourceSQL = new DashboardSqlDataSource("SQL Data Source", "NWindConnectionStringSQL");
    var query = SelectQueryFluentBuilder
        .AddTable("Products")
        .SelectAllColumns()
        .Build("Products");
    dataSourceSQL.Queries.Add(query);

    dataSourceStorage.RegisterDataSource("sqlDataSource", dataSourceSQL.SaveToXml());

    var objDataSource = new DashboardObjectDataSource("Object Data Source") { DataId = "odsSales" };
    dataSourceStorage.RegisterDataSource("objDataSource", objDataSource.SaveToXml());

    configurator.SetDataSourceStorage(dataSourceStorage);

    configurator.DataLoading += (s, e) => {
        if (e.DataId == "odsSales") {
            e.Data = ProductSales.GetProductSales();
        }
    };

    return configurator;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseDevExpressControls();
EndpointRouteBuilderExtension.MapDashboardRoute(app, "api/dashboard", "DefaultDashboard");

app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();