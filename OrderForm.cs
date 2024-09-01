using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProductAdo
{
    public partial class OrderForm : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        public OrderForm()
        {
            InitializeComponent();
            RefreshDataGridView();
            dataGridViewOrders.SelectionChanged += dataGridViewOrders_SelectionChanged;
        }

        private void RefreshDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Orders";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable ordersTable = new DataTable();
                    adapter.Fill(ordersTable);
                    dataGridViewOrders.DataSource = ordersTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count > 0)
            {
                int selectedOrderID = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells["OrderID"].Value);
                DisplayOrderDetails(selectedOrderID);
            }
        }

        private void DisplayOrderDetails(int orderID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM OrderDetails WHERE OrderID = @OrderID";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@OrderID", orderID);
                    DataTable orderDetailsTable = new DataTable();
                    adapter.Fill(orderDetailsTable);
                    dataGridViewOrderDetails.DataSource = orderDetailsTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Orders (CustomerName, OrderDate, TotalAmount) VALUES (@CustomerName, @OrderDate, @TotalAmount)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
                    command.Parameters.AddWithValue("@OrderDate", DateTime.Value);
                    command.Parameters.AddWithValue("@TotalAmount", Convert.ToDecimal(txtTotalAmount.Text));
                    command.ExecuteNonQuery();
                    MessageBox.Show("Order added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count > 0)
            {
                int selectedOrderID = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells["OrderID"].Value);
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Orders WHERE OrderID = @OrderID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@OrderID", selectedOrderID);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Order deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshDataGridView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an order to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpenOrderDetailsForm_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count > 0)
            {
                int selectedOrderID = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells["OrderID"].Value);
                OrderDetailsForm orderDetailsForm = new OrderDetailsForm(selectedOrderID);
                orderDetailsForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select an order first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
