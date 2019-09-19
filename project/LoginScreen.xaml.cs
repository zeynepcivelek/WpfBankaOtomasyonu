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
using System.Net;
using System.Net.Mail; 
namespace project
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
      
        public LoginScreen()
        {
            InitializeComponent();
        }
        private static int id;
        private static string kullanıcı;
        public void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
          SqlConnection sqlConnec =new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
          try
          {
              if (sqlConnec.State == System.Data.ConnectionState.Closed) 
              {
                  sqlConnec.Open();
              }
              
      
              String query = "SELECT COUNT(1) FROM Musteri WHERE kullaniciAdi=@kullaniciAdi AND sifre=@sifre";
              String query2 = "SELECT musteriID FROM Musteri WHERE kullaniciAdi=@kullaniciAdi";
              SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
              SqlCommand sqlCmd2 = new SqlCommand(query2, sqlConnec);
              sqlCmd.CommandType = System.Data.CommandType.Text;
              sqlCmd2.CommandType = System.Data.CommandType.Text;
              sqlCmd.Parameters.AddWithValue("@kullaniciAdi", txtUsername.Text);
              sqlCmd.Parameters.AddWithValue("@sifre", txtPassword.Password);
              sqlCmd2.Parameters.AddWithValue("@kullaniciAdi", txtUsername.Text);
              id = Convert.ToInt32(sqlCmd2.ExecuteScalar());
              kullanıcı = txtUsername.Text;
              int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
              if (count == 1)
              {
          
                  MainWindow mainWindow = new MainWindow();
                  mainWindow.Show();

                //  mainWindow.welcome.Content = "Hoşgeldin " + txtUsername.Text + "!";
                  this.Close();

              }
              else {
                  MessageBox.Show("Kullanıcı adı veya şifre yanlış!");
              }
           /*   SmtpClient sc = new SmtpClient();
              sc.Port = 587;
              sc.Host = "mail.biscozum.com.tr";
              sc.EnableSsl = true;
              sc.UseDefaultCredentials = false;

              string kime = "fatmay@biscozum.com.tr";
              string konu = "slm";
              string icerik = "nbr";

              sc.Credentials = new NetworkCredential("zeynepc@biscozum.com.tr", "zey95zey*");
              MailMessage mail = new MailMessage();
              mail.From = new MailAddress("zeynepc@biscozum.com.tr", "Zeynep Civelek");
              mail.To.Add(kime);
              mail.Subject = konu;
              mail.IsBodyHtml = true;
              mail.Body = icerik;
              sc.Send(mail);
              MessageBox.Show("mail gönderildi!");*/
              
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
        public int Aid
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string kullanıcıAl
        {
            get
            {
                return kullanıcı;
            }
            set
            {
                kullanıcı = value;
            }
        }

        private void btnKayıt_Click(object sender, RoutedEventArgs e)
        {
            Register registerWindow = new Register();
            registerWindow.Show();
            this.Close();

        }
    }
}
