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
        public ErrorLevel ErrorModifyLevel { get; set; }

        public LDAPuser()
        {
            ErrorModifyLevel = ErrorLevel.ALL;
        }

        private string m_username;
        private string m_email;
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

        public String Domain { get; set; }

        public bool Enabled { get; set; }

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
