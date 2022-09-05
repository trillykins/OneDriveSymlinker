namespace WinFormsApp1
{
    public partial class OneDriveSymlinker : Form
    {
        private string path = string.Empty;
        private string targetPath = string.Empty;
        private string username = string.Empty;
        private bool moveSaveFilesToOneDrive = false;

        public OneDriveSymlinker()
        {
            InitializeComponent();
            Text = "OneDriveSymlinker";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            username = Environment.UserName;
            path = $"C:\\Users\\{username}\\Documents\\";
            targetPath = $"C:\\Users\\{username}\\OneDrive";
            label1.Text = path;
            label2.Text = targetPath;
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = DirectoryPicker(path);
            if (string.IsNullOrEmpty(result)) return;
            path = result;
            label1.Text = path;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = DirectoryPicker(targetPath);
            if (string.IsNullOrEmpty(result)) return;
            targetPath = result;
            label2.Text = targetPath;
        }

        private string DirectoryPicker(string? initialPath = null)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (initialPath != null) fbd.InitialDirectory = initialPath;
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
            }
            return string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (moveSaveFilesToOneDrive)
            {
                // Directory.Move(string, string)
                // CreateSymbolicLink(string, string)
                // Verify shit
            }
            else
            {
                // CreateSymbolicLink(string, string)
                // Verify shit
            }
            FileInfo dir = new FileInfo(targetPath);
            path = $"{path}{dir.Name}";
            if (Directory.Exists(path))
            {
                MessageBox.Show($"Symlink already exists!");
                return;
            }
            var result = Directory.CreateSymbolicLink(path, targetPath);
            if (!(result.Exists && Directory.GetFiles(result.FullName).Length == Directory.GetFiles(targetPath).Length))
            {
                throw new Exception("Symlink botched!");
            }
            MessageBox.Show($"Symlink from {targetPath} to {path} successfully created!");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            moveSaveFilesToOneDrive = checkBox1.Checked;
        }
    }
}