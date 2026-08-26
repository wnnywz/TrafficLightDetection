namespace Lab2_Task1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label labelFolder;

        private System.Windows.Forms.Label labelAvgR;
        private System.Windows.Forms.Label labelAvgG;
        private System.Windows.Forms.Label labelAvgB;
        private System.Windows.Forms.Label labelKR;
        private System.Windows.Forms.Label labelKG;
        private System.Windows.Forms.Label labelKB;
        private System.Windows.Forms.GroupBox groupBoxCorrection;

        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.RadioButton radioSelectArea;
        private System.Windows.Forms.RadioButton radioReadPixel;

        private System.Windows.Forms.GroupBox groupBoxColorKey;
        private System.Windows.Forms.Label labelRMin;
        private System.Windows.Forms.Label labelRMax;
        private System.Windows.Forms.Label labelGMin;
        private System.Windows.Forms.Label labelGMax;
        private System.Windows.Forms.Label labelBMin;
        private System.Windows.Forms.Label labelBMax;
        private System.Windows.Forms.TextBox textRMin;
        private System.Windows.Forms.TextBox textRMax;
        private System.Windows.Forms.TextBox textGMin;
        private System.Windows.Forms.TextBox textGMax;
        private System.Windows.Forms.TextBox textBMin;
        private System.Windows.Forms.TextBox textBMax;
        private System.Windows.Forms.Button buttonApplyFilter;
        private System.Windows.Forms.Button buttonShowOriginal;
        private System.Windows.Forms.Button buttonShowMask;
        private System.Windows.Forms.Button buttonApplyCorrection;

        private System.Windows.Forms.GroupBox groupBoxObjectDetection;
        private System.Windows.Forms.RadioButton radioFindObject;
        private System.Windows.Forms.Label labelDensityThreshold;
        private System.Windows.Forms.TextBox textDensityThreshold;
        private System.Windows.Forms.Label labelMinObjectSize;
        private System.Windows.Forms.TextBox textMinObjectSize;
        private System.Windows.Forms.Button buttonFindGrid;
        private System.Windows.Forms.Button buttonClearObjects;
        private System.Windows.Forms.Label labelObjectCount;

        private System.Windows.Forms.GroupBox groupBoxTrafficSignal;
        private System.Windows.Forms.Label labelTrafficMode;
        private System.Windows.Forms.CheckBox checkShowSignal;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            listBox1 = new ListBox();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            textBoxFolder = new TextBox();
            buttonRefresh = new Button();
            labelFolder = new Label();
            labelAvgR = new Label();
            labelAvgG = new Label();
            labelAvgB = new Label();
            labelKR = new Label();
            labelKG = new Label();
            labelKB = new Label();
            groupBoxCorrection = new GroupBox();
            groupBoxMode = new GroupBox();
            buttonApplyCorrection = new Button();
            radioSelectArea = new RadioButton();
            radioReadPixel = new RadioButton();
            radioFindObject = new RadioButton();
            groupBoxColorKey = new GroupBox();
            labelRMin = new Label();
            labelRMax = new Label();
            labelGMin = new Label();
            labelGMax = new Label();
            labelBMin = new Label();
            labelBMax = new Label();
            textRMin = new TextBox();
            textRMax = new TextBox();
            textGMin = new TextBox();
            textGMax = new TextBox();
            textBMin = new TextBox();
            textBMax = new TextBox();
            buttonApplyFilter = new Button();
            buttonShowOriginal = new Button();
            buttonShowMask = new Button();
            groupBoxObjectDetection = new GroupBox();
            labelDensityThreshold = new Label();
            textDensityThreshold = new TextBox();
            labelMinObjectSize = new Label();
            textMinObjectSize = new TextBox();
            buttonFindGrid = new Button();
            buttonClearObjects = new Button();
            labelObjectCount = new Label();
            groupBoxTrafficSignal = new GroupBox();
            labelTrafficMode = new Label();
            checkShowSignal = new CheckBox();
            buttonBrowse = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBoxCorrection.SuspendLayout();
            groupBoxMode.SuspendLayout();
            groupBoxColorKey.SuspendLayout();
            groupBoxObjectDetection.SuspendLayout();
            groupBoxTrafficSignal.SuspendLayout();
            SuspendLayout();

            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(11, 79);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(178, 324);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
         
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(204, 79);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(456, 544);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            label1.Location = new Point(11, 56);
            label1.Name = "label1";
            label1.Size = new Size(159, 20);
            label1.TabIndex = 2;
            label1.Text = "Список файлов:";
            
            textBoxFolder.Location = new Point(132, 12);
            textBoxFolder.Name = "textBoxFolder";
            textBoxFolder.Size = new Size(312, 27);
            textBoxFolder.TabIndex = 3;
            textBoxFolder.Text = "C:\\Users\\Public\\Pictures";
            
            buttonRefresh.Location = new Point(553, 11);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(107, 30);
            buttonRefresh.TabIndex = 4;
            buttonRefresh.Text = "Обновить";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
         
            labelFolder.AutoSize = true;
            labelFolder.Location = new Point(11, 15);
            labelFolder.Name = "labelFolder";
            labelFolder.Size = new Size(115, 20);
            labelFolder.TabIndex = 5;
            labelFolder.Text = "Рабочая папка:";
           
            labelAvgR.AutoSize = true;
            labelAvgR.Location = new Point(9, 25);
            labelAvgR.Name = "labelAvgR";
            labelAvgR.Size = new Size(57, 20);
            labelAvgR.TabIndex = 0;
            labelAvgR.Text = "R ср: --";
            
            labelAvgG.AutoSize = true;
            labelAvgG.Location = new Point(9, 45);
            labelAvgG.Name = "labelAvgG";
            labelAvgG.Size = new Size(58, 20);
            labelAvgG.TabIndex = 1;
            labelAvgG.Text = "G ср: --";
            
            labelAvgB.AutoSize = true;
            labelAvgB.Location = new Point(9, 65);
            labelAvgB.Name = "labelAvgB";
            labelAvgB.Size = new Size(57, 20);
            labelAvgB.TabIndex = 2;
            labelAvgB.Text = "B ср: --";
            
            labelKR.AutoSize = true;
            labelKR.Location = new Point(9, 95);
            labelKR.Name = "labelKR";
            labelKR.Size = new Size(44, 20);
            labelKR.TabIndex = 3;
            labelKR.Text = "kR: --";
           
            labelKG.AutoSize = true;
            labelKG.Location = new Point(9, 115);
            labelKG.Name = "labelKG";
            labelKG.Size = new Size(45, 20);
            labelKG.TabIndex = 4;
            labelKG.Text = "kG: --";
         
            labelKB.AutoSize = true;
            labelKB.Location = new Point(9, 135);
            labelKB.Name = "labelKB";
            labelKB.Size = new Size(44, 20);
            labelKB.TabIndex = 5;
            labelKB.Text = "kB: --";
            
            groupBoxCorrection.Controls.Add(labelAvgR);
            groupBoxCorrection.Controls.Add(labelAvgG);
            groupBoxCorrection.Controls.Add(labelAvgB);
            groupBoxCorrection.Controls.Add(labelKR);
            groupBoxCorrection.Controls.Add(labelKG);
            groupBoxCorrection.Controls.Add(labelKB);
            groupBoxCorrection.Location = new Point(11, 409);
            groupBoxCorrection.Name = "groupBoxCorrection";
            groupBoxCorrection.Size = new Size(178, 158);
            groupBoxCorrection.TabIndex = 6;
            groupBoxCorrection.TabStop = false;
            groupBoxCorrection.Text = "Цветокоррекция";
          
            groupBoxMode.Controls.Add(buttonApplyCorrection);
            groupBoxMode.Controls.Add(radioSelectArea);
            groupBoxMode.Controls.Add(radioReadPixel);
            groupBoxMode.Controls.Add(radioFindObject);
            groupBoxMode.Location = new Point(11, 573);
            groupBoxMode.Name = "groupBoxMode";
            groupBoxMode.Size = new Size(178, 156);
            groupBoxMode.TabIndex = 7;
            groupBoxMode.TabStop = false;
            groupBoxMode.Text = "Режим мыши";
            
            buttonApplyCorrection.Location = new Point(9, 113);
            buttonApplyCorrection.Name = "buttonApplyCorrection";
            buttonApplyCorrection.Size = new Size(150, 30);
            buttonApplyCorrection.TabIndex = 20;
            buttonApplyCorrection.Text = "Применить коррекцию";
            buttonApplyCorrection.UseVisualStyleBackColor = true;
            buttonApplyCorrection.Click += buttonApplyCorrection_Click;
            
            radioSelectArea.AutoSize = true;
            radioSelectArea.Location = new Point(9, 26);
            radioSelectArea.Name = "radioSelectArea";
            radioSelectArea.Size = new Size(168, 24);
            radioSelectArea.TabIndex = 0;
            radioSelectArea.TabStop = true;
            radioSelectArea.Text = "Выделение области";
            radioSelectArea.UseVisualStyleBackColor = true;
            radioSelectArea.CheckedChanged += radioSelectArea_CheckedChanged;
           
            radioReadPixel.AutoSize = true;
            radioReadPixel.Location = new Point(9, 52);
            radioReadPixel.Name = "radioReadPixel";
            radioReadPixel.Size = new Size(175, 24);
            radioReadPixel.TabIndex = 1;
            radioReadPixel.TabStop = true;
            radioReadPixel.Text = "Считывание пикселя";
            radioReadPixel.UseVisualStyleBackColor = true;
            radioReadPixel.CheckedChanged += radioReadPixel_CheckedChanged;
             
            radioFindObject.AutoSize = true;
            radioFindObject.Location = new Point(9, 78);
            radioFindObject.Name = "radioFindObject";
            radioFindObject.Size = new Size(133, 24);
            radioFindObject.TabIndex = 2;
            radioFindObject.TabStop = true;
            radioFindObject.Text = "Поиск объекта";
            radioFindObject.UseVisualStyleBackColor = true;
            radioFindObject.CheckedChanged += radioFindObject_CheckedChanged;
            
            groupBoxColorKey.Controls.Add(labelRMin);
            groupBoxColorKey.Controls.Add(labelRMax);
            groupBoxColorKey.Controls.Add(labelGMin);
            groupBoxColorKey.Controls.Add(labelGMax);
            groupBoxColorKey.Controls.Add(labelBMin);
            groupBoxColorKey.Controls.Add(labelBMax);
            groupBoxColorKey.Controls.Add(textRMin);
            groupBoxColorKey.Controls.Add(textRMax);
            groupBoxColorKey.Controls.Add(textGMin);
            groupBoxColorKey.Controls.Add(textGMax);
            groupBoxColorKey.Controls.Add(textBMin);
            groupBoxColorKey.Controls.Add(textBMax);
            groupBoxColorKey.Controls.Add(buttonApplyFilter);
            groupBoxColorKey.Controls.Add(buttonShowOriginal);
            groupBoxColorKey.Controls.Add(buttonShowMask);
            groupBoxColorKey.Location = new Point(241, 738);
            groupBoxColorKey.Name = "groupBoxColorKey";
            groupBoxColorKey.Size = new Size(378, 145);
            groupBoxColorKey.TabIndex = 8;
            groupBoxColorKey.TabStop = false;
            groupBoxColorKey.Text = "Цветовой ключ (RGB)";
          
            labelRMin.AutoSize = true;
            labelRMin.Location = new Point(10, 30);
            labelRMin.Name = "labelRMin";
            labelRMin.Size = new Size(21, 20);
            labelRMin.TabIndex = 0;
            labelRMin.Text = "R:";
           
            labelRMax.AutoSize = true;
            labelRMax.Location = new Point(100, 30);
            labelRMax.Name = "labelRMax";
            labelRMax.Size = new Size(15, 20);
            labelRMax.TabIndex = 2;
            labelRMax.Text = "-";
             
            labelGMin.AutoSize = true;
            labelGMin.Location = new Point(180, 30);
            labelGMin.Name = "labelGMin";
            labelGMin.Size = new Size(22, 20);
            labelGMin.TabIndex = 4;
            labelGMin.Text = "G:";
            
            labelGMax.AutoSize = true;
            labelGMax.Location = new Point(270, 30);
            labelGMax.Name = "labelGMax";
            labelGMax.Size = new Size(15, 20);
            labelGMax.TabIndex = 6;
            labelGMax.Text = "-";
            
            labelBMin.AutoSize = true;
            labelBMin.Location = new Point(10, 70);
            labelBMin.Name = "labelBMin";
            labelBMin.Size = new Size(21, 20);
            labelBMin.TabIndex = 8;
            labelBMin.Text = "B:";
           
            labelBMax.AutoSize = true;
            labelBMax.Location = new Point(100, 70);
            labelBMax.Name = "labelBMax";
            labelBMax.Size = new Size(15, 20);
            labelBMax.TabIndex = 10;
            labelBMax.Text = "-";
            
            textRMin.Location = new Point(40, 27);
            textRMin.Name = "textRMin";
            textRMin.Size = new Size(50, 27);
            textRMin.TabIndex = 1;
            textRMin.Text = "0";
            
            textRMax.Location = new Point(120, 27);
            textRMax.Name = "textRMax";
            textRMax.Size = new Size(50, 27);
            textRMax.TabIndex = 3;
            textRMax.Text = "255";
            
            textGMin.Location = new Point(210, 27);
            textGMin.Name = "textGMin";
            textGMin.Size = new Size(50, 27);
            textGMin.TabIndex = 5;
            textGMin.Text = "0";
           
            textGMax.Location = new Point(290, 27);
            textGMax.Name = "textGMax";
            textGMax.Size = new Size(50, 27);
            textGMax.TabIndex = 7;
            textGMax.Text = "255";
           
            textBMin.Location = new Point(40, 67);
            textBMin.Name = "textBMin";
            textBMin.Size = new Size(50, 27);
            textBMin.TabIndex = 9;
            textBMin.Text = "0";
          
            textBMax.Location = new Point(120, 67);
            textBMax.Name = "textBMax";
            textBMax.Size = new Size(50, 27);
            textBMax.TabIndex = 11;
            textBMax.Text = "255";
            
            buttonApplyFilter.Location = new Point(40, 100);
            buttonApplyFilter.Name = "buttonApplyFilter";
            buttonApplyFilter.Size = new Size(70, 40);
            buttonApplyFilter.TabIndex = 12;
            buttonApplyFilter.Text = "Фильтр";
            buttonApplyFilter.UseVisualStyleBackColor = true;
            buttonApplyFilter.Click += buttonApplyFilter_Click;
          
            buttonShowOriginal.Location = new Point(120, 100);
            buttonShowOriginal.Name = "buttonShowOriginal";
            buttonShowOriginal.Size = new Size(80, 40);
            buttonShowOriginal.TabIndex = 13;
            buttonShowOriginal.Text = "Оригинал";
            buttonShowOriginal.UseVisualStyleBackColor = true;
            buttonShowOriginal.Click += buttonShowOriginal_Click;
           
            buttonShowMask.Location = new Point(210, 100);
            buttonShowMask.Name = "buttonShowMask";
            buttonShowMask.Size = new Size(60, 40);
            buttonShowMask.TabIndex = 14;
            buttonShowMask.Text = "Маска";
            buttonShowMask.UseVisualStyleBackColor = true;
            buttonShowMask.Click += buttonShowMask_Click;
          
            groupBoxObjectDetection.Controls.Add(labelDensityThreshold);
            groupBoxObjectDetection.Controls.Add(textDensityThreshold);
            groupBoxObjectDetection.Controls.Add(labelMinObjectSize);
            groupBoxObjectDetection.Controls.Add(textMinObjectSize);
            groupBoxObjectDetection.Controls.Add(buttonFindGrid);
            groupBoxObjectDetection.Controls.Add(buttonClearObjects);
            groupBoxObjectDetection.Controls.Add(labelObjectCount);
            groupBoxObjectDetection.Location = new Point(204, 636);
            groupBoxObjectDetection.Name = "groupBoxObjectDetection";
            groupBoxObjectDetection.Size = new Size(378, 93);
            groupBoxObjectDetection.TabIndex = 9;
            groupBoxObjectDetection.TabStop = false;
            groupBoxObjectDetection.Text = "Поиск объектов";
         
            labelDensityThreshold.AutoSize = true;
            labelDensityThreshold.Location = new Point(10, 25);
            labelDensityThreshold.Name = "labelDensityThreshold";
            labelDensityThreshold.Size = new Size(121, 20);
            labelDensityThreshold.TabIndex = 0;
            labelDensityThreshold.Text = "Плотность (0-1):";
          
            textDensityThreshold.Location = new Point(130, 22);
            textDensityThreshold.Name = "textDensityThreshold";
            textDensityThreshold.Size = new Size(60, 27);
            textDensityThreshold.TabIndex = 1;
            textDensityThreshold.Text = "0.3";
            textDensityThreshold.TextChanged += textDensityThreshold_TextChanged;
            
            labelMinObjectSize.AutoSize = true;
            labelMinObjectSize.Location = new Point(192, 25);
            labelMinObjectSize.Name = "labelMinObjectSize";
            labelMinObjectSize.Size = new Size(102, 20);
            labelMinObjectSize.TabIndex = 2;
            labelMinObjectSize.Text = "Мин. размер:";
           
            textMinObjectSize.Location = new Point(296, 22);
            textMinObjectSize.Name = "textMinObjectSize";
            textMinObjectSize.Size = new Size(60, 27);
            textMinObjectSize.TabIndex = 3;
            textMinObjectSize.Text = "21";
            textMinObjectSize.TextChanged += textMinObjectSize_TextChanged;
          
            buttonFindGrid.Location = new Point(10, 55);
            buttonFindGrid.Name = "buttonFindGrid";
            buttonFindGrid.Size = new Size(147, 30);
            buttonFindGrid.TabIndex = 4;
            buttonFindGrid.Text = "Поиск по сетке";
            buttonFindGrid.UseVisualStyleBackColor = true;
            buttonFindGrid.Click += buttonFindGrid_Click;
           
            buttonClearObjects.Location = new Point(160, 57);
            buttonClearObjects.Name = "buttonClearObjects";
            buttonClearObjects.Size = new Size(80, 30);
            buttonClearObjects.TabIndex = 5;
            buttonClearObjects.Text = "Очистить";
            buttonClearObjects.UseVisualStyleBackColor = true;
            buttonClearObjects.Click += buttonClearObjects_Click;
           
            labelObjectCount.AutoSize = true;
            labelObjectCount.Location = new Point(246, 60);
            labelObjectCount.Name = "labelObjectCount";
            labelObjectCount.Size = new Size(91, 20);
            labelObjectCount.TabIndex = 6;
            labelObjectCount.Text = "Объектов: 0";
           
            groupBoxTrafficSignal.Controls.Add(labelTrafficMode);
            groupBoxTrafficSignal.Controls.Add(checkShowSignal);
            groupBoxTrafficSignal.Location = new Point(11, 738);
            groupBoxTrafficSignal.Name = "groupBoxTrafficSignal";
            groupBoxTrafficSignal.Size = new Size(224, 133);
            groupBoxTrafficSignal.TabIndex = 10;
            groupBoxTrafficSignal.TabStop = false;
            groupBoxTrafficSignal.Text = "Сигнал светофора";
          
            labelTrafficMode.AutoSize = true;
            labelTrafficMode.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            labelTrafficMode.Location = new Point(9, 25);
            labelTrafficMode.Name = "labelTrafficMode";
            labelTrafficMode.Size = new Size(210, 20);
            labelTrafficMode.TabIndex = 0;
            labelTrafficMode.Text = "Режим: не определен";
           
            checkShowSignal.Checked = true;
            checkShowSignal.CheckState = CheckState.Checked;
            checkShowSignal.Location = new Point(9, 50);
            checkShowSignal.Name = "checkShowSignal";
            checkShowSignal.Size = new Size(160, 24);
            checkShowSignal.TabIndex = 1;
            checkShowSignal.Text = "Показывать сигнал";
            checkShowSignal.CheckedChanged += checkShowSignal_CheckedChanged;
          
            buttonBrowse.Location = new Point(453, 12);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new Size(94, 29);
            buttonBrowse.TabIndex = 11;
            buttonBrowse.Text = "Обзор";
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += button1_Click;
          
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(691, 894);
            Controls.Add(buttonBrowse);
            Controls.Add(groupBoxTrafficSignal);
            Controls.Add(groupBoxObjectDetection);
            Controls.Add(groupBoxColorKey);
            Controls.Add(groupBoxMode);
            Controls.Add(groupBoxCorrection);
            Controls.Add(labelFolder);
            Controls.Add(buttonRefresh);
            Controls.Add(textBoxFolder);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "241-324 Бочкарева лаб 2";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBoxCorrection.ResumeLayout(false);
            groupBoxCorrection.PerformLayout();
            groupBoxMode.ResumeLayout(false);
            groupBoxMode.PerformLayout();
            groupBoxColorKey.ResumeLayout(false);
            groupBoxColorKey.PerformLayout();
            groupBoxObjectDetection.ResumeLayout(false);
            groupBoxObjectDetection.PerformLayout();
            groupBoxTrafficSignal.ResumeLayout(false);
            groupBoxTrafficSignal.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button buttonBrowse;
    }
}