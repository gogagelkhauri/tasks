using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;

string connectionString = "Server=localhost;Database=DbFirst;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    try
    {
        // Create a DataSet to hold multiple DataTables
        DataSet dataSet = new DataSet();

        // Create a DataTable with columns
        DataTable dataTable = new DataTable("Notes");
        DataColumn columnId = new DataColumn("Id", typeof(int));
        DataColumn columnName = new DataColumn("Name", typeof(string));
        DataColumn columnDescription = new DataColumn("Description", typeof(string));
        dataTable.Columns.AddRange(new DataColumn[] { columnId, columnName, columnDescription });

        // Add the DataTable to the DataSet
        dataSet.Tables.Add(dataTable);

        // Retrieve data using a SELECT query
        using (SqlCommand command = new SqlCommand("SELECT Id, Name, Description FROM Notes", connection))
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                // Fill the DataSet with data
                dataAdapter.Fill(dataSet, "Notes");
            }
        }

        // Display data from the DataTable
        DataTable notesTable = dataSet.Tables["Notes"];
        foreach (DataRow row in notesTable.Rows)
        {
            Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]}, Description: {row["Description"]}");
        }

        // Modify data in the DataTable
        DataRow newRow = notesTable.NewRow();
        newRow["Id"] = 4;
        newRow["Name"] = "John Doe";
        newRow["Description"] = "TestDesc";
        notesTable.Rows.Add(newRow);

        // Use a DbDataAdapter and SqlCommandBuilder to update the database
        using (DbDataAdapter dataAdapter = new SqlDataAdapter())
        {
            dataAdapter.SelectCommand = new SqlCommand("SELECT Id, Name, Description FROM Notes", connection);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder((SqlDataAdapter)dataAdapter);
            dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();

            // Update the database with changes made in the DataTable
            dataAdapter.Update(dataSet, "Notes");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred: " + ex.Message);
    }
}

Console.ReadLine();