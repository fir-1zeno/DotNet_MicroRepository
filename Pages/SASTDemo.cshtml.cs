using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SimpleWebApp.Pages
{
    public class SASTDemoModel : PageModel
    {
        public void OnGet()
        {
            // This page demonstrates intentional vulnerabilities for SAST testing
            // DO NOT use these patterns in production code
        }

        // Hard-coded credentials - VULNERABILITY FOR SAST TESTING
        private readonly string dbConnection = "Server=localhost;Database=test;User=admin;Password=Password123!;";
        private readonly string apiKey = "sk-1234567890abcdef1234567890abcdef12345678";
        private readonly string jwtSecret = "my-super-secret-jwt-key-that-is-hardcoded";

        // SQL Injection vulnerability - VULNERABILITY FOR SAST TESTING
        public string GetUserData(string userId)
        {
            string query = "SELECT * FROM Users WHERE UserId = " + userId;
            return ExecuteQuery(query);
        }

        // SQL Injection vulnerability - VULNERABILITY FOR SAST TESTING
        public bool Login(string username, string password)
        {
            string sql = "SELECT COUNT(*) FROM Users WHERE Username = '" + username + 
                        "' AND Password = '" + password + "'";
            return ExecuteScalar(sql) > 0;
        }

        // XSS vulnerability - VULNERABILITY FOR SAST TESTING
        public string DisplayUserInput(string userInput)
        {
            return "<div>User said: " + userInput + "</div>";
        }

        // XSS vulnerability - VULNERABILITY FOR SAST TESTING
        public void ShowComment(string comment) // FIX 1: Changed return type from int to void
        {
            // In ASP.NET Core, we need to use different approach for Response.Write
            // This is still vulnerable as it outputs unencoded content
            HttpContext.Response.ContentType = "text/html";
            HttpContext.Response.WriteAsync("<h3>Comment: " + comment + "</h3>").Wait();
        }

        // Weak hashing - VULNERABILITY FOR SAST TESTING
        public string HashPasswordMD5(string password)
        {
            using (var md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = md5.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // Weak hashing - VULNERABILITY FOR SAST TESTING
        public string HashPasswordSHA1(string password)
        {
            using (var sha1 = SHA1.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha1.ComputeHash(bytes);
                return Convert.ToHexString(hash);
            }
        }

        // Insecure cookie - VULNERABILITY FOR SAST TESTING
        public void SetInsecureCookie(string name, string value)
        {
            Response.Cookies.Append(name, value, new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.Now.AddDays(30),
                // Missing HttpOnly = true - VULNERABILITY
                // Missing Secure = true - VULNERABILITY
            });
        }

        public string sensitiveRandom() // FIX 2: Changed return type from void to string
        {
            var random = new Random(); // Sensitive use of Random
            byte[] data = new byte[16];
            random.NextBytes(data);
            return BitConverter.ToString(data); // Check if this value is used for hashing or encryption
        }

        // Insecure cookie - VULNERABILITY FOR SAST TESTING
        public void SetLooseCookie(string name, string value)
        {
            Response.Cookies.Append(name, value, new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.Now.AddYears(1),
                HttpOnly = false,  // VULNERABILITY
                Secure = false,    // VULNERABILITY
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None  // VULNERABILITY
            });
        }

        // Hard-coded IP addresses - VULNERABILITY FOR SAST TESTING
        private readonly string databaseServer = "192.168.1.100";
        private readonly string apiEndpoint = "http://10.0.0.50:8080/api";
        private readonly string backupServer = "172.16.0.25";
        private readonly string localService = "http://127.0.0.1:3000";
        private readonly string internalAPI = "http://0.0.0.0:5000";

        // Debug features - VULNERABILITY FOR SAST TESTING
        public void ProcessData(string data)
        {
            Console.WriteLine("DEBUG: Processing data: " + data);  // VULNERABILITY
            System.Diagnostics.Debug.WriteLine("Processing: " + data);  // VULNERABILITY
            
            try
            {
                // Some processing
            }
            catch (Exception ex)
            {
                // In ASP.NET Core, we need to use different approach for Response.Write
                // This is still vulnerable as it exposes stack trace
                HttpContext.Response.ContentType = "text/html";
                HttpContext.Response.WriteAsync("Error: " + ex.StackTrace).Wait();  // VULNERABILITY
            }
        }

        // Helper methods (simplified for demo)
        private string ExecuteQuery(string query)
        {
            // Simulated query execution
            return "Query executed: " + query;
        }

        private int ExecuteScalar(string query)
        {
            // Simulated scalar execution
            return 1;
        }
    }
}
