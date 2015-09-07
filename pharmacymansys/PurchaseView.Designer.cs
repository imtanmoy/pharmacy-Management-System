namespace pharmacymansys
{
    partial class PurchaseView
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.PINVICEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PurchaseData = new pharmacymansys.PurchaseData();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PINVICETableAdapter = new pharmacymansys.PurchaseDataTableAdapters.PINVICETableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.PINVICEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurchaseData)).BeginInit();
            this.SuspendLayout();
            // 
            // PINVICEBindingSource
            // 
            this.PINVICEBindingSource.DataMember = "PINVICE";
            this.PINVICEBindingSource.DataSource = this.PurchaseData;
            // 
            // PurchaseData
            // 
            this.PurchaseData.DataSetName = "PurchaseData";
            this.PurchaseData.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.PINVICEBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "pharmacymansys.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(958, 774);
            this.reportViewer1.TabIndex = 0;
            // 
            // PINVICETableAdapter
            // 
            this.PINVICETableAdapter.ClearBeforeFill = true;
            // 
            // PurchaseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 774);
            this.Controls.Add(this.reportViewer1);
            this.Name = "PurchaseView";
            this.Text = "PurchaseView";
            this.Load += new System.EventHandler(this.PurchaseView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PINVICEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurchaseData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PINVICEBindingSource;
        private PurchaseData PurchaseData;
        private PurchaseDataTableAdapters.PINVICETableAdapter PINVICETableAdapter;

    }
}