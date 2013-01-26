using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThumbnailBuilder;

namespace ThumbnailBuilderTest
{
    [TestClass()]
    public class ThumbnailBuilderTest
    {
        private const string red_white_1000x500 = "red_white_1000x500.png";
        private const string yellow_black_111x333 = "yellow_black_111x333.jpg";
        private const string gif = "sample.gif";
        private const string tif = "sample.tif";
        private const string bmp24bit = "sample24bit.bmp";
        private const string bmp256colors = "sample256colors.bmp";
        private const string bmp16colors = "sample16colors.bmp";
        private const string bmpMonochrome = "sample_monochrome.bmp";
        private const string notExist = "NotExist.gif";


        private static Color red = Color.FromArgb(255, 255, 0, 0);
        private static Color white = Color.FromArgb(255, 255, 255, 255);
        private static Color black = Color.FromArgb(255, 0, 0, 0);
        private static Color blue = Color.FromArgb(255, 0, 0, 255);
        private static Color yellow = Color.FromArgb(255, 255, 255, 0);
        
        [TestMethod()]
        // [ExpectedException(typeof(ArgumentNullException))] // not works...
        public void TestFromFileWithFileNameNull() {
            try {
                Thumbnail.FromFile(null, 10, 10);
                Assert.Fail();
            } catch (ArgumentNullException) { }
        }

        [TestMethod()]
        // [ExpectedException(typeof(ArgumentException))] // not works...
        public void TestFromFileWithThumbnailWidthZero() {
            try {
                Thumbnail.FromFile(gif, 0, 10);
                Assert.Fail();
            } catch (ArgumentException) { }
        }

        [TestMethod()]
        // [ExpectedException(typeof(ArgumentException))] // not works..
        public void TestFromFileWithThumbnailWidthNegative() {
            try {
                Thumbnail.FromFile(gif, -1, 10);
            } catch (ArgumentException) { }
        }

        [TestMethod()]
        // [ExpectedException(typeof(ArgumentException))] // not works...
        public void TestFromFileWithThumbnailHeightZero() {
            try {
                Thumbnail.FromFile(gif, 10, 0);
            } catch (ArgumentException) { }
        }

        [TestMethod()]
        // [ExpectedException(typeof(ArgumentException))] // not works..
        public void TestFromFileWithThumbnailHeightNegative() {
            try {
                Thumbnail.FromFile(gif, 10, -1);
            } catch (ArgumentException) { }
        }

        [TestMethod()]
        // [ExpectedException(typeof(FileNotFoundException))] // not works...
        public void TestFromFileWithFileNameThatIsNotExist() {
            try {
                Thumbnail.FromFile(notExist, 10, 10);
            } catch (FileNotFoundException) { }
        }

        [TestMethod()]
        public void TestBuild() {
            using (Thumbnail builder = Thumbnail.FromFile(red_white_1000x500, 1000, 500))
            using (Image image = builder.Build()) {
                Assert.AreEqual(1000, image.Width);
                Assert.AreEqual(500, image.Height);
                Bitmap bitmap = new Bitmap(image);
                Assert.AreEqual(Color.FromArgb(255, 255, 0, 0), bitmap.GetPixel(0, 0));
                Assert.AreEqual(Color.FromArgb(255, 255, 0, 0), bitmap.GetPixel(0, 500 - 1));
                Assert.AreEqual(Color.FromArgb(255, 255, 255, 255), bitmap.GetPixel(1000 - 1, 0));
                Assert.AreEqual(Color.FromArgb(255, 255, 255, 255), bitmap.GetPixel(1000 - 1, 500 - 1));
            }

            // +---------------------+---------------------+
            // |(0, 0)                             (499, 0)|
            // |                                           |
            // |                    blue                   |
            // |                                           |
            // |(0, 124)                         (499, 124)|
            // +---------------------+---------------------+
            // |(0, 125)   (249, 125)|(250, 125) (499, 125)|
            // |                     |                     |
            // |                     |                     |
            // |                     |                     |
            // |         red         |      white          |
            // |                     |                     |
            // |                     |                     |
            // |                     |                     |
            // |(0, 374)   (249, 374)|(250, 374) (499, 374)|
            // +---------------------+---------------------+
            // |(0, 375)                         (499, 375)|
            // |                                           |
            // |                    blue                   |
            // |                                           |
            // |(0, 499)                         (499, 499)|
            // +---------------------+---------------------+
            using (Thumbnail builder = Thumbnail.FromFile(red_white_1000x500, 500, 500)) {
                builder.BackgroundColor = blue;
                using (Image image = builder.Build())
                using (Bitmap bitmap = new Bitmap(image)) {
                    Assert.AreEqual(500, image.Width);
                    Assert.AreEqual(500, image.Height);

                    Assert.AreEqual(blue, bitmap.GetPixel(0, 0));
                    Assert.AreEqual(blue, bitmap.GetPixel(499, 0));
                    Assert.AreEqual(blue, bitmap.GetPixel(0, 124));
                    Assert.AreEqual(blue, bitmap.GetPixel(499, 124));

                    Assert.AreEqual(red, bitmap.GetPixel(0, 125));
                    Assert.AreEqual(red, bitmap.GetPixel(249, 125));
                    Assert.AreEqual(red, bitmap.GetPixel(0, 374));
                    Assert.AreEqual(red, bitmap.GetPixel(249, 374));

                    Assert.AreEqual(white, bitmap.GetPixel(250, 125));
                    Assert.AreEqual(white, bitmap.GetPixel(499, 125));
                    Assert.AreEqual(white, bitmap.GetPixel(250, 374));
                    Assert.AreEqual(white, bitmap.GetPixel(499, 374));

                    Assert.AreEqual(blue, bitmap.GetPixel(0, 375));
                    Assert.AreEqual(blue, bitmap.GetPixel(499, 375));
                    Assert.AreEqual(blue, bitmap.GetPixel(0, 499));
                    Assert.AreEqual(blue, bitmap.GetPixel(499, 499));
                }
            }


            // +--------------+--------------+--------------+
            // |(0,0)         |              |       (49, 0)|
            // |              |              |              |
            // |              |              |              |
            // |              |              |              |
            // |              |              |              |
            // |              |              |              |
            // |    white     |      yellow  |    white     |
            // |              |              |              |
            // |              |              |              |
            // |              |              |              |
            // |              |              |              |
            // |              |              |              |
            // |       (16,24)|(17, 24)      |              |
            // +--------------+--------------+--------------+
            // |      (16, 25)|(17, 25)      |              |
            // |              |              |              |
            // |              |              |              |
            // |              |              |              |
            // |              |              |              |
            // |              |              |              |
            // |    white     |       black  |    white     |
            // |              |              |              |
            // |              |              |              |
            // |              |              |              |
            // |              |              |              |
            // |              |              |              |
            // |(0, 49)       |              |      (49, 49)|
            // +--------------+--------------+--------------+
            using (Thumbnail builder = Thumbnail.FromFile(yellow_black_111x333, 50, 50))
            using (Image image = builder.Build())
            using (Bitmap bitmap = new Bitmap(image)) {
                Assert.AreEqual(50, image.Width);
                Assert.AreEqual(50, image.Height);
                
                Assert.AreEqual(white, bitmap.GetPixel(16, 24));
                Assert.AreEqual(yellow, bitmap.GetPixel(17, 24));
                Assert.AreEqual(white, bitmap.GetPixel(16, 25));
                Assert.AreEqual(black, bitmap.GetPixel(17, 25));
            }
        }

        [TestMethod()]
        public void TestTryFromFile() {
            Assert.IsNotNull(Thumbnail.TryFromFile(gif, 5, 5));
            Assert.IsNotNull(Thumbnail.TryFromFile(tif, 1, 1));
            Assert.IsNotNull(Thumbnail.TryFromFile(bmp24bit, 10, 10));
            Assert.IsNotNull(Thumbnail.TryFromFile(bmp256colors, 20, 20));
            Assert.IsNotNull(Thumbnail.TryFromFile(bmp16colors, 30, 30));
            Assert.IsNotNull(Thumbnail.TryFromFile(bmpMonochrome, 40, 40));
            Assert.IsNotNull(Thumbnail.TryFromFile(red_white_1000x500, 10, 10));
            Assert.IsNotNull(Thumbnail.TryFromFile(yellow_black_111x333, 10, 10));

            using (Thumbnail image = Thumbnail.TryFromFile("not_exist_filename.bmp", 10, 10)) {
                Assert.IsNull(image);
            }
        }

        [TestMethod()]
        public void TestFromImage() {
            using (Image baseImage = Image.FromFile(gif)) 
            using (Thumbnail builder = Thumbnail.FromImage(baseImage, 15, 15)) {
                Image image = builder.Build();
                Assert.IsNotNull(image);
                image.Dispose();
            }
        }

        [TestMethod()]
        public void TestFromImageWithBaseImageNull() {
            try {
                Thumbnail.FromImage(null, 15, 15);
                Assert.Fail();
            } catch { }
        }

        [TestMethod()]
        public void TestExpand() {
            using (Thumbnail builder = Thumbnail.FromFile(gif, 10000, 10000))
            using (Image image = builder.Build()) {
                Assert.AreEqual(10000, image.Width);
                Assert.AreEqual(10000, image.Height);
            }
        }

        [TestMethod()]
        public void TestMultiTimeBuild() {
            using (Thumbnail builder = Thumbnail.FromFile(gif, 100, 100)) {
                for (int i = 0; i < 10; i++) {
                    Image image = builder.Build();
                    image.Dispose();
                }
            }
        }
    }
}
