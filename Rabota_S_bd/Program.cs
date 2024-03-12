using System;
using Microsoft.Data.SqlClient;


class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            string connectionString = "Server=DESKTOP-5BD88QO\\SQLEXPRESS;Database=CompanyDB;Trusted_Connection=True;TrustServerCertificate=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM Employees;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["EmployeeID"]} {reader["FirstName"]} {reader["LastName"]}, {reader["Position"]}, {reader["Salary"]}");
                        }
                    }
                }


              
                using (SqlCommand update = new SqlCommand(sql, connection))
                {
                    int number = await update.ExecuteNonQueryAsync();
                    Console.WriteLine("\nВведите Salary:");
                    int salary = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Введите ID:");
                    string employeeID = Console.ReadLine();
                    sql = $"UPDATE Employees SET Salary='{salary}' WHERE EmployeeID={employeeID}"; update.CommandText = sql;
                    number = await update.ExecuteNonQueryAsync(); Console.WriteLine($"\nОбновлено объектов: {number}");
                }



              
                using (SqlCommand delete = new SqlCommand(sql, connection))
                {
                    int number = await delete.ExecuteNonQueryAsync(); Console.WriteLine("Введите ID для удаления:");
                    string empID = Console.ReadLine(); sql = $"USE CompanyDB DELETE FROM Employees WHERE EmployeeID={empID}";
                    delete.CommandText = sql; number = await delete.ExecuteNonQueryAsync();
                    Console.WriteLine($"\nУдалено объектов:{number}");
                }
            }
        }
    }
}