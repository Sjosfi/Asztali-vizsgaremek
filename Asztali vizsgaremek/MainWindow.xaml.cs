﻿using System;
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
using MySql.Data.MySqlClient;

namespace Asztali_vizsgaremek;

public partial class MainWindow : Window
{

    private string connectionString = "server=localhost;user=root;password=;database=teaching_website_db;";
    private List<string> tableNames = new List<string> { "admin", "assignment", "student", "studentassignment", "teacher", "user"};

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
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM {tableName}", conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
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

    private void DataGridDisplay_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
    {
        if (TableSelector.SelectedItem == null) return;

        Dispatcher.BeginInvoke(new Action(() =>
        {
            string tableName = TableSelector.SelectedItem.ToString();
            DataRowView rowView = e.Row.Item as DataRowView;

            if (rowView != null)
            {
                UpdateDatabaseRow(tableName, rowView.Row);
            }
        }), System.Windows.Threading.DispatcherPriority.Background);
    }



    private void UpdateDatabaseRow(string tableName, DataRow row)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var columns = row.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();

                string primaryKey = columns[0];
                object primaryKeyValue = row[primaryKey];

                var setClauses = columns.Skip(1).Select(col => $"{col} = @{col}");
                string updateQuery = $"UPDATE {tableName} SET {string.Join(", ", setClauses)} WHERE {primaryKey} = @{primaryKey}";

                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                {
                    foreach (string col in columns)
                    {
                        cmd.Parameters.AddWithValue($"@{col}", row[col]);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error updating row: " + ex.Message);
        }
    }
}
