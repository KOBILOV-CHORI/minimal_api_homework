using Dapper;
using Npgsql;

public class UserService
{
    public IEnumerable<User> GetAllUsers()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                return connection.Query<User>(SqlCommands.GetAllUsers);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public IEnumerable<User> GetUsersByFilter(string? filtername)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                return connection.Query<User>(SqlCommands.GetUsersByFilter, new { filtername });               
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public User? GetUserById(int id)
    {
        try
        {
            using ( NpgsqlConnection conn = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<User>(SqlCommands.GetUserById, new { Id = id });
            }
            
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool AddUser(User user)
    {
        try
        {
            using (NpgsqlConnection connection= new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                return connection.Execute(SqlCommands.AddUser, user) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool UpdateUser(User user)
    {
        try
        {
            using (NpgsqlConnection connection= new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                return connection.Execute(SqlCommands.UpdateUser, user) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool DeleteUser(int id)
    {
        try
        {
            using (NpgsqlConnection connection= new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                connection.Open();
                return connection.Execute(SqlCommands.DeleteUser, new {Id=id}) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}

file class SqlCommands
{
    public const string ConnectionString =
        "Server=localhost;Port=5432;Database=minimal_api_db;User Id=postgres;Password=01062007;";

    public const string GetUsersByFilter = @"SELECT * FROM users WHERE (@filtername IS NULL OR @filtername = ' ' OR name LIKE '%' || @filtername || '%')";
    public const string GetAllUsers = "SELECT * FROM users";
    public const string GetUserById = "SELECT * FROM users WHERE id = @id";
    public const string AddUser = "INSERT INTO users (name,age,address) VALUES (@name,@age,@address)";
    public const string UpdateUser = "UPDATE users SET name = @name WHERE id = @id";
    public const string DeleteUser = "DELETE FROM users WHERE id = @id";
}