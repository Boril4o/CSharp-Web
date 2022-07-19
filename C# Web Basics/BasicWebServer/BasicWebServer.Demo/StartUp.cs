using System.Text;
using System.Web;
using BasicWebServer.Server;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.Responses;
using Microsoft.VisualBasic;

namespace BasicWebServer.Demo
{
    public class StartUp
    {
        private const string HtmlForm = @"<form action='/HTML' method='POST'>
Name: <input type='text' name='Name'/>
Age: <input type='number' name='Age'/>
<input type='submit' value='Save'/>
</form>";

        private const string DownloadForm =
            @"<form action='/Content' method='POST'> <input type='submit' value ='Download Sites Content' /> </form>";

        private const string FileName = "content.txt";

        private const string LoginForm = @"<form action='/Login' method='POST'>
Username: <input type='text' name='Username'/>
Password: <input type='text' name='Password'/>
<input type='submit' value ='Log In' /> </form>";

        private const string Username = "user";

        private const string Password = "user123";

        static async Task Main(string[] args)
        {
            await DownloadSitesAsTextFile(FileName,
                new string[] { "https://softuni.org/" });

            var server = new HttpServer(routes => routes
                .MapGet("/", new TextResponse("Hello from the server!"))
                .MapGet("/Redirect", new RedirectResponse("https://softuni.org/"))
                .MapGet("/HTML", new HtmlResponse(StartUp.HtmlForm))
                .MapPost("/HTML", new TextResponse("", StartUp.AddFormDataAction))
                .MapGet("/Content", new HtmlResponse(StartUp.DownloadForm))
                .MapPost("/Content", new TextFileResponse(StartUp.FileName))
                .MapGet("/Cookies", new HtmlResponse("", AddCookiesAction))
                .MapGet("/Session", new TextResponse("", DisplaySessionInfoAction))
                .MapGet("/Login", new HtmlResponse(LoginForm))
                .MapPost("/Login", new HtmlResponse("", LoginAction))
                .MapGet("/Logout", new HtmlResponse("", LogoutAction))
                .MapGet("/UserProfile", new HtmlResponse("", GetUserDataAction)));

            await server.Start();
        }

        private static void GetUserDataAction(Request request, Response response)
        {
            if (request.Session.ContainsKey(Session.SessionUserKey))
            {
                response.Body = $"<h3>Currently logged-in user " +
                                $"is with username '{Username}'</h3>";
            }
            else
            {
                response.Body = "<h3>You should first log in " +
                                "- <a href='/Login'>Login</a></h3>";
            }
        }

        private static void LogoutAction(Request request, Response response)
        {
            request.Session.Clear();

            response.Body = "";
            response.Body = "<h3>Logged out successfully!</h3>";
        }
        
        private static void LoginAction(Request request, Response response)
        {
            var bodyText = "";

            var usernameMatches = request.Form[nameof(Username)] == Username;
            var passwordMatches = request.Form[nameof(Password)] == Password;

            if (usernameMatches && passwordMatches)
            {
                request.Session[Session.SessionUserKey] = "MyUserId";
                request.Session[Session.SessionCurrentDateKey] = DateTime.Now.ToString();
                response.Cookies.Add(Session.SessionCookieName, request.Session.Id);

                bodyText = "<h3>Logged successfully!</h3>";
            }
            else
            {
                bodyText = LoginForm;
            }

            response.Body = "";
            response.Body = bodyText;
        }

        private static void DisplaySessionInfoAction(Request request, Response response)
        {
            var sessionExist = request.Session
                .ContainsKey(Session.SessionCurrentDateKey);

            var bodyText = "";

            if (sessionExist)
            {
                var currentDate = request.Session[Session.SessionCurrentDateKey];
                bodyText = $"Stored data: {currentDate}";
            }
            else
            {
                bodyText = "Current data stored";
            }

            response.Body = "";
            response.Body = bodyText;
        }

        private static void AddCookiesAction(Request request, Response response)
        {
            var requestHasCookies = request
                .Cookies
                .Any(c => c.Name != Session.SessionCookieName);

            var bodyText = "";

            if (requestHasCookies)
            {
                var sb = new StringBuilder();
                sb.AppendLine("<h1>Cookies</h1>");

                sb.Append("<table border='1'><tr><th>Name</th><th>Value</th></tr>");

                foreach (var cookie in request.Cookies)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{HttpUtility.HtmlEncode(cookie.Name)}</td>");
                    sb.Append($"<td>{HttpUtility.HtmlEncode(cookie.Value)}</td>");
                    sb.Append("</tr>");
                }

                sb.Append("</table>");

                bodyText = sb.ToString();
            }
            else
            {
                bodyText = "<h1>Cookies set!</h1>";

                response.Cookies.Add("My-Cookie", "My-Value");
                response.Cookies.Add("My-Second-Cookie", "My-Second-Value");
            }

            response.Body = bodyText;
        }

        private static async Task DownloadSitesAsTextFile(string fileName, params string[] urls)
        {
            var downloads = new List<Task<string>>();

            foreach (var url in urls)
            {
                downloads.Add(DownloadWebSiteContent(url));
            }

            var responses = await Task.WhenAll(downloads);

            var responsesString = string
                .Join(Environment.NewLine + new string('-', 100), responses);

            await File.WriteAllTextAsync(fileName, responsesString);
        }

        private static async Task<string> DownloadWebSiteContent(string url)
        {
            var httpClient = new HttpClient();

            using (httpClient)
            {
                var response = await httpClient.GetAsync(url);
                var html = await response.Content.ReadAsStringAsync();

                return html.Substring(0, 2000);
            }

        }

        private static void AddFormDataAction(Request request, Response response)
        {
            response.Body = "";

            foreach (var (key, value) in request.Form)
            {
                response.Body += $"{key} - {value}";
                response.Body += Environment.NewLine;
            }
        }
    }
}