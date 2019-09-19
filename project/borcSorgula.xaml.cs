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
    /// Interaction logic for borcSorgula.xaml
    /// </summary>
    public partial class borcSorgula : Window
    {
        private static long hesapno1;
        public long hesapno
        {
            get
            {
                return hesapno1;
            }
            set
            {
                hesapno1 = value;
            }
        }
        public borcSorgula()
        {
            InitializeComponent();
        }
        LoginScreen ls = new LoginScreen();
     
        private void btnSorgula_Click(object sender, RoutedEventArgs e)
        {
           
   
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }
                int denemeID=ls.Aid;
                if (hesapNo.Text != "")
                {
                    String query = "SELECT hesapNumarası,limiti,borc FROM Hesap where musteriHesapID=@id and hesapNumarası=@hesapNo";
                    String query2 = "SELECT count(1) hesapNumarası FROM Hesap where musteriHesapID=@id and hesapNumarası=@hesapNo";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                    SqlCommand sqlCmd2 = new SqlCommand(query2, sqlConnec);
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd2.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@hesapNo", hesapNo.Text);
                    sqlCmd.Parameters.AddWithValue("@id", denemeID);
                    sqlCmd2.Parameters.AddWithValue("@hesapNo", hesapNo.Text);
                    sqlCmd2.Parameters.AddWithValue("@id", denemeID);
                    hesapno1 = Convert.ToInt64(hesapNo.Text);
                    int count = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                    if (count == 1)
                    {
                        borcOde borcOde = new borcOde();
                        borcOde.Show();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = sqlCmd;

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        borcOde.grid1.ItemsSource = dt.DefaultView;

                    }
                    else
                    {
                        MessageBox.Show("Hesap bulunamadı!");
                    }
                }
                else { MessageBox.Show("Lütfen boşluğu doldurunuz!"); }
       

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
