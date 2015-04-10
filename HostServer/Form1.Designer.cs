namespace HostServer
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
            this.listbox_incoming_orders = new System.Windows.Forms.CheckedListBox();
            this.listbox_incoming_requests = new System.Windows.Forms.CheckedListBox();
            this.detailed_order = new System.Windows.Forms.ListBox();
            this.label_requests = new System.Windows.Forms.Label();
            this.label_orders = new System.Windows.Forms.Label();
            this.label_details = new System.Windows.Forms.Label();
            this.btn_remove = new System.Windows.Forms.Button();
            this.btb_removeorder = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pastorderbox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pastorderdetails = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // listbox_incoming_orders
            // 
            this.listbox_incoming_orders.FormattingEnabled = true;
            this.listbox_incoming_orders.Location = new System.Drawing.Point(138, 31);
            this.listbox_incoming_orders.Name = "listbox_incoming_orders";
            this.listbox_incoming_orders.Size = new System.Drawing.Size(120, 184);
            this.listbox_incoming_orders.TabIndex = 0;
            this.listbox_incoming_orders.SelectedIndexChanged += new System.EventHandler(this.listbox_incoming_orders_SelectedIndexChanged);
            // 
            // listbox_incoming_requests
            // 
            this.listbox_incoming_requests.FormattingEnabled = true;
            this.listbox_incoming_requests.Location = new System.Drawing.Point(12, 31);
            this.listbox_incoming_requests.Name = "listbox_incoming_requests";
            this.listbox_incoming_requests.Size = new System.Drawing.Size(120, 184);
            this.listbox_incoming_requests.TabIndex = 2;
            // 
            // detailed_order
            // 
            this.detailed_order.FormattingEnabled = true;
            this.detailed_order.Location = new System.Drawing.Point(264, 31);
            this.detailed_order.Name = "detailed_order";
            this.detailed_order.Size = new System.Drawing.Size(304, 147);
            this.detailed_order.TabIndex = 4;
            // 
            // label_requests
            // 
            this.label_requests.AutoSize = true;
            this.label_requests.Location = new System.Drawing.Point(24, 15);
            this.label_requests.Name = "label_requests";
            this.label_requests.Size = new System.Drawing.Size(98, 13);
            this.label_requests.TabIndex = 6;
            this.label_requests.Text = "Incoming Requests";
            // 
            // label_orders
            // 
            this.label_orders.AutoSize = true;
            this.label_orders.Location = new System.Drawing.Point(150, 15);
            this.label_orders.Name = "label_orders";
            this.label_orders.Size = new System.Drawing.Size(84, 13);
            this.label_orders.TabIndex = 7;
            this.label_orders.Text = "Incoming Orders";
            // 
            // label_details
            // 
            this.label_details.AutoSize = true;
            this.label_details.Location = new System.Drawing.Point(264, 15);
            this.label_details.Name = "label_details";
            this.label_details.Size = new System.Drawing.Size(68, 13);
            this.label_details.TabIndex = 8;
            this.label_details.Text = "Order Details";
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(13, 221);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(119, 23);
            this.btn_remove.TabIndex = 9;
            this.btn_remove.Text = "Remove and Confirm";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btb_removeorder
            // 
            this.btb_removeorder.Location = new System.Drawing.Point(139, 221);
            this.btb_removeorder.Name = "btb_removeorder";
            this.btb_removeorder.Size = new System.Drawing.Size(119, 23);
            this.btb_removeorder.TabIndex = 10;
            this.btb_removeorder.Text = "Remove and Confirm";
            this.btb_removeorder.UseVisualStyleBackColor = true;
            this.btb_removeorder.Click += new System.EventHandler(this.btb_removeorder_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(412, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Clear Orders and Send To DB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pastorderbox
            // 
            this.pastorderbox.FormattingEnabled = true;
            this.pastorderbox.Location = new System.Drawing.Point(138, 289);
            this.pastorderbox.Name = "pastorderbox";
            this.pastorderbox.Size = new System.Drawing.Size(120, 186);
            this.pastorderbox.TabIndex = 12;
            this.pastorderbox.SelectedIndexChanged += new System.EventHandler(this.pastorderbox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Past Orders";
            // 
            // pastorderdetails
            // 
            this.pastorderdetails.FormattingEnabled = true;
            this.pastorderdetails.Location = new System.Drawing.Point(264, 289);
            this.pastorderdetails.Name = "pastorderdetails";
            this.pastorderdetails.Size = new System.Drawing.Size(304, 147);
            this.pastorderdetails.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Past Order Details";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(138, 481);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Update Past Orders";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(667, 31);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(622, 468);
            this.zedGraphControl1.TabIndex = 17;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(667, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 18;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 511);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pastorderdetails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pastorderbox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btb_removeorder);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.label_details);
            this.Controls.Add(this.label_orders);
            this.Controls.Add(this.label_requests);
            this.Controls.Add(this.detailed_order);
            this.Controls.Add(this.listbox_incoming_requests);
            this.Controls.Add(this.listbox_incoming_orders);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox listbox_incoming_orders;
        private System.Windows.Forms.CheckedListBox listbox_incoming_requests;
        private System.Windows.Forms.ListBox detailed_order;
        private System.Windows.Forms.Label label_requests;
        private System.Windows.Forms.Label label_orders;
        private System.Windows.Forms.Label label_details;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btb_removeorder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox pastorderbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox pastorderdetails;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

