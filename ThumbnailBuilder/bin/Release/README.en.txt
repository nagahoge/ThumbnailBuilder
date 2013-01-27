=================
= What is this? =
=================

ThumbnailBuilder is a library only to help creating thumbnail image from file or image.

The usage is simple.
Below is an example.

----------------------------------------
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
----------------------------------------

If you often copy and paste thumbnailing logick, I'm grad if you use this library.

More information is available on github.
=>https://github.com/nagahoge/ThumbnailBuilder


==========
= Tested =
==========

By using this library, you will not have to test thumbnailing algorighm every time.
You will be able to concentrate just at your work!


===========
= License =
===========

This library is published under MIT license.
You can use this library freely, but there is no guarantee on using this library.

If you want to redistribute this library, 
then you must responsible to include original author(nagahoge) information.

