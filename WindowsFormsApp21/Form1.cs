using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp21
{
    public partial class Form1 : Form
    {
        circle[] cirс;
        public Form1()
        {
            InitializeComponent();
            //создание объектов
            cirс = new circle[4];
            cirс[0] = new circle(new Point(0, 100), "Right", false, Brushes.Red);
            cirс[1] = new circle(new Point(1085, 200), "Left", true, Brushes.Blue);
            cirс[2] = new circle(new Point(0, 300), "Right", true, Brushes.Red);
            cirс[3] = new circle(new Point(1085, 400), "Left", true, Brushes.Blue);
            //подписка на обработчик события
            cirс[0].stopm += cirс[1].StopMove;
            cirс[1].stopm += cirс[2].StopMove;
            cirс[2].stopm += cirс[3].StopMove;
            cirс[3].stopm += cirс[0].StopMove;
        }
        //Рисование всех объектов
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            cirс[0].draw(e.Graphics);
            cirс[1].draw(e.Graphics);
            cirс[2].draw(e.Graphics);
            cirс[3].draw(e.Graphics);
        }
        //Происходит по истечении заданного интервала таймера при условии, что таймер включен
        private void timer1_Tick(object sender, EventArgs e)
        { 
            cirс[0].Move();
            cirс[1].Move();
            cirс[2].Move();
            cirс[3].Move();
            Invalidate();
        }
        //запуск таймера
        private void стартToolStripMenuItem_Click(object sender, EventArgs timer1_Tick)
        {
            timer1.Start();
        }
        //остановка таймера
        private void стопToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        //Класс кругов
        class circle
        {
            public Point position;
            Brush color;
            Size size;
            bool stop;
            public event StopDelegate stopm;
            public delegate void StopDelegate();
            string direction;
            public circle(Point beginposition, string Direction, bool Stop, Brush Color)
            {
                color = Color;
                size = new Size(100, 100);
                position = beginposition;
                direction = Direction;
                stop = Stop;
            }
            public void draw(Graphics context)
            {
                context.FillEllipse(color, new Rectangle(position, size));
            }
            public void Move()
            {
                if (stop == false)
                {
                    if (direction == "Right")
                    {
                        
                        position.X += 5;
                        if (position.X == 1090)
                        {
                            stopm();
                            stop = true;
                            direction = "Left";
                        }

                    }
                    if (direction == "Left")
                    {
                        position.X -= 5;
                        if (position.X == 0)
                        {
                            stopm();
                            stop = true;
                            direction = "Right";
                        }
                    }
                }
            }
            public void StopMove()
            {
                stop = false;
            }
        }
        //Информация о разработчике
        private void информацияОРазработчикеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выполнил: студент группы 4207 Антаев М.П.\nВариант: №3", "Информация о разработчике");
        }
    }
}
