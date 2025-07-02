using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace IMS_SPORTS
{
  
    public partial class MyDashboard : Window
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public MyDashboard()
        {
            InitializeComponent();
        }

        private void Userm_btn_Click(object sender, RoutedEventArgs e)
        {
            UserManagement userManagementWindow = new UserManagement(this);
            userManagementWindow.Show();
            this.Hide(); 
        }

        private void audit_btn_Click(object sender, RoutedEventArgs e)
        {
            AuditLog AuditLogWindow = new AuditLog(this);
            AuditLogWindow.Show();
            this.Hide();
        }

        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO AuditLog (userID, datetime, status, action) " +
                    "VALUES (@userID, GETDATE(), 'logout', 'User logged out')", conn);

                cmd.Parameters.AddWithValue("@userID", Session.UserID);
                cmd.ExecuteNonQuery();
            }

            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
