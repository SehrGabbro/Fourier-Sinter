using System;
using System.Drawing;
using System.Windows.Forms;

namespace FourierSinter
{

    public partial class Form1 : Form
    {
        private readonly float pi = (float)Math.PI;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            draw_background();

            Fourier_chart.Update();

            int number_function;
            int period;
            int amplitude;

            try
            {
                amplitude = Int32.Parse(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Inserisci un ampiezza valida!", "Errore!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                period = Int32.Parse(textBox2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Inserire un periodo valido!", "Errore!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                number_function = Int32.Parse(textBox3.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Inserisci un numero di funzioni valido!", "Errore!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (number_function > 100000)
            {
                MessageBox.Show("Inserire un numero di funzioni che sia massimo uguale a 100.000!", "Errore!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            char type;
            if (comboBox1.Text == "Quadra") type = 'S';
            else if (comboBox1.Text == "Triangolare") type = 'T';
            else if (comboBox1.Text == "Dente di sega") type = 'W';
            else
            {
                MessageBox.Show("Scegliere un tipo di onda valido!", "Errore!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            button1.Enabled = false;
            draw_sine(amplitude, period, number_function, type);
            button1.Enabled = true;

        }

        private void draw_sine(int amplitude_percent, int period, int number_function, char type)
        {
            Graphics graphics = Fourier_chart.CreateGraphics();

            Pen red_pen = new Pen(Color.Red, 1.0F);
            Pen white_pen = new Pen(Color.GhostWhite, 1.0F);
            Pen yellow_pen = new Pen(Color.Yellow, 3.0F);
            Pen purple_pen = new Pen(Color.Purple, 2.0F);
            Pen magenta_pen = new Pen(Color.Magenta, 2.0F);

            bool armoniche_check = checkBox1.Checked == true;

            bool flag = false;
            float max_coefficent = 0;

            float coefficient = 0;
            float function_result = 0;

            if (armoniche_check)
            {
                graphics.DrawLine(purple_pen, 30, 20, 30, 20 + 150 + 20);
                graphics.DrawLine(purple_pen, 10, 170, 220 + 20, 170);
            }

            float y = 0;
            float lastY = 0;

            //calcolo in numero più grande
            float biggest_num = 0;

            for (int function = 0; function < number_function; ++function)
            {

                //Disegno max 10 funzioni
                if (function == 11) break;

                for (int x = 0; x <= Fourier_chart.Width; ++x)
                {

                    if (type == 'T') // Triangolare - Tringle
                    {
                        coefficient = 8 / ((float)Math.Pow((2 * function + 1), 2) * (float)Math.Pow(pi, 2));
                        function_result = (float)Math.Cos((2 * function + 1) * 2 * pi * period * x / Fourier_chart.Width);
                    }
                    else if (type == 'S') // Quadra - Square
                    {
                        coefficient = 4 / ((2 * function + 1) * pi);
                        function_result = (float)Math.Sin((2 * function + 1) * 2 * pi * period * x / Fourier_chart.Width);
                    }
                    else if (type == 'W') // Dente di sega - Sawtooth
                    {
                        coefficient = (float)Math.Pow(-1, function + 1) / ((function + 1) * pi);
                        function_result = (float)Math.Sin((function + 1) * 2 * pi * period * x / Fourier_chart.Width);
                    }

                    y = coefficient * function_result;

                    if (y > biggest_num)
                        biggest_num = y;

                }

            }

            for (int x = 0; x < Fourier_chart.Width; ++x)
            {

                y = 0;

                for (int function = 0; function < number_function; ++function)
                {

                    if (type == 'T') // Triangolare - Tringle
                    {
                        coefficient = 8 / ((float)Math.Pow((2 * function + 1), 2) * (float)Math.Pow(pi, 2));
                        function_result = (float)Math.Cos((2 * function + 1) * 2 * pi * period * x / Fourier_chart.Width);
                    }
                    else if (type == 'S') // Quadra - Square
                    {
                        coefficient = 4 / ((2 * function + 1) * pi);
                        function_result = (float)Math.Sin((2 * function + 1) * 2 * pi * period * x / Fourier_chart.Width);
                    }
                    else if (type == 'W') // Dente di sega - Sawtooth
                    {
                        coefficient = (float)Math.Pow(-1, function + 1) / ((function + 1) * pi);
                        function_result = (float)Math.Sin((function + 1) * 2 * pi * period * x / Fourier_chart.Width);
                    }

                    y += coefficient * function_result;

                }

                if (y > biggest_num)
                    biggest_num = y;
            }

            float amplitude = (Fourier_chart.Height / biggest_num) * amplitude_percent / 200;

            for (int function = 0; function < number_function; ++function)
            {

                //Disegno max 10 funzioni
                if (function == 11) break;

                for (int x = 0; x <= Fourier_chart.Width; x += 1)
                {

                    if (type == 'T') // Triangolare - Tringle
                    {
                        coefficient = 8 / ((float)Math.Pow( (2 * function + 1), 2) * (float)Math.Pow(pi, 2));
                        function_result = (float)Math.Cos( (2 * function + 1) * 2 * pi * period * x / Fourier_chart.Width );
                    }
                    else if (type == 'S') // Quadra - Square
                    {
                        coefficient = 4 / ((2 * function + 1) * pi);
                        function_result = (float)Math.Sin( (2 * function + 1) * 2 * pi * period * x / Fourier_chart.Width );
                    }
                    else if (type == 'W') // Dente di sega - Sawtooth
                    {
                        coefficient = (float)Math.Pow(-1, function + 1) / ( (function + 1) * pi);
                        function_result = (float)Math.Sin( (function + 1) * 2 * pi * period * x / Fourier_chart.Width );
                    }

                    y = amplitude * coefficient * function_result;

                    y += Fourier_chart.Height / 2;

                    if (x == 0)
                    {
                        graphics.DrawLine(red_pen, x, y, x, y);
                    }
                    else
                    {
                        graphics.DrawLine(red_pen, x - 1, lastY, x, y);
                    }

                    lastY = y;

                    if (x == 1 && armoniche_check)
                    {
                        if (!flag)
                        {
                            max_coefficent = 140 / coefficient;
                            flag = true;
                        }

                        graphics.DrawLine(magenta_pen, 40 + 10 * function, 170, 40 + 10 * function, 170 - coefficient * max_coefficent);
                    }

                }

            }

            //Calcolo somma sinusoidi
            for (int x = 0; x < Fourier_chart.Width; ++x)
            {

                y = 0;

                for (int function = 0; function < number_function; ++function)
                {

                    if (type == 'T') // Triangolare - Tringle
                    {
                        coefficient = 8 / ((float)Math.Pow((2 * function + 1), 2) * (float)Math.Pow(pi, 2));
                        function_result = (float)Math.Cos((2 * function + 1) * 2 * pi * period * x / Fourier_chart.Width);
                    }
                    else if (type == 'S') // Quadra - Square
                    {
                        coefficient = 4 / ((2 * function + 1) * pi);
                        function_result = (float)Math.Sin((2 * function + 1) * 2 * pi * period * x / Fourier_chart.Width);
                    }
                    else if (type == 'W') // Dente di sega - Sawtooth
                    {
                        coefficient = (float)Math.Pow(-1, function + 1) / ((function + 1) * pi);
                        function_result = (float)Math.Sin((function + 1) * 2 * pi * period * x / Fourier_chart.Width);
                    }

                    y += amplitude * coefficient * function_result;

                }

                y += Fourier_chart.Height / 2;

                if (x == 0)
                {
                    graphics.DrawLine(yellow_pen, x, y, x, y);
                }
                else
                {
                    graphics.DrawLine(yellow_pen, x - 1, lastY, x, y);
                }

                lastY = y;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = null;
            this.MinimumSize = new Size(this.Width, this.Height);
            this.ControlBox = false;
            this.DoubleBuffered = true;


            draw_background();
        }

        private void draw_background()
        {
            if (Fourier_chart.Width == 0 || Fourier_chart.Height == 0) return; //if you minimize without this if, return error cuz is trying to create bitmap with size 0

            Bitmap background = new Bitmap(Fourier_chart.Width, Fourier_chart.Height);

            Graphics graphics = Graphics.FromImage(background);

            graphics.FillRectangle(Brushes.Black, 0, 0, Fourier_chart.Width, Fourier_chart.Height);

            using (Font myFont = new Font("Arial", 12, FontStyle.Italic | FontStyle.Bold))
            {
                graphics.DrawString("Made by Gabriele Serli", myFont, new SolidBrush(Color.FromArgb(100, 231, 41, 57)), new Point(2, 2));
            }

            Fourier_chart.Image = background;
            Fourier_chart.Update();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            //draw_background();
        }

        //Move form
        private bool mouse_down;
        private Point last_location;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_down = true;
            last_location = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            if (mouse_down)
            {
                this.Location = new Point(this.Location.X - last_location.X + e.Location.X, this.Location.Y - last_location.Y + e.Location.Y);
            }
            
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_down = false;
        }
        //END

        // Controlbox
        private void close_button_MouseEnter(object sender, EventArgs e)
        {
            close_button.BackColor = Color.Red;
        }

        private void close_button_MouseLeave(object sender, EventArgs e)
        {
            close_button.BackColor = Color.Transparent;
        }

        private void maximize_button_MouseEnter(object sender, EventArgs e)
        {
            maximize_button.BackColor = Color.FromArgb(85, 105, 135);
        }

        private void maximize_button_MouseLeave(object sender, EventArgs e)
        {
            maximize_button.BackColor = Color.Transparent;
        }

        private void minimize_button_MouseEnter(object sender, EventArgs e)
        {
            minimize_button.BackColor = Color.FromArgb(85, 105, 135);
        }

        private void minimize_button_MouseLeave(object sender, EventArgs e)
        {
            minimize_button.BackColor = Color.Transparent;
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool maximized = false;
        private void maximize_button_Click(object sender, EventArgs e)
        {
            if (maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }

            maximized = !maximized;
        }


        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (!(this.Size.Width == 0 || this.Size.Height == 0))
                draw_background();
        }
        //END
    }
}
