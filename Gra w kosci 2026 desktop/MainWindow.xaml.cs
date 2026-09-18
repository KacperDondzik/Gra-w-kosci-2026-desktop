using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AplikacjaKosciWPF
{
    public class Kosc
    {
        public static int LiczbaInstancji = 0;

        public string[] NazwyPlikow = new string[]
        {
            "kosc0.png", "kosc1.png", "kosc2.png",
            "kosc3.png", "kosc4.png", "kosc5.png", "kosc6.png"
        };

        public int LiczbaOczek;
        public int IdentyfikatorPliku;
        public bool CzyDostepna;

        private static Random rand = new Random();

        public Kosc()
        {
            LiczbaOczek = 0;
            IdentyfikatorPliku = 0;
            CzyDostepna = true;
            LiczbaInstancji++;
        }

        public void Rzuc()
        {
            if (CzyDostepna)
            {
                int losowa = rand.Next(1, 7);
                LiczbaOczek = losowa;
                IdentyfikatorPliku = losowa;
            }
        }
    }

    public partial class MainWindow : Window
    {
        private Kosc[] kosci = new Kosc[5];
        private Image[] obrazyKosci;

        public MainWindow()
        {
            InitializeComponent();

            obrazyKosci = new Image[] { ImgKosc0, ImgKosc1, ImgKosc2, ImgKosc3, ImgKosc4 };

            for (int i = 0; i < 5; i++)
            {
                kosci[i] = new Kosc();
            }
        }

        private void BtnRzut_Click(object sender, RoutedEventArgs e)
        {
            int suma = 0;

            for (int i = 0; i < 5; i++)
            {
                kosci[i].Rzuc();
                string nazwaPliku = kosci[i].NazwyPlikow[kosci[i].IdentyfikatorPliku];
                obrazyKosci[i].Source = new BitmapImage(new Uri($"pack://application:,,,/{nazwaPliku}"));
                suma += kosci[i].LiczbaOczek;
            }

            TxtWynik.Text = suma.ToString();
        }

        private void OnKoscClicked(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image clickedImage)
            {
                int index = Array.IndexOf(obrazyKosci, clickedImage);
                if (index != -1)
                {
                    kosci[index].CzyDostepna = !kosci[index].CzyDostepna;
                    clickedImage.Opacity = kosci[index].CzyDostepna ? 1.0 : 0.5;
                }
            }
        }
    }
}