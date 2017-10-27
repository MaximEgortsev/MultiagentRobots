namespace MultiagentRodots
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_Coloms = new System.Windows.Forms.Label();
            this.label_Rows = new System.Windows.Forms.Label();
            this.label_Robots = new System.Windows.Forms.Label();
            this.numericUpDown_coloms = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Rows = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Robots = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_coloms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Rows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Robots)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(2, 10);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(500, 500);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(500, 500);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 500);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // label_Coloms
            // 
            this.label_Coloms.AutoSize = true;
            this.label_Coloms.Location = new System.Drawing.Point(508, 10);
            this.label_Coloms.Name = "label_Coloms";
            this.label_Coloms.Size = new System.Drawing.Size(41, 13);
            this.label_Coloms.TabIndex = 2;
            this.label_Coloms.Text = "Coloms";
            // 
            // label_Rows
            // 
            this.label_Rows.AutoSize = true;
            this.label_Rows.Location = new System.Drawing.Point(508, 60);
            this.label_Rows.Name = "label_Rows";
            this.label_Rows.Size = new System.Drawing.Size(34, 13);
            this.label_Rows.TabIndex = 4;
            this.label_Rows.Text = "Rows";
            // 
            // label_Robots
            // 
            this.label_Robots.AutoSize = true;
            this.label_Robots.Location = new System.Drawing.Point(508, 109);
            this.label_Robots.Name = "label_Robots";
            this.label_Robots.Size = new System.Drawing.Size(41, 13);
            this.label_Robots.TabIndex = 6;
            this.label_Robots.Text = "Robots";
            // 
            // numericUpDown_coloms
            // 
            this.numericUpDown_coloms.Location = new System.Drawing.Point(511, 26);
            this.numericUpDown_coloms.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_coloms.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_coloms.Name = "numericUpDown_coloms";
            this.numericUpDown_coloms.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_coloms.TabIndex = 7;
            this.numericUpDown_coloms.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDown_Rows
            // 
            this.numericUpDown_Rows.Location = new System.Drawing.Point(511, 86);
            this.numericUpDown_Rows.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_Rows.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_Rows.Name = "numericUpDown_Rows";
            this.numericUpDown_Rows.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_Rows.TabIndex = 8;
            this.numericUpDown_Rows.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDown_Robots
            // 
            this.numericUpDown_Robots.Location = new System.Drawing.Point(511, 135);
            this.numericUpDown_Robots.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_Robots.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Robots.Name = "numericUpDown_Robots";
            this.numericUpDown_Robots.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_Robots.TabIndex = 9;
            this.numericUpDown_Robots.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(497, 526);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericUpDown_Robots);
            this.Controls.Add(this.numericUpDown_Rows);
            this.Controls.Add(this.numericUpDown_coloms);
            this.Controls.Add(this.label_Robots);
            this.Controls.Add(this.label_Rows);
            this.Controls.Add(this.label_Coloms);
            this.Controls.Add(this.pictureBox1);
            this.MaximumSize = new System.Drawing.Size(600, 600);
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "Form1";
            this.Text = "MultiagentRobots";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_coloms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Rows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Robots)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_Coloms;
        private System.Windows.Forms.Label label_Rows;
        private System.Windows.Forms.Label label_Robots;
        private System.Windows.Forms.NumericUpDown numericUpDown_coloms;
        private System.Windows.Forms.NumericUpDown numericUpDown_Rows;
        private System.Windows.Forms.NumericUpDown numericUpDown_Robots;
        private System.Windows.Forms.Button button1;
    }
}

