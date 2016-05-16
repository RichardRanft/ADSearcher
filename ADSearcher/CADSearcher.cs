using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.IO;
using log4net;

/* \ \ 
   Summary
   ADSearcher is a set of tools for managing various Active
   Directory tasks.
   
   
   Remarks
   Breaking changes in 1.3.0.0 require initialization of domains
   prior to using any search functionality.
   
   
   C# Syntax
               // initialize the searcher's domain list.
               CDomainCredentials cred = new CDomainCredentials();
               cred.Username = "USERNAME";
               cred.Password = "PASSW0RD";
   
               CDomain wms = new CDomain();
               wms.Path = "LDAP://wms.com";
               wms.Credentials = cred;
               m_search.Domains.Add(wms);
   
               CDomain agi = new CDomain();
               agi.Path = "LDAP://ad.agi";
               agi.Credentials = new CDomainCredentials();
               m_search.Domains.Add(agi);
   
               CDomain scigames = new CDomain();
               scigames.Path = "LDAP://scientificgames.com";
               scigames.Credentials = new CDomainCredentials();
               m_search.Domains.Add(scigames);
   
               CDomain sga = new CDomain();
               sga.Path = "LDAP://amer.scientificgames.com";
               sga.Credentials = new CDomainCredentials();
               m_search.Domains.Add(sga);                          */
namespace ADSearcher
{
    /* \ \ 
       Summary
       The CADSearcher object provides an interface for searching
       and updating LDAP directory information for users and groups. */
    public class CADSearcher
    {
        private static ILog m_log = LogManager.GetLogger(typeof(CADSearcher));
        public bool Dump { get; set; }
        public bool FieldsOnly { get; set; }
        public bool LimitProperties { get; set; }
        public bool AllowIncompleteUserdata { get; set; }
        public ErrorLevel ErrorModifyLevel { get; set; }
        public List<CDomain> Domains;
        const int ADS_UF_ACCOUNTDISABLE = 0x00000002;

        private DomainDir.DomainListServiceSoapClient m_client01;
        private DomainDir.DomainListServiceSoapClient m_client02;
        private DomainDir.DomainListServiceSoapClient m_client03;

        public CADSearcher()
        {
            Dump = false;
            FieldsOnly = true;
            LimitProperties = true;
            AllowIncompleteUserdata = false;
            ErrorModifyLevel = ErrorLevel.ALL;
            Domains = new List<CDomain>();
        }

        public bool InitializeFromDomainService()
        {
            m_client01 = new DomainDir.DomainListServiceSoapClient("DomainListServiceSoap", "http://scmweb01.ad.agi/domaindirectory/domainlist.asmx");
            m_client02 = new DomainDir.DomainListServiceSoapClient("DomainListServiceSoap", "http://scmweb02.ad.agi/domaindirectory/domainlist.asmx");
            m_client03 = new DomainDir.DomainListServiceSoapClient("DomainListServiceSoap", "http://scmweb03.engscm.ad.agi/domaindirectory/domainlist.asmx");

            String domainDir = m_client01.GetDomainDirectory();
            parseDomainEntries(domainDir);
            domainDir = m_client02.GetDomainDirectory();
            parseDomainEntries(domainDir);
            domainDir = m_client03.GetDomainDirectory();
            parseDomainEntries(domainDir);

            return true;
        }

        private void parseDomainEntries(String data)
        {
            String[] domainEntries = data.Split(';');
            foreach(String entry in domainEntries)
            {
                if (String.IsNullOrEmpty(entry))
                    continue;
                String[] parts = entry.Split(',');
                CDomain dom = new CDomain();
                CDomainCredentials cred = new CDomainCredentials();
                dom.Path = parts[0];
                dom.Credentials = cred;
                if (parts.Length > 1)
                {
                    if (!String.IsNullOrEmpty(parts[1]))
                        dom.Credentials.Username = parts[1];
                    if (!String.IsNullOrEmpty(parts[2]))
                        dom.Credentials.Password = parts[2];
                }
                bool found = false;
                foreach (CDomain d in Domains)
                {
                    if(d.Path.CompareTo(dom.Path) == 0)
                    {
                        found = true;
                        break;
                    }
                }
                if(!found)
                    Domains.Add(dom);
            }
        }

        private void logMessage(LogType type, String message, Exception ex = null)
        {
            switch(ErrorModifyLevel)
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

            switch(type)
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
           This method will try to create a query string from the
           provided string object and use it to locate a user that
           matches.
           Parameters
           inputString :  A string containing a user name, an email
                          address, or a valid query string as detailed <extlink https://msdn.microsoft.com/en-us/library/aa746475(v=vs.85).aspx>here</extlink>.
           Returns
           A List\<<link ADSearcher.LDAPuser, LDAPuser>\> of users
           matching the search criteria.                                                                                                                        */
        public List<LDAPuser> getUserDetailsFromAD(string inputString)
        {
            inputString = inputString.Trim();
            if (string.IsNullOrEmpty(inputString)) return null;
            // Determine whether input string is a username / email / first last / last first / last, first 

            // SearchADUsingLDAPQuery(string query, out string username, out string email, out string first, out string last, out string fullname)
            // Pass search pattern by email
            List<LDAPuser> target;

            if (inputString.Contains("@"))
            {
                string filter = string.Format("(&(mail={0}))", inputString);
                target = SearchADUsingLDAPQuery(filter);
            }

            else if (inputString.Contains(","))
            {
                string first = inputString.Split(',')[1].Trim();
                string last = inputString.Split(',')[0].Trim();
                string filter = string.Format("(&(displayName={0})(sn={1}))", first, last);
                target = SearchADUsingLDAPQuery(filter);
            }
            else if (!inputString.Contains(" "))
            {

                // Try by Alias first 
                string filter = "(SAMAccountName=" + inputString + ")";
                target = SearchADUsingLDAPQuery(filter);
                // Wild goose chase for format of name
            }
            else
            {
                // Then by first last
                string first = inputString.Split(' ')[0];
                string last = inputString.Split(' ')[1];
                string filter = string.Format("(&(displayName={0})(sn={1}))", first, last);
                target = SearchADUsingLDAPQuery(filter);

                if (target != null) return target;

                // if no cigar, try last first
                string x = first;
                first = last;
                last = x;
                filter = string.Format("(&(displayName={0})(sn={1}))", last, first);
                target = SearchADUsingLDAPQuery(filter);
            }

            return target;
        }

        /* Remarks
           This can be pretty slow since it pulls all properties for a
           found object. If it actually finds multiple objects it will
           get all properties for all of them.
           
           The Fields property is a Dictionary\<String, object\>. You
           may need to figure out if it's a simple List\<String\> or
           something else to properly consume the field value.
           Summary
           QueryLDAP gets a list of "raw" objects based on a completely
           user-defined query. Unlike SearchADUsingLDAPQuery(), this
           query takes the entire query as the parameter - nothing is
           added. It returns a list of <link ADSearcher.LDAPRawObject, LDAPRawObjects>,
           which are just wrappers for the properties returned on the
           SearchResult object. This query is made without filtering for
           specific properties - it will return all properties on a
           successful search.
           
           Because the LDAPRawObject.Fields property is a Dictionary\<\>,
           the field names are case sensitive. To simplify things the
           field names returned from the LDAP query are forced to lower
           case, so accessing these fields can be done like so:
           
           <c>username = myLDAPObj.Fields["samaccountname"];</c>
           Returns
           A List\<LDAPRawObject\> containing the results of the search.                */
        public List<LDAPRawObject> QueryLDAP(string query)
        {
            List<LDAPRawObject> ldapObjects = new List<LDAPRawObject>();

            try
            {
                foreach (CDomain domain in Domains)
                {
                    DirectoryEntry de = new DirectoryEntry(domain.Path);
                    if(!String.IsNullOrEmpty(domain.Credentials.Username))
                    {
                        de.Username = domain.Credentials.Username;
                        de.Password = domain.Credentials.Password;
                    }
                    DirectorySearcher ds = new DirectorySearcher(de);

                    ds.Filter = query;
                    SearchResult rs = ds.FindOne();

                    if (rs != null)
                        ldapObjects.Add(new LDAPRawObject(rs));
                }
            }
            catch (Exception e)
            {
                logMessage(LogType.ERROR, "Query failed : " + query, e);
            }

            return ldapObjects;
        }

        /* \ \ 
           Summary
           Searches the Active Directory using
           "(&amp;(&amp;(objectCategory=person)(objectClass=user))" +
           query + ")" as the base query string.
           
           See <extlink https://msdn.microsoft.com/en-us/library/aa746475(v=vs.85).aspx>Search
           Filter Syntax</extlink> on msdn.microsoft.com.
           Parameters
           query :  A subsection of query to add to the base query string
                    (see the Summary).
           
           Returns
           A List\<<link ADSearcher.LDAPuser, LDAPuser>\> of users
           matching the search criteria.                                                       */
        public List<LDAPuser> SearchADUsingLDAPQuery(string query, bool allowIncomplete = false)
        {
            query = "(&(&(objectCategory=person)(objectClass=user))" + query + ")";
            List<LDAPuser> ldapUsers = new List<LDAPuser>();

            try
            {
                foreach (CDomain domain in Domains)
                {
                    DirectoryEntry de = new DirectoryEntry(domain.Path);
                    if (!String.IsNullOrEmpty(domain.Credentials.Username))
                    {
                        de.Username = domain.Credentials.Username;
                        de.Password = domain.Credentials.Password;
                    }
                    DirectorySearcher ds = new DirectorySearcher(de);
                    if (LimitProperties)
                    {
                        ds.PropertiesToLoad.Add("proxyaddresses");
                        ds.PropertiesToLoad.Add("mail");
                        ds.PropertiesToLoad.Add("surname");
                        ds.PropertiesToLoad.Add("displayName");
                        ds.PropertiesToLoad.Add("userprincipalname");
                        ds.PropertiesToLoad.Add("description");
                        ds.PropertiesToLoad.Add("memberOf");
                        ds.PropertiesToLoad.Add("anr");
                        ds.PropertiesToLoad.Add("enabled");
                        ds.PropertiesToLoad.Add("department");
                        ds.PropertiesToLoad.Add("title");
                    }
                    ds.Filter = query;
                    SearchResult rs = ds.FindOne();

                    if (rs != null)
                        ldapUsers.Add(getLDAPUserFromSearch(rs, allowIncomplete));
                }
            }
            catch (Exception e)
            {
                logMessage(LogType.ERROR, "Could not find user by query " + query, e);
            }

            return ldapUsers;
        }

                /* \ \ 
                   Summary
                   Searches the Active Directory using
                   "(&amp;(&amp;(objectCategory=person)(objectClass=user))(samaccountname="
                   \+ username + "))" as the base query string and sets the
                   Active Directory root to one of the following values based on
                   the domain passed in:
                   
                   <code lang="c#">
                   if (domain.ToLower().Equals("agi"))
                      domain = "ad." + domain;
                   if (domain.ToLower().Equals("wms"))
                      domain = domain + ".com";
                   if (domain.ToLower().Equals("scientificgames"))
                      domain = domain + ".com";
                   </code>
                   
                   See <extlink https://msdn.microsoft.com/en-us/library/aa746475(v=vs.85).aspx>Search
                   Filter Syntax</extlink> on msdn.microsoft.com.
                   Parameters
                   username :  The username of the user to retrieve (jdoe)
                   domain :    The domain id (AGI, WMS or scientificgames at the
                               moment) stripped from
                               HttpContext.Current.User.Identity.Name.ToString().
                   
                   Returns
                   A List\<<link ADSearcher.LDAPuser, LDAPuser>\> of users
                   matching the search criteria.                                                       */
        public List<LDAPuser> SearchADUsingLDAPQuery(string username, string domain, bool allowIncomplete = false)
        {
            string query = "(&(&(objectCategory=person)(objectClass=user))(samaccountname=" + username + "))";
            List<LDAPuser> ldapUsers = new List<LDAPuser>();

            try
            {
                foreach (CDomain dom in Domains)
                {
                    DirectoryEntry de = new DirectoryEntry(dom.Path);
                    if(!String.IsNullOrEmpty(dom.Credentials.Username))
                    {
                        de.Username = dom.Credentials.Username;
                        de.Password = dom.Credentials.Password;
                    }
                    DirectorySearcher ds = new DirectorySearcher(de);

                    if (LimitProperties)
                    {
                        ds.PropertiesToLoad.Add("proxyaddresses");
                        ds.PropertiesToLoad.Add("mail");
                        ds.PropertiesToLoad.Add("surname");
                        ds.PropertiesToLoad.Add("displayName");
                        ds.PropertiesToLoad.Add("userprincipalname");
                        ds.PropertiesToLoad.Add("description");
                        ds.PropertiesToLoad.Add("memberOf");
                        ds.PropertiesToLoad.Add("membership");
                        ds.PropertiesToLoad.Add("anr");
                        ds.PropertiesToLoad.Add("enabled");
                        ds.PropertiesToLoad.Add("department");
                        ds.PropertiesToLoad.Add("title");
                    }
                    ds.Filter = query;
                    SearchResult rs = ds.FindOne();

                    if (rs != null)
                        ldapUsers.Add(getLDAPUserFromSearch(rs, allowIncomplete));
                }
            }
            catch (Exception e)
            {
                logMessage(LogType.ERROR, "Could not find user by query " + query, e);
                throw e;
            }

            return ldapUsers;
        }

        /* \ \ 
           Summary
           Checks to see if the specified email address belongs to the
           specified user.
           Parameters
           user :   A LDAPuser object to query against.
           email :  The email address to check.
           
           Returns
           True if the email address belongs to the specified user,
           false if not.                                               */
        public static bool EmailOwnedByAdUser(LDAPuser user, string email)
        {
            email = email.ToLower().Trim();
            if (user.Email == email) return true;
            if (user.OtherEmails.Contains(email)) return true;

            return false;
        }

        /* \ \ 
           Summary
           This private method constructs a LDAPuser object from a
           provided SearchResult object.
           Parameters
           rs :  A SearchResult from an Active Directory query.
           Returns
           A constructed <link ADSearcher.LDAPuser, LDAPuser> object
           containing the user data in the provided SearchResult object. */
        private LDAPuser getLDAPUserFromSearch(SearchResult rs, bool allowIncomplete = false)
        {
            LDAPuser ldapUser = new LDAPuser();
            DirectoryEntry de = rs.GetDirectoryEntry();

            String[] domparts = de.Path.Split('/');
            foreach(String part in domparts)
            {
                Console.WriteLine(part);
            }

            if(Dump)
                dumpValues(rs, domparts[2]);
            
            var user = rs.GetDirectoryEntry().Properties;
            ldapUser.Domain = domparts[0] + "//" + domparts[2];
            if (user["samaccountname"].Value != null)
                ldapUser.Username = user["samaccountname"].Value.ToString().ToLower();
            else if (user["anr"].Value != null)
            {
                ldapUser.Username = user["anr"].Value.ToString().ToLower();
            }
            else
            {
                if (AllowIncompleteUserdata)
                    ldapUser.Username = "";
                else
                {
                    if(!allowIncomplete)
                        throw new Exception("Active Directory user information incomplete.  Missing username.");
                    else
                        ldapUser.Username = "";
                }
            }

            if (user["sn"].Value != null)
                ldapUser.LastName = user["sn"].Value.ToString();
            else
            {
                if (AllowIncompleteUserdata)
                    ldapUser.LastName = "";
                else
                {
                    if (!allowIncomplete)
                        throw new Exception("Active Directory user information incomplete.  Missing last name.");
                    else
                        ldapUser.LastName = "";
                }
            }

            if (user["mail"].Value != null)
                ldapUser.Email = user["mail"].Value.ToString().ToLower();
            else
            {
                if (AllowIncompleteUserdata)
                    ldapUser.Email = "";
                else
                {
                    if (!allowIncomplete)
                        throw new Exception("Active Directory user information incomplete.  Missing email address.");
                    else
                        ldapUser.Email = "";
                }
            }
            
            if (user["proxyaddresses"].Value != null)
            {
                ldapUser.OtherEmails = new StringCollection();
                foreach (string address in user["proxyaddresses"])
                {
                    if (address.StartsWith("smtp:"))
                    {
                        string otherEmail = address.Substring(5).ToLower();
                        ldapUser.OtherEmails.Add(otherEmail);
                    }
                }
            }

            if (user["displayName"].Value != null)
                ldapUser.FullName = user["displayName"].Value.ToString();
            else
            {
                if (AllowIncompleteUserdata)
                    ldapUser.FullName = "";
                else
                {
                    if (!allowIncomplete)
                        throw new Exception("Active Directory user information incomplete.  Missing full name.");
                    else
                        ldapUser.FullName = "";
                }
            }

            if (user["userprincipalname"].Value != null)
                ldapUser.PrincipalName = user["userprincipalname"].Value.ToString();
            else
            {
                ldapUser.PrincipalName = "";
            }

            if (user["description"].Value != null)
                ldapUser.Title = user["description"].Value.ToString();
            else if (user["title"].Value != null)
                ldapUser.Title = user["title"].Value.ToString();
            else
            {
                if (AllowIncompleteUserdata)
                    ldapUser.Title = "";
                else
                {
                    if (!allowIncomplete)
                        throw new Exception("Active Directory user information incomplete.  Missing title/description.");
                    else
                        ldapUser.Title = "";
                }
            }

            if (user["memberOf"].Value != null)
            {
                ldapUser.Groups = new List<LDAPgroup>();
                foreach (string group in user["memberOf"])
                {
                    String root = rs.Path.Substring(0, rs.Path.LastIndexOf('/') + 1);
                    LDAPgroup entry = new LDAPgroup(root + group);
                    ldapUser.Groups.Add(entry);
                }
            }
            else
            {
                if (AllowIncompleteUserdata)
                    ldapUser.Groups = new List<LDAPgroup>();
                else
                {
                    if (!allowIncomplete)
                        throw new Exception("Active Directory user information incomplete.  Missing group membership information.");
                    else
                        ldapUser.Groups = new List<LDAPgroup>();
                }
            }

            if (user["department"].Value != null)
                ldapUser.Department = user["department"].Value.ToString();

            ldapUser.UserDN = user["DistinguishedName"][0].ToString();

            if (user["userAccountControl"].Value != null)
                ldapUser.Disabled = Convert.ToBoolean(Convert.ToInt32(user["userAccountControl"].Value) & ADS_UF_ACCOUNTDISABLE);

            return ldapUser;
        }

        /* \ \ 
         Summary
         This private method constructs a LDAPuser object from a
         provided SearchResult object.
         Parameters
         rs :  A SearchResult from an Active Directory query.
         Returns
         A constructed <link ADSearcher.LDAPuser, LDAPuser> object
         containing the user data in the provided SearchResult object. */
        private LDAPuser getLDAPContactFromSearchResult(SearchResult rs)
        {
            LDAPuser ldapUser = new LDAPuser();
            DirectoryEntry de = rs.GetDirectoryEntry();

            String[] domparts = de.Path.Split('/');
            foreach (String part in domparts)
            {
                Console.WriteLine(part);
            }

            if (Dump)
                dumpValues(rs, domparts[2]);

            var user = rs.GetDirectoryEntry().Properties;
            ldapUser.Domain = domparts[0] + "//" + domparts[2];
          
            if (user["mail"].Value != null)
                ldapUser.Email = user["mail"].Value.ToString().ToLower();
            else
            {
                if (AllowIncompleteUserdata)
                    ldapUser.Email = "";
                else
                    throw new Exception("Active Directory user information incomplete.  Missing email address.");
            }

            if (user["proxyaddresses"].Value != null)
            {
                ldapUser.OtherEmails = new StringCollection();
                foreach (string address in user["proxyaddresses"])
                {
                    if (address.StartsWith("smtp:"))
                    {
                        string otherEmail = address.Substring(5).ToLower();
                        ldapUser.OtherEmails.Add(otherEmail);
                    }
                }
            }

            if (user["displayName"].Value != null)
                ldapUser.FullName = user["displayName"].Value.ToString();
            else
            {
                if (AllowIncompleteUserdata)
                    ldapUser.FullName = "";
                else
                    throw new Exception("Active Directory user information incomplete.  Missing full name.");
            }

            if (user["description"].Value != null)
                ldapUser.Title = user["description"].Value.ToString();
            else if (user["title"].Value != null)
                ldapUser.Title = user["title"].Value.ToString();
            else
            {
                if (AllowIncompleteUserdata)
                    ldapUser.Title = "";
                else
                    throw new Exception("Active Directory user information incomplete.  Missing title/description.");
            }

            if (user["memberOf"].Value != null)
            {
                ldapUser.Groups = new List<LDAPgroup>();
                foreach (string group in user["memberOf"])
                {
                    String root = rs.Path.Substring(0, rs.Path.LastIndexOf('/') + 1);
                    LDAPgroup entry = new LDAPgroup(root + group);
                    ldapUser.Groups.Add(entry);
                }
            }
            else
            {
                if (AllowIncompleteUserdata)
                    ldapUser.Groups = new List<LDAPgroup>();
                else
                    throw new Exception("Active Directory user information incomplete.  Missing group membership information.");
            }

            if (user["department"].Value != null)
                ldapUser.Department = user["department"].Value.ToString();

            ldapUser.UserDN = user["DistinguishedName"][0].ToString();
            return ldapUser;
        }


        private void dumpValues(SearchResult rs, string domain)
        {
            DateTime time = DateTime.Now;
            String timestamp = getTimeString(time);
            using (StreamWriter sr = new StreamWriter("CADSearcher_" + timestamp + "_" + domain + ".txt"))
            {
                foreach (DictionaryEntry item in rs.Properties)
                {
                    sr.WriteLine(item.Key);
                    if (!FieldsOnly)
                    {
                        try
                        {
                            ResultPropertyValueCollection col = (ResultPropertyValueCollection)item.Value;
                            for (int i = 0; i < col.Count; ++i)
                            {
                                sr.WriteLine(" -- " + col[i].ToString());
                            }
                        }
                        catch
                        {
                            sr.WriteLine(" -- " + item.Value.ToString());
                        }
                    }
                }
            }
        }

        private String getTimeString(DateTime time)
        {
            String timestr = time.Year.ToString() + "_" +
                time.Month.ToString("D2") + "_" +
                time.Day.ToString("D2") + "_" +
                time.Hour.ToString("D2") + "_" +
                time.Minute.ToString("D2") + "_" +
                time.Second.ToString("D2") + "_" +
                time.Millisecond.ToString("D4");
            return timestr;
        }

        /* \ \ 
           Summary
           Currently, this gets all distribution groups from the SCM
           Groups group in LDAP://ad.agi.
           Returns
           \Returns a List\<<link ADSearcher.LDAPgroup, LDAPgroup>\> of
           distribution groups in SCM Groups.                           */
        public List<LDAPgroup> GetDistributionGroups(String groupFilter, String root = "LDAP://ad.agi")
        {
            List<LDAPgroup> grouplist = new List<LDAPgroup>();

            DirectoryEntry entry = new DirectoryEntry(root);
            DirectorySearcher srch = new DirectorySearcher(entry);
            srch.SizeLimit = 2500;
            srch.PageSize = 2500;
            srch.Filter = "(&(objectCategory=group)(!groupType:1.2.840.113556.1.4.803:=" + (uint)ActiveDirectoryGroupType.ADS_GROUP_TYPE_DISTRIBUTION_GROUP + "))";
            SearchResultCollection results = srch.FindAll();

            foreach (SearchResult res in results)
            {
                if (res.Properties["DistinguishedName"][0].ToString().Contains(groupFilter))
                {
                    LDAPgroup g = new LDAPgroup(res.Path);
                    if(res.Properties["description"].Count > 0)
                        g.Description = res.Properties["description"][0].ToString();
                    if (res.Properties["mail"].Count > 0)
                    {
                        foreach (String mail in res.Properties["mail"])
                            g.Email.Add(mail);
                    }
                    grouplist.Add(g);
                }
            }
            results.Dispose();
            return grouplist;
        }

        /* \ \ 
           Summary
           This gets all groups in the specified LDAP domain.
           Parameters
           groupFilter :  This will be used to do a string compare
                          against the search results. If any results
                          contain the provided filter that will be added
                          to the list of returned groups.
           root :         The LDAP domain root to search in.
           Returns
           \Returns a List\<LDAPgroup\> of results, or an empty list if
           none are found.                                               */
        public List<LDAPgroup> GetSecurityGroups(String groupFilter, String root = "LDAP://ad.agi")
        {
            if (groupFilter == "")
                groupFilter = "*";
            List<LDAPgroup> grouplist = new List<LDAPgroup>();

            DirectoryEntry entry = new DirectoryEntry(root);
            DirectorySearcher srch = new DirectorySearcher(entry);
            srch.SizeLimit = 2500;
            srch.PageSize = 2500;
            srch.Filter = "(&(objectCategory=group)(groupType:1.2.840.113556.1.4.803:=" + (uint)ActiveDirectoryGroupType.ADS_GROUP_TYPE_DISTRIBUTION_GROUP + "))";
            SearchResultCollection results = srch.FindAll();

            foreach (SearchResult res in results)
            {
                if (res.Properties["DistinguishedName"][0].ToString().Contains(groupFilter))
                {
                    LDAPgroup g = new LDAPgroup(res.Path);
                    if (res.Properties["description"].Count > 0)
                        g.Description = res.Properties["description"][0].ToString();
                    grouplist.Add(g);
                }
            }
            results.Dispose();
            return grouplist;
        }

        /* \ \ 
           Summary
           Gets a collection of groups.
           
           
           Parameters
           groupFilter :  This will be used to do a string compare
                          against the search results. If any results
                          contain the provided filter that will be added
                          to the list of returned groups.
           root :         This is the LDAP domain root to search.
           
           Returns
           \Returns a List\<LDAPgroup\> of results, or an empty list.    */
        public List<LDAPgroup> GetGroups(String groupFilter, String root = "LDAP://ad.agi")
        {
            List<LDAPgroup> grouplist = new List<LDAPgroup>();

            DirectoryEntry entry = new DirectoryEntry(root);
            DirectorySearcher srch = new DirectorySearcher(entry);
            srch.SizeLimit = 2500;
            srch.PageSize = 2500;
            srch.Filter = "(&(objectCategory=group)(CN=" + groupFilter + "))";
            SearchResultCollection results = srch.FindAll();

            foreach (SearchResult res in results)
            {
                LDAPgroup g = new LDAPgroup(res.Path);
                if (res.Properties["description"].Count > 0)
                    g.Description = res.Properties["description"][0].ToString();
                if (res.Properties["mail"].Count > 0)
                {
                    foreach (String mail in res.Properties["mail"])
                        g.Email.Add(mail);
                }
                grouplist.Add(g);
            }
            results.Dispose();
            return grouplist;
        }

        /* \ \ 
           Summary
           Currently, this returns a group from LDAP://ad.agi by name.
           Parameters
           name :  The name of the group to retrieve.
           Returns
           \Returns a <link ADSearcher.LDAPgroup, LDAPgroup> object
           containing the data for the specified group.                */
        public LDAPgroup GetGroup(String name, String root = "LDAP://ad.agi")
        {
            DirectoryEntry entry = new DirectoryEntry(root);
            DirectorySearcher srch = new DirectorySearcher(entry);
            srch.Filter = "(&(objectCategory=group)(CN=" + name + "))";
            SearchResult res = srch.FindOne();
            if(res != null)
            {
                LDAPgroup g = new LDAPgroup(res.Path);
                if (res.Properties["description"].Count > 0)
                    g.Description = res.Properties["description"][0].ToString();
                if (res.Properties["mail"].Count > 0)
                {
                    foreach (String mail in res.Properties["mail"])
                        g.Email.Add(mail);
                }
                return g;
            }

            return new LDAPgroup();
        }

        /* \ \ 
           Description
           Check to see if the specified username is disabled in Active
           Directory.
           
           
           Parameters
           username :  String containing the SMA Account name to check
           
           Returns
           True if the user is disabled, or false if not.               */
        public bool GetUserDisabled(string username)
        {
            // (userAccountControl:1.2.840.113556.1.4.803:=2) - enabled/disabled
            // apparently unreliable - check user ekwan as example.  This shows ekwan as disabled, but AD does not show this.
            List<LDAPRawObject> users = QueryLDAP("(&(objectCategory=person)(objectClass=user)(samaccountname=" + username + ")(userAccountControl:1.2.840.113556.1.4.803:=2))");
            if (users != null && users.Count > 0)
                return true;
            return false;
        }

        /* \ \ 
           Description
           Check to see if the specified username is disabled in Active
           Directory. This overload uses the user's full name to attempt
           to avoid false reports based on users with the same SMA
           Account name but on different domains.
           
           
           Parameters
           username :  The SMA Account name to check
           fullname :  The user's "displayname" property, or full name \-
                       "Doe, John"
           
           Returns
           True if the user is disabled, or false if not.                 */
        public bool GetUserDisabled(string username, string fullname)
        {
            // (userAccountControl:1.2.840.113556.1.4.803:=2) - enabled/disabled
            // apparently unreliable - check user ekwan as example.  This shows ekwan as disabled, but AD does not show this.
            List<LDAPuser> users = SearchADUsingLDAPQuery("(&(&(&(samaccountname=" + username + ")(userAccountControl:1.2.840.113556.1.4.803:=2))(displayName=" + fullname + "))(objectCategory=person))");
            return (!(users == null));
        }

        /* \ \ 

        See <extlink https://msdn.microsoft.com/en-us/library/aa746475(v=vs.85).aspx>Search
        Filter Syntax</extlink> on msdn.microsoft.com.
        Parameters
        username :  The username of the user to retrieve (jdoe)
        domain :    The domain id (AGI, WMS or scientificgames at the
                    moment) stripped from
                    HttpContext.Current.User.Identity.Name.ToString().
                           
        Returns
        A List\<<link ADSearcher.LDAPuser, LDAPuser>\> of users
        matching the search criteria.                                                       */

        public List<LDAPuser> SearchAGIADForContactUsingLDAPQuery(string username, string domain)
        {
            string query = "(&(&(objectCategory=person)(objectClass=user))(samaccountname=" + username + "))";
            List<LDAPuser> ldapUsers = new List<LDAPuser>();
            List<LDAPuser> thisUser = new List<LDAPuser>();
            if (domain.ToLower().Equals("agi"))
                domain = "ad." + domain;
            else
            {
                thisUser = SearchADUsingLDAPQuery(username, domain, true);
                if (thisUser == null || thisUser.Count < 1) return null;

                domain = "ad.agi";
                query = "(mail=" + thisUser[0].Email + ")";

            }
 
            try
            {
                DirectoryEntry de = new DirectoryEntry("LDAP://" + domain);
                DirectorySearcher ds = new DirectorySearcher(de);
                
                if (LimitProperties)
                {
                    ds.PropertiesToLoad.Add("proxyaddresses");
                    ds.PropertiesToLoad.Add("mail");
                    ds.PropertiesToLoad.Add("surname");
                    ds.PropertiesToLoad.Add("displayName");
                    ds.PropertiesToLoad.Add("userprincipalname");
                    ds.PropertiesToLoad.Add("description");
                    ds.PropertiesToLoad.Add("memberOf");
                    ds.PropertiesToLoad.Add("membership");
                    ds.PropertiesToLoad.Add("anr");
                    ds.PropertiesToLoad.Add("enabled");
                    ds.PropertiesToLoad.Add("department");
                    ds.PropertiesToLoad.Add("title");
                    ds.PropertiesToLoad.Add("userAccountControl");
                }
                ds.Filter = query;
                SearchResult rs = ds.FindOne();

                if (rs != null)
                    ldapUsers.Add(getLDAPContactFromSearchResult(rs));
                else
                {
                    if (thisUser.Count < 1)
                        return null;
                    var emails = thisUser[0].OtherEmails.Cast<String>().ToList();
                    foreach (String email in emails)
                    {
                        domain = "ad.agi";
                        query = "(mail=" + email + ")";
                        de = new DirectoryEntry("LDAP://" + domain);
                        ds = new DirectorySearcher(de);

                        if (LimitProperties)
                        {
                            ds.PropertiesToLoad.Add("proxyaddresses");
                            ds.PropertiesToLoad.Add("mail");
                            ds.PropertiesToLoad.Add("surname");
                            ds.PropertiesToLoad.Add("displayName");
                            ds.PropertiesToLoad.Add("userprincipalname");
                            ds.PropertiesToLoad.Add("description");
                            ds.PropertiesToLoad.Add("memberOf");
                            ds.PropertiesToLoad.Add("membership");
                            ds.PropertiesToLoad.Add("anr");
                            ds.PropertiesToLoad.Add("enabled");
                            ds.PropertiesToLoad.Add("department");
                            ds.PropertiesToLoad.Add("title");
                        }
                        ds.Filter = query;
                        rs = ds.FindOne();

                        if (rs != null)
                            ldapUsers.Add(getLDAPContactFromSearchResult(rs));
                    }
                }
            }
            catch (Exception e)
            {
                logMessage(LogType.ERROR, "Could not find user by query " + query, e);
                throw e;
            }

            return ldapUsers;
        }

        public List<LDAPuser> SearchAGIADForContactUsingSID(string SID, string domain)
        {
            String username = "";
            if (domain.ToLower().Equals("agi"))
                domain = "ad." + domain;
            try
            {
                System.Security.Principal.SecurityIdentifier sid = new System.Security.Principal.SecurityIdentifier(SID);
                System.Security.Principal.NTAccount acct = (System.Security.Principal.NTAccount)sid.Translate(typeof(System.Security.Principal.NTAccount));
                
                username = acct.ToString().Split('\\')[1];
            }
            catch { return null; }

            return SearchADUsingLDAPQuery("samaccountname=" + username, true);
        }
    }

    public enum ErrorLevel
    {
        ALL,
        ERRORTOWARN,
        ALLTOWARN,
        ALLTOINFO
    }

    public enum LogType
    {
        INFO,
        WARN,
        ERROR
    }
}
