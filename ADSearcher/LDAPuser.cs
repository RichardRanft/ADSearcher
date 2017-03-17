using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Security.Principal;
using log4net;

namespace ADSearcher
{
    /* \ \ 
       Summary
       The LDAPuser object is for holding data associated with an
       Active Directory user account.                             */
    public class LDAPuser
    {
        private static ILog m_log = LogManager.GetLogger(typeof(LDAPuser));
        /* Description
           This field can be used to limit error and warning logging to
           information status. See the ErrorLevel enumeration.          */
        public ErrorLevel ErrorModifyLevel { get; set; }

        public LDAPuser()
        {
            ErrorModifyLevel = ErrorLevel.ALL;
        }

        public LDAPuser(String userEntry)
        {
            m_path = userEntry;
            Root = userEntry.Substring(0, userEntry.LastIndexOf('/'));
            CN = new List<string>();
            OU = new List<string>();
            DC = new List<string>();
            Properties = new List<KeyValuePair<string, string>>();
            String tempEntry = userEntry.Remove(0, userEntry.LastIndexOf('/') + 1);
            tempEntry = tempEntry.Replace("\\,", "|");
            String[] parts = tempEntry.Split(',');
            foreach (String part in parts)
            {
                if (part.Contains("CN="))
                    CN.Add(part.Replace("|", "\\,"));
                if (part.Contains("OU="))
                    OU.Add(part);
                if (part.Contains("DC="))
                    DC.Add(part);
            }

            ErrorModifyLevel = ErrorLevel.ALL;
        }

        private string m_username;
        private string m_email;
        private string m_path;

        public String Root;
        public List<string> OU;
        public List<string> DC;
        public List<string> CN;
        public List<KeyValuePair<string, string>> Properties;
        /* \ \ 
           Description
           The user's Active Directory user name. */
        public string Username { get { return m_username; } set { m_username = value.ToLower(); } }
        /* \ \ 
           Description
           The user's password - when read, returns the password protect
           character for all characters in password. Could be used to
           reset the user's Active Directory password if the application
           executor's permissions were high enough.                      */
        public string Password { get; set; }
        /* \ \ 
           Description
           The user's Active Directory full name entry. */
        public string FullName { get; set; }
        /* \ \ 
           Description
           The user's Active Directory User Principal Name. */
        public string PrincipalName { get; set; }
        /* \ \ 
           Description
           The user's first name. */
        public string FirstName { get; set; }
        /* \ \ 
           Description
           The user's last name. */
        public string LastName { get; set; }
        /* \ \ 
           Description
           The user's primary email address. */
        public string Email { get { return m_email; } set { m_email = value.ToLower(); } }
        /* \ \ 
           Description
           The user's job title/view/description as contained in Active
           Directory.                                                   */
        public string Title { get; set; }
        /* \ \ 
           Description
           The user's full Active Directory "Distinguished Name" entry
           (DN="name\\, user",OU="",DC="").                            */
        public string UserDN { get; set; }
        /* \ \ 
           Description
           A List\<LDAPgroup\> containing all groups the user belongs
           to.                                                        */
        public List<LDAPgroup> Groups { get; set; }
        /* \ \ 
           Description
           A StringCollection containing all of the user's current email
           addresses listed in Active Directory.                         */
        public StringCollection OtherEmails { get; set; }

        /* \ \ 
           Description
           The domain that the user belongs to. */
        public String Domain { get; set; }

        /* \ \ 
           Description
           The user's "Enabled" status in Active Directory - is true if
           the user is "Enabled."                                       */
        public bool Enabled { get; set; }

        /* \ \ 
           Description
           The user's department attribute. */
        public String Department { get; set; }

        public SecurityIdentifier GetSecurityIdentifier()
        {
            String domain = Domain.Replace("ldap://", "");
            domain = domain.Replace("LDAP://", "");
            NTAccount acct = new NTAccount(domain, Username);
            SecurityIdentifier sid = (SecurityIdentifier)acct.Translate(typeof(SecurityIdentifier));
            return sid;
        }

        public String GetSIDString()
        {
            return GetSecurityIdentifier().ToString();
        }

        private void logMessage(LogType type, String message, Exception ex = null)
        {
            switch (ErrorModifyLevel)
            {
                case ErrorLevel.ALL:
                    break;
                case ErrorLevel.ALLTOINFO:
                    type = LogType.INFO;
                    break;
                case ErrorLevel.ALLTOWARN:
                    type = LogType.WARN;
                    break;
                case ErrorLevel.ERRORTOWARN:
                    if (type > LogType.WARN)
                        type = LogType.WARN;
                    break;
            }

            switch (type)
            {
                case LogType.INFO:
                    if (ex != null)
                        m_log.Info(message, ex);
                    else
                        m_log.Info(message);
                    break;
                case LogType.WARN:
                    if (ex != null)
                        m_log.Warn(message, ex);
                    else
                        m_log.Warn(message);
                    break;
                case LogType.ERROR:
                    if (ex != null)
                        m_log.Error(message, ex);
                    else
                        m_log.Error(message);
                    break;
            }
        }

        /* \ \ 
           Description
           The user's "Enabled" status in Active Directory. This is true
           if the user is NOT "Enabled."                                 */
        public bool Disabled { get; set; }

        public bool OwnsEmail(string email)
        {
            email = email.ToLower().Trim();
            if (m_email == email.ToLower()) return true;
            if (OtherEmails.Contains(email.ToLower())) return true;

            return false;
        }
    }
}
