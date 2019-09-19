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
    /// Interaction logic for paraGonder.xaml
    /// </summary>
    public partial class paraGonder : Window
    {
        public paraGonder()
        {
            InitializeComponent();
        }
        LoginScreen ls = new LoginScreen();
        private void btnGonder_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnec = new SqlConnection(@"Data Source = BISSQLDEV1\DB; Initial Catalog=dbedefter; Integrated Security=True;");
            try
            {
                if (sqlConnec.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnec.Open();
                }
                int denemeID = ls.Aid;
                if(paraMiktarı.Text==""||alıcıHesap.Text==""){
                MessageBox.Show("Lütfen boşlukları doldurun!");
                }
              
                string controlquery1 = "select count(1) from hesap where  musteriHesapID=@id and hesapNumarası=@hesapNumarası ";
                string controlquery2 = "select count(1) from hesap where hesapNumarası=@hesapNo ";
                String query = "update hesap set limiti=limiti-@tutar where musteriHesapID=@id and hesapNumarası=@hesapNumarası";
                String query2 = "update hesap set limiti=limiti+@tutar where hesapNumarası=@hesapNo";
                SqlCommand sqlCmd = new SqlCommand(query, sqlConnec);
                SqlCommand sqlCmd2= new SqlCommand(query2, sqlConnec);
                SqlCommand sqlCmd3 = new SqlCommand(controlquery1, sqlConnec);
                sqlCmd3.Parameters.AddWithValue("@id", denemeID);
                sqlCmd3.Parameters.AddWithValue("@hesapNumarası", gondericiHesap.Text);
                SqlCommand sqlCmd4 = new SqlCommand(controlquery2, sqlConnec);
                sqlCmd4.Parameters.AddWithValue("@id", denemeID);
                sqlCmd4.Parameters.AddWithValue("@hesapNo", alıcıHesap.Text);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@id", denemeID);
                sqlCmd.Parameters.AddWithValue("@hesapNumarası", gondericiHesap.Text);
                sqlCmd.Parameters.AddWithValue("@tutar", paraMiktarı.Text);
                sqlCmd2.Parameters.AddWithValue("@hesapNo", alıcıHesap.Text);
                sqlCmd2.Parameters.AddWithValue("@tutar", paraMiktarı.Text);
                sqlCmd2.CommandType = System.Data.CommandType.Text;

                sqlCmd3.ExecuteNonQuery();
                sqlCmd4.ExecuteNonQuery();
                string paraquery = "select limiti from hesap where musteriHesapID=@id and hesapNumarası=@hesapNumarası ";
                SqlCommand sqlCmd5 = new SqlCommand(paraquery, sqlConnec);
                sqlCmd5.Parameters.AddWithValue("@id", denemeID);
                sqlCmd5.Parameters.AddWithValue("@hesapNumarası", gondericiHesap.Text);
                int control3 = Convert.ToInt32(sqlCmd3.ExecuteScalar());
               
                int control4 = Convert.ToInt32(sqlCmd4.ExecuteScalar());
                if (control4 > 0 && control3 > 0)
                {
                    sqlCmd5.ExecuteNonQuery();//kalan parayı göstermek için
                    int para = Convert.ToInt32(sqlCmd5.ExecuteScalar()) - Convert.ToInt32(paraMiktarı.Text);
                    var dlgResult =
                MessageBox.Show(paraMiktarı.Text + " tl'yi göndermek istediğinizden emin misiniz?",
               "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    string sorgu = "SELECT musteri.kullaniciAdi from musteri inner join hesap on musteri.musteriID=musteriHesapID where hesapNumarası=@alıcı";
                    string sorgu2= "SELECT kullaniciAdi from musteri  where musteriID=@denemeID";
                    SqlCommand sqlCmdSorgu = new SqlCommand(sorgu, sqlConnec);
                    sqlCmdSorgu.CommandType = System.Data.CommandType.Text;
                    sqlCmdSorgu.Parameters.AddWithValue("@alıcı", alıcıHesap.Text);
                    SqlDataReader dr = sqlCmdSorgu.ExecuteReader();
                   
                    string alıcıismi;
                    if (dr.Read())
                    {
                       
                        alıcıismi = dr["kullaniciAdi"].ToString();
                        dr.Close();
                        SqlCommand sqlCmdSorgu2 = new SqlCommand(sorgu2, sqlConnec);
                        sqlCmdSorgu2.CommandType = System.Data.CommandType.Text;
                        sqlCmdSorgu2.Parameters.AddWithValue("@denemeID", denemeID);
                        SqlDataReader dr2 = sqlCmdSorgu2.ExecuteReader();
                        string gönderenismi;
                        if (dr2.Read())
                        {
                            gönderenismi = dr2["kullaniciAdi"].ToString();
                            dr2.Close();


                            if (dlgResult == MessageBoxResult.Yes)
                            {
                                if (para > 0)
                                {
                                    sqlCmd2.ExecuteNonQuery();
                                    sqlCmd.ExecuteNonQuery();
                                    MessageBox.Show("Paranız Gönderilmiştir, Kalan Tutarınız: " + para + " tl");
                                    dekont dekont = new dekont();
                                    dekont.Show();
                                    SqlDataAdapter da = new SqlDataAdapter();
                                    da.SelectCommand = sqlCmdSorgu;
                                    DataTable dt = new DataTable();
                                    dt.Columns.Add("Gönderen İsmi");
                                    dt.Columns.Add("Gönderen Hesap");
                                    dt.Columns.Add("Alıcı İsmi");
                                    dt.Columns.Add("Alıcı Hesap");
                                    dt.Columns.Add("Gönderilen Tutar");
                                    dt.Columns.Add("Kalan Limit");
                                    dt.Columns.Add("Gönderilme Tarihi");
                                    dt.Columns.Add("Gönderilme Saati");
                                  
                                    DataRow rowlar = dt.NewRow();

                                    rowlar["Gönderen İsmi"] = gönderenismi;
                                    rowlar["Gönderen Hesap"] = gondericiHesap.Text;
                                    rowlar["Alıcı İsmi"] = alıcıismi;
                                    rowlar["Alıcı Hesap"] = alıcıHesap.Text;
                                    rowlar["Gönderilen Tutar"] = paraMiktarı.Text;
                                    rowlar["Kalan Limit"] = para;
                                    rowlar["Gönderilme Tarihi"] = DateTime.Now.ToLongDateString();
                                    rowlar["Gönderilme Saati"] = DateTime.Now.ToLongTimeString();
                                    dt.Rows.Add(rowlar); 
                                    dekont.gridD.ItemsSource = dt.DefaultView;

                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Limitiniz yetersizdir!");
                                    this.Close();
                                }



                            }
                            else if (dlgResult == MessageBoxResult.No)
                            {
                                paraGonder pg = new paraGonder();
                                pg.Show();

                                this.Close();


                            }
                        }

                    }
                }

                else
                {

                    MessageBox.Show("Bilgileriniz hatalı!");
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
