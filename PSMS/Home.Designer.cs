namespace PSMS
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.teachersButton = new System.Windows.Forms.Button();
            this.committeeButton = new System.Windows.Forms.Button();
            this.registrationButton = new System.Windows.Forms.Button();
            this.logInButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.studentsButton = new System.Windows.Forms.Button();
            this.logInBox = new System.Windows.Forms.GroupBox();
            this.logOutButton = new System.Windows.Forms.Button();
            this.schoolName = new System.Windows.Forms.TextBox();
            this.developerLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.aboutButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DetailsButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.logInBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // teachersButton
            // 
            this.teachersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teachersButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.teachersButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.teachersButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.teachersButton.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teachersButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.teachersButton.Image = global::PSMS.Properties.Resources.Teachers;
            this.teachersButton.Location = new System.Drawing.Point(0, 188);
            this.teachersButton.Margin = new System.Windows.Forms.Padding(0);
            this.teachersButton.Name = "teachersButton";
            this.teachersButton.Size = new System.Drawing.Size(175, 103);
            this.teachersButton.TabIndex = 5;
            this.teachersButton.Text = "Teacher\'s Panel";
            this.teachersButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.teachersButton.UseVisualStyleBackColor = false;
            this.teachersButton.Click += new System.EventHandler(this.TeachersButton_Click);
            // 
            // committeeButton
            // 
            this.committeeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.committeeButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.committeeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.committeeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.committeeButton.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.committeeButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.committeeButton.Image = global::PSMS.Properties.Resources.Committee;
            this.committeeButton.Location = new System.Drawing.Point(0, 317);
            this.committeeButton.Margin = new System.Windows.Forms.Padding(0);
            this.committeeButton.Name = "committeeButton";
            this.committeeButton.Size = new System.Drawing.Size(175, 103);
            this.committeeButton.TabIndex = 6;
            this.committeeButton.Text = "Committee Members";
            this.committeeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.committeeButton.UseVisualStyleBackColor = false;
            this.committeeButton.Click += new System.EventHandler(this.CommitteeButton_Click);
            // 
            // registrationButton
            // 
            this.registrationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.registrationButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.registrationButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.registrationButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.registrationButton.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registrationButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.registrationButton.Image = global::PSMS.Properties.Resources.Registration;
            this.registrationButton.Location = new System.Drawing.Point(0, 188);
            this.registrationButton.Margin = new System.Windows.Forms.Padding(0);
            this.registrationButton.Name = "registrationButton";
            this.registrationButton.Size = new System.Drawing.Size(154, 107);
            this.registrationButton.TabIndex = 7;
            this.registrationButton.Text = "Registration";
            this.registrationButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.registrationButton.UseVisualStyleBackColor = false;
            this.registrationButton.Click += new System.EventHandler(this.RegistrationButton_Click);
            // 
            // logInButton
            // 
            this.logInButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.logInButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logInButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.logInButton.Location = new System.Drawing.Point(193, 100);
            this.logInButton.Margin = new System.Windows.Forms.Padding(0);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(105, 36);
            this.logInButton.TabIndex = 3;
            this.logInButton.Text = "Log In";
            this.logInButton.UseVisualStyleBackColor = false;
            this.logInButton.Click += new System.EventHandler(this.LogInButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.textBox1.Location = new System.Drawing.Point(112, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(186, 26);
            this.textBox1.TabIndex = 1;
            this.textBox1.Tag = "";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.textBox2.Location = new System.Drawing.Point(112, 63);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(186, 26);
            this.textBox2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Monotype Corsiva", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Monotype Corsiva", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Password:";
            // 
            // studentsButton
            // 
            this.studentsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.studentsButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.studentsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.studentsButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.studentsButton.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.studentsButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.studentsButton.Image = global::PSMS.Properties.Resources.Students;
            this.studentsButton.Location = new System.Drawing.Point(0, 59);
            this.studentsButton.Margin = new System.Windows.Forms.Padding(0);
            this.studentsButton.Name = "studentsButton";
            this.studentsButton.Size = new System.Drawing.Size(154, 107);
            this.studentsButton.TabIndex = 8;
            this.studentsButton.Text = "Students";
            this.studentsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.studentsButton.UseVisualStyleBackColor = false;
            this.studentsButton.Click += new System.EventHandler(this.StudentsButton_Click);
            // 
            // logInBox
            // 
            this.logInBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logInBox.BackColor = System.Drawing.Color.Transparent;
            this.logInBox.Controls.Add(this.label3);
            this.logInBox.Controls.Add(this.label2);
            this.logInBox.Controls.Add(this.textBox2);
            this.logInBox.Controls.Add(this.textBox1);
            this.logInBox.Controls.Add(this.logInButton);
            this.logInBox.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logInBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.logInBox.Location = new System.Drawing.Point(316, 202);
            this.logInBox.Name = "logInBox";
            this.logInBox.Size = new System.Drawing.Size(304, 147);
            this.logInBox.TabIndex = 0;
            this.logInBox.TabStop = false;
            this.logInBox.Text = "Log In";
            this.logInBox.Visible = false;
            // 
            // logOutButton
            // 
            this.logOutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logOutButton.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.logOutButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logOutButton.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logOutButton.ForeColor = System.Drawing.Color.White;
            this.logOutButton.Location = new System.Drawing.Point(24, 372);
            this.logOutButton.Margin = new System.Windows.Forms.Padding(0);
            this.logOutButton.Name = "logOutButton";
            this.logOutButton.Size = new System.Drawing.Size(107, 36);
            this.logOutButton.TabIndex = 9;
            this.logOutButton.Text = "Log Out";
            this.logOutButton.UseVisualStyleBackColor = false;
            this.logOutButton.Click += new System.EventHandler(this.LogOutButton_Click);
            // 
            // schoolName
            // 
            this.schoolName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.schoolName.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.schoolName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.schoolName.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.schoolName.Font = new System.Drawing.Font("Monotype Corsiva", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.schoolName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.schoolName.Location = new System.Drawing.Point(178, 22);
            this.schoolName.Multiline = true;
            this.schoolName.Name = "schoolName";
            this.schoolName.ReadOnly = true;
            this.schoolName.Size = new System.Drawing.Size(572, 116);
            this.schoolName.TabIndex = 41;
            this.schoolName.Text = "School Name";
            this.schoolName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // developerLabel
            // 
            this.developerLabel.AutoSize = true;
            this.developerLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.developerLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.developerLabel.Location = new System.Drawing.Point(623, 445);
            this.developerLabel.Name = "developerLabel";
            this.developerLabel.Size = new System.Drawing.Size(210, 18);
            this.developerLabel.TabIndex = 33;
            this.developerLabel.Text = "Developed By:- Md Tahmid Hasan";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.aboutButton);
            this.panel1.Controls.Add(this.teachersButton);
            this.panel1.Controls.Add(this.committeeButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 477);
            this.panel1.TabIndex = 34;
            // 
            // aboutButton
            // 
            this.aboutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.aboutButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aboutButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.aboutButton.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.aboutButton.Image = global::PSMS.Properties.Resources.About;
            this.aboutButton.Location = new System.Drawing.Point(0, 59);
            this.aboutButton.Margin = new System.Windows.Forms.Padding(0);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(175, 103);
            this.aboutButton.TabIndex = 4;
            this.aboutButton.Text = "About School";
            this.aboutButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.aboutButton.UseVisualStyleBackColor = false;
            this.aboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DetailsButton);
            this.panel2.Controls.Add(this.studentsButton);
            this.panel2.Controls.Add(this.registrationButton);
            this.panel2.Controls.Add(this.logOutButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(756, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(154, 477);
            this.panel2.TabIndex = 35;
            // 
            // DetailsButton
            // 
            this.DetailsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DetailsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DetailsButton.Font = new System.Drawing.Font("Monotype Corsiva", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetailsButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.DetailsButton.Location = new System.Drawing.Point(83, 441);
            this.DetailsButton.Name = "DetailsButton";
            this.DetailsButton.Size = new System.Drawing.Size(59, 27);
            this.DetailsButton.TabIndex = 43;
            this.DetailsButton.Text = "Details";
            this.DetailsButton.UseVisualStyleBackColor = true;
            this.DetailsButton.Click += new System.EventHandler(this.DetailsButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Location = new System.Drawing.Point(271, 144);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(393, 264);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 42;
            this.pictureBox1.TabStop = false;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(910, 477);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.developerLabel);
            this.Controls.Add(this.logInBox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.schoolName);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Monotype Corsiva", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Primary School Management System";
            this.logInBox.ResumeLayout(false);
            this.logInBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.Button teachersButton;
        private System.Windows.Forms.Button committeeButton;
        private System.Windows.Forms.Button registrationButton;
        private System.Windows.Forms.Button logInButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button studentsButton;
        private System.Windows.Forms.GroupBox logInBox;
        private System.Windows.Forms.Button logOutButton;
        private System.Windows.Forms.TextBox schoolName;
        private System.Windows.Forms.Label developerLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button DetailsButton;
    }
}

