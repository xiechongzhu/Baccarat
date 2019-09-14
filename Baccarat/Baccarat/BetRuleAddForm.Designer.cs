namespace Baccarat
{
    partial class BetRuleAddForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbResult = new System.Windows.Forms.ComboBox();
            this.editValue = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.editValue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "下注结果:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "下注金额:";
            // 
            // cbResult
            // 
            this.cbResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResult.FormattingEnabled = true;
            this.cbResult.Location = new System.Drawing.Point(127, 18);
            this.cbResult.Name = "cbResult";
            this.cbResult.Size = new System.Drawing.Size(253, 32);
            this.cbResult.TabIndex = 2;
            // 
            // editValue
            // 
            this.editValue.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.editValue.Location = new System.Drawing.Point(127, 66);
            this.editValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.editValue.Name = "editValue";
            this.editValue.Size = new System.Drawing.Size(253, 35);
            this.editValue.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(282, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 49);
            this.button1.TabIndex = 4;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // BetRuleAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 185);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.editValue);
            this.Controls.Add(this.cbResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BetRuleAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加规则";
            ((System.ComponentModel.ISupportInitialize)(this.editValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbResult;
        private System.Windows.Forms.NumericUpDown editValue;
        private System.Windows.Forms.Button button1;
    }
}