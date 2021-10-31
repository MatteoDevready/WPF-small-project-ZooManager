using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_ZooManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SqlConnection sqlConnection;

        public MainWindow()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["WPF_ZooManager.Properties.Settings.MatteoRealDBConnectionString"].ConnectionString;

            sqlConnection = new SqlConnection(connectionString);
            ShowZoos();
        }

        private void ShowZoos()
        {
            try
            {
                string query = "select * from Zoo";
                //the SQL Data adapter can be imagined like an interface to make tables Usable by C# Objects
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                using (sqlDataAdapter)
                {
                    DataTable zooTable = new DataTable();
                    sqlDataAdapter.Fill(zooTable);

                    //Which information of the table in datatable should be shown in the listbox?
                    listZoos.DisplayMemberPath = "Location";
                    //which values should be delivered, when an item from the list box is selected?
                    listZoos.SelectedValuePath = "Id";
                    //The reference to the data the listbox should populate
                    listZoos.ItemsSource = zooTable.DefaultView;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
        }
    }
}
