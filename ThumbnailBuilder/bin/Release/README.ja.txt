==========================
= ���̃��C�u�����ɂ��� =
==========================

ThumbnailBuilder�́A�t�@�C���܂��̓C���[�W����T���l�C�����쐬����A�������ꂾ���̃��C�u�����ł��B

�g�����̓V���v���B
���ɗ���Љ�܂��B

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

���������Ȃ����T���l�C�����쐬���郍�W�b�N���悭�R�s�[���Ă���Ȃ�A
���̃��C�u�������g�p���Ē�����Ƃ��ꂵ���ł��B

���ڂ�������GitHub�ł����������B
=>https://github.com/nagahoge/ThumbnailBuilder


==================
= �e�X�g�ɂ��� =
==================

���̃��C�u�����𗘗p���邱�Ƃɂ��A�T���l�C���쐬�����̃A���S���Y���̃e�X�g�𖈉�s���K�v�������Ȃ�܂��B
���Ȃ��͂��Ȃ��̎d���ɏW�����邱�Ƃ��ł���悤�ɂȂ�܂��I


==============
= ���C�Z���X =
==============

���̃��C�u������MIT License�Ń��C�Z���X����Ă��܂��B
���C�u�����̎g�p�͎��R�ł����A��҂ɐӔC�͐����܂���B

���������Ȃ������̃��C�u�������Ĕz�z�������ꍇ�A
����҂̏����܂߂Ȃ���΂����܂���B
