using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChatBot
{
    /// <summary>
    /// Lógica de interacción para Configuracion.xaml
    /// </summary>
    public partial class Configuracion : Window
    {
        public Color ColorFondo { get; set; }
        public Color ColorUsuario { get; set; }
        public Color ColorRobot { get; set; }
        public string Sexo { get; set; }
        public Configuracion()
        {
            InitializeComponent();
            DataContext = this;

            fondoComboBox.ItemsSource = typeof(Colors).GetProperties();
            usuarioComboBox.ItemsSource = typeof(Colors).GetProperties();
            robotComboBox.ItemsSource = typeof(Colors).GetProperties();
            ColorFondo = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.colorFondo);
            ColorUsuario = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.colorUsuario);
            ObservableCollection<string> sexos = new ObservableCollection<string>{ "Hombre" , "Mujer" };
            sexoComboBox.ItemsSource = sexos;
            Sexo = Properties.Settings.Default.sexo;
        }

        private void Button_Click_Aceptar(object sender, RoutedEventArgs e)
        {
            ColorFondo = (Color)(fondoComboBox.SelectedItem as PropertyInfo).GetValue(null, null);
            ColorUsuario = (Color)(usuarioComboBox.SelectedItem as PropertyInfo).GetValue(null, null);
            ColorRobot = (Color)(robotComboBox.SelectedItem as PropertyInfo).GetValue(null, null);
            Sexo = sexoComboBox.SelectedItem.ToString().Substring(0, 1);
            DialogResult = true;
        }
    }
}
