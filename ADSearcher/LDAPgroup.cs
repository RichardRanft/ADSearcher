using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Security.Principal;
using log4net;

namespace ADSearcher
{

    /* \ \ 
       Summary
       The LDAPgroup object is for holding data associated with an
       Active Directory group.                                     */
    public class LDAPgroup : object
    {
        private String m_path;
        /* \ \ 
           Description
           This field can be used to limit error and warning logging to
           information status. See the ErrorLevel enumeration.          */
        public ErrorLevel ErrorModifyLevel { get; set; }
        private static ILog m_log = LogManager.GetLogger(typeof(LDAPgroup));

        /* \ \ 
           Summary
           This is the Active Directory root path (i.e. LDAP://ad.agi). */
        public String Root { get; set; }

        /* Summary
           The Common Name of this group (CN="this group name"). */
        public String CN { get; set; }

        /* \ \ 
           Summary
           A List\<String\> containing the Organizational Unit entries
           for this group (OU="OrgUnit1")                              */
        public List<String> OU { get; set; }
        /* \ \ 
           Summary
           A List\<String\> containing all of the Domain Controller
           entries for this group (DC="com").                       */
        public List<String> DC { get; set; }

        /* \ \ 
           Summary
           This is the group description. */
        public string Description { get; set; }

        /* \ \ 
           Description
           A list of email addresses that route to members of this
           group.                                                  */
        public List<String> Email { get; set; }

        /* \ \ 
           Summary
           The "empty" constructor for a group creates an uninitialized
           group object.                                                */
        public LDAPgroup()
        {
            m_path = "";
            Root = "";
            CN = "";
            OU = new List<string>();
            DC = new List<string>();
            Description = "";
            Email = new List<string>();
            ErrorModifyLevel = ErrorLevel.ALL;
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
           Summary
           This constructor takes an Active Directory path and
           initializes a new LDAPgroup from it.
           Parameters
           groupEntry :  The full Active Directory name of the group to
                         create.                                        */
        public LDAPgroup(String groupEntry)
        {
            m_path = groupEntry;
            Root = groupEntry.Substring(0, groupEntry.LastIndexOf('/'));
            OU = new List<string>();
            DC = new List<string>();
            groupEntry = groupEntry.Remove(0, groupEntry.LastIndexOf('/') + 1);
            String[] parts = groupEntry.Split(',');
            foreach (String part in parts)
            {
                if (part.Contains("CN="))
                    CN = part;
                if (part.Contains("OU="))
                    OU.Add(part);
                if (part.Contains("DC="))
                    DC.Add(part);
            }
            Description = "";
            Email = new List<string>();
        }

        /* \ \ 
           Summary
           This constructor takes an Active Directory path and
           initializes a new LDAPgroup from it.
           Parameters
           groupEntry :  The full Active Directory name of the group to
                         create.                                        */
        public LDAPgroup(String groupEntry, String description)
        {
            m_path = groupEntry;
            Root = groupEntry.Substring(0, groupEntry.LastIndexOf('/'));
            OU = new List<string>();
            DC = new List<string>();
            groupEntry = groupEntry.Remove(0, groupEntry.LastIndexOf('/') + 1);
            String[] parts = groupEntry.Split(',');
            foreach (String part in parts)
            {
                if (part.Contains("CN="))
                    CN = part;
                if (part.Contains("OU="))
                    OU.Add(part);
                if (part.Contains("DC="))
                    DC.Add(part);
            }
            Description = description;
            Email = new List<string>();
        }

        /* \ \ 
           Summary
           This method adds the requested LDAPuser to this group. The
           permissions used are the same as the context under which the
           application is running (the credentials of the person running
           the application are used).
           Parameters
           user :  The LDAPuser to add to this group.
           
           Returns
           True if successful, false if it fails.                        */
        public bool AddUser(LDAPuser user)
        {
            String groupname = "";
            bool success = true;
            try
            {
                String path = getUserLDAPSource(user);
                DirectoryEntry group = new DirectoryEntry(m_path);
                String sidStr = "";
                if (path.ToLower() != Root.ToLower())
                {
                    sidStr = user.GetSIDString();
                }
                groupname = group.Name;
                String userPath = "";
                if (String.IsNullOrEmpty(sidStr))
                {
                    userPath = path + "/" + user.UserDN;
                    group.Invoke("Add", new object[] { userPath });
                }
                else
                {
                    userPath = "<SID=" + sidStr + ">";
                    foreach (String entry in group.Properties["member"])
                    {
                        // for some reason, system will re-add Foreign Security Principals to groups
                        // without throwing an exception, so let's just catch that case here and
                        // return false if we find them in here.
                        if (containsUser(group, user, sidStr))
                            return false;
                    }
                    group.Properties["member"].Add(userPath);
                }

                group.CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no such object on the server"))
                {
                    success = false;
                }
                else
                {
                    m_log.Info("AddUser(user, userName, password): ", ex);
                    Exception except = new Exception("AddUser() failed: " + user.Username + " to " + groupname, ex);
                    throw except;
                }
            }
            if (!success)
            {
                try
                {
                    String path = getUserLDAPSource(user);
                    DirectoryEntry group = new DirectoryEntry(m_path);
                    groupname = group.Name;
                    String userPath = "";
                    userPath = path + "/" + user.UserDN;
                    foreach (String entry in group.Properties["member"])
                    {
                        if (entry.Contains(user.UserDN))
                            return false;
                    }
                    group.Invoke("Add", new object[] { userPath });

                    group.CommitChanges();
                    return true;
                }
                catch (Exception nEx)
                {
                    m_log.Info("AddUser(user, userName, password): ", nEx);
                    Exception except = new Exception("AddUser() failed: " + user.Username + " to " + groupname, nEx);
                    throw except;
                }
            }
            return success;
        }

        /* \ \ 
           Summary
           This method adds the provided LDAPuser to the group using the
           credentials provided.
           Parameters
           user :      The LDAPuser representing the user to add to this
                       group.
           userName :  The requesting user \- this user will be used to
                       determine permission to add the LDAPuser to this
                       group.
           password :  The password of the requesting user.
           
           Returns
           True if successful, false if it fails.                        */
        public bool AddUser(LDAPuser user, String userName, String password)
        {
            String groupname = "";
            bool success = true;
            try
            {
                String path = getUserLDAPSource(user);
                DirectoryEntry group = new DirectoryEntry(m_path);
                String sidStr = "";
                if(path.ToLower() != Root.ToLower())
                {
                    sidStr = user.GetSIDString();
                }
                groupname = group.Name;
                group.Username = userName;
                group.Password = password;
                String userPath = "";
                if (String.IsNullOrEmpty(sidStr))
                {
                    userPath = path + "/" + user.UserDN;
                    foreach (String entry in group.Properties["member"])
                    {
                        if (entry.Contains(user.UserDN))
                            return false;
                    }
                    group.Invoke("Add", new object[] { userPath });
                }
                else
                {
                    userPath = "<SID=" + sidStr + ">";
                    foreach(String entry in group.Properties["member"])
                    {
                        // for some reason, system will re-add Foreign Security Principals to groups
                        // without throwing an exception, so let's just catch that case here and
                        // return false if we find them in here.
                        if (containsUser(group, user, sidStr))
                            return false;
                    }
                    group.Properties["member"].Add(userPath);
                }

                group.CommitChanges();
                return success;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no such object on the server"))
                {
                    success = false;
                }
                else
                {
                    m_log.Info("AddUser(user, userName, password): ", ex);
                    Exception except = new Exception("AddUser() failed: " + user.Username + " to " + groupname, ex);
                    throw except;
                }
            }
            if (!success)
            {
                try
                {
                    String path = getUserLDAPSource(user);
                    DirectoryEntry group = new DirectoryEntry(m_path);
                    groupname = group.Name;
                    group.Username = userName;
                    group.Password = password;
                    String userPath = "";
                    userPath = path + "/" + user.UserDN;
                    foreach (String entry in group.Properties["member"])
                    {
                        if (entry.Contains(user.UserDN))
                            return false;
                    }
                    group.Invoke("Add", new object[] { userPath });

                    group.CommitChanges();
                    return true;
                }
                catch (Exception nEx)
                {
                    m_log.Info("AddUser(user, userName, password): ", nEx);
                    Exception except = new Exception("AddUser() failed: " + user.Username + " to " + groupname, nEx);
                    throw except;
                }
            }
            return success;
        }

        private void dumpFSP(DirectoryEntry fsp)
        {
            using (StreamWriter sw = new StreamWriter("fspdump.txt"))
            {
                foreach (PropertyValueCollection child in fsp.Properties)
                {
                    foreach (object val in child)
                        sw.WriteLine(string.Format("Name: {0} \t\tValue: {1}", child.PropertyName, val.ToString()));
                }
            }
        }

        /* \ \ 
           Summary
           This method removes the requested LDAPuser from this group.
           Parameters
           user :  The LDAPuser to remove from this group.             */
        public void RemoveUser(LDAPuser user)
        {
            String groupname = "";
            try
            {
                String path = getUserLDAPSource(user);
                DirectoryEntry group = new DirectoryEntry(m_path);
                String sidStr = "";
                if (path.ToLower() != Root.ToLower())
                {
                    sidStr = user.GetSIDString();
                }
                groupname = group.Name;
                String userPath = "";
                if (String.IsNullOrEmpty(sidStr))
                    userPath = path + "/" + user.UserDN;
                else
                    userPath = "<SID=" + sidStr + ">";
                if (group != null)
                {
                    if (containsUser(group, user, sidStr))
                    {
                        group.Invoke("Remove", new object[] { path + "/" + user.UserDN });
                        group.CommitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                m_log.Error("RemoveUser: ", ex);
                Exception except = new Exception("RemoveUser() failed: " + user.Username + " to " + groupname, ex);
                throw except;
            }
        }

        private bool containsUser(DirectoryEntry group, LDAPuser user, String sid = "")
        {
            foreach (String member in group.Properties["member"]) 
            {
                if (member.Contains(user.UserDN))
                    return true;
                if (!String.IsNullOrEmpty(sid) && member.Contains(sid))
                    return true;
            }
            return false;
        }

        /* \ \ 
           Summary
           This method gets the list of member user names for this
           group.
           
           
           Returns
           \Returns a List\<String\> containing all member user names. */
        public List<String> GetGroupMembers()
        {
            List<String> userlist = new List<String>();
            String groupname = "";
            try
            {
                DirectoryEntry group = new DirectoryEntry(m_path);
                groupname = group.Name;
                if (group != null)
                {
                    foreach (String name in group.Properties["member"])
                    {
                        userlist.Add(name);
                    }
                }
            }
            catch (Exception ex)
            {
                m_log.Error("GetGroupMembers: ", ex);
                Exception except = new Exception("GetGroupMembers() failed: " + groupname, ex);
                throw except;
            }
            return userlist;
        }

        /* \ \ 
           Summary
           This method updates the Active Directory description for this
           group.
           
           
           Returns
           A boolean indicating success or failure of the update.      */
        public bool UpdateDescription()
        {
            String groupname = "";
            try
            {
                DirectoryEntry group = new DirectoryEntry(m_path);
                groupname = group.Name;
                group.InvokeSet("description", new object[] { Description });
                group.CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                m_log.Error("UpdateDescription: ", ex);
                Exception except = new Exception("UpdateDescription() failed: " + groupname, ex);
                throw except;
            }
        }

        /* \ \ 
           Summary
           This method updates the Active Directory description for this
           group.
           Parameters
           username :  User name to use for authentication.
           password :  Password to use for authentication.
           
           Returns
           A boolean indicating success or failure of the update.        */
        public bool UpdateDescription(string username, string password)
        {
            String groupname = "";
            try
            {
                DirectoryEntry group = new DirectoryEntry(m_path);
                groupname = group.Name;
                group.Username = username;
                group.Password = password;
                group.InvokeSet("description", new object[] { Description });
                group.CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                m_log.Error("UpdateDescription(username, password): ", ex);
                Exception except = new Exception("GetGroupMembers() failed: " + groupname + ", username=" + username + ", password=" + password, ex);
                throw except;
            }

        }

        /* \ \ 
           Summary
           This method returns the full Active Directory path for this
           group.
           
           
           Returns
           A string containing the fully qualified Active Directory path
           for the group.                                                */
        public String ToPath()
        {
            String path = CN;
            foreach (String ou in OU)
                path += "," + ou;
            foreach (String dc in DC)
                path += "," + dc;

            return path;
        }

        /* \ \ 
           Summary
           This method overloads object::ToString() so that this object
           can display its name in ListBoxes and whatnot instead of its
           type name.                                                   */
        public override String ToString()
        {
            String name = CN == null ? this.ToPath() : CN.Replace("CN=", "");
            return name;
        }

        /* \ \ 
           Summary
           \Returns the path to the LDAP service that the user belongs
           to.
           Parameters
           user :  The LDAPuser to find.
           
           Returns
           The LDAP service path (LDAP://ad.agi for example            */
        private String getUserLDAPSource(LDAPuser user)
        {
            String[] parts = user.Domain.Split('/');
            String sourcePath = "LDAP://" + parts[2];
            return sourcePath;
        }

        private DirectoryEntry getFSPGroup()
        {
            String path = "LDAP://CN=ForeignSecurityPrincipals";
            foreach (String dc in DC)
                path += "," + dc;

            DirectoryEntry de = new DirectoryEntry(path);
            return de;
        }
    }

    /* \ \ 
       Summary
       The ActiveDirectoryGroupType enum is used to hold type
       constants for filtering Active Directory group searches.
       
       This enum effectively duplicates the ADS_GROUP_TYPE_ENUM
       described <extlink https://msdn.microsoft.com/en-us/library/aa772263(v=vs.85).aspx>in
       Microsoft's documentation</extlink>.                                                  */
    public enum ActiveDirectoryGroupType : uint
    {
        ADS_GROUP_TYPE_GLOBAL_GROUP = 0x2, /* \ \ 
           Summary
           Specifies a group that can contain accounts from the same
           domain and other global groups from the same domain. This
           type of group can be exported to a different domain.      */

        ADS_GROUP_TYPE_DOMAIN_LOCAL_GROUP = 0x4,/* \ \ 
                                                   Summary
                                                   Specifies a group that can contain accounts from any domain,
                                                   other domain local groups from the same domain, global groups
                                                   from any domain, and universal groups. This type of group
                                                   should not be included in access-control lists of resources
                                                   in other domains.
                                                   
                                                   This type of group is intended for use with the LDAP
                                                   provider.
                                                                                                                 */
        ADS_GROUP_TYPE_LOCAL_GROUP = 0x4,/* \ \ 
           Summary
           This group has the same value as <link ADSearcher.ActiveDirectoryGroupType.ADS_GROUP_TYPE_DOMAIN_LOCAL_GROUP, ActiveDirectoryGroupType.ADS_GROUP_TYPE_DOMAIN_LOCAL_GROUP Field> */

        ADS_GROUP_TYPE_UNIVERSAL_GROUP = 0x8, /* \ \ 
           Summary
           Specifies a group that can contain accounts from any domain,
           global groups from any domain, and other universal groups.
           This type of group cannot contain domain local groups.       */

        ADS_GROUP_TYPE_SECURITY_ENABLED = 0x80000000, /* \ \ 
           Summary
           Specifies a group that is security enabled. This group can be
           used to apply an access-control list on an ADSI object or a
           \file system.                                                 */
        ADS_GROUP_TYPE_DISTRIBUTION_GROUP = 0x80000000/* \ \ 
           Summary
           See <link ADSearcher.ActiveDirectoryGroupType.ADS_GROUP_TYPE_SECURITY_ENABLED, ActiveDirectoryGroupType.ADS_GROUP_TYPE_SECURITY_ENABLED Field> */

    }
}
