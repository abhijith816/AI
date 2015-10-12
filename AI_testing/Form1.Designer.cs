namespace AI_testing
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
            this.datagridview_array = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.datagridview_array)).BeginInit();
            this.SuspendLayout();
            // 
            // datagridview_array
            // 
            this.datagridview_array.AllowUserToAddRows = false;
            this.datagridview_array.AllowUserToDeleteRows = false;
            this.datagridview_array.AllowUserToResizeColumns = false;
            this.datagridview_array.AllowUserToResizeRows = false;
            this.datagridview_array.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagridview_array.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridview_array.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datagridview_array.Location = new System.Drawing.Point(0, 0);
            this.datagridview_array.Name = "datagridview_array";
            this.datagridview_array.RowTemplate.Height = 24;
            this.datagridview_array.Size = new System.Drawing.Size(282, 253);
            this.datagridview_array.TabIndex = 0;
            this.datagridview_array.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.datagridview_array);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.datagridview_array)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView datagridview_array;

    }
}

