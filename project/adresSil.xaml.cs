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
    /// Interaction logic for adresSil.xaml
    /// </summary>
    public partial class adresSil : Window
    {
        public adresSil()
        {
            InitializeComponent();
        }
        LoginScreen ls=new LoginScreen();
        private void btnadresSil_Click(object sender, RoutedEventArgs e)
        {
             var dlgResult =
                 MessageBox.Show("Adresinizi silmek istediğinizden emin misiniz?",
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

                     if (txtAtip.Text == "")
                     {
                         MessageBox.Show("Lütfen silmek istediğiniz adres tipini giriniz!");
                     }
                     else
                     {
                         int denemeID = ls.Aid;
                         String cont = "select count(*) from MusteriAdres where musteriAdresID=@id and tipi=@tip";
                         SqlCommand sqlCmd2 = new SqlCommand(cont, sqlConnec);
                         sqlCmd2.CommandType = System.Data.CommandType.Text;
                         sqlCmd2.Parameters.AddWithValue("@id", denemeID);
                         sqlCmd2.Parameters.AddWithValue("@tip", txtAtip.Text);
                         sqlCmd2.ExecuteNonQuery();
                         int countAdres = Convert.ToInt32(sqlCmd2.ExecuteScalar());

                         if (countAdres > 0)
                         {
                             String query = "delete from MusteriAdres where musteriAdresID=@id and tipi=@tip";
                             SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                             sqlCmd.CommandType = System.Data.CommandType.Text;
                             sqlCmd.Parameters.AddWithValue("@id", denemeID);
                             sqlCmd.Parameters.AddWithValue("@tip", txtAtip.Text);
                             sqlCmd.ExecuteNonQuery();
                             MessageBox.Show("Adres Silindi!");
                         }
                         else
                         {
                             MessageBox.Show("Böyle bir adres bulunmamaktadır!");
                         }



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
             else if (dlgResult == MessageBoxResult.No)
             {
             
                 this.Close();
             }
        }
    }
}
