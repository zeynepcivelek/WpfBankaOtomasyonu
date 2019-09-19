using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;


namespace project
{
    /// <summary>
    /// Interaction logic for duzenle.xaml
    /// </summary>
    public partial class duzenle : Window
    {
        public duzenle()
        {
            InitializeComponent();
        }
        LoginScreen ls = new LoginScreen();
        private void btnDuzenle_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }



                if (txtSehir.Text == ("") || txtIlce.Text == ("") || txtMahalle.Text == ("") || txtSokak.Text == ("") || txtNumara.Text == ("") || txtTipi.Text == (""))
                {

                    MessageBox.Show("Lütfen boşlukları doldurunuz!");
                }
                else
                {

                    String kisiID = "SELECT count(1) musteriID FROM Musteri WHERE kullaniciAdi=@kullaniciAdi AND sifre=@sifre";
                    String query = "INSERT INTO MusteriAdres VALUES(@sehir, @ilce, @mahalle, @sokak, @numara, @tipi,@id)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                    SqlCommand sqlCmd2 = new SqlCommand(kisiID, sqlConnec);
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@sehir", txtSehir.Text);
                    sqlCmd.Parameters.AddWithValue("@ilce", txtIlce.Text);
                    sqlCmd.Parameters.AddWithValue("@mahalle", txtMahalle.Text);
                    sqlCmd.Parameters.AddWithValue("@sokak", txtSokak.Text);
                    sqlCmd.Parameters.AddWithValue("@numara", txtNumara.Text);
                    sqlCmd.Parameters.AddWithValue("@tipi", txtTipi.Text);
                    int denemeID = ls.Aid;

                    sqlCmd.Parameters.AddWithValue("@id", denemeID);
                    
                 
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Yeni Adres Kaydınız Alındı!");
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();

                }
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
        private void btnGuncelle_Click(object sender, RoutedEventArgs e) {
            adresTipi adt = new adresTipi();
            adt.Show();
            this.Close();

        
        }

        private void btnAdresSil_Click(object sender, RoutedEventArgs e)
        {
            adresSil Asil = new adresSil();
            Asil.Show();
            this.Close();
        }
       
    }
}
