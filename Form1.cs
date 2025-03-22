namespace WinFormsApp2
{
    using System.Reflection;
    using System.Threading;
    public partial class Form1 : Form
    {
        private Conway conway;
        private int cellSize;
        public Form1()
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                                    | BindingFlags.Instance | BindingFlags.NonPublic, null,
                                    panel1, new object[] { true });
            conway = new(50, 50);
            conway.isLooped = checkBox1.Checked;
            timer1.Interval = 100 - trackBar1.Value;
            cellSize = trackBar2.Value;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            bool[,] grid = conway.GetBoard();

            // Loop through each element in the grid.
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    // Calculate the rectangle's position.
                    int y = col * cellSize;
                    int x = row * cellSize;

                    // Choose a color based on the boolean value.
                    Brush brush = grid[row, col] ? Brushes.Black : Brushes.White;
                    e.Graphics.FillRectangle(brush, x, y, cellSize, cellSize);

                    // Draw a border around the cell for grid lines.
                    e.Graphics.DrawRectangle(Pens.Black, x, y, cellSize, cellSize);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                timer1.Enabled = false;
            else
                timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int gen = conway.NextTick();
            panel1.Invalidate();
            label1.Text = $"Generation: {gen}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = 100 - trackBar1.Value;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / cellSize;
            int y = e.Y / cellSize;
            if (!(x > conway.mX - 1 || y > conway.mY - 1))
            {
                if (timer1.Enabled)
                    timer1.Enabled = false;
                conway.Toggle(x, y);
                panel1.Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
                timer1_Tick(new object(), new EventArgs());
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            cellSize = trackBar2.Value;
            panel1.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            conway.isLooped = checkBox1.Checked;
        }
    }
}
