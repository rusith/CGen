using System.Windows;
using System.Windows.Forms;
using System.IO;

namespace TemplateManager.Windows
{
    public partial class TemplateFileDialog : Window
    {
        public FileInfo SelectedFile { get; set; }
        public RootFolderDialog()
        {
            InitializeComponent();
        }

        private void OnBrowseButtonClick(object sender, RoutedEventArgs e)
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
            var fileDialog = new OpenFileDialog {Title = "Select template file"};
            var result = fileDialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK)
                return;
            SelectedFile = new FileInfo(fileDialog.FileName);
            DialogResult = true;
        }
    }
}
