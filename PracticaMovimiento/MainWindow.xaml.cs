using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//librerias
using System.Threading;
using System.Diagnostics;
namespace PracticaMovimiento
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch stopwatch;
        TimeSpan tiempoAnterior;
        public MainWindow()
        {
            InitializeComponent();
            miCanvas.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            tiempoAnterior = stopwatch.Elapsed;
            //1.- Establecer instrucciones
            ThreadStart threadStart = new ThreadStart(moverEnemigos);
            //2.- Inicializar el Thread
            Thread threadMoverEnemigos = new Thread(threadStart);
            //3.- Ejecutar el thread
            threadMoverEnemigos.Start();
            void moverEnemigos()
            {
                while (true)
                {
                    Dispatcher.Invoke(
                        () =>
                        {
                            var tiempoActual = stopwatch.Elapsed;
                            var deltaTime = tiempoActual - tiempoAnterior;

                            double leftAutoActual = Canvas.GetLeft(imgAuto);
                            Canvas.SetLeft(imgAuto, leftAutoActual - (90 * deltaTime.TotalSeconds));
                            if(Canvas.GetLeft(imgAuto) <= -100)
                            {
                                Canvas.SetLeft(imgAuto, 800);
                            }
                            tiempoAnterior = tiempoActual;
                        }
                        );
                }
                }
        }

        private void miCanvas_KeyDown(object sender, KeyEventArgs e)
        {
           if (e.Key == Key.Up)
            {
                double topScarfaceActual = Canvas.GetTop(imgScarface);
                Canvas.SetTop(imgScarface, topScarfaceActual - 15);
            }
            if (e.Key == Key.Down)
            {
                double downScarfaceActual = Canvas.GetBottom(imgScarface);
                Canvas.SetBottom(imgScarface, downScarfaceActual + 15);
            }
            if (e.Key == Key.Right)
            {
                double rightScarfaceActual = Canvas.GetRight(imgScarface);
                Canvas.SetRight(imgScarface, rightScarfaceActual - 15);
            }
            if (e.Key == Key.Right)
            {
                double leftScarfaceActual = Canvas.GetLeft(imgScarface);
                Canvas.SetLeft(imgScarface, leftScarfaceActual + 15);
            }
        }
    }
}
