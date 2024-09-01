using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductAdo
{
    public partial class OrderDetailsForm : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        public OrderDetailsForm()
        {
            InitializeComponent();
            RefreshDataGridView();
        }

        // Method to refresh the DataGridView with updated data
        private void RefreshDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM OrderDetails";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
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

        private void btnAddOrderDetail_Click(object sender, EventArgs e)
        {// Add new order detail
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice, TotalPrice) VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice, @TotalPrice)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@OrderID", Convert.ToInt32(txtOrderID.Text));
                    command.Parameters.AddWithValue("@ProductID", Convert.ToInt32(txtProductID.Text));
                    command.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtQuantity.Text));
                    command.Parameters.AddWithValue("@UnitPrice", Convert.ToDecimal(txtUnitPrice.Text));
                    command.Parameters.AddWithValue("@TotalPrice", Convert.ToDecimal(txtTotalPrice.Text));
                    command.ExecuteNonQuery();
                    MessageBox.Show("Order detail added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDeleteOrderDetail_Click(object sender, EventArgs e)
        {// Delete selected order detail
            if (dataGridViewOrderDetails.SelectedRows.Count > 0)
            {
                int selectedOrderDetailID = Convert.ToInt32(dataGridViewOrderDetails.SelectedRows[0].Cells["OrderDetailID"].Value);
                // Delete the selected order detail
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM OrderDetails WHERE OrderDetailID = @OrderDetailID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@OrderDetailID", selectedOrderDetailID);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Order detail deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Please select an order detail to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void dataGridViewOrderDetails_SelectionChanged(object sender, EventArgs e)
        {

        }
        private int selectedOrderID;

        public OrderDetailsForm(int orderId)
        {
            InitializeComponent();
            this.selectedOrderID = orderId;
            RefreshDataGridView();
        }

        private void btnBackToOrderForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
