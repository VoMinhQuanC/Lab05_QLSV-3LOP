namespace GUI
{
    partial class Form2
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
            this.dgv_Student = new System.Windows.Forms.DataGridView();
            this.cmbFaculty = new System.Windows.Forms.ComboBox();
            this.cmbMajor = new System.Windows.Forms.ComboBox();
            this.btn_DangKi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Student)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Student
            // 
            this.dgv_Student.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Student.Location = new System.Drawing.Point(493, 36);
            this.dgv_Student.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgv_Student.Name = "dgv_Student";
            this.dgv_Student.RowHeadersWidth = 51;
            this.dgv_Student.Size = new System.Drawing.Size(557, 476);
            this.dgv_Student.TabIndex = 0;
            // 
            // cmbFaculty
            // 
            this.cmbFaculty.FormattingEnabled = true;
            this.cmbFaculty.Location = new System.Drawing.Point(83, 55);
            this.cmbFaculty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbFaculty.Name = "cmbFaculty";
            this.cmbFaculty.Size = new System.Drawing.Size(231, 24);
            this.cmbFaculty.TabIndex = 1;
            this.cmbFaculty.SelectedIndexChanged += new System.EventHandler(this.cmbFaculty_SelectedIndexChanged);
            // 
            // cmbMajor
            // 
            this.cmbMajor.FormattingEnabled = true;
            this.cmbMajor.Location = new System.Drawing.Point(83, 110);
            this.cmbMajor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbMajor.Name = "cmbMajor";
            this.cmbMajor.Size = new System.Drawing.Size(231, 24);
            this.cmbMajor.TabIndex = 2;
            this.cmbMajor.SelectedIndexChanged += new System.EventHandler(this.cmbMajor_SelectedIndexChanged);
            // 
            // btn_DangKi
            // 
            this.btn_DangKi.Location = new System.Drawing.Point(83, 171);
            this.btn_DangKi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_DangKi.Name = "btn_DangKi";
            this.btn_DangKi.Size = new System.Drawing.Size(113, 37);
            this.btn_DangKi.TabIndex = 3;
            this.btn_DangKi.Text = "Đăng kí";
            this.btn_DangKi.UseVisualStyleBackColor = true;
            this.btn_DangKi.Click += new System.EventHandler(this.btn_DangKi_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.btn_DangKi);
            this.Controls.Add(this.cmbMajor);
            this.Controls.Add(this.cmbFaculty);
            this.Controls.Add(this.dgv_Student);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Student)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Student;
        private System.Windows.Forms.ComboBox cmbFaculty;
        private System.Windows.Forms.ComboBox cmbMajor;
        private System.Windows.Forms.Button btn_DangKi;
    }
}