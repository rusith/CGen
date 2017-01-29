using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private MainWindowViewModel Model;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            var settingsFile = Path.Combine(Environment.CurrentDirectory, ".settings");
            if (File.Exists(settingsFile))
            {
                var content = File.ReadAllText(settingsFile);
                var file = new FileInfo(content);
                if (file.Exists)
                {
                    TemplateFile = file;
                    ReadTemplateFile();
                    return;
                }
            }

            OpenFile();
            ReadTemplateFile();
        }

        private void OpenFile()
        {
            var fileDialog = new OpenFileDialog();
            var result = fileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                TemplateFile = new FileInfo(fileDialog.FileName);
            }
            else Close();
        }

        private void ReadTemplateFile()
        {
            var templates = ReadTemplate();
            Model = new MainWindowViewModel { Templates = templates };
            Model.CurrentTemplate = templates.First();
            DataContext = Model;
        }

        private List<Template> ReadTemplate()
        {
            var lines = File.ReadLines(TemplateFile.FullName).ToList();// Resources.templates.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            if (lines.Count < 1)
                return null;

            var templates = new List<Template>();

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
                    currentTemplateName = currentTemplateName.Substring(0,index-1);
                }

                i++;

                var currentTemplateBuilder = new StringBuilder();
                while (i < (lines.Count - 1))
                {
                    line = lines[i];
                    if (line == "###")
                        break;
                    currentTemplateBuilder.AppendLine(line);
                    i++;
                }
                templates.Add(new Template() {Name = currentTemplateName,TabCount = tabCount,Content = currentTemplateBuilder.ToString()});
            }
            return templates;
        }

        private void EditInTextEditorButton_Click(object sender, RoutedEventArgs e)
        {
            if (Model.CurrentTemplate == null)
                return;

            var tempPath = Path.GetTempPath();
            var fileName = Model.CurrentTemplate.Name + "-Temp.cs";
            var fullPath = Path.Combine(tempPath, fileName);
            File.WriteAllText(fullPath,Model.CurrentTemplate.Content);
            Process.Start(fullPath);

            var watcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(fullPath),
                Filter = Path.GetFileName(fullPath)
            };

            watcher.Changed += OnTempFileChanged;
            watcher.EnableRaisingEvents = true;
        }

        private void OnTempFileChanged(object source, FileSystemEventArgs e)
        {
            using (var stream = File.Open(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                        Model.CurrentTemplate.Content = reader.ReadToEnd();
                }
            }
        }

        private void SaveBitton_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {

            var builder = new StringBuilder();
            foreach (var item in Model.Templates)
            {
                builder.Append("##");
                builder.Append(item.Name);
                if (item.TabCount > 0)
                    builder.Append("$" + item.TabCount);
                builder.AppendLine();
                builder.Append(item.Content);
                builder.Append("###");
                builder.AppendLine();
            }
            File.WriteAllText(TemplateFile.FullName, builder.ToString());
            var settingsFile = Path.Combine(Environment.CurrentDirectory, ".settings");
            File.WriteAllText(settingsFile, TemplateFile.FullName);
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
            ReadTemplateFile();
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(NewTextBox.Text))
                return;
            var name = NewTextBox.Text.ToUpper();
            var template = new Template {Name = name, Content = Environment.NewLine, TabCount = 0};
            Model.Templates.Add(template);
            Model.CurrentTemplate = template;
            Save();
            ReadTemplateFile();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Model.Templates.Remove(Model.Templates.FirstOrDefault(t => t.Name==Model.CurrentTemplate.Name));
            Model.CurrentTemplate = Model.Templates.First();
            Save();
            ReadTemplateFile();
        }
    }
}
