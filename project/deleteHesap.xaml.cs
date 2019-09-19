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
    /// Interaction logic for deleteHesap.xaml
    /// </summary>
    public partial class deleteHesap : Window
    {
        public deleteHesap()
        {
            InitializeComponent();
        }
        LoginScreen ls= new LoginScreen();
        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }
                 int denemeID = ls.Aid;
                 String query = "select count(*) from Hesap where musteriHesapID =@denemeID and hesapNumarası=@hesapNo";

                SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@hesapNo", txtHesapno.Text);
                sqlCmd.Parameters.AddWithValue("@denemeID", denemeID);
                sqlCmd.ExecuteNonQuery();
     
                int counthesap = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (counthesap> 0)
                {  
                    var dlgResult =
                 MessageBox.Show(txtHesapno.Text+" numaralı hesabınızı silmek istediğinizden emin misiniz?",
                "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (dlgResult == MessageBoxResult.Yes)
                    {
                        String query2 = "Delete from Hesap where musteriHesapID =@denemeID and hesapNumarası=@hesapNo";

                        SqlCommand sqlCmd2 = new SqlCommand(query2, sqlConnec);
                        sqlCmd2.CommandType = System.Data.CommandType.Text;
                        sqlCmd2.Parameters.AddWithValue("@hesapNo", txtHesapno.Text);
                        sqlCmd2.Parameters.AddWithValue("@denemeID", denemeID);
                        sqlCmd2.ExecuteNonQuery();

                        MessageBox.Show("Hesap Silindi!");
                        this.Close();
                    }
                    else { deleteHesap dl = new deleteHesap();
                    dl.Show();
                    this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Böyle bir hesap bulunmamaktadır!");
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
    }
}
