namespace HumanityService.Client
{
    partial class Dashboard
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
            this.treeView = new System.Windows.Forms.TreeView();
            this.DeliveryCodeLabel = new System.Windows.Forms.Label();
            this.DeliveryCodeTextBox = new System.Windows.Forms.MaskedTextBox();
            this.ValidateDeliveryButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.Signout = new System.Windows.Forms.Button();
            this.ApproveVolunteerButton = new System.Windows.Forms.Button();
            this.ValidateContributionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.treeView.Location = new System.Drawing.Point(312, 12);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(741, 483);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            this.treeView.Enter += new System.EventHandler(this.treeView_Enter);
            // 
            // DeliveryCodeLabel
            // 
            this.DeliveryCodeLabel.AutoSize = true;
            this.DeliveryCodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.DeliveryCodeLabel.Location = new System.Drawing.Point(12, 415);
            this.DeliveryCodeLabel.Name = "DeliveryCodeLabel";
            this.DeliveryCodeLabel.Size = new System.Drawing.Size(141, 25);
            this.DeliveryCodeLabel.TabIndex = 1;
            this.DeliveryCodeLabel.Text = "Delivery Code:";
            // 
            // DeliveryCodeTextBox
            // 
            this.DeliveryCodeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.DeliveryCodeTextBox.Location = new System.Drawing.Point(172, 412);
            this.DeliveryCodeTextBox.Mask = "000000";
            this.DeliveryCodeTextBox.Name = "DeliveryCodeTextBox";
            this.DeliveryCodeTextBox.Size = new System.Drawing.Size(78, 30);
            this.DeliveryCodeTextBox.TabIndex = 2;
            // 
            // ValidateDeliveryButton
            // 
            this.ValidateDeliveryButton.Location = new System.Drawing.Point(17, 455);
            this.ValidateDeliveryButton.Name = "ValidateDeliveryButton";
            this.ValidateDeliveryButton.Size = new System.Drawing.Size(233, 40);
            this.ValidateDeliveryButton.TabIndex = 3;
            this.ValidateDeliveryButton.Text = "Validate Delivery";
            this.ValidateDeliveryButton.UseVisualStyleBackColor = true;
            this.ValidateDeliveryButton.Click += new System.EventHandler(this.ValidateDeliveryButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.ForeColor = System.Drawing.Color.Green;
            this.RefreshButton.Location = new System.Drawing.Point(172, 12);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(74, 33);
            this.RefreshButton.TabIndex = 4;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // Signout
            // 
            this.Signout.ForeColor = System.Drawing.Color.Red;
            this.Signout.Location = new System.Drawing.Point(17, 12);
            this.Signout.Name = "Signout";
            this.Signout.Size = new System.Drawing.Size(80, 33);
            this.Signout.TabIndex = 5;
            this.Signout.Text = "Sign Out";
            this.Signout.UseVisualStyleBackColor = true;
            this.Signout.Click += new System.EventHandler(this.Signout_Click);
            // 
            // ApproveVolunteerButton
            // 
            this.ApproveVolunteerButton.Location = new System.Drawing.Point(12, 341);
            this.ApproveVolunteerButton.Name = "ApproveVolunteerButton";
            this.ApproveVolunteerButton.Size = new System.Drawing.Size(238, 48);
            this.ApproveVolunteerButton.TabIndex = 6;
            this.ApproveVolunteerButton.Text = "Approve Volunteer";
            this.ApproveVolunteerButton.UseVisualStyleBackColor = true;
            this.ApproveVolunteerButton.Click += new System.EventHandler(this.ApproveVolunteerButton_Click);
            // 
            // ValidateContributionButton
            // 
            this.ValidateContributionButton.Location = new System.Drawing.Point(12, 341);
            this.ValidateContributionButton.Name = "ValidateContributionButton";
            this.ValidateContributionButton.Size = new System.Drawing.Size(238, 48);
            this.ValidateContributionButton.TabIndex = 7;
            this.ValidateContributionButton.Text = "Validate Contribution";
            this.ValidateContributionButton.UseVisualStyleBackColor = true;
            this.ValidateContributionButton.Click += new System.EventHandler(this.ValidateContributionButton_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 520);
            this.Controls.Add(this.ValidateContributionButton);
            this.Controls.Add(this.ApproveVolunteerButton);
            this.Controls.Add(this.Signout);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.ValidateDeliveryButton);
            this.Controls.Add(this.DeliveryCodeTextBox);
            this.Controls.Add(this.DeliveryCodeLabel);
            this.Controls.Add(this.treeView);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Label DeliveryCodeLabel;
        private System.Windows.Forms.MaskedTextBox DeliveryCodeTextBox;
        private System.Windows.Forms.Button ValidateDeliveryButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button Signout;
        private System.Windows.Forms.Button ApproveVolunteerButton;
        private System.Windows.Forms.Button ValidateContributionButton;
    }
}