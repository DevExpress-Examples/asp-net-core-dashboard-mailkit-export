# Dashboard for ASP.NET Core - How to send a email with the exported Dashboard document using MailKit

This example demonstrates how to email the exported Dashboard document (PDF in this example) with the [MailKit](https://github.com/jstedfast/MailKit) cross-platform mail client library. In this example we handle the [ViewerApiExtension.onDashboardTitleToolbarUpdated](https://docs.devexpress.com/Dashboard/js-DevExpress.Dashboard.ViewerApiExtensionOptions#js_devexpress_dashboard_viewerapiextensionoptions_ondashboardtitletoolbarupdated) event to add a custom **Email** button to the [Dashboard Title](https://docs.devexpress.com/Dashboard/117383/web-dashboard/ui-elements-and-customization/ui-elements/dashboard-title). Once the the end-user clicks the button we send and **$.ajax** POST request to the server using the technique from the following code example to pass the dashboard's ID and [State](https://docs.devexpress.com/Dashboard/119997/web-dashboard/aspnet-core-dashboard-control/manage-dashboard-state) from the client side to the server: [Dashboard for MVC - How to implement server-side export](https://github.com/DevExpress-Examples/asp-net-mvc-dashboard-implement-server-side-export).

On the server side we accept the callback and its parameters in the **IndexModel.OnPostEmail** method. There we use the [DashboardExporter](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardExporter) class instance to export the corresponding  dashboard to the PDF document and send it as an attachment along with the **MimeMessage** object from the [MailKit](https://github.com/jstedfast/MailKit) library.

Note that we use the [Ethereal Email](https://ethereal.email/) fake SMTP server to test the email sending. You can replace authentication parameters passed to the **SmtpClient.Connect** and **SmtpClient.Authenticate** methods with parameters that correspond to your email server.

The resulting dashboard looks as follows:

![](images/screenshot.png)

## Files to Look at

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