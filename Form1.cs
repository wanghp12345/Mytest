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
        private Bitmap grayBitmap; // 原始图片
        private Bitmap zoomedBitmap; // 放大后的图片
        private Point zoomCenter; // 放大区域的中心
        private IPredictor yolov8 = null;
        private PictureBox followerPictureBox; // 跟随鼠标的PictureBox
        private Panel _previewPanel;

        private Bitmap originalBitmapL;
        private Bitmap originalBitmapR;

        private Bitmap originalBitmapCut;
        private Point? startPoint = null;
        private Rectangle selectionRectangle = Rectangle.Empty;
        private Rectangle originalImageRectangle; // 图像的原始尺寸（在拉伸前）
        public Form1()
        {
            InitializeComponent();
            yolov8 = YoloV8Predictor.Create("yolov8n.onnx");

        }
        /// <summary>
        /// 加载本体图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "图像文件(*.jpg;*.jpeg;*.bmp)|*.jpg;*.jpeg;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                originalBitmap = new Bitmap(dialog.FileName);
                ImagePath = dialog.FileName;
                log.Items.Add(ImagePath);
                pictureBox1.Image = originalBitmap;
            }
            else
                log.Items.Add("用户已取消");
        }
        /// <summary>
        /// 彩色图转灰度图
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
                // 返回灰度图像
                TimeSpan ts = stopwatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                log.Items.Add($"转换完成，耗时{ts}");

            }
            else
                log.Items.Add("图片不存在");
        }
        /// <summary>
        /// 推理预测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPredict_Click(object sender, EventArgs e)
        {
           
            if (originalBitmap == null) { log.Items.Add("图片不存在"); return; }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var image = Image.FromFile(ImagePath);

            // 检查图像的像素格式，如果不是32位ARGB，则转换它
            if (image.PixelFormat != PixelFormat.Format32bppArgb)
            {
                // 创建一个新的Bitmap，使用Format32bppArgb像素格式
                Bitmap bitmap32bpp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

                // 使用Graphics对象将原始图像绘制到新的Bitmap上，这会隐式地转换像素格式
                using (Graphics g = Graphics.FromImage(bitmap32bpp))
                {
                    // 设置高质量插值法（可选，但可以提高图像质量）
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    // 将原始图像绘制到新的Bitmap上
                    g.DrawImage(image, 0, 0);
                }
                image = bitmap32bpp;
                //图像预测
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
                // 返回灰度图像
                TimeSpan ts = stopwatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                log.Items.Add($"推理完成，耗时{ts}");
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int cropSize = 150;

            // 假设img已经被设置
            if (grayBitmap == null) return;

            // 获取鼠标位置（相对于PictureBox）
            Point mousePos = new Point(0, 0);

            double x = (double)grayBitmap.Width / (double)pictureBox1.Width;
            double y = (double)grayBitmap.Height / (double)pictureBox1.Height;
            mousePos.X = (int)((int)e.Location.X * x);
            mousePos.Y = (int)((int)e.Location.Y * y);
            zoomCenter = mousePos;
            int grayValue = grayBitmap.GetPixel(mousePos.X, mousePos.Y).R;
            // 显示灰度值和坐标
            label1.Text = $"X: {mousePos.X}, Y: {mousePos.Y}, Gray: {grayValue}";

            zoomedBitmap = CreateZoomedBitmap(originalBitmap, zoomCenter, cropSize, 2); // 放大2倍    
            if (followerPictureBox == null)
            {
                followerPictureBox = new PictureBox
                {
                    Size = new Size(zoomedBitmap.Width, zoomedBitmap.Height),
                    BorderStyle = BorderStyle.Fixed3D, // 可选：为了更容易看到边界
                    BackColor = Color.White // 可选：设置背景色以突出图像

                };
                this.Controls.Add(followerPictureBox); // 将Panel添加到窗体上
                // 注意：这里不将followerPictureBox添加到窗体的Controls集合中
                // 因为我们想要手动控制其位置和显示
            }
            // 设置跟随PictureBox的图像和位置
            followerPictureBox.Image = zoomedBitmap;

            followerPictureBox.Location = new Point(e.X + 20, e.Y + 100); // 将Panel定位到鼠标位置
            followerPictureBox.Visible = true; // 显示Panel
            followerPictureBox.BringToFront();

        }
        public Bitmap CreateZoomedBitmap(Bitmap original, Point zoomCenter, int zoomAreaSize, float zoomFactor)
        {
            // 确保zoomAreaSize是偶数，否则在缩放时可能会出现不对称
            if (zoomAreaSize % 2 != 0) zoomAreaSize++;

            // 计算裁剪区域的左上角和右下角坐标
            int left = Math.Max(0, zoomCenter.X - zoomAreaSize / 2);
            int top = Math.Max(0, zoomCenter.Y - zoomAreaSize / 2);
            int right = Math.Min(original.Width, zoomCenter.X + zoomAreaSize / 2);
            int bottom = Math.Min(original.Height, zoomCenter.Y + zoomAreaSize / 2);

            // 创建一个新的Bitmap，用于存储放大后的图像
            int zoomedWidth = (int)(zoomAreaSize * zoomFactor);
            int zoomedHeight = (int)(zoomAreaSize * zoomFactor);
            Bitmap zoomedBitmap = new Bitmap(zoomedWidth, zoomedHeight);

            // 创建一个Graphics对象，用于在zoomedBitmap上绘制放大的图像
            using (Graphics graphics = Graphics.FromImage(zoomedBitmap))
            {
                // 设置高质量插值法，以便在缩放时获得更好的图像质量
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // 创建一个矩形，表示原始图像上的裁剪区域
                Rectangle sourceRect = new Rectangle(left, top, right - left, bottom - top);

                // 创建一个矩形，表示目标Bitmap上的绘制区域（整个zoomedBitmap）
                Rectangle destRect = new Rectangle(0, 0, zoomedWidth, zoomedHeight);

                // 绘制放大的图像
                graphics.DrawImage(original, destRect, sourceRect, GraphicsUnit.Pixel);
            }

            // 返回放大后的Bitmap
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
            //dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "图像文件(*.jpg;*.jpeg;*.bmp)|*.jpg;*.jpeg;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                originalBitmapL = new Bitmap(dialog.FileName);
                log.Items.Add(dialog.FileName);
                pictureBoxL.Image = originalBitmapL;
            }
            else
                log.Items.Add("用户已取消");
        }

        private void btnLoadR_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "图像文件(*.jpg;*.jpeg;*.bmp)|*.jpg;*.jpeg;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                originalBitmapR = new Bitmap(dialog.FileName);
                log.Items.Add(dialog.FileName);
                pictureBoxR.Image = originalBitmapR;
            }
            else
                log.Items.Add("用户已取消");

        }

        private void btnM_Click(object sender, EventArgs e)
        {
            int height = Math.Max(originalBitmapL.Height, originalBitmapR.Height);
            int width = originalBitmapL.Width + originalBitmapR.Width;

            Bitmap combined = new Bitmap(width, height);

            // 创建一个Graphics对象来绘制图片
            using (Graphics g = Graphics.FromImage(combined))
            {
                // 清除背景（可选）
                g.Clear(Color.White);

                // 绘制第一张图片
                g.DrawImage(originalBitmapL, 0, 0);

                // 绘制第二张图片在第一张图片的下方
                g.DrawImage(originalBitmapR, originalBitmapL.Width, 0);
            }

            // 将合并后的图片显示在PictureBox上
            pictureBoxM.Image = combined;



        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "图像文件(*.jpg;*.jpeg;*.bmp)|*.jpg;*.jpeg;*.bmp";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                originalBitmapCut = new Bitmap(dialog.FileName);
                ImagePath = dialog.FileName;
                log.Items.Add(ImagePath);
                pictureBox2.Image = originalBitmapCut;


                originalImageRectangle = new Rectangle(0, 0, originalBitmapCut.Width, originalBitmapCut.Height);

            }
            else
                log.Items.Add("用户已取消");
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
                // 裁剪图像
                Bitmap croppedImage = CropImage(pictureBox2.Image, actualCropArea);
                pictureBox5.Image = croppedImage; // 显示截图
                // 显示裁剪后的图像
                pictureBox5.Image = croppedImage;

                // 重置开始点
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
                    pictureBox2.Invalidate(); // 强制重绘以更新矩形框
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


        // 截图方法的示例（可选）
        private Bitmap CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        // 将截图区域从PictureBox坐标转换为原始图像坐标
        private Rectangle ConvertToOriginalImageCoordinates(Rectangle selection)
        {
            // 这里需要根据StretchImage的拉伸方式来计算转换
            // 这里仅提供一个简单的比例计算示例
            float scaleX = (float)originalImageRectangle.Width / pictureBox2.ClientSize.Width;
            float scaleY = (float)originalImageRectangle.Height / pictureBox2.ClientSize.Height;

            int left = (int)(selection.Left * scaleX);
            int top = (int)(selection.Top * scaleY);
            int right = (int)(selection.Right * scaleX);
            int bottom = (int)(selection.Bottom * scaleY);

            // 确保转换后的坐标在原始图像范围内
            left = Math.Max(0, left);
            top = Math.Max(0, top);
            right = Math.Min(originalImageRectangle.Width, right);
            bottom = Math.Min(originalImageRectangle.Height, bottom);

            return new Rectangle(left, top, right - left, bottom - top);
        }

    }
}
