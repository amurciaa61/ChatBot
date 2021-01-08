using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ChatBot
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string MENSAJE_TEXTO_BOT = "Lo siento, estoy un poco cansado para hablar.";
        const string DIRECTORIO_DATOS = "Datos";
        ObservableCollection<Mensaje> mensajes = new ObservableCollection<Mensaje>();
        public MainWindow()
        {
            InitializeComponent();
            listaItemsControl.DataContext = mensajes;
        }

        private void CommandBinding_Executed_Conexion(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Conexión correcta con el servidor del Bot", "Comprobar conexión",MessageBoxButton.OK,MessageBoxImage.Information);
        }
        private void CommandBinding_CanExecuted_Conexion(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CommandBinding_Executed_Salir(object sender, ExecutedRoutedEventArgs e)
        {
             App.Current.Shutdown();
        }
        private void CommandBinding_CanExecuted_Salir(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_Nueva(object sender, ExecutedRoutedEventArgs e)
        {
            mensajes.Clear();
        }
        private void CommandBinding_CanExecuted_NuevaYGuardar(object sender, CanExecuteRoutedEventArgs e)
        {
            if (mensajes.Count > 0)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
        private void CommandBinding_Executed_Guardar(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = DirectorioActual(DIRECTORIO_DATOS),
                Filter = "Text files |*.txt",
                AddExtension = true,
                DefaultExt = "txt"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter sw = File.CreateText(saveFileDialog.FileName))
                    {
                        foreach (Mensaje mensaje in mensajes)
                        {
                            sw.WriteLine(mensaje.Origen == "U"?"Usuario":"Robot");
                            sw.WriteLine(mensaje.Texto);
                        }
                    }
                    MessageBox.Show("Generado Fichero txt: " + saveFileDialog.FileName,
                                    "Guardar conversación", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Errores", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        static private string DirectorioActual(string directorioFinal)
        {
            // Proponemos el directorio deseado para el OpenDialog, SaveDialog y Seleccion de imagenes
            string directorioActual = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            directorioActual = Path.GetDirectoryName(directorioActual);
            directorioActual += Path.DirectorySeparatorChar + directorioFinal;
            return directorioActual;
        }
        private void CommandBinding_CanExecute_Configuracion(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        private void CommandBinding_CanExecute_EnviarMensaje(object sender, CanExecuteRoutedEventArgs e)
        {
            if (mensajeTextBox.Text.Length > 0)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void CommandBinding_Executed_EnviarMensaje(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                mensajes.Add(new Mensaje(mensajeTextBox.Text, "U"));
                mensajes.Add(new Mensaje(MENSAJE_TEXTO_BOT, "B"));
                mensajeTextBox.Text = "";
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Errores", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
