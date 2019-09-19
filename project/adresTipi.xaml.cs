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
    /// Interaction logic for adresTipi.xaml
    /// </summary>
    public partial class adresTipi : Window
    {
        public adresTipi()
        {
            InitializeComponent();
        }
        private static string tip;
        public string tipadres
        {
            get
            {
                return tip;
            }
            set
            {
                tip = value;
            }
        }
        private void btnGuncel_Click(object sender, RoutedEventArgs e)
        {

            güncelle gnc = new güncelle();
            gnc.Show();
            this.Close();
            tip = txtadresTipi.Text;
      
          
        }
    }
}
