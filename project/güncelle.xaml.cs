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
    /// Interaction logic for güncelle.xaml
    /// </summary>
    public partial class güncelle : Window
    {
        public güncelle()
        {
            InitializeComponent();
        }
        adresTipi adt = new adresTipi();
        LoginScreen ls = new LoginScreen();
        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
           
            string atip=adt.tipadres;
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }


                String query = "Update MusteriAdres Set sehir=@sehir, ilce=@ilce, mahalle=@mahalle, sokak=@sokak, numara=@numara, tipi=@tipi where musteriAdresID=@id and tipi=@Atipi";
                SqlCommand sqlCmd = new SqlCommand(query, sqlConnec); 
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@sehir", txtSehir.Text);
                sqlCmd.Parameters.AddWithValue("@ilce", txtIlce.Text);
                sqlCmd.Parameters.AddWithValue("@mahalle", txtMahalle.Text);
                sqlCmd.Parameters.AddWithValue("@sokak", txtSokak.Text);
                sqlCmd.Parameters.AddWithValue("@numara", txtNumara.Text);
                sqlCmd.Parameters.AddWithValue("@tipi", txtTipi.Text);
                sqlCmd.Parameters.AddWithValue("@Atipi", atip);
                int denemeID = ls.Aid;
                sqlCmd.Parameters.AddWithValue("@id", denemeID);
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Değişiklikleriniz Kaydedildi!");
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();


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
    }
}
