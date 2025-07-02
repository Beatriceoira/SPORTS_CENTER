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
using System.Windows.Shapes;

namespace IMS_SPORTS
{
    public partial class AuditLog : Window
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        Window dashboard;
        public AuditLog(Window parent)
        {
            InitializeComponent();
            dashboard = parent;
            ApplyRoleRestrictions();
            LoadAuditLog();
        }

 
        private void ApplyRoleRestrictions()
        {
            string role = Session.RoleName;

            if (role == "Manager")
            {
                
            }
            else if (role == "Staff")
            {
                MessageBox.Show("You do not have permission to access the Audit Log.");
                this.Close();
                dashboard?.Show();
            }
        }
        private void LoadAuditLog()
        {
            DataTable auditTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"
                    SELECT a.userID, u.username, a.status, a.action, a.datetime
                    FROM [dbo].[AuditLog] a
                    JOIN [dbo].[User] u ON a.userID = u.userID
                    ORDER BY a.datetime DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(auditTable);
            }

            auditLogDataGrid.ItemsSource = auditTable.DefaultView;
        }


        private void dashboardAL_btn1_Click(object sender, RoutedEventArgs e)
        {
            MyDashboard myDashboardWindow = new MyDashboard();
            myDashboardWindow.Show();
            this.Close();
        }

        private void usermAL_btn1_Click(object sender, RoutedEventArgs e)
        {
            UserManagement userManagementWindow = new UserManagement(this);
            userManagementWindow.Show();
            this.Close();
        }

        private void logoutAL_btn1_Click(object sender, RoutedEventArgs e)
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
