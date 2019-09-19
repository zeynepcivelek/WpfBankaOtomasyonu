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
    /// Interaction logic for borcOde.xaml
    /// </summary>
    public partial class borcOde : Window
    {
        public borcOde()
        {
            InitializeComponent();
        }
        LoginScreen ls=new LoginScreen();
        borcSorgula borcSorgula = new borcSorgula();
        private void btnOde_Click(object sender, RoutedEventArgs e)
        {  var dlgResult =
                 MessageBox.Show("Borcunuzu ödemek istediğinizden emin misiniz?",
                "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (dlgResult == MessageBoxResult.Yes)
        {
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }

                int denemeID = ls.Aid;
                String query = "SELECT borc FROM Hesap where musteriHesapID=@id and hesapNumarası=@hesapNo";

                SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);

                sqlCmd.CommandType = System.Data.CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@id", denemeID);
                sqlCmd.Parameters.AddWithValue("@hesapNo", borcSorgula.hesapno);

                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                String query1 = "update Hesap set borc='0' where musteriHesapID=@id and hesapNumarası=@hesapNo";
                SqlCommand sqlCmd2 = new SqlCommand(query1, sqlConnec);

                sqlCmd2.CommandType = System.Data.CommandType.Text;

                sqlCmd2.Parameters.AddWithValue("@id", denemeID);
                sqlCmd2.Parameters.AddWithValue("@hesapNo", borcSorgula.hesapno);
                sqlCmd2.ExecuteNonQuery();

                string ilklimit = "SELECT limiti FROM Hesap where musteriHesapID=@id and hesapNumarası=@hesapNo";

                SqlCommand sqlCmd4 = new SqlCommand(ilklimit, sqlConnec);
                sqlCmd4.CommandType = System.Data.CommandType.Text;
                sqlCmd4.Parameters.AddWithValue("@id", denemeID);
                sqlCmd4.Parameters.AddWithValue("@hesapNo", borcSorgula.hesapno);
                int limit1 = Convert.ToInt32(sqlCmd4.ExecuteScalar());
                String query2 = "update Hesap set limiti=@yenilimit where musteriHesapID=@id and hesapNumarası=@hesapNo";
                SqlCommand sqlCmd3 = new SqlCommand(query2, sqlConnec);
                sqlCmd3.CommandType = System.Data.CommandType.Text;
                if (limit1 >= count)
                {
                    if (count == 0)
                    {
                        MessageBox.Show("Borcunuz bulunmamaktadır!");
                    }
                    else
                    {

                        sqlCmd3.Parameters.AddWithValue("@yenilimit", limit1 - count);
                        sqlCmd3.Parameters.AddWithValue("@id", denemeID);
                        sqlCmd3.Parameters.AddWithValue("@hesapNo", borcSorgula.hesapno);
                        sqlCmd3.ExecuteNonQuery();
                        int p=limit1 - count;
                        MessageBox.Show("Borcunuz ödenmiştir! Kalan tutarınız: "+p);

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Limitiniz yetersiz!");
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
        else if (dlgResult == MessageBoxResult.No)
        {
         
            this.Close();
        }
        }
    }
}
