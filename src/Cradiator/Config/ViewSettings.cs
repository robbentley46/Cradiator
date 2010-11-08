using System.ComponentModel;
using Cradiator.Extensions;

namespace Cradiator.Config
{
    public class ViewSettings : IViewSettings, INotifyPropertyChanged
    {
        public string ID { get; set; }

        protected string _url;
        public string URL
        {
            get { return _url; }
            set
            {
                if (_url == value) return;
                _url = value;
                Notify("URL");
            }
        }

        protected string _skinName;
        public string SkinName
        {
            get { return _skinName; }
            set
            {
                if (_skinName == value) return;
                _skinName = value;
                Notify("SkinName");
            }
        }

        protected string _projectNameRegEx;
        public string ProjectNameRegEx
        {
            get { return _projectNameRegEx.GetRegEx(); }
            set
            {
                if (_projectNameRegEx == value) return;
                _projectNameRegEx = value;
                Notify("ProjectNameRegEx");
            }
        }

        protected string _categoryRegEx;
        public string CategoryRegEx
        {
            get { return _categoryRegEx.GetRegEx(); }
            set
            {
                if (_categoryRegEx == value) return;
                _categoryRegEx = value;
                Notify("CategoryRegEx");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}