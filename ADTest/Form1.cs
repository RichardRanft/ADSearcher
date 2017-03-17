using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;

using ADSearcher;

namespace ADTest
{
    public partial class Form1 : Form
    {
        private static ILog m_log = LogManager.GetLogger(typeof(Form1));
        private CADSearcher m_search;
        private LDAPuser m_user;
        private LDAPgroup m_group;

        public Form1()
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
            m_search = new CADSearcher();
            m_search.ErrorModifyLevel = ErrorLevel.ALLTOINFO;
            try
            {
                m_search.InitializeFromDomainService();
            }
            catch
            {
                initADSearchDomains();
            }
            m_log.Info("Starting ADTest...");
        }

        private void initADSearchDomains()
        {
            CDomainCredentials cred = new CDomainCredentials();
            cred.Username = "admin-ldapqueries";
            cred.Password = "d3rping110";

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
            m_search.Domains.Add(sga);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_search != null && textBox1.Text != "")
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox5.Items.Clear();
                listBox6.Items.Clear();
                String[] nameparts = textBox1.Text.Split('\\');
                String domain = "";
                String username = "";
                if (nameparts.Length > 1)
                {
                    domain = nameparts[0];
                    username = nameparts[1];
                }
                else
                    username = nameparts[0];
                try
                {
                    //m_search.Dump = true;
                    m_search.LimitProperties = false;
                    m_search.AllowIncompleteUserdata = true;
                    List<LDAPuser> users = new List<LDAPuser>();
                    if (domain == "")
                    {
                        users = m_search.SearchADUsingLDAPQuery("(samaccountname=" + username + ")");
                    }
                    else
                    {
                        CDomain dom = new CDomain();
                        dom.Path = "*"; // search all available domains
                        users = m_search.SearchADUsingLDAPQuery("(samaccountname=" + username + ")");
                    }

                    if (users.Count < 1)
                    {
                        String query = "(displayname=" + username + ")";
                        users = m_search.SearchADUsingLDAPQuery(query, true);
                        if (users == null || users.Count < 1)
                        {
                            query = "(samaccountname=" + username + ")";
                            users = m_search.SearchADUsingLDAPQuery(query, true);
                        }
                        if (users.Count < 1)
                        {
                            MessageBox.Show("User " + textBox1.Text + " was not found in Active Directory", "User Not Found.");
                            return;
                        }
                    }
                    foreach (LDAPuser user in users)
                    {
                        if (user == null)
                        {
                            MessageBox.Show("User " + textBox1.Text + " returned a null entry from Active Directory", "User Error");
                            continue;
                        }
                        user.Enabled = !m_search.GetUserDisabled(user.Username);
                        pbxUserEnabled.Image = (user.Enabled == true ? ADTest.Properties.Resources.status_up : ADTest.Properties.Resources.status_down);
                        pbxUserDisabled.Image = (user.Disabled == true ? ADTest.Properties.Resources.status_up : ADTest.Properties.Resources.status_down);
                        listBox1.Items.Add(user.Email);
                        foreach (String email in user.OtherEmails)
                        {
                            listBox1.Items.Add(email);
                        }
                        if (!String.IsNullOrEmpty(user.FullName))
                            listBox2.Items.Add(user.FullName);
                        if (!String.IsNullOrEmpty(user.PrincipalName))
                            listBox2.Items.Add(user.PrincipalName);
                        if (!String.IsNullOrEmpty(user.FirstName))
                            listBox3.Items.Add(user.FirstName);
                        if (!String.IsNullOrEmpty(user.LastName))
                            listBox4.Items.Add(user.LastName);
                        if (!String.IsNullOrEmpty(user.Title))
                            listBox5.Items.Add(user.Title);
                        if (!String.IsNullOrEmpty(user.PrincipalName))
                            listBox5.Items.Add("UPN = " + user.PrincipalName);
                        if (user.Groups == null)
                            continue;
                        foreach (LDAPgroup group in user.Groups)
                        {
                            listBox6.Items.Add(group.CN);
                        }
                    }
                    if (listBox1.Items.Count > 0)
                        btnGetSID.Enabled = true;
                    else
                        btnGetSID.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "User Retrieval Error");
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<LDAPgroup> groups = m_search.GetDistributionGroups("SCM Groups");
            listBox7.Items.Clear();
            foreach (LDAPgroup group in groups)
            {
                listBox7.Items.Add(group);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                LDAPgroup group = (LDAPgroup)listBox7.SelectedItem;
                List<LDAPuser> users = m_search.getUserDetailsFromAD(textBox2.Text);
                foreach (LDAPuser user in users)
                {
                    if (textBox3.Text != "")
                        group.AddUser(user, textBox2.Text, textBox3.Text);
                    else
                        group.AddUser(user);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                LDAPgroup group = (LDAPgroup)listBox7.SelectedItem;
                List<LDAPuser> users = m_search.getUserDetailsFromAD(textBox2.Text);
                foreach (LDAPuser user in users)
                    group.RemoveUser(user);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox8.Items.Clear();
            if (listBox7.SelectedIndex >= 0)
            {
                LDAPgroup group = (LDAPgroup)listBox7.SelectedItem;
                foreach (String user in group.GetGroupMembers())
                    listBox8.Items.Add(user);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string description = textBox5.Text;
            LDAPgroup group = (LDAPgroup)listBox7.SelectedItem;
            group.Description = description;
            group.UpdateDescription(textBox2.Text, textBox3.Text);
        }

        private void listBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            LDAPgroup group = (LDAPgroup)listBox7.SelectedItem;
            textBox5.Text = group.Description;
        }

        private void btnRunQuery_Click(object sender, EventArgs e)
        {
            List<LDAPuser> users = m_search.SearchADUsingLDAPQuery(tbQueryText.Text);
            listBox9.Items.Clear();
            lblCount.Text = users.Count.ToString();
            foreach (LDAPuser user in users)
                listBox9.Items.Add(user.Username);
        }

        private void btnGrpSearchUser_Click(object sender, EventArgs e)
        {
            if (tbGrpUsername.Text == "" && tbGrpFullName.Text == "")
                return;
            List<LDAPuser> users = new List<LDAPuser>();
            if (tbGrpUsername.Text == "")
                users = m_search.SearchADUsingLDAPQuery("(displayName=" + tbGrpFullName.Text.Replace(",", "\\,") + ")");
            else
                users = m_search.SearchADUsingLDAPQuery("(samaccountname=" + tbGrpUsername.Text + ")");
            if (users != null && users.Count > 0)
            {
                if (tbGrpFullName.Text == "")
                    tbGrpFullName.Text = users[0].FullName;
                if (tbGrpUsername.Text == "")
                    tbGrpUsername.Text = users[0].Username;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            List<LDAPgroup> agigroups = m_search.GetSecurityGroups("");
            List<LDAPgroup> wmsgroups = m_search.GetSecurityGroups("", "LDAP://wms.com");
            List<LDAPgroup> scigroups = m_search.GetSecurityGroups("", "LDAP://scientificgames.com");
            lbGrpSecGrps.Items.Clear();
            List<String> grpNames = new List<String>();
            if (agigroups != null)
            {
                foreach (LDAPgroup group in agigroups)
                    grpNames.Add(group.ToString());
            }
            if (wmsgroups != null)
            {
                foreach (LDAPgroup group in wmsgroups)
                    grpNames.Add(group.ToString());
            }
            if (scigroups != null)
            {
                foreach (LDAPgroup group in scigroups)
                    grpNames.Add(group.ToString());
            }
            grpNames.Sort();
            foreach (String name in grpNames)
                lbGrpSecGrps.Items.Add(name);
        }

        private void btnGrpFindGroup_Click(object sender, EventArgs e)
        {
            List<LDAPgroup> agigroups = m_search.GetGroups(tbGrpGroupSearch.Text);
            List<LDAPgroup> wmsgroups = m_search.GetGroups(tbGrpGroupSearch.Text, "LDAP://wms.com");
            List<LDAPgroup> scigroups = m_search.GetGroups(tbGrpGroupSearch.Text, "LDAP://scientificgames.com");
            lbGrpFoundGroups.Items.Clear();
            List<String> grpNames = new List<String>();
            if (agigroups != null)
            {
                foreach (LDAPgroup group in agigroups)
                    grpNames.Add(group.ToString());
            }
            if (wmsgroups != null)
            {
                foreach (LDAPgroup group in wmsgroups)
                    grpNames.Add(group.ToString());
            }
            if (scigroups != null)
            {
                foreach (LDAPgroup group in scigroups)
                    grpNames.Add(group.ToString());
            }
            grpNames.Sort();
            foreach (String name in grpNames)
                lbGrpFoundGroups.Items.Add(name);

        }

        private void btnRunRawQuery_Click(object sender, EventArgs e)
        {
            if (tbxRawQueryText.Text.Length > 0)
            {
                List<LDAPRawObject> res = m_search.QueryLDAP(tbxRawQueryText.Text);
                lblRawCount.Text = res.Count.ToString();
                lbxRawQueryResults.Items.Clear();
                foreach (LDAPRawObject obj in res)
                {
                    lbxRawQueryResults.Items.Add(obj.Fields["samaccountname"]);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listBox11.Items.Clear();
            if (listBox10.SelectedIndex >= 0)
            {
                LDAPgroup group = (LDAPgroup)listBox10.SelectedItem;
                foreach (String user in group.GetGroupMembers())
                {
                    String temp = user.Replace("CN=", "");
                    if (temp.StartsWith("S-"))
                    {
                        String username = temp.Split(',')[0];
                        List<LDAPuser> contacts = m_search.SearchAGIADForContactUsingSID(username, "AGI");
                        // we have a foreign security principal.  Try to get contact information
                        if (contacts == null || contacts.Count < 1)
                            continue;
                        else
                            listBox11.Items.Add(contacts[0].FullName);
                    }
                    else
                    {
                        String[] parts = user.Split(',');
                        String username = parts[0] + ", " + parts[1];
                        List<LDAPuser> contacts = m_search.SearchADUsingLDAPQuery("(" + username + ")");
                        // we have a foreign security principal.  Try to get contact information
                        if (contacts == null || contacts.Count < 1)
                            listBox11.Items.Add(temp);
                        else
                            listBox11.Items.Add(contacts[0].FullName);
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<LDAPgroup> groups = m_search.GetSecurityGroups("SCM Groups");
            listBox10.Items.Clear();
            foreach (LDAPgroup group in groups)
            {
                listBox10.Items.Add(group);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (m_search != null && textBox1.Text != "")
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox5.Items.Clear();
                listBox6.Items.Clear();
                String[] nameparts = textBox1.Text.Trim().Split('\\');
                String domain = "AGI";
                String username = "";
                if (nameparts.Length > 1)
                {
                    domain = nameparts[0].Trim();
                    username = nameparts[1].Trim();
                }
                else
                {
                    username = textBox1.Text.Trim(); ;
                }

                try
                {
                    //m_search.Dump = true;
                    m_search.LimitProperties = false;
                    m_search.AllowIncompleteUserdata = true;
                    //List<LDAPuser> users = m_search.SearchAGIADForContactUsingLDAPQuery(username, domain);
                    List<LDAPuser> users = m_search.SearchAGIADForContactUsingLDAPQuery(username, domain);
                    if (users != null && users.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(users[0].Email))
                            tbxContactEmail.Text = users[0].Email;
                        if (!String.IsNullOrEmpty(users[0].UserDN))
                            tbxContactDC.Text = "USER DN  = " + users[0].UserDN;
                        if (!String.IsNullOrEmpty(users[0].PrincipalName))
                            tbxContactDC.Text += Environment.NewLine + "USER UPN = " + users[0].PrincipalName;
                    }
                    else
                    {
                        domain = "AGI";
                        users = m_search.SearchAGIADForContactUsingSID(username, domain);
                        if (users != null && users.Count > 0)
                        {
                            if (!String.IsNullOrEmpty(users[0].Email))
                                tbxContactEmail.Text = users[0].Email;
                            if (!String.IsNullOrEmpty(users[0].UserDN))
                                tbxContactDC.Text = "USER DN  = " + users[0].UserDN;
                            if (!String.IsNullOrEmpty(users[0].PrincipalName))
                                tbxContactDC.Text += Environment.NewLine + "USER UPN = " + users[0].PrincipalName;
                        }
                    }
                    if (users == null || users.Count < 1)
                    {
                        MessageBox.Show("User " + textBox1.Text + " was not found in Active Directory", "User Not Found.");
                        return;
                    }
                    foreach (LDAPuser user in users)
                    {
                        if (user == null)
                        {
                            MessageBox.Show("User " + textBox1.Text + " returned a null entry from Active Directory", "User Error");
                            continue;
                        }
                        listBox1.Items.Add(user.Email);
                        if (user.OtherEmails != null)
                        {
                            foreach (String email in user.OtherEmails)
                            {
                                listBox1.Items.Add(email);
                            }
                        }
                        if (!String.IsNullOrEmpty(user.FullName))
                            listBox2.Items.Add(user.FullName);
                        if (!String.IsNullOrEmpty(user.FirstName))
                            listBox3.Items.Add(user.FirstName);
                        if (!String.IsNullOrEmpty(user.LastName))
                            listBox4.Items.Add(user.LastName);
                        if (!String.IsNullOrEmpty(user.Title))
                            listBox5.Items.Add(user.Title);
                        if (user.Groups == null)
                            continue;
                        LDAPgroup[] groups = user.Groups.ToArray();
                        try
                        {
                            Array.Sort(groups, StringComparer.CurrentCulture);
                        }
                        catch { }
                        foreach (LDAPgroup group in groups)
                        {
                            listBox6.Items.Add(group.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "User Retrieval Error");
                    return;
                }
            }
        }

        private void btnFindContactInfo_Click(object sender, EventArgs e)
        {
            String[] name = tbxContactSAM.Text.Split('\\');
            List<LDAPuser> contacts = m_search.SearchAGIADForContactUsingLDAPQuery(name[1], name[0]);
            if (contacts.Count > 0)
            {
                if (contacts[0].Email != null)
                    tbxContactEmail.Text = contacts[0].Email;
                tbxContactDC.Text = "USER DN  = " + contacts[0].UserDN;
                tbxContactDC.Text += Environment.NewLine + "USER UPN = " + contacts[0].PrincipalName;
            }
            else
            {
                name[0] = "AGI";
                contacts = m_search.SearchAGIADForContactUsingSID(name[1], name[0]);
                if (contacts.Count > 0)
                {
                    if (contacts[0].Email != null)
                        tbxContactEmail.Text = contacts[0].Email;
                    tbxContactDC.Text = "USER DN  = " + contacts[0].UserDN;
                    tbxContactDC.Text += Environment.NewLine + "USER UPN = " + contacts[0].PrincipalName;
                }
            }
        }

        private void listBox_KeyUp(object sender, KeyEventArgs e)
        {
            ListBox box = (ListBox)sender;
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (object row in box.SelectedItems)
                    {
                        sb.Append(row.ToString());
                        sb.AppendLine();
                    }
                    sb.Remove(sb.Length - 1, 1); // Just to avoid copying last empty row
                    Clipboard.SetData(System.Windows.Forms.DataFormats.Text, sb.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (m_search != null && textBox10.Text != "")
            {
                String[] nameparts = textBox10.Text.Split('\\');
                String domain = "";
                String username = "";
                if (nameparts.Length > 1)
                {
                    domain = nameparts[0];
                    username = nameparts[1];
                }
                else
                    username = nameparts[0];
                try
                {
                    //m_search.Dump = true;
                    m_search.LimitProperties = false;
                    m_search.AllowIncompleteUserdata = true;
                    List<LDAPuser> users = new List<LDAPuser>();
                    if (domain == "")
                    {
                        users = m_search.SearchADUsingLDAPQuery("(samaccountname=" + username + ")");
                    }
                    else
                    {
                        CDomain dom = new CDomain();
                        foreach (CDomain d in m_search.Domains)
                        {
                            if(d.Name.Contains(domain))
                            {
                                dom = d;
                                break;
                            }
                        }
                        if(String.IsNullOrEmpty(dom.Path))
                        {
                            MessageBox.Show("Domain " + domain + " not found in list of available domains.");
                            return;
                        }
                        users = m_search.SearchADUsingLDAPQuery("(samaccountname=" + username + ")", dom);
                    }
                    if (users.Count < 1)
                    {
                        MessageBox.Show("User " + textBox1.Text + " was not found in Active Directory", "User Not Found.");
                        return;
                    }
                    foreach (LDAPuser user in users)
                    {
                        if (user == null)
                        {
                            MessageBox.Show("User " + textBox1.Text + " returned a null entry from Active Directory", "User Error");
                            continue;
                        }
                        user.Enabled = !m_search.GetUserDisabled(user.Username);
                        pictureBox2.Image = (user.Enabled == true ? ADTest.Properties.Resources.status_up : ADTest.Properties.Resources.status_down);
                        pictureBox1.Image = (user.Disabled == true ? ADTest.Properties.Resources.status_up : ADTest.Properties.Resources.status_down);
                        m_user = user;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "User Retrieval Error");
                    return;
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (m_user != null && textBox10.Text != "")
            {
                try
                {
                    bool added = false;
                    LDAPgroup group = (LDAPgroup)listBox10.SelectedItem;
                    if (String.IsNullOrEmpty(textBox9.Text))
                        added = group.AddUser(m_user);
                    else
                        added = group.AddUser(m_user, textBox9.Text, textBox8.Text);
                    if (!added)
                        MessageBox.Show("The user was not added to the group - is probably already a member.", "Unable to add user to group");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Could not add user to group");
                }
            }
        }

        private void btnFindGrpByName_Click(object sender, EventArgs e)
        {
            List<LDAPgroup> agigroups = m_search.GetGroups(tbxFindGroupName.Text);
            List<LDAPgroup> wmsgroups = m_search.GetGroups(tbxFindGroupName.Text, "LDAP://wms.com");
            List<LDAPgroup> scigroups = m_search.GetGroups(tbxFindGroupName.Text, "LDAP://scientificgames.com");
            lbxGrpSrchRes.Items.Clear();
            List<String> grpNames = new List<String>();
            if (agigroups != null)
            {
                foreach (LDAPgroup group in agigroups)
                {
                    String name = group.ToString();
                    if (name.StartsWith("\\"))
                        name = name.Remove(0, 1);
                    grpNames.Add(name);
                }
            }
            if (wmsgroups != null)
            {
                foreach (LDAPgroup group in wmsgroups)
                {
                    String name = group.ToString();
                    if (name.StartsWith("\\"))
                        name = name.Remove(0, 1);
                    grpNames.Add(name);
                }
            }
            if (scigroups != null)
            {
                foreach (LDAPgroup group in scigroups)
                {
                    String name = group.ToString();
                    if (name.StartsWith("\\"))
                        name = name.Remove(0, 1);
                    grpNames.Add(name);
                }
            }
            grpNames.Sort();
            foreach (String name in grpNames)
                lbxGrpSrchRes.Items.Add(name);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (lbxGrpSrchRes.SelectedIndex > -1)
            {
                LDAPgroup group = m_search.GetGroup(lbxGrpSrchRes.SelectedItem.ToString());
                if (group == null || String.IsNullOrEmpty(group.CN))
                    group = m_search.GetGroup(lbxGrpSrchRes.SelectedItem.ToString(), "LDAP://wms.com");
                if (group == null || String.IsNullOrEmpty(group.CN))
                    group = m_search.GetGroup(lbxGrpSrchRes.SelectedItem.ToString(), "LDAP://scientificgames.com");
                if (group != null && !String.IsNullOrEmpty(group.CN))
                {
                    lbxMbrList.Items.Clear();
                    List<String> members = group.GetGroupMembers();
                    foreach (String name in members)
                    {
                        lbxMbrList.Items.Add(name);
                    }
                }
            }
        }

        private void btnResolveSID_Click(object sender, EventArgs e)
        {
            if (lbxMbrList.SelectedIndex >= 0)
            {
                String[] parts = lbxMbrList.SelectedItem.ToString().Split(',');
                String name = parts[0].Replace("CN=", "");
                if (name.StartsWith("S-"))
                {
                    foreach (CDomain dom in m_search.Domains)
                    {
                        String resname = m_search.ResolveSIDToName(name, dom.Name);
                        if (String.IsNullOrEmpty(resname))
                            continue;
                        tbxResSID.Text = resname;
                        break;
                    }
                }
                else
                    tbxResSID.Text = name;
            }
        }

        private void btnGetSID_Click(object sender, EventArgs e)
        {
            String[] nameparts = textBox1.Text.Split('\\');
            String domain = "";
            String username = "";
            if (nameparts.Length > 1)
            {
                domain = nameparts[0];
                username = nameparts[1];
            }
            else
                username = nameparts[0];
            try
            {
                //m_search.Dump = true;
                m_search.LimitProperties = false;
                m_search.AllowIncompleteUserdata = true;
                List<LDAPuser> users = new List<LDAPuser>();
                if (domain == "")
                {
                    users = m_search.SearchADUsingLDAPQuery("(samaccountname=" + username + ")");
                }
                else
                {
                    CDomain dom = new CDomain();
                    dom.Path = "*"; // search all available domains
                    users = m_search.SearchADUsingLDAPQuery("(samaccountname=" + username + ")");
                }

                if (users.Count < 1)
                {
                    String query = "(displayname=" + username + ")";
                    users = m_search.SearchADUsingLDAPQuery(query, true);
                    if (users == null || users.Count < 1)
                    {
                        query = "(samaccountname=" + username + ")";
                        users = m_search.SearchADUsingLDAPQuery(query, true);
                    }
                    if (users.Count < 1)
                    {
                        MessageBox.Show("User " + textBox1.Text + " was not found in Active Directory", "User Not Found.");
                        return;
                    }
                }
                String sid = users[0].GetSIDString();
                if (!String.IsNullOrEmpty(sid))
                    lblSID.Text = sid;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "User Retrieval Error");
                return;
            }
        }
    }
}
