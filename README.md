# ADO.NET Project with Master-Details

## Overview

The **ADO.NET Project with Master-Details** is a Windows Forms application that demonstrates how to use ADO.NET to implement a Master-Details approach for managing data. Built with the ADO.NET framework and MS SQL Server, this project showcases the use of connected classes to interact with a database, providing a practical example of data management in a desktop application.

## Features

- **Master-Details Interface:** Display and manage a list of master records and their associated details.
- **Data Operations:** Perform Create, Read, Update, and Delete (CRUD) operations using ADO.NET.
- **Database Integration:** Connects to an MS SQL Server database for data storage and retrieval.
- **Windows Forms UI:** Utilizes a user-friendly Windows Forms interface for interacting with the data.

## Technologies Used

- ADO.NET
- Microsoft SQL Server
- C#
- Windows Forms
- SQL Server Management Studio (SSMS)

## Project Structure

- **Forms:** Contains Windows Forms for displaying and interacting with data.
- **Data Access Layer:** Includes classes responsible for database connectivity and operations using ADO.NET.
- **Database:** SQL scripts for database schema creation and sample data.

## Getting Started

### Prerequisites

- Microsoft SQL Server
- SQL Server Management Studio (SSMS)
- Visual Studio 2019 or later

### Installation

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/TausifImtiaz/ADO_DOTNET_Project_WithMasterDetails.git
   ```

2. **Open the Solution:**
   Open the `.sln` file in Visual Studio.

3. **Restore NuGet Packages:**
   Ensure all required NuGet packages are restored by building the solution or using the NuGet Package Manager.

4. **Set Up the Database:**
   - Open SQL Server Management Studio (SSMS).
   - Run the `DatabaseSchema.sql` script to create the necessary tables and schema.
   - Run the `SampleData.sql` script to populate the database with sample data.

5. **Configure Connection String:**
   - Open `App.config` and update the connection string to point to your SQL Server instance.

6. **Run the Application:**
   Press `F5` or click on the "Start" button in Visual Studio to build and run the application.

## Usage

1. **Access the Application:**
   - Launch the application from Visual Studio or the compiled executable.

2. **Master-Details Interaction:**
   - Use the master list to select a record and view or edit its details.
   - Perform CRUD operations using the provided buttons and forms.

3. **Data Management:**
   - Add new records or update existing ones through the master-detail forms.
   - Delete records as needed.

## Code Example

### Data Access Layer Example

```csharp
public class DataAccess
{
    private string connectionString = "your_connection_string_here";

    public DataTable GetMasterData()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM MasterTable", connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }
    }

    public void AddDetailRecord(Detail detail)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("INSERT INTO DetailTable (MasterID, DetailInfo) VALUES (@MasterID, @DetailInfo)", connection);
            command.Parameters.AddWithValue("@MasterID", detail.MasterID);
            command.Parameters.AddWithValue("@DetailInfo", detail.DetailInfo);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
```

## Contributing

Contributions are welcome! To contribute:
- Fork the repository.
- Create a feature branch.
- Commit your changes.
- Push to the branch.
- Open a pull request with a description of your changes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgements

- ADO.NET documentation
- SQL Server documentation
- Windows Forms resources

## Contact

For any questions or support, please contact [Tausif Imtiaz](mailto:tausifimtiaz@gmail.com).

