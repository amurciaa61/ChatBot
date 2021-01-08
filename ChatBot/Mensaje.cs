using System;
using System.ComponentModel;

namespace ChatBot
{
    class Mensaje : INotifyPropertyChanged
    {
        private string _texto;
        private string _origen;

        public Mensaje(string texto, string origen)
        {
            if (origen != "U" && origen != "B")
                throw new ArgumentException("Origen de mensaje debe ser de tipo U o B");
            Texto = texto;
            Origen = origen;
        }

        public Mensaje()
        {
        }

        public string Texto
        {
            get { return _texto; }
            set
            {
                if (_texto != value)
                {
                    _texto = value;
                    NotifyPropertyChanged("Texto");
                }
            }
        }

        public string Origen
        {
            get {return _origen;}
            set
            {
                if (_origen != value)
                {
                    _origen = value;
                    NotifyPropertyChanged("Origen");
                }
            }
        }
            
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
