using CCWin;
using CCWin.SkinControl;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Yolov8Net;

namespace Mytest
{
    public partial class Form1 : CCSkinMain
    {
        private string ImagePath;
        private Bitmap originalBitmap;
        private Bitmap grayBitmap; // ԭʼͼƬ
        private Bitmap zoomedBitmap; // �Ŵ���ͼƬ
        private Point zoomCenter; // �Ŵ����������
        private IPredictor yolov8 = null;
        private PictureBox followerPictureBox; // ��������PictureBox
        private Panel _previewPanel;

        private Bitmap originalBitmapL;
        private Bitmap originalBitmapR;

        private Bitmap originalBitmapCut;
        private Point? startPoint = null;
        private Rectangle selectionRectangle = Rectangle.Empty;
        private Rectangle originalImageRectangle; // ͼ���ԭʼ�ߴ磨������ǰ��
        public Form1()
        {
            InitializeComponent();
            yolov8 = YoloV8Predictor.Create("yolov8n.onnx");

        }
        /// <summary>
        /// ���ر���ͼƬ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = true;//��ֵȷ���Ƿ����ѡ�����ļ�
            dialog.Title = "��ѡ���ļ�";
            dialog.Filter = "ͼ���ļ�(*.jpg;*.jpeg;*.bmp)|*.jpg;*.jpeg;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                originalBitmap = new Bitmap(dialog.FileName);
                ImagePath = dialog.FileName;
                log.Items.Add(ImagePath);
                pictureBox1.Image = originalBitmap;
            }
            else
                log.Items.Add("�û���ȡ��");
        }
        /// <summary>
        /// ��ɫͼת�Ҷ�ͼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColorTMono_Click(object sender, EventArgs e)
        {
            if (originalBitmap != null)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                grayBitmap = @class.imgge.ConvertToGrayscale(originalBitmap);
                pictureBox1.Image = grayBitmap;
                stopwatch.Stop();
                // ���ػҶ�ͼ��
                TimeSpan ts = stopwatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                log.Items.Add($"ת����ɣ���ʱ{ts}");

            }
            else
                log.Items.Add("ͼƬ������");
        }
        /// <summary>
        /// ����Ԥ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPredict_Click(object sender, EventArgs e)
        {
           
            if (originalBitmap == null) { log.Items.Add("ͼƬ������"); return; }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var image = Image.FromFile(ImagePath);

            // ���ͼ������ظ�ʽ���������32λARGB����ת����
            if (image.PixelFormat != PixelFormat.Format32bppArgb)
            {
                // ����һ���µ�Bitmap��ʹ��Format32bppArgb���ظ�ʽ
                Bitmap bitmap32bpp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

                // ʹ��Graphics����ԭʼͼ����Ƶ��µ�Bitmap�ϣ������ʽ��ת�����ظ�ʽ
                using (Graphics g = Graphics.FromImage(bitmap32bpp))
                {
                    // ���ø�������ֵ������ѡ�����������ͼ��������
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    // ��ԭʼͼ����Ƶ��µ�Bitmap��
                    g.DrawImage(image, 0, 0);
                }
                image = bitmap32bpp;
                //ͼ��Ԥ��
                var predictions = yolov8.Predict(image);


                // Draw your boxes
                //using var graphics = Graphics.FromImage(image);
                foreach (var pred in predictions)
                {
                    var originalImageHeight = image.Height;
                    var originalImageWidth = image.Width;

                    var x = Math.Max(pred.Rectangle.X, 0);
                    var y = Math.Max(pred.Rectangle.Y, 0);
                    var width = Math.Min(originalImageWidth - x, pred.Rectangle.Width);
                    var height = Math.Min(originalImageHeight - y, pred.Rectangle.Height);

                    ////////////////////////////////////////////////////////////////////////////////////////////
                    // *** Note that the output is already scaled to the original image height and width. ***
                    ////////////////////////////////////////////////////////////////////////////////////////////

                    // Bounding Box Text
                    string text = $"{pred.Label.Name} [{pred.Score}]";

                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        // Define Text Options
                        Font drawFont = new Font("consolas", 11, FontStyle.Regular);
                        SizeF size = graphics.MeasureString(text, drawFont);
                        SolidBrush fontBrush = new SolidBrush(Color.Black);
                        Point atPoint = new Point((int)x, (int)y - (int)size.Height - 1);

                        // Define BoundingBox options
                        Pen pen = new Pen(Color.Yellow, 2.0f);
                        SolidBrush colorBrush = new SolidBrush(Color.Yellow);

                        // Draw text on image 
                        graphics.FillRectangle(colorBrush, (int)x, (int)(y - size.Height - 1), (int)size.Width, (int)size.Height);
                        graphics.DrawString(text, drawFont, fontBrush, atPoint);

                        // Draw bounding box on image
                        graphics.DrawRectangle(pen, x, y, width, height);

                    }

                }
                pictureBox1.Image = (Bitmap)image.Clone();
                stopwatch.Stop();
                // ���ػҶ�ͼ��
                TimeSpan ts = stopwatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                log.Items.Add($"������ɣ���ʱ{ts}");
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int cropSize = 150;

            // ����img�Ѿ�������
            if (grayBitmap == null) return;

            // ��ȡ���λ�ã������PictureBox��
            Point mousePos = new Point(0, 0);

            double x = (double)grayBitmap.Width / (double)pictureBox1.Width;
            double y = (double)grayBitmap.Height / (double)pictureBox1.Height;
            mousePos.X = (int)((int)e.Location.X * x);
            mousePos.Y = (int)((int)e.Location.Y * y);
            zoomCenter = mousePos;
            int grayValue = grayBitmap.GetPixel(mousePos.X, mousePos.Y).R;
            // ��ʾ�Ҷ�ֵ������
            label1.Text = $"X: {mousePos.X}, Y: {mousePos.Y}, Gray: {grayValue}";

            zoomedBitmap = CreateZoomedBitmap(originalBitmap, zoomCenter, cropSize, 2); // �Ŵ�2��    
            if (followerPictureBox == null)
            {
                followerPictureBox = new PictureBox
                {
                    Size = new Size(zoomedBitmap.Width, zoomedBitmap.Height),
                    BorderStyle = BorderStyle.Fixed3D, // ��ѡ��Ϊ�˸����׿����߽�
                    BackColor = Color.White // ��ѡ�����ñ���ɫ��ͻ��ͼ��

                };
                this.Controls.Add(followerPictureBox); // ��Panel��ӵ�������
                // ע�⣺���ﲻ��followerPictureBox��ӵ������Controls������
                // ��Ϊ������Ҫ�ֶ�������λ�ú���ʾ
            }
            // ���ø���PictureBox��ͼ���λ��
            followerPictureBox.Image = zoomedBitmap;

            followerPictureBox.Location = new Point(e.X + 20, e.Y + 100); // ��Panel��λ�����λ��
            followerPictureBox.Visible = true; // ��ʾPanel
            followerPictureBox.BringToFront();

        }
        public Bitmap CreateZoomedBitmap(Bitmap original, Point zoomCenter, int zoomAreaSize, float zoomFactor)
        {
            // ȷ��zoomAreaSize��ż��������������ʱ���ܻ���ֲ��Գ�
            if (zoomAreaSize % 2 != 0) zoomAreaSize++;

            // ����ü���������ϽǺ����½�����
            int left = Math.Max(0, zoomCenter.X - zoomAreaSize / 2);
            int top = Math.Max(0, zoomCenter.Y - zoomAreaSize / 2);
            int right = Math.Min(original.Width, zoomCenter.X + zoomAreaSize / 2);
            int bottom = Math.Min(original.Height, zoomCenter.Y + zoomAreaSize / 2);

            // ����һ���µ�Bitmap�����ڴ洢�Ŵ���ͼ��
            int zoomedWidth = (int)(zoomAreaSize * zoomFactor);
            int zoomedHeight = (int)(zoomAreaSize * zoomFactor);
            Bitmap zoomedBitmap = new Bitmap(zoomedWidth, zoomedHeight);

            // ����һ��Graphics����������zoomedBitmap�ϻ��ƷŴ��ͼ��
            using (Graphics graphics = Graphics.FromImage(zoomedBitmap))
            {
                // ���ø�������ֵ�����Ա�������ʱ��ø��õ�ͼ������
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // ����һ�����Σ���ʾԭʼͼ���ϵĲü�����
                Rectangle sourceRect = new Rectangle(left, top, right - left, bottom - top);

                // ����һ�����Σ���ʾĿ��Bitmap�ϵĻ�����������zoomedBitmap��
                Rectangle destRect = new Rectangle(0, 0, zoomedWidth, zoomedHeight);

                // ���ƷŴ��ͼ��
                graphics.DrawImage(original, destRect, sourceRect, GraphicsUnit.Pixel);
            }

            // ���طŴ���Bitmap
            return zoomedBitmap;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (followerPictureBox == null) return;
            followerPictureBox.Visible = false;

        }

        private void btnLoadL_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = true;//��ֵȷ���Ƿ����ѡ�����ļ�
            dialog.Title = "��ѡ���ļ�";
            dialog.Filter = "ͼ���ļ�(*.jpg;*.jpeg;*.bmp)|*.jpg;*.jpeg;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                originalBitmapL = new Bitmap(dialog.FileName);
                log.Items.Add(dialog.FileName);
                pictureBoxL.Image = originalBitmapL;
            }
            else
                log.Items.Add("�û���ȡ��");
        }

        private void btnLoadR_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = true;//��ֵȷ���Ƿ����ѡ�����ļ�
            dialog.Title = "��ѡ���ļ�";
            dialog.Filter = "ͼ���ļ�(*.jpg;*.jpeg;*.bmp)|*.jpg;*.jpeg;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                originalBitmapR = new Bitmap(dialog.FileName);
                log.Items.Add(dialog.FileName);
                pictureBoxR.Image = originalBitmapR;
            }
            else
                log.Items.Add("�û���ȡ��");

        }

        private void btnM_Click(object sender, EventArgs e)
        {
            int height = Math.Max(originalBitmapL.Height, originalBitmapR.Height);
            int width = originalBitmapL.Width + originalBitmapR.Width;

            Bitmap combined = new Bitmap(width, height);

            // ����һ��Graphics����������ͼƬ
            using (Graphics g = Graphics.FromImage(combined))
            {
                // �����������ѡ��
                g.Clear(Color.White);

                // ���Ƶ�һ��ͼƬ
                g.DrawImage(originalBitmapL, 0, 0);

                // ���Ƶڶ���ͼƬ�ڵ�һ��ͼƬ���·�
                g.DrawImage(originalBitmapR, originalBitmapL.Width, 0);
            }

            // ���ϲ����ͼƬ��ʾ��PictureBox��
            pictureBoxM.Image = combined;



        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = true;//��ֵȷ���Ƿ����ѡ�����ļ�
            dialog.Title = "��ѡ���ļ�";
            dialog.Filter = "ͼ���ļ�(*.jpg;*.jpeg;*.bmp)|*.jpg;*.jpeg;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                originalBitmapCut = new Bitmap(dialog.FileName);
                ImagePath = dialog.FileName;
                log.Items.Add(ImagePath);
                pictureBox2.Image = originalBitmapCut;


                originalImageRectangle = new Rectangle(0, 0, originalBitmapCut.Width, originalBitmapCut.Height);

            }
            else
                log.Items.Add("�û���ȡ��");
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {

            if (originalBitmapCut != null && e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {

            if (startPoint.HasValue && !selectionRectangle.IsEmpty)
            {
                Rectangle actualCropArea = ConvertToOriginalImageCoordinates(selectionRectangle);
                // �ü�ͼ��
                Bitmap croppedImage = CropImage(pictureBox2.Image, actualCropArea);
                pictureBox5.Image = croppedImage; // ��ʾ��ͼ
                // ��ʾ�ü����ͼ��
                pictureBox5.Image = croppedImage;

                // ���ÿ�ʼ��
                startPoint = null;


            }

        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            double x, y = 1;
            if (originalBitmapCut == null)
                return;
            if (startPoint.HasValue)
            {
                if (startPoint.HasValue)
                {
                    selectionRectangle = Rectangle.FromLTRB(
                       Math.Min(startPoint.Value.X, e.X),
                        Math.Min(startPoint.Value.Y, e.Y),
                        Math.Max(startPoint.Value.X, e.X),
                        Math.Max(startPoint.Value.Y, e.Y)
                    );
                    pictureBox2.Invalidate(); // ǿ���ػ��Ը��¾��ο�
                }
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if (!selectionRectangle.IsEmpty)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, selectionRectangle);
                }
            }

        }


        // ��ͼ������ʾ������ѡ��
        private Bitmap CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        // ����ͼ�����PictureBox����ת��Ϊԭʼͼ������
        private Rectangle ConvertToOriginalImageCoordinates(Rectangle selection)
        {
            // ������Ҫ����StretchImage�����췽ʽ������ת��
            // ������ṩһ���򵥵ı�������ʾ��
            float scaleX = (float)originalImageRectangle.Width / pictureBox2.ClientSize.Width;
            float scaleY = (float)originalImageRectangle.Height / pictureBox2.ClientSize.Height;

            int left = (int)(selection.Left * scaleX);
            int top = (int)(selection.Top * scaleY);
            int right = (int)(selection.Right * scaleX);
            int bottom = (int)(selection.Bottom * scaleY);

            // ȷ��ת�����������ԭʼͼ��Χ��
            left = Math.Max(0, left);
            top = Math.Max(0, top);
            right = Math.Min(originalImageRectangle.Width, right);
            bottom = Math.Min(originalImageRectangle.Height, bottom);

            return new Rectangle(left, top, right - left, bottom - top);
        }

    }
}
