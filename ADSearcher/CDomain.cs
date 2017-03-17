using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSearcher
{
    /* \ \  */
    public class CDomain
    {
        /* \ \ 
           Summary
           This is the "common" name of the domain, like AGI or WMS -
           this should match the domain descriptor at the beginning of
           the user name, like AGI\\rranft.                            */
        public String Name = "";
        /* Summary
           The Active Directory path to use for the domain. For example,
           LDAP://ad.agi.                                                */
        public String Path = "";
        /* Summary
           A CDomainCredentials object containing the user name and
           password with authority to make Active Directory queries in
           the CDomain object's Path. If the Authenticated User whose
           credentials are used to run the application have sufficient
           domain permissions to make Active Directory queries on the
           CDomain object's Path then this can be left blank.          */
        public CDomainCredentials Credentials;
        /* Summary
           The wildcard character is currently * - if a domain Path is
           set to * then searches that use this domain as the criteria
           will search all domains available to the CADSearcher object.
           Returns
           \Returns true if the Path is set to *.                       */
        public bool IsWildcard()
        {
            return Path == "*";
        }
    }

    /* Summary
       User name and password information for connecting to and
       making queries against Active Directory domain controllers. */
    public class CDomainCredentials
    {
        /* \ \ 
           Description
           Domain user name with authority to access and update Active
           Directory entries.                                          */
        public String Username;
        /* \ \ 
           Description
           Domain user password. */
        public String Password;
    }
}
