using System;
using System.Collections.Generic;
using System.Data;
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

namespace Matrix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private const int N = 25;
        private int[,] matrix1 = new int[N, N];
        private int[,] matrix2 = new int[N, N];
        private double[,] matrix3 = new double[N, N];
        private static Random random = new Random();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FillMatrix(matrix1);
            FillMatrix(matrix2);
            ResultMatrix.ItemsSource = PrintMatrix(matrix2).AsDataView();
        }

        private static void FillMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = random.Next(11);
        }

        private static DataTable PrintMatrix(int[,] numbers)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < numbers.GetLength(1); i++)
            {
                dt.Columns.Add();
            }

            for (var i = 0; i < numbers.GetLength(0); ++i)
            {
                DataRow row = dt.NewRow();
                for (var j = 0; j < numbers.GetLength(1); ++j)
                {
                    row[j] = numbers[i, j];
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
