using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ThumbnailBuilder {
    /// <summary>
    /// This class is a wrapper of creating thumbnail image of strategy.
    /// Thumbnail picture is just a scale-down image and its aspect ratio is not changed.
    /// </summary>
    public class Thumbnail : IDisposable {
        private Image baseImage;
        private int thumbnailWidth;
        private int thumbnailHeight;
        private Color backgroundColor = Color.White;
        private Bitmap resultImage;
        private Graphics g;
        private bool isBuilded = false;
        private bool isDisposed = false;

        protected Thumbnail(Image baseImage, int thumbnailWidth, int thumbnailHeight) {
            if (thumbnailWidth <= 0 || thumbnailHeight <= 0) throw new ArgumentException("size of thumbnail shouldn't less or equal to 0 value.");

            this.baseImage = baseImage;
            resultImage = new Bitmap(thumbnailWidth, thumbnailHeight);
            g = Graphics.FromImage(resultImage);
            this.thumbnailWidth = thumbnailWidth;
            this.thumbnailHeight = thumbnailHeight;
        }

        public Graphics Graphics {
            get { return g; }
        }

        public Color BackgroundColor {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        /// <summary>
        /// Create a scale downed image(its x and y ratio is kept) instance.
        /// If call this method, the caller must dispose created Image class instance after use.
        /// </summary>
        /// <returns></returns>
        public Image Build() {
            CreateThumbnail();
            return (Image)resultImage.Clone();
        }

        /// <summary>
        /// Create a Thumbnail class instance.
        /// </summary>
        /// <exception>InvalidOperationException</exception>
        public static Thumbnail FromFile(string fileName, int thumbnailWidth, int thumbnailHeight) {
            if (fileName == null) throw new ArgumentNullException("fileName shouldn't null.");

            Image baseImage = Image.FromFile(fileName);
            return new Thumbnail(baseImage, thumbnailWidth, thumbnailHeight);
        }

        public static Thumbnail FromImage(Image baseImage, int thumbnailWidth, int thumbnailHeight) {
            if (baseImage == null) throw new NullReferenceException("baseImage shouldn't null.");
            return new Thumbnail(baseImage, thumbnailWidth, thumbnailHeight);
        }

        /// <summary>
        /// Create a Thumbnail class instance.
        /// Returns null value instead of thrown an exception when any error occurred.
        /// </summary>
        /// <returns></returns>
        public static Thumbnail TryFromFile(string fileName, int thumbnailWidth, int thumbnailHeight) {
            try {
                return FromFile(fileName, thumbnailWidth, thumbnailHeight);
            } catch {
                return null;
            }
        }

        /// <summary>
        /// Origin of this method was borrowed from http://www.atmarkit.co.jp/fdotnet/dotnettips/336listviewimage/listviewimage.html
        /// </summary>
        private void CreateThumbnail() {
            g.FillRectangle(new SolidBrush(backgroundColor), 0, 0, thumbnailWidth, thumbnailHeight);

            float fw = (float)thumbnailWidth / (float)baseImage.Width;
            float fh = (float)thumbnailHeight / (float)baseImage.Height;

            float scale = Math.Min(fw, fh);
            fw = baseImage.Width * scale;
            fh = baseImage.Height * scale;

            g.DrawImage(baseImage, (thumbnailWidth - fw) / 2, (thumbnailHeight - fh) / 2, fw, fh);
        }

        public void Dispose() {
            if (!isDisposed) {
                baseImage.Dispose();
                g.Dispose();
                if (!isBuilded) resultImage.Dispose();
            }
            isDisposed = true;
        }
    }
}
