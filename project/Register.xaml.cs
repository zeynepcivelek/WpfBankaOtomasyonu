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
using System.Text.RegularExpressions;

namespace project
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
     
        
    
        public Register()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }


                string mailTest = txtEmail.Text;
                string telTest = txtPhone.Text;
                if (txtUsername.Text == ("") || txtPassword.Password == ("") || txtPhone.Text == ("") || txtEmail.Text == ("") ||  txtName.Text == ("") || txtSurname.Text == (""))
                {
                   
                    MessageBox.Show("Wrong Entry!");
                }
                else
                {
                    bool flag1 = true;
                    bool flag2 = true;
                    if (telTest != null && telTest.Length > 0 && !Regex.IsMatch((string)telTest, @"^05[0534][0-9]{8}$"))
                    {


                        MessageBox.Show("Telefon numaranızı lütfen kontrol ediniz!");
                        flag1 = false;
                    }
                   
                    if (mailTest != null && mailTest.Length > 0 && !Regex.IsMatch((string)mailTest, 
                      @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                    {
                        MessageBox.Show("Emailinizi lütfen kontrol ediniz");
                        flag2 = false;
                    }
                    if (flag1 == true && flag2 == true)
                    {

                    
                   
                    String query = "INSERT INTO Musteri VALUES(@isim, @soyisim, @telefon, @email, @kullaniciAdi, @sifre)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                    sqlCmd.CommandType = System.Data.CommandType.Text;
               
                    string unique = "select count(1) from Musteri where kullaniciAdi like @kullaniciAdi";
                    SqlCommand sqlCmd2 = new SqlCommand(unique, sqlConnec);
                    sqlCmd2.CommandType = System.Data.CommandType.Text;
                    sqlCmd2.Parameters.AddWithValue("@kullaniciAdi", txtUsername.Text);
                    int like = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                   
                    sqlCmd.Parameters.AddWithValue("@isim",  txtName.Text);
                    sqlCmd.Parameters.AddWithValue("@soyisim", txtSurname.Text);
                    sqlCmd.Parameters.AddWithValue("@telefon", txtPhone.Text);
                    sqlCmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    sqlCmd.Parameters.AddWithValue("@kullaniciAdi", txtUsername.Text);
                    sqlCmd.Parameters.AddWithValue("@sifre", txtPassword.Password);
                    if (like == 0) {
                        sqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Üye Oldun!");
                        LoginScreen Log = new LoginScreen();
                        Log.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Bu kullanıcı adı daha önce alınmıştır!");
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
