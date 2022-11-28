using System.IO;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using MimeKit.Text;
using System.Drawing;
using DevExpress.DashboardCommon;
using DevExpress.DashboardAspNetCore;

namespace WebDashboardSendEmail.Pages {
    public class IndexModel : PageModel {
        private readonly IWebHostEnvironment _env; 
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment env) {
            _env = env;
            _logger = logger;
        }

        public void OnGet() {

        }

        public IActionResult OnPostEmail(string dashboardID, string dashboardState) {
            var smtp = new SmtpClient();
            var email = new MimeMessage();
            var builder = new BodyBuilder();
            var exporter = new DashboardExporter();
     
            email.From.Add(MailboxAddress.Parse("someone@example.com"));
            email.To.Add(MailboxAddress.Parse("someone@example.com"));
            email.Subject = "Test Email Subject";

            using (var stream = new MemoryStream()) {
                var dashboardLayoutPath = _env.ContentRootFileProvider.GetFileInfo($"Data/Dashboards/{dashboardID}.xml").PhysicalPath;
                var opts = new DashboardPdfExportOptions() { ExportFilters = true };
                var state = new DashboardState();

                state.LoadFromJson(dashboardState);
                exporter.ExportToPdf(dashboardLayoutPath, stream, new Size(1920, 1080), state, opts);
                stream.Seek(0, SeekOrigin.Begin);
                builder.Attachments.Add($"{dashboardID}.pdf", stream.ToArray(), new ContentType("application", "pdf"));
            }

            builder.TextBody = "This is a test e-mail message sent by an application.";
            email.Body = builder.ToMessageBody();

            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls); // See https://ethereal.email
            smtp.Authenticate("ottis.kirlin32@ethereal.email", "m3nP2VEsnwSZWD7jAt");
            smtp.Send(email);
            smtp.Disconnect(true);

            return new JsonResult("OK");
        }
    }
}