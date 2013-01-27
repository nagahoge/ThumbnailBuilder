==========================
= このライブラリについて =
==========================

ThumbnailBuilderは、ファイルまたはイメージからサムネイルを作成する、ただそれだけのライブラリです。

使い方はシンプル。
次に例を紹介します。

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

もしもあなたがサムネイルを作成するロジックをよくコピーしているなら、
このライブラリを使用して頂けるとうれしいです。

より詳しい情報はGitHubでご覧下さい。
=>https://github.com/nagahoge/ThumbnailBuilder


==================
= テストについて =
==================

このライブラリを利用することにより、サムネイル作成部分のアルゴリズムのテストを毎回行う必要が無くなります。
あなたはあなたの仕事に集中することができるようになります！


==============
= ライセンス =
==============

このライブラリはMIT Licenseでライセンスされています。
ライブラリの使用は自由ですが、作者に責任は生じません。

もしもあなたがこのライブラリを再配布したい場合、
原作者の情報を含めなければいけません。
