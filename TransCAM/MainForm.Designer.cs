namespace TransCAM
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSensitivity = new System.Windows.Forms.TabPage();
            this.btnNotesTCR = new System.Windows.Forms.Button();
            this.btnManualEntryTCR = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lineEditChartTCR = new TransCAM.LineEditChart();
            this.tabEmissions = new System.Windows.Forms.TabPage();
            this.rbMaxEmit = new System.Windows.Forms.RadioButton();
            this.rbMedianEmit = new System.Windows.Forms.RadioButton();
            this.rbMinEmit = new System.Windows.Forms.RadioButton();
            this.btnNotesEmissions = new System.Windows.Forms.Button();
            this.btnManualEmissions = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lineEditChartEmissions = new TransCAM.LineEditChart();
            this.tabAerosols = new System.Windows.Forms.TabPage();
            this.lineEditChartAerosols = new TransCAM.LineEditChart();
            this.btnNotesAerosols = new System.Windows.Forms.Button();
            this.btnManualAerosols = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabDamages = new System.Windows.Forms.TabPage();
            this.lineEditChartDamages = new TransCAM.LineEditChart();
            this.btnNotesDamage = new System.Windows.Forms.Button();
            this.btnManualDamage = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tabProperties = new System.Windows.Forms.TabPage();
            this.btnPropertiesNotes = new System.Windows.Forms.Button();
            this.dgProperties = new System.Windows.Forms.DataGridView();
            this.PropName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Median = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toMSWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabSensitivity.SuspendLayout();
            this.tabEmissions.SuspendLayout();
            this.tabAerosols.SuspendLayout();
            this.tabDamages.SuspendLayout();
            this.tabProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProperties)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabSensitivity);
            this.tabControl1.Controls.Add(this.tabEmissions);
            this.tabControl1.Controls.Add(this.tabAerosols);
            this.tabControl1.Controls.Add(this.tabDamages);
            this.tabControl1.Controls.Add(this.tabProperties);
            this.tabControl1.Location = new System.Drawing.Point(12, 37);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(744, 412);
            this.tabControl1.TabIndex = 1;
            // 
            // tabSensitivity
            // 
            this.tabSensitivity.Controls.Add(this.btnNotesTCR);
            this.tabSensitivity.Controls.Add(this.btnManualEntryTCR);
            this.tabSensitivity.Controls.Add(this.label5);
            this.tabSensitivity.Controls.Add(this.label4);
            this.tabSensitivity.Controls.Add(this.lineEditChartTCR);
            this.tabSensitivity.Location = new System.Drawing.Point(4, 22);
            this.tabSensitivity.Name = "tabSensitivity";
            this.tabSensitivity.Padding = new System.Windows.Forms.Padding(3);
            this.tabSensitivity.Size = new System.Drawing.Size(736, 386);
            this.tabSensitivity.TabIndex = 0;
            this.tabSensitivity.Text = "1. Sensitivity";
            this.tabSensitivity.UseVisualStyleBackColor = true;
            // 
            // btnNotesTCR
            // 
            this.btnNotesTCR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotesTCR.Location = new System.Drawing.Point(613, 30);
            this.btnNotesTCR.Name = "btnNotesTCR";
            this.btnNotesTCR.Size = new System.Drawing.Size(120, 23);
            this.btnNotesTCR.TabIndex = 4;
            this.btnNotesTCR.Text = "Notes / Justification";
            this.btnNotesTCR.UseVisualStyleBackColor = true;
            this.btnNotesTCR.Click += new System.EventHandler(this.btnNotesTCR_Click);
            // 
            // btnManualEntryTCR
            // 
            this.btnManualEntryTCR.Location = new System.Drawing.Point(163, 30);
            this.btnManualEntryTCR.Name = "btnManualEntryTCR";
            this.btnManualEntryTCR.Size = new System.Drawing.Size(138, 23);
            this.btnManualEntryTCR.TabIndex = 3;
            this.btnManualEntryTCR.Text = "Manually Enter Data";
            this.btnManualEntryTCR.UseVisualStyleBackColor = true;
            this.btnManualEntryTCR.Click += new System.EventHandler(this.btnManualEntryTCR_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Click on graph to add points or";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(328, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "Input transient sensitivity PDF";
            // 
            // lineEditChartTCR
            // 
            this.lineEditChartTCR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineEditChartTCR.Location = new System.Drawing.Point(6, 54);
            this.lineEditChartTCR.Name = "lineEditChartTCR";
            this.lineEditChartTCR.Size = new System.Drawing.Size(713, 322);
            this.lineEditChartTCR.TabIndex = 0;
            // 
            // tabEmissions
            // 
            this.tabEmissions.Controls.Add(this.rbMaxEmit);
            this.tabEmissions.Controls.Add(this.rbMedianEmit);
            this.tabEmissions.Controls.Add(this.rbMinEmit);
            this.tabEmissions.Controls.Add(this.btnNotesEmissions);
            this.tabEmissions.Controls.Add(this.btnManualEmissions);
            this.tabEmissions.Controls.Add(this.label7);
            this.tabEmissions.Controls.Add(this.label6);
            this.tabEmissions.Controls.Add(this.lineEditChartEmissions);
            this.tabEmissions.Location = new System.Drawing.Point(4, 22);
            this.tabEmissions.Name = "tabEmissions";
            this.tabEmissions.Padding = new System.Windows.Forms.Padding(3);
            this.tabEmissions.Size = new System.Drawing.Size(736, 386);
            this.tabEmissions.TabIndex = 1;
            this.tabEmissions.Text = "2. CO2 Emission Trajectory";
            this.tabEmissions.UseVisualStyleBackColor = true;
            // 
            // rbMaxEmit
            // 
            this.rbMaxEmit.AutoSize = true;
            this.rbMaxEmit.Location = new System.Drawing.Point(123, 54);
            this.rbMaxEmit.Name = "rbMaxEmit";
            this.rbMaxEmit.Size = new System.Drawing.Size(45, 17);
            this.rbMaxEmit.TabIndex = 8;
            this.rbMaxEmit.Text = "Max";
            this.rbMaxEmit.UseVisualStyleBackColor = true;
            this.rbMaxEmit.CheckedChanged += new System.EventHandler(this.rbMedianEmit_CheckedChanged);
            // 
            // rbMedianEmit
            // 
            this.rbMedianEmit.AutoSize = true;
            this.rbMedianEmit.Checked = true;
            this.rbMedianEmit.Location = new System.Drawing.Point(57, 54);
            this.rbMedianEmit.Name = "rbMedianEmit";
            this.rbMedianEmit.Size = new System.Drawing.Size(60, 17);
            this.rbMedianEmit.TabIndex = 7;
            this.rbMedianEmit.TabStop = true;
            this.rbMedianEmit.Text = "Median";
            this.rbMedianEmit.UseVisualStyleBackColor = true;
            this.rbMedianEmit.CheckedChanged += new System.EventHandler(this.rbMedianEmit_CheckedChanged);
            // 
            // rbMinEmit
            // 
            this.rbMinEmit.AutoSize = true;
            this.rbMinEmit.Location = new System.Drawing.Point(9, 54);
            this.rbMinEmit.Name = "rbMinEmit";
            this.rbMinEmit.Size = new System.Drawing.Size(42, 17);
            this.rbMinEmit.TabIndex = 6;
            this.rbMinEmit.Text = "Min";
            this.rbMinEmit.UseVisualStyleBackColor = true;
            this.rbMinEmit.CheckedChanged += new System.EventHandler(this.rbMedianEmit_CheckedChanged);
            // 
            // btnNotesEmissions
            // 
            this.btnNotesEmissions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotesEmissions.Location = new System.Drawing.Point(610, 31);
            this.btnNotesEmissions.Name = "btnNotesEmissions";
            this.btnNotesEmissions.Size = new System.Drawing.Size(120, 23);
            this.btnNotesEmissions.TabIndex = 5;
            this.btnNotesEmissions.Text = "Notes / Justification";
            this.btnNotesEmissions.UseVisualStyleBackColor = true;
            this.btnNotesEmissions.Click += new System.EventHandler(this.btnNotesEmissions_Click);
            // 
            // btnManualEmissions
            // 
            this.btnManualEmissions.Location = new System.Drawing.Point(163, 31);
            this.btnManualEmissions.Name = "btnManualEmissions";
            this.btnManualEmissions.Size = new System.Drawing.Size(138, 23);
            this.btnManualEmissions.TabIndex = 4;
            this.btnManualEmissions.Text = "Manually Enter Data";
            this.btnManualEmissions.UseVisualStyleBackColor = true;
            this.btnManualEmissions.Click += new System.EventHandler(this.btnManualEmissions_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Click on graph to add points or";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(451, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "CO2 Emissions Estimate without Specific Policy";
            // 
            // lineEditChartEmissions
            // 
            this.lineEditChartEmissions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineEditChartEmissions.Location = new System.Drawing.Point(6, 77);
            this.lineEditChartEmissions.Name = "lineEditChartEmissions";
            this.lineEditChartEmissions.Size = new System.Drawing.Size(724, 293);
            this.lineEditChartEmissions.TabIndex = 0;
            // 
            // tabAerosols
            // 
            this.tabAerosols.Controls.Add(this.lineEditChartAerosols);
            this.tabAerosols.Controls.Add(this.btnNotesAerosols);
            this.tabAerosols.Controls.Add(this.btnManualAerosols);
            this.tabAerosols.Controls.Add(this.label9);
            this.tabAerosols.Controls.Add(this.label8);
            this.tabAerosols.Location = new System.Drawing.Point(4, 22);
            this.tabAerosols.Name = "tabAerosols";
            this.tabAerosols.Size = new System.Drawing.Size(736, 386);
            this.tabAerosols.TabIndex = 2;
            this.tabAerosols.Text = "3. Anthropogenic Aerosol Forcing";
            this.tabAerosols.UseVisualStyleBackColor = true;
            // 
            // lineEditChartAerosols
            // 
            this.lineEditChartAerosols.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineEditChartAerosols.Location = new System.Drawing.Point(6, 56);
            this.lineEditChartAerosols.Name = "lineEditChartAerosols";
            this.lineEditChartAerosols.Size = new System.Drawing.Size(724, 327);
            this.lineEditChartAerosols.TabIndex = 8;
            // 
            // btnNotesAerosols
            // 
            this.btnNotesAerosols.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotesAerosols.Location = new System.Drawing.Point(607, 27);
            this.btnNotesAerosols.Name = "btnNotesAerosols";
            this.btnNotesAerosols.Size = new System.Drawing.Size(120, 23);
            this.btnNotesAerosols.TabIndex = 7;
            this.btnNotesAerosols.Text = "Notes / Justification";
            this.btnNotesAerosols.UseVisualStyleBackColor = true;
            this.btnNotesAerosols.Click += new System.EventHandler(this.btnNotesAerosols_Click);
            // 
            // btnManualAerosols
            // 
            this.btnManualAerosols.Location = new System.Drawing.Point(160, 27);
            this.btnManualAerosols.Name = "btnManualAerosols";
            this.btnManualAerosols.Size = new System.Drawing.Size(138, 23);
            this.btnManualAerosols.TabIndex = 6;
            this.btnManualAerosols.Text = "Manually Enter Data";
            this.btnManualAerosols.UseVisualStyleBackColor = true;
            this.btnManualAerosols.Click += new System.EventHandler(this.btnManualAerosols_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Click on graph to add points or";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(468, 24);
            this.label8.TabIndex = 2;
            this.label8.Text = "Present Day Anthropogenic Aerosol Forcing PDF";
            // 
            // tabDamages
            // 
            this.tabDamages.Controls.Add(this.lineEditChartDamages);
            this.tabDamages.Controls.Add(this.btnNotesDamage);
            this.tabDamages.Controls.Add(this.btnManualDamage);
            this.tabDamages.Controls.Add(this.label10);
            this.tabDamages.Controls.Add(this.label11);
            this.tabDamages.Location = new System.Drawing.Point(4, 22);
            this.tabDamages.Name = "tabDamages";
            this.tabDamages.Size = new System.Drawing.Size(736, 386);
            this.tabDamages.TabIndex = 3;
            this.tabDamages.Text = "4. Damage Function";
            this.tabDamages.UseVisualStyleBackColor = true;
            // 
            // lineEditChartDamages
            // 
            this.lineEditChartDamages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineEditChartDamages.Location = new System.Drawing.Point(5, 59);
            this.lineEditChartDamages.Name = "lineEditChartDamages";
            this.lineEditChartDamages.Size = new System.Drawing.Size(724, 327);
            this.lineEditChartDamages.TabIndex = 12;
            // 
            // btnNotesDamage
            // 
            this.btnNotesDamage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotesDamage.Location = new System.Drawing.Point(609, 31);
            this.btnNotesDamage.Name = "btnNotesDamage";
            this.btnNotesDamage.Size = new System.Drawing.Size(120, 23);
            this.btnNotesDamage.TabIndex = 11;
            this.btnNotesDamage.Text = "Notes / Justification";
            this.btnNotesDamage.UseVisualStyleBackColor = true;
            // 
            // btnManualDamage
            // 
            this.btnManualDamage.Location = new System.Drawing.Point(162, 31);
            this.btnManualDamage.Name = "btnManualDamage";
            this.btnManualDamage.Size = new System.Drawing.Size(138, 23);
            this.btnManualDamage.TabIndex = 10;
            this.btnManualDamage.Text = "Manually Enter Data";
            this.btnManualDamage.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(151, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Click on graph to add points or";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(5, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(175, 24);
            this.label11.TabIndex = 8;
            this.label11.Text = "Damage Function";
            // 
            // tabProperties
            // 
            this.tabProperties.Controls.Add(this.btnPropertiesNotes);
            this.tabProperties.Controls.Add(this.dgProperties);
            this.tabProperties.Controls.Add(this.label12);
            this.tabProperties.Location = new System.Drawing.Point(4, 22);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.Size = new System.Drawing.Size(736, 386);
            this.tabProperties.TabIndex = 4;
            this.tabProperties.Text = "5. Other Properties";
            this.tabProperties.UseVisualStyleBackColor = true;
            // 
            // btnPropertiesNotes
            // 
            this.btnPropertiesNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPropertiesNotes.Location = new System.Drawing.Point(12, 48);
            this.btnPropertiesNotes.Name = "btnPropertiesNotes";
            this.btnPropertiesNotes.Size = new System.Drawing.Size(120, 23);
            this.btnPropertiesNotes.TabIndex = 11;
            this.btnPropertiesNotes.Text = "Notes / Justification";
            this.btnPropertiesNotes.UseVisualStyleBackColor = true;
            this.btnPropertiesNotes.Click += new System.EventHandler(this.btnPropertiesNotes_Click);
            // 
            // dgProperties
            // 
            this.dgProperties.AllowUserToAddRows = false;
            this.dgProperties.AllowUserToDeleteRows = false;
            this.dgProperties.AllowUserToResizeColumns = false;
            this.dgProperties.AllowUserToResizeRows = false;
            this.dgProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PropName,
            this.Min,
            this.Median,
            this.Max});
            this.dgProperties.Location = new System.Drawing.Point(3, 88);
            this.dgProperties.Name = "dgProperties";
            this.dgProperties.RowHeadersVisible = false;
            this.dgProperties.Size = new System.Drawing.Size(730, 88);
            this.dgProperties.TabIndex = 10;
            // 
            // PropName
            // 
            this.PropName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PropName.DefaultCellStyle = dataGridViewCellStyle1;
            this.PropName.FillWeight = 25F;
            this.PropName.Frozen = true;
            this.PropName.HeaderText = "";
            this.PropName.Name = "PropName";
            this.PropName.ReadOnly = true;
            this.PropName.Width = 19;
            // 
            // Min
            // 
            this.Min.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Min.FillWeight = 33F;
            this.Min.Frozen = true;
            this.Min.HeaderText = "Min";
            this.Min.Name = "Min";
            this.Min.Width = 112;
            // 
            // Median
            // 
            this.Median.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Median.FillWeight = 33F;
            this.Median.HeaderText = "Median";
            this.Median.Name = "Median";
            // 
            // Max
            // 
            this.Max.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Max.FillWeight = 33F;
            this.Max.HeaderText = "Max";
            this.Max.Name = "Max";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(8, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 24);
            this.label12.TabIndex = 9;
            this.label12.Text = "Other Properties";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.executeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(768, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toMSWordToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // toMSWordToolStripMenuItem
            // 
            this.toMSWordToolStripMenuItem.Name = "toMSWordToolStripMenuItem";
            this.toMSWordToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.toMSWordToolStripMenuItem.Text = "To MS Word";
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem});
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.executeToolStripMenuItem.Text = "Execute";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 462);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "MainForm";
            this.Text = "TransCAM - Transparent Climate Assessment Model";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabSensitivity.ResumeLayout(false);
            this.tabSensitivity.PerformLayout();
            this.tabEmissions.ResumeLayout(false);
            this.tabEmissions.PerformLayout();
            this.tabAerosols.ResumeLayout(false);
            this.tabAerosols.PerformLayout();
            this.tabDamages.ResumeLayout(false);
            this.tabDamages.PerformLayout();
            this.tabProperties.ResumeLayout(false);
            this.tabProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProperties)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LineEditChart lineEditChartTCR;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSensitivity;
        private System.Windows.Forms.TabPage tabEmissions;
        private System.Windows.Forms.TabPage tabAerosols;
        private System.Windows.Forms.TabPage tabDamages;
        private System.Windows.Forms.TabPage tabProperties;
        private System.Windows.Forms.Button btnManualEntryTCR;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toMSWordToolStripMenuItem;
        private System.Windows.Forms.Button btnNotesTCR;
        private LineEditChart lineEditChartEmissions;
        private System.Windows.Forms.Button btnNotesEmissions;
        private System.Windows.Forms.Button btnManualEmissions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbMaxEmit;
        private System.Windows.Forms.RadioButton rbMedianEmit;
        private System.Windows.Forms.RadioButton rbMinEmit;
        private System.Windows.Forms.Button btnNotesAerosols;
        private System.Windows.Forms.Button btnManualAerosols;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private LineEditChart lineEditChartAerosols;
        private LineEditChart lineEditChartDamages;
        private System.Windows.Forms.Button btnNotesDamage;
        private System.Windows.Forms.Button btnManualDamage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgProperties;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Min;
        private System.Windows.Forms.DataGridViewTextBoxColumn Median;
        private System.Windows.Forms.DataGridViewTextBoxColumn Max;
        private System.Windows.Forms.Button btnPropertiesNotes;
        private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
    }
}

