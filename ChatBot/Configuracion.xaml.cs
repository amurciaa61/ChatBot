using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            ObservableCollection<string> sexos = new ObservableCollection<string>{ "Mujer", "Hombre" };
            sexoComboBox.ItemsSource = sexos;
        }

        private void fondoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorFondo = (Color)(fondoComboBox.SelectedItem as PropertyInfo).GetValue(null,null);
        }
        private void usuarioComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorUsuario = (Color)(usuarioComboBox.SelectedItem as PropertyInfo).GetValue(null, null);
        }
        private void robotComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorRobot = (Color)(robotComboBox.SelectedItem as PropertyInfo).GetValue(null, null);
        }
        private void sexoComboBox_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            Sexo = sexoComboBox.SelectedItem.ToString().Substring(0,1);
        }

        private void Button_Click_Aceptar(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
