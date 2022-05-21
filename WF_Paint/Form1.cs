using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_Paint
{
    public partial class Form1 : Form
    {
        private SolidBrush _brush;
        private Graphics _graphics;
        private bool _isDrawing;
        private bool _isRectangle;
        private bool _isEllipse;
        private bool _isLine;
        private Point _start;
        private Point _finish;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _brush = new SolidBrush(cPanel.BackColor);
            _graphics = paintPanel.CreateGraphics();

        }

        private void cPanel_DoubleClick(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                cPanel.BackColor = colorDialog1.Color;
                _brush.Color = colorDialog1.Color;
            }
        }

        private void paintPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isLine)
            {
                var _pen = new Pen(_brush.Color);
                _pen.Width = (float)trackBar1.Value;
                _graphics.DrawLine(_pen, _start,_finish);
            }
            _isDrawing = false;
            
        }

        private void paintPanel_MouseDown(object sender, MouseEventArgs e)
        {
            _isDrawing = true;
            if (_isRectangle)
            {
                _graphics.DrawRectangle(new Pen(_brush.Color), e.X, e.Y, (int)widNumber.Value * 10,
                    (int)lenNumber.Value * 10);
            }

            if (_isEllipse)
            {
                _graphics.DrawEllipse(new Pen(_brush.Color),e.X,e.Y,(int) radNumber.Value,(int) radNumber.Value);
            }

            if (_isLine)
            {
                _start = new Point(e.X, e.Y);
            }
        }

        private void paintPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing && !_isRectangle && !_isEllipse && !_isLine)
            {
                _graphics.FillEllipse(_brush, e.X, e.Y, trackBar1.Value, trackBar1.Value);
            }

            if (_isLine)
            {
                _finish = new Point(e.X, e.Y);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            _brush.Color = Color.White;

        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                _isRectangle = true;
                _isLine = false;
                _isEllipse = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
            else _isRectangle = false;
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                _isRectangle = false;
                _isLine = false;
                _isEllipse = true;
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }
            else _isEllipse = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                _isRectangle = false;
                _isLine = true;
                _isEllipse = false;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
            else _isLine = false;
        }
    }
}