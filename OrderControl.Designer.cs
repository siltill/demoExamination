namespace demoNEW_EFCoreVersion
{
    partial class OrderControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblArticle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPickupAddress = new System.Windows.Forms.Label();
            this.lblDeliveryDate = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.lblDeliveryDateRight = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblArticle
            // 
            this.lblArticle.AutoSize = true;
            this.lblArticle.Location = new System.Drawing.Point(4, 4);
            this.lblArticle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArticle.Name = "lblArticle";
            this.lblArticle.Size = new System.Drawing.Size(115, 16);
            this.lblArticle.TabIndex = 0;
            this.lblArticle.Text = "Артикул заказа:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(4, 28);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(106, 16);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Статус заказа:";
            // 
            // lblPickupAddress
            // 
            this.lblPickupAddress.AutoSize = true;
            this.lblPickupAddress.Location = new System.Drawing.Point(4, 53);
            this.lblPickupAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPickupAddress.Name = "lblPickupAddress";
            this.lblPickupAddress.Size = new System.Drawing.Size(151, 16);
            this.lblPickupAddress.TabIndex = 2;
            this.lblPickupAddress.Text = "Адрес пункта выдачи:";
            // 
            // lblDeliveryDate
            // 
            this.lblDeliveryDate.AutoSize = true;
            this.lblDeliveryDate.Location = new System.Drawing.Point(4, 78);
            this.lblDeliveryDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeliveryDate.Name = "lblDeliveryDate";
            this.lblDeliveryDate.Size = new System.Drawing.Size(92, 16);
            this.lblDeliveryDate.TabIndex = 3;
            this.lblDeliveryDate.Text = "Дата заказа:";
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.lblDeliveryDateRight);
            this.panelRight.Location = new System.Drawing.Point(875, 4);
            this.panelRight.Margin = new System.Windows.Forms.Padding(4);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(117, 123);
            this.panelRight.TabIndex = 4;
            // 
            // lblDeliveryDateRight
            // 
            this.lblDeliveryDateRight.AutoSize = true;
            this.lblDeliveryDateRight.Location = new System.Drawing.Point(4, 4);
            this.lblDeliveryDateRight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeliveryDateRight.Name = "lblDeliveryDateRight";
            this.lblDeliveryDateRight.Size = new System.Drawing.Size(103, 16);
            this.lblDeliveryDateRight.TabIndex = 0;
            this.lblDeliveryDateRight.Text = "Дата доставки";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(4, 102);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 28);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(112, 102);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 28);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // OrderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.lblDeliveryDate);
            this.Controls.Add(this.lblPickupAddress);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblArticle);
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "OrderControl";
            this.Size = new System.Drawing.Size(996, 135);
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblArticle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPickupAddress;
        private System.Windows.Forms.Label lblDeliveryDate;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblDeliveryDateRight;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
    }
}
