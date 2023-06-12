using Microsoft.Data.SqlClient;

string connectionString = "Server=localhost;Database=DbFirst;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();

    try
    {
        using (SqlCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Notes";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(reader.GetOrdinal("Id"));
                    string column1 = reader.GetString(reader.GetOrdinal("Name"));
                    string column2 = reader.GetString(reader.GetOrdinal("Description"));

                    Console.WriteLine($"Id: {id}, Name: {column1}, Description: {column2}");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred: " + ex.Message);
    }
}

Console.ReadLine();