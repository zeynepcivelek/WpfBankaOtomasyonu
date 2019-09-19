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
    /// Interaction logic for adresSorgu.xaml
    /// </summary>
    public partial class adresSorgu : Window
    {
        public adresSorgu()
        {
            InitializeComponent();
        }

        private void btnSorgula_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mn = new MainWindow();
            mn.Show();
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }

                String query = "SELECT * FROM MusteriAdres where musteriAdresID in (select musteriID from Musteri where isim=@isim)";
                String query2 = "SELECT count(*) FROM MusteriAdres where musteriAdresID in (select musteriID from Musteri where isim=@isim)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                SqlCommand sqlCmd2 = new SqlCommand(query2, sqlConnec);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@isim", txtUsername1.Text);
                sqlCmd2.Parameters.AddWithValue("@isim", txtUsername1.Text);
               SqlDataAdapter da = new SqlDataAdapter();
               da.SelectCommand = sqlCmd;
               int count = Convert.ToInt32(sqlCmd2.ExecuteScalar());
               if (count > 0)
               {
                   DataTable dt = new DataTable();
                   da.Fill(dt);
                   mn.grid1.ItemsSource = dt.DefaultView;


               }
               else {
                   MessageBox.Show("Aradığınız adres bulunamadı!");

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
            this.Close();
        }
    }
}
