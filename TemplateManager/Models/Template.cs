namespace TemplateManager.Models
{
    public class Template:ModelBase
    {
        private string _name;
        private string _content;
        private int _tabCount;

        public string Name { get { return _name; } set { _name = value;OnPropertyChanged("Name"); } }
        public string Content { get { return _content; } set { _content = value;OnPropertyChanged("Content"); } }
        public int TabCount { get { return _tabCount; } set { _tabCount = value;OnPropertyChanged("TabCount"); } }
    }
}
