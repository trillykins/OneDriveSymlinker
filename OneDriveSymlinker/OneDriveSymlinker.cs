namespace WinFormsApp1
{
    public partial class OneDriveSymlinker : Form
    {
        private string userPath = string.Empty;
        private string oneDrivePath = string.Empty;
        private string username = string.Empty;
        private bool moveSaveFilesToOneDrive = false;

        public OneDriveSymlinker()
        {
            InitializeComponent();
            Text = nameof(OneDriveSymlinker);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            username = Environment.UserName;
            userPath = $"C:\\Users\\{username}\\Documents";
            oneDrivePath = $"C:\\Users\\{username}\\OneDrive";
            label1.Text = userPath;
            label2.Text = oneDrivePath;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = DirectoryPicker(userPath);
            if (string.IsNullOrEmpty(result)) return;
            userPath = result;
            label1.Text = userPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = DirectoryPicker(oneDrivePath);
            if (string.IsNullOrEmpty(result)) return;
            oneDrivePath = result;
            label2.Text = oneDrivePath;
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
                FileInfo dir = new FileInfo(userPath);
                oneDrivePath = $"{oneDrivePath}\\{dir.Name}";
                if (!Directory.Exists(userPath))
                {
                    MessageBox.Show("Save folder not found in user profile path!");
                    return;
                }
                if (Directory.Exists(oneDrivePath))
                {
                    MessageBox.Show("Folder already exists in OneDrive!");
                    return;
                }

                Directory.Move(userPath, oneDrivePath);
            }
            else
            {
                FileInfo dir = new FileInfo(oneDrivePath);
                userPath = $"{userPath}\\{dir.Name}";
                if (Directory.Exists(userPath))
                {
                    MessageBox.Show("Save folder already exists in user profile path!");
                    return;
                }
                if (!Directory.Exists(oneDrivePath))
                {
                    MessageBox.Show("Save folder does not exist in OneDrive!");
                    return;
                }
            }
            var result = Directory.CreateSymbolicLink(userPath, oneDrivePath);
            if (!(result.Exists && Directory.GetFiles(result.FullName).Length == Directory.GetFiles(oneDrivePath).Length))
            {
                throw new Exception("Symlink botched!");
            }
            MessageBox.Show($"Symlink from {oneDrivePath} to {userPath} successfully created!");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            moveSaveFilesToOneDrive = checkBox1.Checked;
            if (moveSaveFilesToOneDrive)
            {
                label3.Text = "This will move save files from user profile to OneDrive and create symlink";
            }
            else
            {
                label3.Text = "This will create symlink from save files already in OneDrive to user profile";
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}