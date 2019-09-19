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
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Collections;
using System.Windows.Threading;
using System.Drawing;

namespace project
{
    

    public partial class MainWindow : Window
    {
        private System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        LoginScreen ls = new LoginScreen();
        
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.IsEnabled = true;
            timer.Tick += timer_say;
            timer.Start();
        }

        private void timer_say(object sender, EventArgs e)
        {
            DateTime simdi = DateTime.Now;

            //string pathImage = "Images/" + ls.kullanıcıAl + ".png";
            string pathImage = "Images/" + "warning.png";
           profil.Source = new BitmapImage(new Uri(@pathImage, UriKind.RelativeOrAbsolute));
           
          welcome.Content = "Hoşgeldin  "+ls.kullanıcıAl+"!"+"                                      Saat: "+ simdi.ToLongTimeString();
       
        }

        private void btn_kapat_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void brd_ust_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
           
        }

        private void Liste(object sender, RoutedEventArgs e)
        {
           
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }

                String query = "SELECT isim,soyisim,kullaniciAdi FROM Musteri";
                SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                sqlCmd.CommandType = System.Data.CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(query,sqlConnec);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid1.ItemsSource = dt.DefaultView;

              
               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnec.Close();

            }

        }

        private void btn_sayi(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }

                String query = "SELECT COUNT(*) FROM Musteri";
                SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                sqlCmd.CommandType = System.Data.CommandType.Text;

                MainWindow mn = new MainWindow();
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
           
                SqlDataAdapter da = new SqlDataAdapter(query, sqlConnec);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid1.ItemsSource = dt.DefaultView;
                



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnec.Close();

            }
        }

      

        private void Duzenle_btn(object sender, RoutedEventArgs e)
        {
            duzenle duzenle = new duzenle();
            duzenle.Show();
        }

        private void Sorgula_btn(object sender, RoutedEventArgs e)
        {
            adresSorgu adresSorgu = new adresSorgu();
            adresSorgu.Show();
        }

        private void hesaplar_btn(object sender, RoutedEventArgs e)
        {
            hesaplar hesaplar = new hesaplar();
            hesaplar.Show();

        }

        private void cıkısBtn(object sender, RoutedEventArgs e)
        {
            LoginScreen ls = new LoginScreen();
            ls.Show();
            this.Close();
        }

        private void btn_main(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void sifre_btn(object sender, RoutedEventArgs e)
        {
            sifre sfr = new sifre();
            sfr.Show();

        }

       
   

       

    }
}
