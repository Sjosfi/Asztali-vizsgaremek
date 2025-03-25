using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Asztali_vizsgaremek;

public partial class MainWindow : Window
{

    private string connectionString = "Server=localhost;Database=teaching_website_db;Uid=root;Pwd=;";
    private List<string> tableNames = new List<string> { "admin", "assignment", "student", "teacher", "token", "_studentassignments", "_teacherassignments" };

    public MainWindow()
    {
        InitializeComponent();
        LoadTables();
    }

    private void LoadTables()
    {
        TableSelector.ItemsSource = tableNames;
        if (tableNames.Count > 0)
            TableSelector.SelectedIndex = 0;
    }

    private void TableSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (TableSelector.SelectedItem != null)
        {
            string selectedTable = TableSelector.SelectedItem.ToString();
            LoadData(selectedTable);
        }
    }

    private void LoadData(string tableName)
    {
        DataTable dt = new DataTable();
        try
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                conn.Open();
                using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand($"SELECT * FROM {tableName}", conn))
                using (Microsoft.Data.SqlClient.SqlDataAdapter adapter = new Microsoft.Data.SqlClient.SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            DataGridDisplay.ItemsSource = dt.DefaultView;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
    }
}
