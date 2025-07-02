using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using static System.Collections.Specialized.BitVector32;

namespace IMS_SPORTS
{

    public partial class MainWindow : Window
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            string username = username_txtbox.Text;
            string password = password_box.Password;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("usp_UserLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    int userID = Convert.ToInt32(reader["userID"]);
                    string uname = reader["username"].ToString();

                    Session.UserID = userID;
                    Session.Username = uname;
                    Session.RoleName = reader["roleName"].ToString();
                    Session.Permission = reader["permission"].ToString();
                    Session.RoleName = reader["roleName"].ToString();


                    reader.Close();
                    SqlCommand checkActiveCmd = new SqlCommand("SELECT isActive FROM [User] WHERE userID = @userID", conn);
                    checkActiveCmd.Parameters.AddWithValue("@userID", userID);
                    var isActiveResult = checkActiveCmd.ExecuteScalar();

                    if (Convert.ToBoolean(isActiveResult))
                    {
                        MessageBox.Show("Login successful!");

                        MyDashboard dashboard = new MyDashboard();
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("This account is deactivated.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
        }
    }
}