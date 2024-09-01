using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ProductAdo
{
    public partial class MainForm : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        private string selectedImagePath = null;

        public MainForm()
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
                    string query = "SELECT * FROM Products";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable productsTable = new DataTable();
                    adapter.Fill(productsTable);
                    dgDisplay.DataSource = productsTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Add new product
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Products (ProductName, Description, Price, StockQuantity, ImagePath) VALUES (@ProductName, @Description, @Price, @StockQuantity, @ImagePath)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                    command.Parameters.AddWithValue("@Description", txtDescription.Text);
                    command.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                    command.Parameters.AddWithValue("@StockQuantity", Convert.ToInt32(txtStockQuantity.Text));
                    command.Parameters.AddWithValue("@ImagePath", selectedImagePath ?? "default_image_path.jpg");
                    command.ExecuteNonQuery();
                    MessageBox.Show("Product added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshDataGridView();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgDisplay.SelectedRows.Count > 0)
            {
                int selectedProductID = Convert.ToInt32(dgDisplay.SelectedRows[0].Cells["ProductID"].Value);
                // Update the selected product
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Products SET ProductName = @ProductName, Description = @Description, Price = @Price, StockQuantity = @StockQuantity, ImagePath = @ImagePath WHERE ProductID = @ProductID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                        command.Parameters.AddWithValue("@Description", txtDescription.Text);
                        command.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                        command.Parameters.AddWithValue("@StockQuantity", Convert.ToInt32(txtStockQuantity.Text));
                        command.Parameters.AddWithValue("@ImagePath", selectedImagePath ?? "default_image_path.jpg");
                        command.Parameters.AddWithValue("@ProductID", selectedProductID);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Product updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshDataGridView();
                        connection.Close();
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Please select a product to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgDisplay.SelectedRows.Count > 0)
            {
                int selectedProductID = Convert.ToInt32(dgDisplay.SelectedRows[0].Cells["ProductID"].Value);
                // Delete the selected product
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ProductID", selectedProductID);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Please select a product to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //  searching products
            string searchText = txtBoxSearch.Text;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Products WHERE ProductName LIKE @SearchText OR Description LIKE @SearchText";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");
                    DataTable filteredTable = new DataTable();
                    adapter.Fill(filteredTable);
                    dgDisplay.DataSource = filteredTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void dgDisplay_SelectionChanged(object sender, EventArgs e)
        {
            // When the selection in DataGridView changes, load the corresponding image into the PictureBox
            if (dgDisplay.SelectedRows.Count > 0)
            {
                string imagePath = dgDisplay.SelectedRows[0].Cells["ImagePath"].Value.ToString();
                LoadImage(imagePath);
            }
        }

        private void LoadImage(string imagePath)
        {
            // Check if the image path is not empty or null
            if (!string.IsNullOrEmpty(imagePath))
            {
                try
                {
                    // Load the image from the specified path into the PictureBox
                    pictureBox.Image = Image.FromFile(imagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // If the image path is empty or null, clear the PictureBox
                pictureBox.Image = null;
            }
        }

        private void btnBrowse_Click_1(object sender, EventArgs e)
        {
            // Open file dialog to select image file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path and display it in the textbox
                selectedImagePath = openFileDialog.FileName;
                txtImagePath.Text = selectedImagePath;

                // Display the selected image in the PictureBox
                LoadImage(selectedImagePath);
            }

        }

        private void btnOpenOrderForm_Click(object sender, EventArgs e)
        {
            OrderForm orderForm = new OrderForm();
            orderForm.ShowDialog();
        }
    }
}
