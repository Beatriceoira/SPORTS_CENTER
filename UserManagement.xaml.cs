using System;
using System.Collections.Generic;
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
using System.Configuration;


namespace IMS_SPORTS
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
    }

    public partial class UserManagement : Window
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        Window dashboard;
        public UserManagement(Window parent)
        {
            InitializeComponent();
            dashboard = parent;
            ApplyPermissions();
            LoadUsers();
        }

        private void ApplyPermissions()
        {
            string role = Session.RoleName;

            if (role == "Inventory Manager")
            {

                add_btn.IsEnabled = false;
                edit_btn.IsEnabled = false;
                deactivate_btn.IsEnabled = false;
                activate_btn.IsEnabled = false;
                confirm1_Btn.IsEnabled = false;
            }
            else if (role == "Staff")
            {
                MessageBox.Show("You do not have permission to access User Management.");
                this.Close();
                dashboard?.Show();
            }
        }


        private void LoadUsers()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT userID, username FROM [User]", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                searchuser_combobox.ItemsSource = dt.DefaultView;
                searchuser_combobox.DisplayMemberPath = "username";
                searchuser_combobox.SelectedValuePath = "userID";
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("usp_AddUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username_txtbox.Text);
                cmd.Parameters.AddWithValue("@password", pw_txtbox.Text);
                cmd.Parameters.AddWithValue("@email", email_txtbox.Text);
                cmd.Parameters.AddWithValue("@roleID", int.Parse(role_txtbox.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("User added successfully.");
                LoadUsers();
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("usp_EditUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userID", int.Parse(userID_txtbox.Text));
                cmd.Parameters.AddWithValue("@username", username_txtbox.Text);
                cmd.Parameters.AddWithValue("@password", pw_txtbox.Text);
                cmd.Parameters.AddWithValue("@email", email_txtbox.Text);
                cmd.Parameters.AddWithValue("@roleID", int.Parse(role_txtbox.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("User updated successfully.");
                LoadUsers();
            }
        }

        private void ActivateUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userID_txtbox.Text))
            {
                MessageBox.Show("Please select a user to activate.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("usp_ReactivateUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userID", int.Parse(userID_txtbox.Text));
                cmd.ExecuteNonQuery();

                MessageBox.Show("User successfully reactivated.");
                LoadUsers();  
            }
        }
        private void DeactivateUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userID_txtbox.Text))
            {
                MessageBox.Show("Please select a user.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr)) 
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("usp_DeactivateUser", conn); 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userID", int.Parse(userID_txtbox.Text));
                cmd.ExecuteNonQuery();

                MessageBox.Show("User deactivated (login disabled).");
                LoadUsers(); 
            }
        }
        private void searchuser_combobox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (searchuser_combobox.SelectedValue == null) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE userID = @userID", conn);
                cmd.Parameters.AddWithValue("@userID", searchuser_combobox.SelectedValue);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userID_txtbox.Text = reader["userID"].ToString();
                    username_txtbox.Text = reader["username"].ToString();
                    pw_txtbox.Text = reader["password"]?.ToString();
                    email_txtbox.Text = reader["email"].ToString();
                    role_txtbox.Text = reader["roleID"].ToString();
                }
            }
        }

        private void ClearFields_Click(object sender, RoutedEventArgs e)
        {
            username_txtbox.Clear();
            userID_txtbox.Clear();
            pw_txtbox.Clear();
            email_txtbox.Clear();
            role_txtbox.Clear();
            searchuser_combobox.SelectedIndex = -1;
        }

        private void confirm1_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dashboardUM_btn1_Click(object sender, RoutedEventArgs e)
        {
           MyDashboard myDashboardWindow = new MyDashboard();
            myDashboardWindow.Show();
            this.Close();
        }

        private void auditUM_btn1_Click(object sender, RoutedEventArgs e)
        {
            AuditLog AuditLogWindow = new AuditLog(this);
            AuditLogWindow.Show();
            this.Close();
        }

        private void logoutPO_btn1_Click(object sender, RoutedEventArgs e)
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