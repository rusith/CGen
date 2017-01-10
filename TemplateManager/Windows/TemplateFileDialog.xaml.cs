using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace TemplateManager.Windows
{

    public partial class TemplateFileDialog : Window
    {
        public FileInfo SelectedFile { get; set; }

        public TemplateFileDialog()
        {
            InitializeComponent();
        }

        private void BrowseButton_OnClick(object sender, RoutedEventArgs e)
        {
            const string settingsFile = "\\.settings";
            if (File.Exists(settingsFile))
            {
                var content = File.ReadAllText(settingsFile);
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var file = new FileInfo(content);
                    if (file.Exists)
                        SelectedFile = file;
                }
            }
            var fileDialog = new OpenFileDialog { Title = "Select template file" };
            var result = fileDialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK)
                return;
            SelectedFile = new FileInfo(fileDialog.FileName);
            DialogResult = true;
        }
    }
}
