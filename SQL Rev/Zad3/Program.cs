using System;
using System.Data.SqlClient;

namespace Zad3
{
    class Program
    {
        static void Main(string[] args)
        {
            int villianID = int.Parse(Console.ReadLine());

            using (var conn = new SqlConnection("Server=.;Database=Minions;Integrated Security=true"))
            {
                conn.Open();
                SqlCommand sql = new SqlCommand
                    (
                    "SELECT minions.name " +
                    "FROM minionsvillians " +
                    "JOIN minions on minionsvillains.MinionId = minions.id " +
                    $"WHERE minionsvillains.VillianId = @villainID", conn
                    );
                sql.Parameters.AddWithValue("@villainID", villianID);
                var reader = sql.ExecuteReader();

                bool hasNames = false;
                int i = 1;
                while (reader.Read())
                {
                    hasNames = true;
                    Console.WriteLine("{0}. {1}",i++, reader[0]);
                }
                if (hasNames == false)
                {
                    Console.WriteLine($"No villain with ID {villianID} exists in the database.");
                }
            }
        }
    }
}
