using Microsoft.Data.SqlClient;

string connectionString = "Server=localhost;Database=DbFirst;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    SqlTransaction transaction = connection.BeginTransaction();

    try
    {
        using (SqlCommand command = connection.CreateCommand())
        {
            command.Transaction = transaction;
            command.CommandText = "INSERT INTO Notes (Name, Description) VALUES (@value1, @value2)";
            command.Parameters.AddWithValue("@value1", "task8Name");
            command.Parameters.AddWithValue("@value2", "task8 description");
            command.ExecuteNonQuery();
        }

        transaction.Commit();

        Console.WriteLine("Transaction committed successfully.");
    }
    catch (Exception ex)
    {
        transaction.Rollback();

        Console.WriteLine("Transaction rolled back due to an error: " + ex.Message);
    }
}
