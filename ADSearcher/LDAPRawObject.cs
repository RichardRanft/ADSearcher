using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.DirectoryServices;
using log4net;

namespace ADSearcher
{
    /* Description
       This object is used to collect results from an arbitrary
       Active Directory query. It is a wrapper for all of the fields
       returned in on a SearchResult object.                         */
    public class LDAPRawObject
    {
        public ErrorLevel ErrorModifyLevel { get; set; }
        private static ILog m_log = LogManager.GetLogger(typeof(LDAPRawObject));

        private Dictionary<String, object> m_fields;

        /* Summary
           The Fields property is a Dictionary\<String, object\> of
           fields returned from on a SearchResult object. All fields
           that have multiple values are consolidated into a single
           comma-separated string.
           
           The keys have all been taken from the property names in the
           SearchResult object and forced to lower case to simplify
           retrieval.                                                  */
        public Dictionary<String, object> Fields
        {
            get { return m_fields; }
        }

        /* Summary
           Creates an empty LDAPRawObject. */
        public LDAPRawObject()
        {
            ErrorModifyLevel = ErrorLevel.ALL;
            m_fields = new Dictionary<String, object>();
        }

        /* Summary
           This constructor creates a new LDAPRawObject and populates it
           with the values of the provided SearchResult object.          */
        public LDAPRawObject(SearchResult res)
        {
            m_fields = new Dictionary<String, object>(); 
            DirectoryEntry de = res.GetDirectoryEntry();
            var obj = res.GetDirectoryEntry().Properties;
            foreach (String prop in obj.PropertyNames)
            {
                m_fields.Add(prop.ToLower(), obj[prop].Value);
            }
        }

        /* Summary
           This function takes a SearchResult object and populates the
           LDAPRawObject's Fields property from it.
           Parameters
           res - A SearchResult object.
           Returns
           void                                                        */
        public void SetFields(SearchResult res)
        {
            m_fields.Clear();
            DirectoryEntry de = res.GetDirectoryEntry();
            var obj = res.GetDirectoryEntry().Properties;
            foreach (String prop in obj.PropertyNames)
            {
                m_fields.Add(prop.ToLower(), obj[prop].Value);
            }
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
    }
}
