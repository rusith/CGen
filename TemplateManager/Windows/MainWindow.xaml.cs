using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using TemplateManager.Models;

namespace TemplateManager.Windows
{

    public partial class MainWindow : Window
    {
        public FileInfo TemplateFile { get; set; }
        public List<Template> Templates { get; set; }
        public Template CurrentTemplate { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            const string settingsFile = "\\.settings";
            if (File.Exists(settingsFile))
            {
                var content = File.ReadAllText(settingsFile);
                var file = new FileInfo(content);
                if (file.Exists)
                {
                    TemplateFile = file;
                    return;
                }
            }

            var fileDialog = new OpenFileDialog();
            var result = fileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                TemplateFile = new FileInfo(fileDialog.FileName);
                ReadTemplate();
                DataContext = this;
            }
            else Close();
        }

        private void ReadTemplate()
        {
            var lines = File.ReadLines(TemplateFile.FullName).ToList();// Resources.templates.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            if (lines.Count < 1)
                return;

            Templates = new List<Template>();

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];

                if (line.Length <= 2 || !line.StartsWith("##"))
                    continue;

                var currentTemplateName = line.Substring(2);
                var tabCount = 0;
                if (currentTemplateName.Contains("$"))
                {
                    var index = currentTemplateName.IndexOf("$", StringComparison.Ordinal) + 1;
                    tabCount = int.Parse(currentTemplateName.Substring(index));
                }
                var currentTemplateBuilder = new StringBuilder();
                string tabs = null;
                while (i < (lines.Count - 1))
                {
                    line = lines[i];
                    if (line == "###")
                        break;

                    if (tabCount > 0)
                        currentTemplateBuilder.Append(tabs == null ? (tabs = CreateTabs(tabCount)) : tabs);
                    currentTemplateBuilder.AppendLine(line);
                    i++;
                }
                Templates.Add(new Template() {Name = currentTemplateName,TabCount = tabCount,Content = currentTemplateBuilder.ToString()});
            }
        }

        private static string CreateTabs(int count)
        {
            if (count < 1)
                return "";
            var builder = new StringBuilder();
            for (var i = 0; i < count; i++)
                builder.Append("\t");
            return builder.ToString();
        }
    }
}
