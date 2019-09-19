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


using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;



namespace project
{
    /// <summary>
    /// Interaction logic for dekont.xaml
    /// </summary>
    public partial class dekont : System.Windows.Window
    {
        paraGonder pg = new paraGonder();

        public dekont()
        {
            InitializeComponent();
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true; //www.yazilimkodlama.com
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < gridD.Columns.Count; j++) //Başlıklar için
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true; //Başlığın Kalın olması için
                sheet1.Columns[j + 1].ColumnWidth = 15; //Sütun genişliği ayarı
                myRange.Value2 = gridD.Columns[j].Header;
            }
            for (int i = 0; i < gridD.Columns.Count; i++)
            { //www.yazilimkodlama.com
                for (int j = 0; j < gridD.Items.Count; j++)
                {
                    TextBlock b = gridD.Columns[i].GetCellContent(gridD.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
            
        }
      
   
    }
}
