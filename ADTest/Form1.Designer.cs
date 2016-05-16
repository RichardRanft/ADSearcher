namespace ADTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.listBox5 = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.listBox6 = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.pbxUserDisabled = new System.Windows.Forms.PictureBox();
            this.label20 = new System.Windows.Forms.Label();
            this.pbxUserEnabled = new System.Windows.Forms.PictureBox();
            this.button10 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.listBox8 = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listBox7 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tbxContactDC = new System.Windows.Forms.TextBox();
            this.tbxContactEmail = new System.Windows.Forms.TextBox();
            this.btnFindContactInfo = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.tbxContactSAM = new System.Windows.Forms.TextBox();
            this.lbGrpFoundGroups = new System.Windows.Forms.ListBox();
            this.btnGrpFindGroup = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.tbGrpGroupSearch = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lbGrpSecGrps = new System.Windows.Forms.ListBox();
            this.button7 = new System.Windows.Forms.Button();
            this.btnGrpSearchUser = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.tbGrpFullName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbGrpUsername = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblRawCount = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lbxRawQueryResults = new System.Windows.Forms.ListBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbxRawQueryText = new System.Windows.Forms.TextBox();
            this.btnRunRawQuery = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.listBox9 = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbQueryText = new System.Windows.Forms.TextBox();
            this.btnRunQuery = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.button9 = new System.Windows.Forms.Button();
            this.listBox11 = new System.Windows.Forms.ListBox();
            this.listBox10 = new System.Windows.Forms.ListBox();
            this.button8 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUserDisabled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUserEnabled)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username (mjohnson):";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(247, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "WMS\\PTrotter";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 99);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(247, 95);
            this.listBox1.TabIndex = 3;
            this.listBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(4, 213);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(249, 95);
            this.listBox2.TabIndex = 5;
            this.listBox2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "get user information";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Email addresses:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Full name";
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(259, 213);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(249, 95);
            this.listBox3.TabIndex = 6;
            this.listBox3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.Location = new System.Drawing.Point(514, 213);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(249, 95);
            this.listBox4.TabIndex = 7;
            this.listBox4.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(259, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "First name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(511, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Last name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(256, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Title";
            // 
            // listBox5
            // 
            this.listBox5.FormattingEnabled = true;
            this.listBox5.Location = new System.Drawing.Point(259, 99);
            this.listBox5.Name = "listBox5";
            this.listBox5.Size = new System.Drawing.Size(249, 95);
            this.listBox5.TabIndex = 4;
            this.listBox5.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 311);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Groups";
            // 
            // listBox6
            // 
            this.listBox6.FormattingEnabled = true;
            this.listBox6.HorizontalScrollbar = true;
            this.listBox6.Location = new System.Drawing.Point(6, 327);
            this.listBox6.Name = "listBox6";
            this.listBox6.Size = new System.Drawing.Size(757, 342);
            this.listBox6.TabIndex = 8;
            this.listBox6.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(10, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 708);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.button10);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.listBox6);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.listBox2);
            this.tabPage1.Controls.Add(this.listBox5);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.listBox4);
            this.tabPage1.Controls.Add(this.listBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 682);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "User Information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.pbxUserDisabled);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.pbxUserEnabled);
            this.groupBox1.Location = new System.Drawing.Point(336, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 65);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Account Status";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 39);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(99, 13);
            this.label21.TabIndex = 19;
            this.label21.Text = "LDAPuser.Disabled";
            // 
            // pbxUserDisabled
            // 
            this.pbxUserDisabled.Image = global::ADTest.Properties.Resources.status_up;
            this.pbxUserDisabled.Location = new System.Drawing.Point(108, 36);
            this.pbxUserDisabled.Name = "pbxUserDisabled";
            this.pbxUserDisabled.Size = new System.Drawing.Size(21, 21);
            this.pbxUserDisabled.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxUserDisabled.TabIndex = 18;
            this.pbxUserDisabled.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 16);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(97, 13);
            this.label20.TabIndex = 17;
            this.label20.Text = "LDAPuser.Enabled";
            // 
            // pbxUserEnabled
            // 
            this.pbxUserEnabled.Image = global::ADTest.Properties.Resources.status_up;
            this.pbxUserEnabled.Location = new System.Drawing.Point(108, 13);
            this.pbxUserEnabled.Name = "pbxUserEnabled";
            this.pbxUserEnabled.Size = new System.Drawing.Size(21, 21);
            this.pbxUserEnabled.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxUserEnabled.TabIndex = 16;
            this.pbxUserEnabled.TabStop = false;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(171, 45);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(159, 23);
            this.button10.TabIndex = 15;
            this.button10.Text = "get AGI contact information";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox5);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.listBox8);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.listBox7);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 682);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Group Information";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(10, 552);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(396, 20);
            this.textBox5.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 535);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Description";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(412, 550);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(127, 23);
            this.button6.TabIndex = 5;
            this.button6.Text = "Update Description";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(399, 8);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(140, 23);
            this.button5.TabIndex = 2;
            this.button5.Text = "Get Group Members";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // listBox8
            // 
            this.listBox8.FormattingEnabled = true;
            this.listBox8.HorizontalScrollbar = true;
            this.listBox8.Location = new System.Drawing.Point(399, 37);
            this.listBox8.Name = "listBox8";
            this.listBox8.Size = new System.Drawing.Size(387, 472);
            this.listBox8.Sorted = true;
            this.listBox8.TabIndex = 3;
            this.listBox8.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(210, 620);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Password";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(209, 636);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(197, 20);
            this.textBox3.TabIndex = 7;
            this.textBox3.Text = "&to60qww294e8w7to6";
            this.textBox3.UseSystemPasswordChar = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(545, 634);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(127, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "Leave Group";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(412, 634);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(127, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Join Group";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 620);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Username";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 636);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(197, 20);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "rranft";
            // 
            // listBox7
            // 
            this.listBox7.FormattingEnabled = true;
            this.listBox7.Location = new System.Drawing.Point(7, 37);
            this.listBox7.Name = "listBox7";
            this.listBox7.Size = new System.Drawing.Size(377, 472);
            this.listBox7.Sorted = true;
            this.listBox7.TabIndex = 1;
            this.listBox7.SelectedIndexChanged += new System.EventHandler(this.listBox7_SelectedIndexChanged);
            this.listBox7.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(196, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Get SCM Distribution Groups";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tbxContactDC);
            this.tabPage4.Controls.Add(this.tbxContactEmail);
            this.tabPage4.Controls.Add(this.btnFindContactInfo);
            this.tabPage4.Controls.Add(this.label17);
            this.tabPage4.Controls.Add(this.tbxContactSAM);
            this.tabPage4.Controls.Add(this.lbGrpFoundGroups);
            this.tabPage4.Controls.Add(this.btnGrpFindGroup);
            this.tabPage4.Controls.Add(this.label16);
            this.tabPage4.Controls.Add(this.tbGrpGroupSearch);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.lbGrpSecGrps);
            this.tabPage4.Controls.Add(this.button7);
            this.tabPage4.Controls.Add(this.btnGrpSearchUser);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.tbGrpFullName);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.tbGrpUsername);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(792, 682);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Additional Group Functions";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tbxContactDC
            // 
            this.tbxContactDC.Location = new System.Drawing.Point(380, 371);
            this.tbxContactDC.Multiline = true;
            this.tbxContactDC.Name = "tbxContactDC";
            this.tbxContactDC.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxContactDC.Size = new System.Drawing.Size(406, 173);
            this.tbxContactDC.TabIndex = 16;
            // 
            // tbxContactEmail
            // 
            this.tbxContactEmail.Location = new System.Drawing.Point(380, 345);
            this.tbxContactEmail.Name = "tbxContactEmail";
            this.tbxContactEmail.Size = new System.Drawing.Size(406, 20);
            this.tbxContactEmail.TabIndex = 15;
            // 
            // btnFindContactInfo
            // 
            this.btnFindContactInfo.Location = new System.Drawing.Point(383, 316);
            this.btnFindContactInfo.Name = "btnFindContactInfo";
            this.btnFindContactInfo.Size = new System.Drawing.Size(89, 23);
            this.btnFindContactInfo.TabIndex = 14;
            this.btnFindContactInfo.Text = "Find Contact";
            this.btnFindContactInfo.UseVisualStyleBackColor = true;
            this.btnFindContactInfo.Click += new System.EventHandler(this.btnFindContactInfo_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(380, 270);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 13);
            this.label17.TabIndex = 13;
            this.label17.Text = "Username";
            // 
            // tbxContactSAM
            // 
            this.tbxContactSAM.Location = new System.Drawing.Point(380, 289);
            this.tbxContactSAM.Name = "tbxContactSAM";
            this.tbxContactSAM.Size = new System.Drawing.Size(205, 20);
            this.tbxContactSAM.TabIndex = 12;
            // 
            // lbGrpFoundGroups
            // 
            this.lbGrpFoundGroups.FormattingEnabled = true;
            this.lbGrpFoundGroups.Location = new System.Drawing.Point(383, 159);
            this.lbGrpFoundGroups.Name = "lbGrpFoundGroups";
            this.lbGrpFoundGroups.Size = new System.Drawing.Size(403, 108);
            this.lbGrpFoundGroups.TabIndex = 11;
            this.lbGrpFoundGroups.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // btnGrpFindGroup
            // 
            this.btnGrpFindGroup.Location = new System.Drawing.Point(383, 130);
            this.btnGrpFindGroup.Name = "btnGrpFindGroup";
            this.btnGrpFindGroup.Size = new System.Drawing.Size(89, 23);
            this.btnGrpFindGroup.TabIndex = 10;
            this.btnGrpFindGroup.Text = "Find Group";
            this.btnGrpFindGroup.UseVisualStyleBackColor = true;
            this.btnGrpFindGroup.Click += new System.EventHandler(this.btnGrpFindGroup_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(383, 88);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Group Name";
            // 
            // tbGrpGroupSearch
            // 
            this.tbGrpGroupSearch.Location = new System.Drawing.Point(383, 104);
            this.tbGrpGroupSearch.Name = "tbGrpGroupSearch";
            this.tbGrpGroupSearch.Size = new System.Drawing.Size(403, 20);
            this.tbGrpGroupSearch.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 88);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "Security Groups";
            // 
            // lbGrpSecGrps
            // 
            this.lbGrpSecGrps.FormattingEnabled = true;
            this.lbGrpSecGrps.Location = new System.Drawing.Point(7, 104);
            this.lbGrpSecGrps.Name = "lbGrpSecGrps";
            this.lbGrpSecGrps.Size = new System.Drawing.Size(370, 563);
            this.lbGrpSecGrps.TabIndex = 6;
            this.lbGrpSecGrps.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(105, 62);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(126, 23);
            this.button7.TabIndex = 5;
            this.button7.Text = "Get Security Groups";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnGrpSearchUser
            // 
            this.btnGrpSearchUser.Location = new System.Drawing.Point(10, 62);
            this.btnGrpSearchUser.Name = "btnGrpSearchUser";
            this.btnGrpSearchUser.Size = new System.Drawing.Size(89, 23);
            this.btnGrpSearchUser.TabIndex = 4;
            this.btnGrpSearchUser.Text = "Find User";
            this.btnGrpSearchUser.UseVisualStyleBackColor = true;
            this.btnGrpSearchUser.Click += new System.EventHandler(this.btnGrpSearchUser_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(221, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Full Name";
            // 
            // tbGrpFullName
            // 
            this.tbGrpFullName.Location = new System.Drawing.Point(221, 35);
            this.tbGrpFullName.Name = "tbGrpFullName";
            this.tbGrpFullName.Size = new System.Drawing.Size(425, 20);
            this.tbGrpFullName.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Username";
            // 
            // tbGrpUsername
            // 
            this.tbGrpUsername.Location = new System.Drawing.Point(7, 35);
            this.tbGrpUsername.Name = "tbGrpUsername";
            this.tbGrpUsername.Size = new System.Drawing.Size(205, 20);
            this.tbGrpUsername.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblRawCount);
            this.tabPage3.Controls.Add(this.label18);
            this.tabPage3.Controls.Add(this.lbxRawQueryResults);
            this.tabPage3.Controls.Add(this.label19);
            this.tabPage3.Controls.Add(this.tbxRawQueryText);
            this.tabPage3.Controls.Add(this.btnRunRawQuery);
            this.tabPage3.Controls.Add(this.lblCount);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.listBox9);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.tbQueryText);
            this.tabPage3.Controls.Add(this.btnRunQuery);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 682);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Arbitrary Query Test";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblRawCount
            // 
            this.lblRawCount.AutoSize = true;
            this.lblRawCount.Location = new System.Drawing.Point(196, 324);
            this.lblRawCount.Name = "lblRawCount";
            this.lblRawCount.Size = new System.Drawing.Size(31, 13);
            this.lblRawCount.TabIndex = 11;
            this.lblRawCount.Text = "none";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(152, 324);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(38, 13);
            this.label18.TabIndex = 10;
            this.label18.Text = "Count:";
            // 
            // lbxRawQueryResults
            // 
            this.lbxRawQueryResults.FormattingEnabled = true;
            this.lbxRawQueryResults.Location = new System.Drawing.Point(9, 349);
            this.lbxRawQueryResults.Name = "lbxRawQueryResults";
            this.lbxRawQueryResults.Size = new System.Drawing.Size(777, 199);
            this.lbxRawQueryResults.TabIndex = 9;
            this.lbxRawQueryResults.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 277);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(180, 13);
            this.label19.TabIndex = 8;
            this.label19.Text = "Arbitrary query (no provided wrapper)";
            // 
            // tbxRawQueryText
            // 
            this.tbxRawQueryText.Location = new System.Drawing.Point(6, 293);
            this.tbxRawQueryText.Name = "tbxRawQueryText";
            this.tbxRawQueryText.Size = new System.Drawing.Size(780, 20);
            this.tbxRawQueryText.TabIndex = 7;
            // 
            // btnRunRawQuery
            // 
            this.btnRunRawQuery.Location = new System.Drawing.Point(9, 319);
            this.btnRunRawQuery.Name = "btnRunRawQuery";
            this.btnRunRawQuery.Size = new System.Drawing.Size(137, 23);
            this.btnRunRawQuery.TabIndex = 6;
            this.btnRunRawQuery.Text = "Run Query";
            this.btnRunRawQuery.UseVisualStyleBackColor = true;
            this.btnRunRawQuery.Click += new System.EventHandler(this.btnRunRawQuery_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(196, 50);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(31, 13);
            this.lblCount.TabIndex = 5;
            this.lblCount.Text = "none";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(152, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Count:";
            // 
            // listBox9
            // 
            this.listBox9.FormattingEnabled = true;
            this.listBox9.Location = new System.Drawing.Point(9, 75);
            this.listBox9.Name = "listBox9";
            this.listBox9.Size = new System.Drawing.Size(777, 199);
            this.listBox9.TabIndex = 3;
            this.listBox9.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(397, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Arbitrary query text - (&(&(objectCategory=person)(objectClass=user))( your query" +
    " text ))";
            // 
            // tbQueryText
            // 
            this.tbQueryText.Location = new System.Drawing.Point(6, 19);
            this.tbQueryText.Name = "tbQueryText";
            this.tbQueryText.Size = new System.Drawing.Size(780, 20);
            this.tbQueryText.TabIndex = 1;
            // 
            // btnRunQuery
            // 
            this.btnRunQuery.Location = new System.Drawing.Point(9, 45);
            this.btnRunQuery.Name = "btnRunQuery";
            this.btnRunQuery.Size = new System.Drawing.Size(137, 23);
            this.btnRunQuery.TabIndex = 0;
            this.btnRunQuery.Text = "Run Query";
            this.btnRunQuery.UseVisualStyleBackColor = true;
            this.btnRunQuery.Click += new System.EventHandler(this.btnRunQuery_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.button9);
            this.tabPage5.Controls.Add(this.listBox11);
            this.tabPage5.Controls.Add(this.listBox10);
            this.tabPage5.Controls.Add(this.button8);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(792, 682);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Security Group Members";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(393, 6);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(140, 23);
            this.button9.TabIndex = 4;
            this.button9.Text = "Get Group Members";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // listBox11
            // 
            this.listBox11.FormattingEnabled = true;
            this.listBox11.HorizontalScrollbar = true;
            this.listBox11.Location = new System.Drawing.Point(393, 35);
            this.listBox11.Name = "listBox11";
            this.listBox11.Size = new System.Drawing.Size(387, 472);
            this.listBox11.Sorted = true;
            this.listBox11.TabIndex = 5;
            this.listBox11.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // listBox10
            // 
            this.listBox10.FormattingEnabled = true;
            this.listBox10.Location = new System.Drawing.Point(6, 36);
            this.listBox10.Name = "listBox10";
            this.listBox10.Size = new System.Drawing.Size(377, 472);
            this.listBox10.Sorted = true;
            this.listBox10.TabIndex = 3;
            this.listBox10.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyUp);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(6, 6);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(196, 23);
            this.button8.TabIndex = 2;
            this.button8.Text = "Get SCM Security Groups";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 732);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUserDisabled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUserEnabled)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listBox6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listBox7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListBox listBox8;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbQueryText;
        private System.Windows.Forms.Button btnRunQuery;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox listBox9;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListBox lbGrpSecGrps;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnGrpSearchUser;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbGrpFullName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbGrpUsername;
        private System.Windows.Forms.Button btnGrpFindGroup;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbGrpGroupSearch;
        private System.Windows.Forms.ListBox lbGrpFoundGroups;
        private System.Windows.Forms.Label lblRawCount;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ListBox lbxRawQueryResults;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbxRawQueryText;
        private System.Windows.Forms.Button btnRunRawQuery;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.ListBox listBox11;
        private System.Windows.Forms.ListBox listBox10;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TextBox tbxContactEmail;
        private System.Windows.Forms.Button btnFindContactInfo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbxContactSAM;
        private System.Windows.Forms.TextBox tbxContactDC;
        private System.Windows.Forms.PictureBox pbxUserEnabled;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.PictureBox pbxUserDisabled;
        private System.Windows.Forms.Label label20;
    }
}

