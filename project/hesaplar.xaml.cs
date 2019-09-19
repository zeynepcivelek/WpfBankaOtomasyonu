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

namespace project
{
    /// <summary>
    /// Interaction logic for hesaplar.xaml
    /// </summary>
    public partial class hesaplar : Window
    {
        public hesaplar()
        {
            InitializeComponent();
        }


        LoginScreen ls = new LoginScreen();

        private void hesaplist_btn(object sender, RoutedEventArgs e)
        {

            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }
                String query = "SELECT bankaIsmi, hesapNumarası, limiti, sonKullanmaTarihi FROM Hesap where musteriHesapID=@id";
            
                SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                int denemeID = ls.Aid;
                sqlCmd.Parameters.AddWithValue("@id", denemeID);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlCmd;
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

        private void ekle_btn(object sender, RoutedEventArgs e)
        {
            hesapekle hesapekle = new hesapekle();
            hesapekle.Show();


        }

        private void hesapsil(object sender, RoutedEventArgs e)
        {
            deleteHesap deleteHesap = new deleteHesap();
            deleteHesap.Show();
        }

        private void borc_btn(object sender, RoutedEventArgs e)
        {
            borcSorgula borcSorgula = new borcSorgula();
            borcSorgula.Show();
        }

        private void borcOde_btn(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }

                String query = "SELECT * FROM Hesap";
                SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                sqlCmd.CommandType = System.Data.CommandType.Text;

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

        private void gonder_btn(object sender, RoutedEventArgs e)
        {
            paraGonder paraG = new paraGonder();
            paraG.Show();
        
        }

        private void btn_main(object sender, RoutedEventArgs e)
        {
            MainWindow mn = new MainWindow();
            mn.Show();
            this.Close();
        }

        private void cıkısBtn(object sender, RoutedEventArgs e)
        {
            LoginScreen ls = new LoginScreen();
            ls.Show();
            this.Close();
        }
    }
}
