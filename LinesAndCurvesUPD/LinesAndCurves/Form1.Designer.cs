namespace LinesAndCurves
{
    partial class Main_Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Generate_button = new Button();
            Save_button = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            panel1 = new Panel();
            tabPage2 = new TabPage();
            panel2 = new Panel();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // Generate_button
            // 
            Generate_button.Location = new Point(12, 12);
            Generate_button.Name = "Generate_button";
            Generate_button.Size = new Size(114, 33);
            Generate_button.TabIndex = 0;
            Generate_button.Text = "Generate Curves";
            Generate_button.UseVisualStyleBackColor = true;
            Generate_button.Click += Main_Form_Generate_Button_Clicked;
            // 
            // Save_button
            // 
            Save_button.Location = new Point(132, 12);
            Save_button.Name = "Save_button";
            Save_button.Size = new Size(114, 33);
            Save_button.TabIndex = 1;
            Save_button.Text = "Save as SVG";
            Save_button.UseVisualStyleBackColor = true;
            Save_button.Click += Save_Button_Clicked;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 51);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1043, 596);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(panel1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1035, 568);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Location = new Point(6, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(1029, 562);
            panel1.TabIndex = 0;
            //panel1.Click += panel1_Click;
            panel1.Paint += panel1_Paint;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(panel2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1035, 568);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Location = new Point(6, 7);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 400);
            panel2.TabIndex = 1;
            // 
            // Main_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1079, 697);
            Controls.Add(tabControl1);
            Controls.Add(Save_button);
            Controls.Add(Generate_button);
            Name = "Main_Form";
            Text = "Main_Form";
            //Paint += Main_Form_Paint;
            //MouseDown += Main_Form_Mouse_Down;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button Generate_button;
        private Button Save_button;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Panel panel1;
        private TabPage tabPage2;
        private Panel panel2;
    }
}
