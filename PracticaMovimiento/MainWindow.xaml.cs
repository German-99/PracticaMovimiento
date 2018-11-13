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

        enum EstadoJuego { Gameplay, Gameover };
        EstadoJuego estadoActual = EstadoJuego.Gameplay;

        public MainWindow()
        {

            InitializeComponent();
            miCanvas.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            tiempoAnterior = stopwatch.Elapsed;

            //1.- Establecer instrucciones
            ThreadStart threadStart =
                new ThreadStart(actualizar);
            //2.- Inicializar el Thread
            Thread threadMoverEnemigos =
                new Thread(threadStart);
            //3.- Ejecutar el Thread
            threadMoverEnemigos.Start();

            /*imgRana.RenderTransform =
                new RotateTransform(90);*/

        }

        void actualizar()
        {
            while (true)
            {
                Dispatcher.Invoke(
                () =>
                {
                    var tiempoActual = stopwatch.Elapsed;
                    var deltaTime =
                        tiempoActual - tiempoAnterior;

                    if (estadoActual == EstadoJuego.Gameplay)
                    {
                        double leftCarroActual =
                        Canvas.GetLeft(imgAuto);
                        Canvas.SetLeft(
                            imgAuto, leftCarroActual - (20 * deltaTime.TotalSeconds));
                        if (Canvas.GetLeft(imgAuto) <= -100)
                        {
                            Canvas.SetLeft(imgAuto, 800);
                        }


                        //Intersección en X
                        double xCarro =
                            Canvas.GetLeft(imgAuto);
                        double xRana =
                            Canvas.GetLeft(imgScarface);
                        if (xRana + imgScarface.Width >= xCarro &&
                            xRana <= xCarro + imgAuto.Width)
                        {
                            lblInterseccionX.Text =
                            "SI HAY INTERSECCION EN X!!!";
                        }
                        else
                        {
                            lblInterseccionX.Text =
                            "No hay interseccion en X";
                        }
                        double yCarro =
                            Canvas.GetTop(imgAuto);
                        double yRana =
                            Canvas.GetTop(imgScarface);
                        if (yRana + imgScarface.Height >= yCarro &&
                            yRana <= yCarro + imgAuto.Height)
                        {
                            lblInterseccionY.Text =
                                "SI HAY INTERSECCION EN Y!!!";
                        }
                        else
                        {
                            lblInterseccionY.Text =
                                "No hay interseccion en Y";
                        }

                        if (xRana + imgScarface.Width >= xCarro &&
                            xRana <= xCarro + imgAuto.Width &&
                            yRana + imgScarface.Height >= yCarro &&
                            yRana <= yCarro + imgAuto.Height)
                        {
                            lblColision.Text =
                                "HAY COLISION!!!";
                            estadoActual = EstadoJuego.Gameover;
                            miCanvas.Visibility = Visibility.Collapsed;
                            canvasGameOver.Visibility =
                                Visibility.Visible;
                        }
                        else
                        {
                            lblColision.Text =
                                "No hay colision";
                        }
                    }
                    else if (estadoActual == EstadoJuego.Gameover)
                    {

                    }






                    tiempoAnterior = tiempoActual;


                }
                );
            }

        }
        private void miCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (estadoActual == EstadoJuego.Gameplay)
            {
                if (e.Key == Key.Up)
                {
                    double topRanaActual =
                        Canvas.GetTop(imgScarface);
                    Canvas.SetTop(imgScarface, topRanaActual - 15);
                }
                if (e.Key == Key.Down)
                {
                    double topRanaActual =
                        Canvas.GetTop(imgScarface);
                    Canvas.SetTop(imgScarface, topRanaActual + 15);
                }
                if (e.Key == Key.Left)
                {
                    double leftRanaActual =
                        Canvas.GetLeft(imgScarface);
                    Canvas.SetLeft(imgScarface, leftRanaActual - 15);
                }
                if (e.Key == Key.Right)
                {
                    double leftRanaActual =
                        Canvas.GetLeft(imgScarface);
                    Canvas.SetLeft(imgScarface, leftRanaActual + 15);
                }
            }
        }
    }
}
