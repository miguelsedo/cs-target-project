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
using System.Windows.Threading;
using System.Media;

namespace Dianes
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Posar aquí les variables globals i constants.
        const byte NUM_DIANES = 8; // Exemple constant amb el número de dianes.
        Ellipse[] cercles = new Ellipse[NUM_DIANES];
        Boolean[] posicio = new Boolean[NUM_DIANES];
        int vides=5, punts=0;
        Random atzar = new Random();
        bool visible;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Aquest mètode s'executarà quan es toqui una tecla.
            int tecla = KeyInterop.VirtualKeyFromKey(e.Key) - 48; // 48 = Codi ASCII del caràcter '0'.
            // Ara la variable 'tecla' tindrà un valor que, si s'ha tocat un número, anirà de 1 a 9.
            // Compte perquè si ha tocat una altra tecla tindrà un valor invàlid!

            for (int i = 0; i < cercles.Length; i++)
            {
                if ((tecla-1) == i && posicio[i] == true)
                {
                    cercles[i].Visibility = Visibility.Hidden;
                    posicio[i] = false;
                    AudioDiana();
                    punts++;
                    labelPunts.Content = punts.ToString();
                }
            }

            e.Handled = true;   // IMPORTANT: Deixar aquesta línia. Li indica a l'ordinador que ja s'ha gestionat la tecla.
        }
        private void AudioDiana() //li hem posat un efecte d'audio quan es "dispara" a les dianes
        {
            SoundPlayer TocDiana = new SoundPlayer(DianesII.Properties.Resources.pacman_eating_cherry);
            TocDiana.Play();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SoundPlayer IniciJoc = new SoundPlayer(DianesII.Properties.Resources.pacman_song);
            IniciJoc.Play();

            // Aquest mètode s'executa a l'iniciar l'aplicació.
            int i, x=-140, m=-125, n=0; //la variable x serà la coordenada x del canvas, la m serà la coordenada x del textblock, i la variable n serà el número de cada cercle (text del textblock)

            TextGameOver.Visibility = Visibility.Hidden;

            TextBlock numCercles;

            labelPunts.Content = punts.ToString();
            labelVides.Content = vides.ToString();

            for (i = 0; i < cercles.Length; i++)
            {
                x += 45;
                cercles[i] = creaCercle( x, -30, 35, Colors.Red);
                canvasCercle.Children.Add(cercles[i]);
                n ++;
                m += 45;
                numCercles = creaText( m, 10, n.ToString(), Colors.Black);
                canvasCercle.Children.Add(numCercles);
            }

            sortejaDianes(); //es sorteja les dianes que es veuran

            //temporizador
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick); // Estableix que cada cop que es compleixi el temps, s'executi la funció dispatcherTimer_Tick
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1); // Defineix el timer a 0 hores, 0 minuts i 1 segon. Al passar aquest període s'executarà la funció que hagueu indicat en el pas anterior.
            dispatcherTimer.Start(); // Inicia el Timer. Compte! iniciar-lo quan sigui necessari. Si ho poseu aquí s'iniciarà només començar el programa!
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Fer aquí el codi que s'executarà cada vegada que es compleix el temps especificat.
            Temporizador.Value++;
            encaraVius();
            if (Temporizador.Value==10)
            {
                if (vides<=0)   //Si s'arriba a 0 vides, es perdrà i apareix un text "GAME OVER"
                {
                    TextEnJoc.Visibility = Visibility.Hidden;
                    TextGameOver.Visibility = Visibility.Visible;
                    SoundPlayer GameOver = new SoundPlayer(DianesII.Properties.Resources.pacman_dies);
                    GameOver.Play();
                    dispatcherTimer.Stop();
                }
                Temporizador.Value = 0;
                sortejaDianes();
            }
        }

        private int sortejaValor(int minim, int maxim)
        {

            int valorSortejat = atzar.Next(0, 2);
            if (valorSortejat == 0)
            {
                visible = false;
            }
            if (valorSortejat == 1)
            {
                visible = true;
            }
            return valorSortejat;   // Fer el mètode i reotrnar el valor sortejat. 
        }

        private bool encaraVius()
        {
            // Fer aquí el mètode.
            for (int i = 0; i < cercles.Length; i++)
            {
                if (posicio[i] == true && Temporizador.Value==10)
                {
                    vides--;
                    labelVides.Content = vides.ToString();
                }
            }
            return false || true;
        }

        private void sortejaDianes()
        {
            // Sortejar un valor per cada posició de l'array de dianes, fent servir sortejaValor. 
            // Els valors poden ser false (inactiu) o true (actiu).
            // Mostra o amaga el cercle corresponent.
            // Aquest mètode no retorna cap valor.

            for (int i = 0; i < cercles.Length; i++)
            {
                sortejaValor(0, 2);
                posicio[i] = visible;
                if (posicio[i]==false)
                {
                    cercles[i].Visibility = Visibility.Hidden;
                }
                else
                {
                    cercles[i].Visibility = Visibility.Visible;
                }
            }

        }

        private void BotonReinica_Click(object sender, RoutedEventArgs e) //botó per reiniciar el joc quan vulguis
        {
            Temporizador.Value = 0;
            sortejaDianes();
            SoundPlayer IniciJoc = new SoundPlayer(DianesII.Properties.Resources.pacman_song);
            IniciJoc.Play();
            punts = 0;
            vides = 5;
            labelPunts.Content = punts.ToString();
            labelVides.Content = vides.ToString();
            TextGameOver.Visibility = Visibility.Hidden;
            TextEnJoc.Visibility = Visibility.Visible;
            dispatcherTimer.Start();
        }

        // --- Mètodes ja fets. NO MODIFICAR-LOS!!!! -------------------------------------------

        // Aquest mètode serveix per crear un cercle. Rep 4 paràmetres:
        //   * Coordenada X on anirà el cercle.
        //   * Coordenada Y on anirà el cercle.
        //   * Diàmetre del cercle.
        //   * Color del cercle.
        // Retorna un objecte de tipus Ellipse.
        private Ellipse creaCercle(int x, int y, int diametre, Color color)
        {
            Ellipse elipse = new Ellipse();
            elipse.Fill = new SolidColorBrush(color);
            elipse.Width = diametre;
            elipse.Height = diametre;
            Canvas.SetLeft(elipse, x);
            Canvas.SetTop(elipse, y);
            return elipse;
        }

        // Aquest mètode serveix per crear un text. Rep 4 paràmetres:
        //   * Coordenada X on anirà el text.
        //   * Coordenada Y on anirà el text.
        //   * Text que es vol escriure.
        //   * Color del text.
        // Retorna un objecte de tipus TextBlock.
        private TextBlock creaText(int x, int y, String text, Color color)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            return textBlock;
        }
    }
}
