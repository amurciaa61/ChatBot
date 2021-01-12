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
            ObservableCollection<string> sexos = new ObservableCollection<string>{ "Hombre" , "Mujer" };
            sexoComboBox.ItemsSource = sexos;
        }

        private void FondoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorFondo = (Color)(fondoComboBox.SelectedItem as PropertyInfo).GetValue(null,null);
        }
        private void UsuarioComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorUsuario = (Color)(usuarioComboBox.SelectedItem as PropertyInfo).GetValue(null, null);
        }
        private void RobotComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorRobot = (Color)(robotComboBox.SelectedItem as PropertyInfo).GetValue(null, null);
        }
        private void SexoComboBox_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            Sexo = sexoComboBox.SelectedItem.ToString().Substring(0,1);
        }

        private void Button_Click_Aceptar(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
