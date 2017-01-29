
using System.Collections.Generic;

namespace TemplateManager.Models
{
    public class MainWindowViewModel : ModelBase
    {
        private List<Template> _templates;
        private Template _currentTemplate;
         
        public List<Template> Templates { get { return _templates; } set { _templates = value;OnPropertyChanged("Templates"); } }
        public Template CurrentTemplate { get { return _currentTemplate; }
            set { _currentTemplate = value; OnPropertyChanged("CurrentTemplate"); } }
    }
}
