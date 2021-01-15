using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker;
using Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker.Models;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ChatBot
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string DIRECTORIO_DATOS = "Datos";
        const string MENSAJE_BOT_INACCESIBLE = "Lo siento, estoy un poco cansado para hablar.";
        bool hayConexion = true;
        string origen = Properties.Settings.Default.sexo;
        ObservableCollection<Mensaje> mensajes = new ObservableCollection<Mensaje>();
        public MainWindow()
        {
            InitializeComponent();
            listaItemsControl.DataContext = mensajes;
            if (MensajeRobot("hola") == null)
                hayConexion = false;
        }

        private async void CommandBinding_Executed_Conexion(object sender, ExecutedRoutedEventArgs e)
        {
            if (await MensajeRobot("hola") == null)
            {
                hayConexion = false;
                MessageBox.Show("No es posible conectar con el servidor del Bot",
                                "Comprobar conexión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                hayConexion = true;
                MessageBox.Show("Conexión correcta con el servidor del Bot", "Comprobar conexión",
                                 MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
                            sw.WriteLine(mensaje.ToString());
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

        private void CommandBinding_CanExecute_EnviarMensaje(object sender, CanExecuteRoutedEventArgs e)
        {
            if (mensajeTextBox.Text.Length > 0)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private async void CommandBinding_Executed_EnviarMensaje(object sender, ExecutedRoutedEventArgs e)
        {
            //MENSAJE_BOT_INACCESIBLE
            mensajes.Add(new Mensaje(mensajeTextBox.Text, origen));
            if (hayConexion)
            {
                mensajes.Add(new Mensaje("Procesando", "B"));
                mensajes[mensajes.Count - 1].Texto = await MensajeRobot(mensajeTextBox.Text);
            }
            else
                mensajes.Add(new Mensaje(MENSAJE_BOT_INACCESIBLE, "B"));
            mensajeTextBox.Text = "";

        }
        private void CommandBinding_CanExecute_Configuracion(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CommandBinding_Executed_Configuracion(object sender, ExecutedRoutedEventArgs e)
        {
            Configuracion configuracion = new Configuracion();
            configuracion.Owner = this;
            configuracion.ColorFondo = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.colorFondo);
            configuracion.ColorUsuario = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.colorUsuario);
            configuracion.Sexo = Properties.Settings.Default.sexo;
            if (configuracion.ShowDialog() == true)
            {
                Properties.Settings.Default.sexo = configuracion.Sexo;
                origen = Properties.Settings.Default.sexo;
                Properties.Settings.Default.colorFondo = configuracion.ColorFondo.ToString();
                Properties.Settings.Default.colorUsuario = configuracion.ColorUsuario.ToString();
                Properties.Settings.Default.colorRobot = configuracion.ColorRobot.ToString();
                Properties.Settings.Default.Save();
            }
        }
        public async Task<string> MensajeRobot(string pregunta)
        {
            //Para usar la API QnA añadir el paquete NuGet 
            //Microsoft.Azure.CognitiveServices.Knowledge.QnAMaker

            //Creamos el cliente de QnA
            string EndPoint = Properties.Settings.Default.EndPoint;
            string EndPointKey = Properties.Settings.Default.EndPointKey;
            string KnowledgeBaseId = Properties.Settings.Default.KnowledgeBaseId;
            var cliente = new QnAMakerRuntimeClient(new EndpointKeyServiceClientCredentials(EndPointKey)) { RuntimeEndpoint = EndPoint };
            hayConexion = true;
            //Realizamos la pregunta a la API
            try
            {
                QnASearchResultList response = await cliente.Runtime.GenerateAnswerAsync(KnowledgeBaseId, new QueryDTO { Question = pregunta });
                return response.Answers[0].Answer;
            }
            catch (Exception)
            {
                hayConexion = false;
                return null;
            }
        }
    }
}
