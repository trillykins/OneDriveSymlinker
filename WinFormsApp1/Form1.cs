namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private string path;
        private string targetPath;
        private string username;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            username = Environment.UserName;
            path = $"C:\\Users\\{username}\\Documents\\";
            targetPath = $"C:\\Users\\{username}\\OneDrive";
            label1.Text = path;
            label2.Text = targetPath;

            //var splat = Directory.CreateSymbolicLink(path, targetPath);
            //if (splat.Exists)
            //{
            //    Console.WriteLine($"{splat.FullName} created!");
            //}
            //else
            //{
            //    Console.WriteLine($"{splat.FullName} was not created!");
            //}
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            targetPath = DirectoryPicker();
            label1.Text = targetPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            path = DirectoryPicker();
            label2.Text = path;
        }

        private string DirectoryPicker()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
            }
            throw new Exception("whatever");
        }
    }
}