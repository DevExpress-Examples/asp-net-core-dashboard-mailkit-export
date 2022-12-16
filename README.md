<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/571490634/22.1.6%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1130228)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Dashboard for ASP.NET Core - How to Use MailKit to Send a Dashboard as a Document in PDF

This example demonstrates how to email an exported Dashboard document (PDF in this example) with the [MailKit](https://github.com/jstedfast/MailKit) cross-platform mail client library. 

![Exported Dashboard](images/screenshot.png)

Handle the [ViewerApiExtension.onDashboardTitleToolbarUpdated](https://docs.devexpress.com/Dashboard/js-DevExpress.Dashboard.ViewerApiExtensionOptions#js_devexpress_dashboard_viewerapiextensionoptions_ondashboardtitletoolbarupdated) event to add a custom **Email** button to the [Dashboard Title](https://docs.devexpress.com/Dashboard/117383/web-dashboard/ui-elements-and-customization/ui-elements/dashboard-title). Once a user clicks the button, an **$.ajax** POST request is sent to the server to pass the dashboard's ID and [State](https://docs.devexpress.com/Dashboard/119997/web-dashboard/aspnet-core-dashboard-control/manage-dashboard-state) from the client.

On the server side, the **IndexModel.OnPostEmail** method is called to accept the callback and its parameters. The [DashboardExporter](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardExporter) class instance exports a corresponding  dashboard to a PDF document and sends it as an attachment along with the **MimeMessage** object from the [MailKit](https://github.com/jstedfast/MailKit) library.

> **Note**:
>  The test [Ethereal Email](https://ethereal.email/) SMTP server is used in this example. You can replace the authentication parameters passed to the **SmtpClient.Connect** and **SmtpClient.Authenticate** methods with parameters that correspond to your email server.

## Files to Review

* [DashboardConfig.cs](./CS/Program.cs)
* [Index.cshtml](./CS/Pages/Index.cshtml)
* [Index.cshtml.cs](./CS/Pages/Index.cshtml.cs)

## Documentation

- [DashboardExporter](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardExporter)
- [Dashboard Title](https://docs.devexpress.com/Dashboard/117383/web-dashboard/ui-elements-and-customization/ui-elements/dashboard-title)
- [Manage Dashboard State in ASP.NET Core Applications](https://docs.devexpress.com/Dashboard/119997/web-dashboard/aspnet-core-dashboard-control/manage-dashboard-state)

## More Examples

- [Dashboard for MVC - How to implement server-side export](https://github.com/DevExpress-Examples/asp-net-mvc-dashboard-implement-server-side-export)
- [BI Dashboard - How to Use MailKit to Send a Dashboard as a Document in PDF](https://github.com/DevExpress-Examples/bi-dashboard-mailkit-export)
- [BI Dashboard - How to Email a Dashboard that Displays Different Data Depending on the Addressee](https://github.com/DevExpress-Examples/bi-dashboard-mailkit-export-console-app)
