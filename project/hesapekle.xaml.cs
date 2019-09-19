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
    /// Interaction logic for hesapekle.xaml
    /// </summary>
    public partial class hesapekle : Window
    {
        public hesapekle()
        {
            InitializeComponent();
        }
        LoginScreen ls = new LoginScreen();
        private void btnHesapekle(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }



                if (txtBankName.Text == ("") || txtSontarih.Text == ("") || txtLimit.Text == ("") || txtHesapno.Text == (""))
                {

                    MessageBox.Show("Lütfen boşlukları doldurunuz!");
                }
                else
                {
                 
                    Random rnd = new Random();
                    int rndBorc = rnd.Next(100, 1000); 
                    String query = "INSERT INTO Hesap VALUES(@bankAdı, @hesapNo, @limit, @sonTarih,@rndBorc,@id)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    string denemeHesap = txtHesapno.Text;
                    string unique="select count(1) from hesap where hesapNumarası like @likeHesap";
                    SqlCommand sqlCmd2 = new SqlCommand(unique, sqlConnec);
                    sqlCmd2.CommandType = System.Data.CommandType.Text;
                    string tarih = txtSontarih.Text;
                    sqlCmd2.Parameters.AddWithValue("@likeHesap", denemeHesap);
                    int like = Convert.ToInt32(sqlCmd2.ExecuteScalar());
                    bool flag = true;
                    bool flag2 = true;
                    if (denemeHesap != null && denemeHesap.Length > 0 && !Regex.IsMatch((string)denemeHesap, 
                     @"^[0-9]{16}$"))
                    {

                        flag = false;
                        MessageBox.Show("Hesap numaranızı lütfen kontrol ediniz!");
                     
                    }
                    if (tarih != null && tarih.Length > 0 && !Regex.IsMatch((string)tarih,
                        @"^((0?[1-9])|(1[0-12]))\2019|(20[2-9][0-9])$"))
                    {

                        flag2 = false;
                        MessageBox.Show("Son kullanma tarihini lütfen kontrol ediniz!");

                    }
                    if (flag == true && flag2 == true)
                    {

                        if (like == 0)
                        {
                            sqlCmd.Parameters.AddWithValue("@rndBorc", rndBorc);
                            sqlCmd.Parameters.AddWithValue("@bankAdı", txtBankName.Text);
                            sqlCmd.Parameters.AddWithValue("@hesapNo", txtHesapno.Text);
                            sqlCmd.Parameters.AddWithValue("@sonTarih", txtSontarih.Text);
                            sqlCmd.Parameters.AddWithValue("@limit", txtLimit.Text);

                            int denemeID = ls.Aid;
                            sqlCmd.Parameters.AddWithValue("@id", denemeID);
                            sqlCmd.ExecuteNonQuery();
                            MessageBox.Show("Hesabınız Eklendi!");
                            this.Close();


                        }
                        else
                        {
                            MessageBox.Show("Hesap numarası unique olmalı!");
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
