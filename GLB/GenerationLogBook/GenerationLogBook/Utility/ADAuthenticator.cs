using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices;
using System.Net.Mail;

namespace GenerationLogBook.Utility
{
    public class ADAuthenticator
    {
        public class AuthenticationResult
        {
            public string Status { get; set; }
            public string Text { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public static AuthenticationResult Authenticate(string emailaddress, string password)
        {
            MailAddress objEmail;
            DirectoryEntry objDirEntry;
            DirectorySearcher objDirSearcher;
            SearchResult objSearchResult;
            AuthenticationResult result;
            string strpath;

            if (password == null)
                password = "";

            try
            {
                objEmail = new MailAddress(emailaddress);
                strpath = @"LDAP://" + Domain.GetComputerDomain().ToString().ToLower();
                objDirEntry = new DirectoryEntry(strpath, objEmail.User, password);
                objDirSearcher = new DirectorySearcher(objDirEntry);

                objSearchResult = objDirSearcher.FindOne();

                if (objSearchResult != null)
                {
                    result = new AuthenticationResult { Status = "OK", Text = "Authenticated Successfully", Email = emailaddress, Password = password };
                }
                else
                {
                    result = new AuthenticationResult { Status = "FAILED", Text = "Invalid User Name or Password", Email = emailaddress, Password = password };
                }
            }
            catch (Exception e)
            {
                result = new AuthenticationResult { Status = "FAILED", Text = e.Message, Email = emailaddress, Password = password };
            }

            return result;
        }
    }
}