namespace RubY
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.tbxO = new System.Windows.Forms.TextBox();
            this.tbxG = new System.Windows.Forms.TextBox();
            this.tbxR = new System.Windows.Forms.TextBox();
            this.tbxW = new System.Windows.Forms.TextBox();
            this.tbxB = new System.Windows.Forms.TextBox();
            this.tbxY = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(66, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbxO
            // 
            this.tbxO.Location = new System.Drawing.Point(231, 71);
            this.tbxO.Multiline = true;
            this.tbxO.Name = "tbxO";
            this.tbxO.Size = new System.Drawing.Size(90, 67);
            this.tbxO.TabIndex = 0;
            // 
            // tbxG
            // 
            this.tbxG.Location = new System.Drawing.Point(142, 144);
            this.tbxG.Multiline = true;
            this.tbxG.Name = "tbxG";
            this.tbxG.Size = new System.Drawing.Size(83, 73);
            this.tbxG.TabIndex = 2;
            // 
            // tbxR
            // 
            this.tbxR.Location = new System.Drawing.Point(231, 223);
            this.tbxR.Multiline = true;
            this.tbxR.Name = "tbxR";
            this.tbxR.Size = new System.Drawing.Size(90, 72);
            this.tbxR.TabIndex = 3;
            // 
            // tbxW
            // 
            this.tbxW.Location = new System.Drawing.Point(231, 144);
            this.tbxW.Multiline = true;
            this.tbxW.Name = "tbxW";
            this.tbxW.Size = new System.Drawing.Size(90, 73);
            this.tbxW.TabIndex = 4;
            // 
            // tbxB
            // 
            this.tbxB.Location = new System.Drawing.Point(327, 144);
            this.tbxB.Multiline = true;
            this.tbxB.Name = "tbxB";
            this.tbxB.Size = new System.Drawing.Size(88, 73);
            this.tbxB.TabIndex = 5;
            // 
            // tbxY
            // 
            this.tbxY.Location = new System.Drawing.Point(421, 144);
            this.tbxY.Multiline = true;
            this.tbxY.Name = "tbxY";
            this.tbxY.Size = new System.Drawing.Size(90, 73);
            this.tbxY.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 450);
            this.Controls.Add(this.tbxY);
            this.Controls.Add(this.tbxB);
            this.Controls.Add(this.tbxW);
            this.Controls.Add(this.tbxR);
            this.Controls.Add(this.tbxG);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbxO);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbxO;
        private System.Windows.Forms.TextBox tbxG;
        private System.Windows.Forms.TextBox tbxR;
        private System.Windows.Forms.TextBox tbxW;
        private System.Windows.Forms.TextBox tbxB;
        private System.Windows.Forms.TextBox tbxY;
    }
}

