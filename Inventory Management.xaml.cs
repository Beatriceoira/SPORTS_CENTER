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
    public partial class Dashboard : Window
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public Dashboard()
        {
            InitializeComponent();
        }

        private void inventory_btn1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You are already in the Inventory Management section.");
        }

       

        private void userm_btn1_Click(object sender, RoutedEventArgs e)
        {
            UserManagement userWindow = new UserManagement(this);
            userWindow.Show();
            this.Close();
        }

        private void audit_btn1_Click(object sender, RoutedEventArgs e)
        {
            AuditLog auditWindow = new AuditLog(this);
            auditWindow.Show();
            this.Close();
        }

        private void dashboard_btn1_Click(object sender, RoutedEventArgs e)
        {
            MyDashboard dashboard = new MyDashboard();
            dashboard.Show();
            this.Close();
        }

        private void logout_btn1_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO AuditLog (userID, datetime, status, action) VALUES (@userID, GETDATE(), 'logout', 'User logged out')", conn);
                cmd.Parameters.AddWithValue("@userID", Session.UserID); // Assumes Session class is in place
                cmd.ExecuteNonQuery();
            }

            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void dashboard_btn1_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}