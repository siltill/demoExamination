namespace demoNEW_EFCoreVersion
{
    partial class OrderEditForm
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
            this.lblDateOrder = new System.Windows.Forms.Label();
            this.dtpDateOrder = new System.Windows.Forms.DateTimePicker();
            this.lblDateDelivery = new System.Windows.Forms.Label();
            this.dtpDateDelivery = new System.Windows.Forms.DateTimePicker();
            this.lblPickupPoint = new System.Windows.Forms.Label();
            this.cmbPickupPoint = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblReceiptCode = new System.Windows.Forms.Label();
            this.txtReceiptCode = new System.Windows.Forms.TextBox();
            this.lblOrderStatus = new System.Windows.Forms.Label();
            this.cmbOrderStatus = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDateOrder
            // 
            this.lblDateOrder.AutoSize = true;
            this.lblDateOrder.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDateOrder.Location = new System.Drawing.Point(12, 15);
            this.lblDateOrder.Name = "lblDateOrder";
            this.lblDateOrder.Size = new System.Drawing.Size(84, 17);
            this.lblDateOrder.TabIndex = 0;
            this.lblDateOrder.Text = "Дата заказа:";
            // 
            // dtpDateOrder
            // 
            this.dtpDateOrder.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpDateOrder.Location = new System.Drawing.Point(130, 12);
            this.dtpDateOrder.Name = "dtpDateOrder";
            this.dtpDateOrder.Size = new System.Drawing.Size(220, 25);
            this.dtpDateOrder.TabIndex = 1;
            // 
            // lblDateDelivery
            // 
            this.lblDateDelivery.AutoSize = true;
            this.lblDateDelivery.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDateDelivery.Location = new System.Drawing.Point(12, 46);
            this.lblDateDelivery.Name = "lblDateDelivery";
            this.lblDateDelivery.Size = new System.Drawing.Size(102, 17);
            this.lblDateDelivery.TabIndex = 2;
            this.lblDateDelivery.Text = "Дата доставки:";
            // 
            // dtpDateDelivery
            // 
            this.dtpDateDelivery.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpDateDelivery.Location = new System.Drawing.Point(130, 43);
            this.dtpDateDelivery.Name = "dtpDateDelivery";
            this.dtpDateDelivery.Size = new System.Drawing.Size(220, 25);
            this.dtpDateDelivery.TabIndex = 3;
            // 
            // lblPickupPoint
            // 
            this.lblPickupPoint.AutoSize = true;
            this.lblPickupPoint.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPickupPoint.Location = new System.Drawing.Point(12, 78);
            this.lblPickupPoint.Name = "lblPickupPoint";
            this.lblPickupPoint.Size = new System.Drawing.Size(96, 17);
            this.lblPickupPoint.TabIndex = 4;
            this.lblPickupPoint.Text = "Пункт выдачи:";
            // 
            // cmbPickupPoint
            // 
            this.cmbPickupPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPickupPoint.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbPickupPoint.FormattingEnabled = true;
            this.cmbPickupPoint.Location = new System.Drawing.Point(130, 74);
            this.cmbPickupPoint.Name = "cmbPickupPoint";
            this.cmbPickupPoint.Size = new System.Drawing.Size(220, 25);
            this.cmbPickupPoint.TabIndex = 5;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblUser.Location = new System.Drawing.Point(12, 109);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(95, 17);
            this.lblUser.TabIndex = 6;
            this.lblUser.Text = "Пользователь:";
            // 
            // cmbUser
            // 
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(130, 105);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(220, 25);
            this.cmbUser.TabIndex = 7;
            // 
            // lblReceiptCode
            // 
            this.lblReceiptCode.AutoSize = true;
            this.lblReceiptCode.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblReceiptCode.Location = new System.Drawing.Point(12, 140);
            this.lblReceiptCode.Name = "lblReceiptCode";
            this.lblReceiptCode.Size = new System.Drawing.Size(105, 17);
            this.lblReceiptCode.TabIndex = 8;
            this.lblReceiptCode.Text = "Код получения:";
            // 
            // txtReceiptCode
            // 
            this.txtReceiptCode.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtReceiptCode.Location = new System.Drawing.Point(130, 137);
            this.txtReceiptCode.Name = "txtReceiptCode";
            this.txtReceiptCode.Size = new System.Drawing.Size(220, 25);
            this.txtReceiptCode.TabIndex = 9;
            // 
            // lblOrderStatus
            // 
            this.lblOrderStatus.AutoSize = true;
            this.lblOrderStatus.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblOrderStatus.Location = new System.Drawing.Point(12, 171);
            this.lblOrderStatus.Name = "lblOrderStatus";
            this.lblOrderStatus.Size = new System.Drawing.Size(96, 17);
            this.lblOrderStatus.TabIndex = 10;
            this.lblOrderStatus.Text = "Статус заказа:";
            // 
            // cmbOrderStatus
            // 
            this.cmbOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrderStatus.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbOrderStatus.FormattingEnabled = true;
            this.cmbOrderStatus.Location = new System.Drawing.Point(130, 168);
            this.cmbOrderStatus.Name = "cmbOrderStatus";
            this.cmbOrderStatus.Size = new System.Drawing.Size(220, 25);
            this.cmbOrderStatus.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(250)))), ((int)(((byte)(154)))));
            this.btnSave.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(130, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(250, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // OrderEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(364, 251);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbOrderStatus);
            this.Controls.Add(this.lblOrderStatus);
            this.Controls.Add(this.txtReceiptCode);
            this.Controls.Add(this.lblReceiptCode);
            this.Controls.Add(this.cmbUser);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.cmbPickupPoint);
            this.Controls.Add(this.lblPickupPoint);
            this.Controls.Add(this.dtpDateDelivery);
            this.Controls.Add(this.lblDateDelivery);
            this.Controls.Add(this.dtpDateOrder);
            this.Controls.Add(this.lblDateOrder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование заказа";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDateOrder;
        private System.Windows.Forms.DateTimePicker dtpDateOrder;
        private System.Windows.Forms.Label lblDateDelivery;
        private System.Windows.Forms.DateTimePicker dtpDateDelivery;
        private System.Windows.Forms.Label lblPickupPoint;
        private System.Windows.Forms.ComboBox cmbPickupPoint;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.Label lblReceiptCode;
        private System.Windows.Forms.TextBox txtReceiptCode;
        private System.Windows.Forms.Label lblOrderStatus;
        private System.Windows.Forms.ComboBox cmbOrderStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}