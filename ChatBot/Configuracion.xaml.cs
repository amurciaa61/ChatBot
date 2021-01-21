using System.Collections.ObjectModel;

using System.Windows;

using System.Windows.Media;

namespace ChatBot
{
    /// <summary>
    /// Lógica de interacción para Configuracion.xaml
    /// </summary>
    public partial class Configuracion : Window
    {
        public string ColorFondo { get; set; }
        public string ColorUsuario { get; set; }
        public string ColorRobot { get; set; }
        public string Sexo { get; set; }
        public Configuracion()
        {
            InitializeComponent();
            DataContext = this;

            fondoComboBox.ItemsSource = typeof(Colors).GetProperties();
            usuarioComboBox.ItemsSource = typeof(Colors).GetProperties();
            robotComboBox.ItemsSource = typeof(Colors).GetProperties();
            ObservableCollection<string> sexos = new ObservableCollection<string>{ "Hombre" , "Mujer" };      
            sexoComboBox.ItemsSource = sexos;
        }

        private void Button_Click_Aceptar(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
