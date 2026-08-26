using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab2_Task1
{
    public partial class Form1 : Form
    {
        private Bitmap bufferBitmap;    
        private Bitmap filteredBitmap; 
        private Bitmap maskBitmap;    

        private bool isMouseDown = false;  
        private Point startPoint;        
        private Point endPoint;          
        private Rectangle selectionRect; 

        private Point? lastClickPoint = null;        
        private Color lastPixelColor = Color.Empty; 
        private bool showPixelInfo = false;       

        private List<Rectangle> foundObjects = new List<Rectangle>(); 
        private float densityThreshold = 0.3f;                      
        private int minObjectSize = 21;                             

        private string trafficLightMode = "не определен";   
        private Rectangle detectedSignal = Rectangle.Empty; 
        private bool showSignal = true;                 

        private double kR = 1.0;  
        private double kG = 1.0;
        private double kB = 1.0;
        private bool hasCorrectionCoefficients = false; 

        public Form1()
        {
            InitializeComponent();
        }

        private class TrafficSignalInfo
        {
            public Rectangle Bounds { get; set; }
            public string ColorName { get; set; }
            public string Distance { get; set; }

            public string FullDescription => $"{ColorName} ({Distance})";
        }

        // список для хранения всех найденных сигналов
        private List<TrafficSignalInfo> detectedSignals = new List<TrafficSignalInfo>();

        // обработчик загрузки формы
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(workingFolder))
            {
                LoadFileList();
            }

            radioSelectArea.Checked = true;

            textDensityThreshold.Text = densityThreshold.ToString();
            textMinObjectSize.Text = minObjectSize.ToString();

            pictureBox1.Width = 400;
            pictureBox1.Height = 400;
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }

        // загрузка списка файлов из рабочей папки 
        private void LoadFileList()
        {
            listBox1.Items.Clear();

            string[] files = Directory.GetFiles(workingFolder, "*.*");
            foreach (string file in files)
            {
                string ext = Path.GetExtension(file).ToLower();
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" || ext == ".gif")
                {
                    listBox1.Items.Add(Path.GetFileName(file));
                }
            }
        }

        // обработчик выбора файла в списке
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;

            string fileName = Path.Combine(workingFolder, listBox1.SelectedItem.ToString());

            try
            {
                pictureBox1.Image = Image.FromFile(fileName);

                if (bufferBitmap != null)
                {
                    bufferBitmap.Dispose();
                }
                bufferBitmap = new Bitmap(fileName);

                if (filteredBitmap != null)
                {
                    filteredBitmap.Dispose();
                    filteredBitmap = null;
                }
                if (maskBitmap != null)
                {
                    maskBitmap.Dispose();
                    maskBitmap = null;
                }

                selectionRect = Rectangle.Empty;
                lastClickPoint = null;
                showPixelInfo = false;
                foundObjects.Clear();
                detectedSignal = Rectangle.Empty;
                trafficLightMode = "не определен";

                DetectTrafficSignal();

                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
            }
        }

        // обработчик кнопки "Обновить" 
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(workingFolder))
            {
                LoadFileList();
            }
            else
            {
                MessageBox.Show("Указанная папка не существует!");
            }
        }

        // обработчик нажатия кнопки мыши
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (bufferBitmap == null) return;

            if (radioSelectArea.Checked)
            {
                isMouseDown = true;
                startPoint = e.Location;
                endPoint = e.Location;
            }

            else if (radioReadPixel.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    int x = Math.Min(e.X, bufferBitmap.Width - 1);
                    int y = Math.Min(e.Y, bufferBitmap.Height - 1);
                    x = Math.Max(0, x);
                    y = Math.Max(0, y);

                    lastPixelColor = bufferBitmap.GetPixel(x, y);
                    lastClickPoint = new Point(x, y);
                    showPixelInfo = true;

                    pictureBox1.Invalidate();
                }
            }
        }

        // обработчик движения мыши 
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bufferBitmap == null) return;

            if (radioSelectArea.Checked && isMouseDown)
            {
                endPoint = e.Location;

                int x = Math.Min(startPoint.X, endPoint.X);
                int y = Math.Min(startPoint.Y, endPoint.Y);
                int width = Math.Abs(startPoint.X - endPoint.X);
                int height = Math.Abs(startPoint.Y - endPoint.Y);

                selectionRect = new Rectangle(x, y, width, height);
                pictureBox1.Invalidate();
            }
        }

        // обработчик отпускания кнопки мыши
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (bufferBitmap == null) return;

            if (radioSelectArea.Checked)
            {
                isMouseDown = false;
                endPoint = e.Location;

                int x = Math.Min(startPoint.X, endPoint.X);
                int y = Math.Min(startPoint.Y, endPoint.Y);
                int width = Math.Abs(startPoint.X - endPoint.X);
                int height = Math.Abs(startPoint.Y - endPoint.Y);

                selectionRect = new Rectangle(x, y, width, height);

                if (width > 0 && height > 0)
                {
                    ComputeAndSaveColorCorrection(selectionRect);
                }

                pictureBox1.Invalidate();
            }
        }

        // отрисовка 
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (radioSelectArea.Checked && selectionRect != null && selectionRect.Width > 0 && selectionRect.Height > 0)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, selectionRect);
                }
            }

            if (radioReadPixel.Checked && showPixelInfo && lastClickPoint.HasValue && lastPixelColor != Color.Empty)
            {
                Point p = lastClickPoint.Value;
                string info = $"RGB({lastPixelColor.R},{lastPixelColor.G},{lastPixelColor.B})";
                Point textPos = new Point(p.X + 15, p.Y - 15);

                SizeF textSize = e.Graphics.MeasureString(info, Font);
                RectangleF bgRect = new RectangleF(textPos, textSize);

                using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(200, Color.Black)))
                {
                    e.Graphics.FillRectangle(bgBrush, bgRect);
                }

                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.DrawString(info, Font, textBrush, textPos);
                }

                using (Pen circlePen = new Pen(Color.Yellow, 2))
                {
                    e.Graphics.DrawEllipse(circlePen, p.X - 3, p.Y - 3, 6, 6);
                }
            }

            foreach (Rectangle obj in foundObjects)
            {
                using (Pen pen = new Pen(Color.Green, 2))
                {
                    e.Graphics.DrawRectangle(pen, obj);
                }
            }

            foreach (var signal in detectedSignals)
            {
                if (showSignal && signal.Bounds != Rectangle.Empty)
                {
                    Color penColor = Color.Red;
                    if (signal.ColorName == "ЖЕЛТЫЙ") penColor = Color.Orange;
                    if (signal.ColorName == "ЗЕЛЕНЫЙ") penColor = Color.Lime;

                    using (Pen pen = new Pen(penColor, 3))
                    {
                        e.Graphics.DrawRectangle(pen, signal.Bounds);
                    }

                    string modeText = signal.FullDescription;
                    Point textPos = new Point(signal.Bounds.X, signal.Bounds.Y - 20);

                    using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(200, Color.Black)))
                    using (SolidBrush textBrush = new SolidBrush(Color.White))
                    {
                        SizeF textSize = e.Graphics.MeasureString(modeText, Font);
                        RectangleF bgRect = new RectangleF(textPos, textSize);
                        e.Graphics.FillRectangle(bgBrush, bgRect);
                        e.Graphics.DrawString(modeText, Font, textBrush, textPos);
                    }
                }
            }
        }

        // вычисление средних значений цветовых компонентов и коэффициенты коррекции 
        private void ComputeAndSaveColorCorrection(Rectangle rect)
        {
            if (bufferBitmap == null) return;

            long totalR = 0, totalG = 0, totalB = 0;
            int pixelCount = 0;

            for (int y = rect.Y; y < rect.Y + rect.Height && y < bufferBitmap.Height; y++)
            {
                for (int x = rect.X; x < rect.X + rect.Width && x < bufferBitmap.Width; x++)
                {
                    Color pixel = bufferBitmap.GetPixel(x, y);
                    totalR += pixel.R;
                    totalG += pixel.G;
                    totalB += pixel.B;
                    pixelCount++;
                }
            }

            if (pixelCount > 0)
            {
                double avgR = (double)totalR / pixelCount;
                double avgG = (double)totalG / pixelCount;
                double avgB = (double)totalB / pixelCount;

                double overallAvg = (avgR + avgG + avgB) / 3.0;

                kR = overallAvg / avgR;
                kG = overallAvg / avgG;
                kB = overallAvg / avgB;
                hasCorrectionCoefficients = true;

                labelAvgR.Text = $"R ср: {avgR:F1}";
                labelAvgG.Text = $"G ср: {avgG:F1}";
                labelAvgB.Text = $"B ср: {avgB:F1}";
                labelKR.Text = $"kR: {kR:F3}";
                labelKG.Text = $"kG: {kG:F3}";
                labelKB.Text = $"kB: {kB:F3}";
            }
        }

        // применение цветокоррекции по методу серого мира
        private void ApplyColorCorrection()
        {
            if (bufferBitmap == null)
            {
                MessageBox.Show("Сначала загрузите изображение!");
                return;
            }

            if (!hasCorrectionCoefficients)
            {
                MessageBox.Show("Сначала выделите область интереса для вычисления коэффициентов!");
                return;
            }

            Bitmap correctedBitmap = new Bitmap(bufferBitmap.Width, bufferBitmap.Height);

            for (int y = 0; y < bufferBitmap.Height; y++)
            {
                for (int x = 0; x < bufferBitmap.Width; x++)
                {
                    Color pixel = bufferBitmap.GetPixel(x, y);

                    int newR = (int)(pixel.R * kR);
                    int newG = (int)(pixel.G * kG);
                    int newB = (int)(pixel.B * kB);

                    newR = Math.Max(0, Math.Min(255, newR));
                    newG = Math.Max(0, Math.Min(255, newG));
                    newB = Math.Max(0, Math.Min(255, newB));

                    Color newPixel = Color.FromArgb(newR, newG, newB);
                    correctedBitmap.SetPixel(x, y, newPixel);
                }
            }
            bufferBitmap = (Bitmap)correctedBitmap.Clone();
            DetectTrafficSignal();
            pictureBox1.Image = correctedBitmap;

            if (filteredBitmap != null)
            {
                filteredBitmap.Dispose();
            }
            filteredBitmap = correctedBitmap;
        }

        // обработчики переключений радиокнопок
        private void radioSelectArea_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSelectArea.Checked)
            {
                showPixelInfo = false;
                pictureBox1.Invalidate();
            }
        }

        private void radioReadPixel_CheckedChanged(object sender, EventArgs e)
        {
            if (radioReadPixel.Checked)
            {
                selectionRect = Rectangle.Empty;
                pictureBox1.Invalidate();
            }
        }

        private void radioFindObject_CheckedChanged(object sender, EventArgs e)
        {
            if (radioFindObject.Checked)
            {
                selectionRect = Rectangle.Empty;
                showPixelInfo = false;
                pictureBox1.Invalidate();
            }
        }

        // обработчик кнопки "Фильтр" 
        private void buttonApplyFilter_Click(object sender, EventArgs e)
        {
            if (bufferBitmap == null)
            {
                MessageBox.Show("Сначала загрузите изображение!");
                return;
            }

            int rMin, rMax, gMin, gMax, bMin, bMax;

            try
            {
                rMin = int.Parse(textRMin.Text);
                rMax = int.Parse(textRMax.Text);
                gMin = int.Parse(textGMin.Text);
                gMax = int.Parse(textGMax.Text);
                bMin = int.Parse(textBMin.Text);
                bMax = int.Parse(textBMax.Text);

                if (rMin < 0 || rMin > 255 || rMax < 0 || rMax > 255 ||
                    gMin < 0 || gMin > 255 || gMax < 0 || gMax > 255 ||
                    bMin < 0 || bMin > 255 || bMax < 0 || bMax > 255)
                {
                    MessageBox.Show("Значения должны быть в диапазоне 0-255!");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Введите корректные числовые значения!");
                return;
            }

            if (filteredBitmap != null)
            {
                filteredBitmap.Dispose();
            }
            filteredBitmap = new Bitmap(bufferBitmap.Width, bufferBitmap.Height);

            if (maskBitmap != null)
            {
                maskBitmap.Dispose();
            }
            maskBitmap = new Bitmap(bufferBitmap.Width, bufferBitmap.Height);

            for (int y = 0; y < bufferBitmap.Height; y++)
            {
                for (int x = 0; x < bufferBitmap.Width; x++)
                {
                    Color pixel = bufferBitmap.GetPixel(x, y);

                    bool inRange = (pixel.R >= rMin && pixel.R <= rMax &&
                                   pixel.G >= gMin && pixel.G <= gMax &&
                                   pixel.B >= bMin && pixel.B <= bMax);

                    if (inRange)
                    {
                        filteredBitmap.SetPixel(x, y, pixel);
                        maskBitmap.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        filteredBitmap.SetPixel(x, y, Color.Black);
                        maskBitmap.SetPixel(x, y, Color.Black);
                    }
                }
            }

            pictureBox1.Image = filteredBitmap;
            foundObjects.Clear();
        }

        private void buttonShowOriginal_Click(object sender, EventArgs e)
        {
            if (bufferBitmap != null)
            {
                pictureBox1.Image = bufferBitmap;
                foundObjects.Clear();
                pictureBox1.Invalidate();
            }
        }

        private void buttonShowMask_Click(object sender, EventArgs e)
        {
            if (maskBitmap != null)
            {
                pictureBox1.Image = maskBitmap;
                foundObjects.Clear();
                pictureBox1.Invalidate();
            }
        }

        // метод поиска объекта 
        private Rectangle FindObjectAtPoint(Point startPoint)
        {
            if (maskBitmap == null) return Rectangle.Empty;

            if (startPoint.X < 0 || startPoint.X >= maskBitmap.Width ||
                startPoint.Y < 0 || startPoint.Y >= maskBitmap.Height)
                return Rectangle.Empty;

            Color startColor = maskBitmap.GetPixel(startPoint.X, startPoint.Y);
            if (startColor.R < 128) return Rectangle.Empty;

            int radius = 10;
            int maxRadius = Math.Min(maskBitmap.Width, maskBitmap.Height) / 2;
            int lastGoodRadius = 0;

            while (radius <= maxRadius)
            {
                int x = startPoint.X - radius;
                int y = startPoint.Y - radius;
                int width = radius * 2;
                int height = radius * 2;

                if (x < 0) { width += x; x = 0; }
                if (y < 0) { height += y; y = 0; }
                if (x + width > maskBitmap.Width) { width = maskBitmap.Width - x; }
                if (y + height > maskBitmap.Height) { height = maskBitmap.Height - y; }

                if (width <= 0 || height <= 0) break;

                int whiteCount = 0;
                int totalPixels = 0;

                for (int j = y; j < y + height; j++)
                {
                    for (int i = x; i < x + width; i++)
                    {
                        Color pixel = maskBitmap.GetPixel(i, j);
                        if (pixel.R > 128) whiteCount++;
                        totalPixels++;
                    }
                }

                float density = (float)whiteCount / totalPixels;

                if (density >= densityThreshold)
                {
                    lastGoodRadius = radius;
                    radius += 5;
                }
                else
                {
                    break;
                }
            }

            if (lastGoodRadius > 0)
            {
                int size = lastGoodRadius * 2;
                int x = startPoint.X - lastGoodRadius;
                int y = startPoint.Y - lastGoodRadius;

                if (x < 0) x = 0;
                if (y < 0) y = 0;
                if (x + size > maskBitmap.Width) x = maskBitmap.Width - size;
                if (y + size > maskBitmap.Height) y = maskBitmap.Height - size;

                if (size >= minObjectSize)
                {
                    return new Rectangle(x, y, size, size);
                }
            }

            return Rectangle.Empty;
        }

        // поиск объектов по сетке 
        private void buttonFindGrid_Click(object sender, EventArgs e)
        {
            if (maskBitmap == null)
            {
                MessageBox.Show("Сначала примените фильтрацию для создания маски!");
                return;
            }

            try { densityThreshold = float.Parse(textDensityThreshold.Text); } catch { }
            try { minObjectSize = int.Parse(textMinObjectSize.Text); } catch { }

            foundObjects.Clear();
            List<Rectangle> gridObjects = new List<Rectangle>();

            for (int y = 5; y < maskBitmap.Height; y += 10)
            {
                for (int x = 5; x < maskBitmap.Width; x += 10)
                {
                    if (x < maskBitmap.Width && y < maskBitmap.Height)
                    {
                        Color pixel = maskBitmap.GetPixel(x, y);
                        if (pixel.R > 128)
                        {
                            bool alreadyFound = false;
                            foreach (Rectangle obj in gridObjects)
                            {
                                Rectangle expandedObj = new Rectangle(obj.X - 5, obj.Y - 5, obj.Width + 10, obj.Height + 10);
                                if (expandedObj.Contains(x, y))
                                {
                                    alreadyFound = true;
                                    break;
                                }
                            }

                            if (!alreadyFound)
                            {
                                Rectangle foundObj = FindObjectAtPoint(new Point(x, y));
                                if (foundObj != Rectangle.Empty)
                                {
                                    Rectangle refinedObj = RefineObjectBounds(maskBitmap, foundObj);

                                    int newX = refinedObj.X;
                                    int newY = refinedObj.Y;

                                    if (newX < 0) newX = 0;
                                    if (newY < 0) newY = 0;
                                    if (newX + refinedObj.Width > maskBitmap.Width) newX = maskBitmap.Width - refinedObj.Width;
                                    if (newY + refinedObj.Height > maskBitmap.Height) newY = maskBitmap.Height - refinedObj.Height;

                                    Rectangle shiftedObj = new Rectangle(newX, newY, refinedObj.Width, refinedObj.Height);
                                    gridObjects.Add(shiftedObj);
                                }
                            }

                        }
                    }
                }
            }

            foundObjects = MergeOverlappingObjects(gridObjects);
            pictureBox1.Invalidate();
            labelObjectCount.Text = $"Объектов: {foundObjects.Count}";
        }

        // слияние пересекающихся объектов 
        private List<Rectangle> MergeOverlappingObjects(List<Rectangle> objects)
        {
            if (objects.Count == 0) return new List<Rectangle>();

            List<Rectangle> result = new List<Rectangle>();
            bool[] merged = new bool[objects.Count];

            for (int i = 0; i < objects.Count; i++)
            {
                if (merged[i]) continue;

                Rectangle current = objects[i];

                for (int j = i + 1; j < objects.Count; j++)
                {
                    if (merged[j]) continue;

                    if (current.IntersectsWith(objects[j]))
                    {
                        int x = Math.Min(current.X, objects[j].X);
                        int y = Math.Min(current.Y, objects[j].Y);
                        int right = Math.Max(current.Right, objects[j].Right);
                        int bottom = Math.Max(current.Bottom, objects[j].Bottom);

                        current = new Rectangle(x, y, right - x, bottom - y);
                        merged[j] = true;
                    }
                }

                result.Add(current);
            }

            return result;
        }

        private void buttonClearObjects_Click(object sender, EventArgs e)
        {
            foundObjects.Clear();
            pictureBox1.Invalidate();
            labelObjectCount.Text = "Объектов: 0";
        }

        private void textDensityThreshold_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float value = float.Parse(textDensityThreshold.Text);
                if (value >= 0 && value <= 1)
                {
                    densityThreshold = value;
                }
            }
            catch { }
        }

        private void textMinObjectSize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int value = int.Parse(textMinObjectSize.Text);
                if (value > 0)
                {
                    minObjectSize = value;
                }
            }
            catch { }
        }

        // функция для вычисления точных границ объекта по белым пикселям маски
        private Rectangle RefineObjectBounds(Bitmap mask, Rectangle roughRect)
        {
            int minX = int.MaxValue, minY = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue;
            bool found = false;

            int startX = Math.Max(0, roughRect.Left);
            int startY = Math.Max(0, roughRect.Top);
            int endX = Math.Min(mask.Width, roughRect.Right);
            int endY = Math.Min(mask.Height, roughRect.Bottom);

            for (int y = startY; y < endY; y++)
            {
                for (int x = startX; x < endX; x++)
                {
                    Color pixel = mask.GetPixel(x, y);
                    if (pixel.R > 0)
                    {
                        if (x < minX) minX = x;
                        if (y < minY) minY = y;
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                        found = true;
                    }
                }
            }

            if (found)

            {
                return new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
            }

            return roughRect;
        }

        // детектирование сигнала светофора 
        private void DetectTrafficSignal()
        {
            if (bufferBitmap == null) return;

            detectedSignals.Clear();


            float originalDensity = densityThreshold;
            densityThreshold = 0.15f;


            List<Rectangle> greenCandidates = FindObjectsByColor(0, 90, 130, 255, 0, 100);
            List<Rectangle> yellowCandidates = FindObjectsByColor(200, 255, 150, 255, 0, 80);
            List<Rectangle> redCandidates = FindObjectsByColor(160, 255, 0, 120, 0, 120);

            densityThreshold = originalDensity;

            List<Rectangle> mergedGreen = MergeOverlappingObjects(greenCandidates);
            List<Rectangle> mergedYellow = MergeOverlappingObjects(yellowCandidates);
            List<Rectangle> mergedRed = MergeOverlappingObjects(redCandidates);

            foreach (var rect in mergedYellow)
            {
                detectedSignals.Add(new TrafficSignalInfo
                {
                    Bounds = ApplySignalOffset(rect),
                    ColorName = "ЖЕЛТЫЙ",
                    Distance = GetDistanceDescription(rect.Width)
                });
            }

            foreach (var rect in mergedRed)
            {
                Rectangle shiftedRect = ApplySignalOffset(rect);
                bool isOverlapping = false;

                foreach (var existingSignal in detectedSignals)
                {
                    if (shiftedRect.IntersectsWith(existingSignal.Bounds))
                    {
                        isOverlapping = true;
                        break;
                    }
                }

                if (!isOverlapping)
                {
                    detectedSignals.Add(new TrafficSignalInfo
                    {
                        Bounds = shiftedRect,
                        ColorName = "КРАСНЫЙ",
                        Distance = GetDistanceDescription(rect.Width)
                    });
                }
            }

            foreach (var rect in mergedGreen)
            {
                Rectangle shiftedRect = ApplySignalOffset(rect);
                bool isOverlapping = false;

                foreach (var existingSignal in detectedSignals)
                {
                    if (shiftedRect.IntersectsWith(existingSignal.Bounds))
                    {
                        isOverlapping = true;
                        break;
                    }
                }

                if (!isOverlapping)
                {
                    detectedSignals.Add(new TrafficSignalInfo
                    {
                        Bounds = shiftedRect,
                        ColorName = "ЗЕЛЕНЫЙ",
                        Distance = GetDistanceDescription(rect.Width)
                    });
                }
            }

            pictureBox1.Invalidate();
            labelTrafficMode.Text = $"Найдено сигналов: {detectedSignals.Count}";
        }

        private Rectangle ApplySignalOffset(Rectangle rect)
        {
            return rect;
        }

        private string GetDistanceDescription(int size)
        {
            if (size >= 50)
                return "близко";
            else if (size >= 25)
                return "средне";
            else
                return "далеко";
        }

        // вспомогательный метод для поиска объектов по цветовому диапазону
        private List<Rectangle> FindObjectsByColor(int rMin, int rMax, int gMin, int gMax, int bMin, int bMax)
        {
            List<Rectangle> results = new List<Rectangle>();
            Bitmap tempMask = new Bitmap(bufferBitmap.Width, bufferBitmap.Height);

            for (int y = 0; y < bufferBitmap.Height; y++)
            {
                for (int x = 0; x < bufferBitmap.Width; x++)
                {
                    Color pixel = bufferBitmap.GetPixel(x, y);

                    bool inRange = (pixel.R >= rMin && pixel.R <= rMax &&
                                   pixel.G >= gMin && pixel.G <= gMax &&
                                   pixel.B >= bMin && pixel.B <= bMax);

                    tempMask.SetPixel(x, y, inRange ? Color.White : Color.Black);
                }
            }

            Bitmap originalMask = maskBitmap;
            maskBitmap = tempMask;

            List<Rectangle> candidates = new List<Rectangle>();

            for (int y = 5; y < maskBitmap.Height; y += 10)
            {
                for (int x = 5; x < maskBitmap.Width; x += 10)
                {
                    if (x < maskBitmap.Width && y < maskBitmap.Height)
                    {
                        Color pixel = maskBitmap.GetPixel(x, y);
                        if (pixel.R > 128)
                        {
                            bool alreadyFound = false;
                            foreach (Rectangle obj in candidates)
                            {
                                Rectangle expandedObj = new Rectangle(obj.X - 5, obj.Y - 5, obj.Width + 10, obj.Height + 10);
                                if (expandedObj.Contains(x, y))
                                {
                                    alreadyFound = true;
                                    break;
                                }
                            }

                            if (!alreadyFound)
                            {
                                Rectangle obj = FindObjectAtPoint(new Point(x, y));
                                if (obj != Rectangle.Empty && obj.Width >= minObjectSize)
                                {
                                    Rectangle refinedObj = RefineObjectBounds(maskBitmap, obj);
                                    candidates.Add(refinedObj);
                                }
                            }
                        }
                    }
                }
            }

            maskBitmap = originalMask;
            tempMask.Dispose();

            return candidates;
        }

        private void checkShowSignal_CheckedChanged(object sender, EventArgs e)
        {
            showSignal = checkShowSignal.Checked;
            pictureBox1.Invalidate();
        }

        private void buttonApplyCorrection_Click(object sender, EventArgs e)
        {
            ApplyColorCorrection();
        }

        // обработчик выбора папки
        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Выберите папку с изображениями";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxFolder.Text = folderDialog.SelectedPath;
                    LoadFileList();
                }
            }
        }

        private string workingFolder
        {
            get { return textBoxFolder.Text; }
        }
    }
}