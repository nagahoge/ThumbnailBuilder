ThumbnailBuilder
================

Just a small library only to build a thumbnail image from file or base-image.

General people copies samplecodes of thumbnailing algorithm from web-sites, 
so there are much copie codes derived from specific web-sites.

This is not a reinventing the wheel but at least remaking the wheel.
So, there is a necessity of testing them.

Now, I made a small library and testing it, there is no need to re-testing.
You just use it.


## Download

You can [download pre-compiled dll file](https://s3.amazonaws.com/nagahoge/github/thumbnail-builder/ThumbnailBuilder_v1.0.zip) (Compiled for .NET Framework4 Extended).


## Usage

The usage is simple.

This is a sample code to use this library.

    using System;
    using System.Windows.Forms;
    using ThumbnailBuilder;
    
    namespace UseThumbnailBuilder {
        public partial class Form1 : Form {
            public Form1() {
                InitializeComponent();
            }
    
            private void Form1_Load(object sender, EventArgs e) {
                const string filePath = @"sample.jpg";
                using (Thumbnail builder = Thumbnail.FromFile(filePath, pictureBox.Width, pictureBox.Height)) {
                    pictureBox.Image = builder.Build();
                }
            }
        }
    }

ThumbnailBuilder rely on System.Drawing.Image.FromFile(string) method so 
Thumnail.FromFile method also depend on its behavior.


## License

ThumbnaiBuilder is released under the MIT License.
You can use this freely but there is some restriction on redistributing.
