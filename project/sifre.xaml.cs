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
    /// Interaction logic for sifre.xaml
    /// </summary>
    public partial class sifre : Window
    {
        public sifre()
        {
            InitializeComponent();
        }
        LoginScreen ls = new LoginScreen();
        private void btnSifre_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }
                int denemeID = ls.Aid;
                String query = "SELECT sifre from Musteri where musteriID=@id";
                SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@id",denemeID);
                sqlCmd.ExecuteNonQuery();
                string oldSifre;
                SqlDataReader dr = sqlCmd.ExecuteReader();
                if (dr.Read())
                {
               
                   oldSifre = dr["sifre"].ToString();
                   dr.Close();
                   if (oldSifre != txtoldsifre.Text)
                   {
                       MessageBox.Show("Şifrenizi yanlış girdiniz!");
                   }
                   else
                   {
                         var dlgResult =
                 MessageBox.Show("Şifrenizi değiştirmek istediğinizden emin misiniz?",
                "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Question);

                         if (dlgResult == MessageBoxResult.Yes)
                         {

                             String query2 = "update Musteri set sifre=@newSifre where musteriID=@id";
                             SqlCommand sqlCmd2 = new SqlCommand(query2, sqlConnec);
                             sqlCmd2.CommandType = System.Data.CommandType.Text;
                             sqlCmd2.Parameters.AddWithValue("@id", denemeID);
                             sqlCmd2.Parameters.AddWithValue("@newSifre", txtnewsifre.Text);
                             sqlCmd2.ExecuteNonQuery();
                             MessageBox.Show("Şifreniz güncellendi!");
                             this.Close();
                         }
                         else if (dlgResult == MessageBoxResult.No)
                         {
                             sifre s = new sifre();
                             s.Show();
                             this.Close();
                         }
                   }
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
