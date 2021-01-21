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
            if (origen != "Mujer" && origen != "Hombre" && origen != "Bot")
                throw new ArgumentException("Origen de mensaje debe ser de tipo Mujer, Hombre, Bot");
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
        public override string ToString()
        {
            string texto = (Origen == "Bot"  ? "Robot":"Usuario")+"\n"+Texto;

            return texto;
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
