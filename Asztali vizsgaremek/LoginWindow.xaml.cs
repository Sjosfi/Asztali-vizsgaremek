using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace Asztali_vizsgaremek
{
    public partial class LoginWindow : Window
    {
        private string connectionString = "server=localhost;user=root;password=;database=teaching_website_db;";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailBox.Text.Trim();
            string inputPassword = PasswordBox.Password;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT password FROM admin WHERE email = @Email LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        var result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string hashedPassword = result.ToString();

                            if (BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword))
                            {
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Hibás jelszó.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hibás email cím.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sikertelen bejelentkezés: " + ex.Message);
            }
        }
    }
}
