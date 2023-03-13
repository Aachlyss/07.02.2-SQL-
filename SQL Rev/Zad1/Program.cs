using System;
using System.Data.SqlClient;

namespace Zad1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var conn = new SqlConnection("Server=.;Database=Minions;Integrated Security=true"))
            {
                conn.Open();
                SqlCommand command1 = new SqlCommand
                    ("SELECT V.Name, COUNT(MV.VillianId) "+
                    "FROM Villians as V " + 
                    "JOIN MinionsVillians AS MV on MV.VillianId = V.Id " +
                    "GROUP BY MV.VillianId, V.Name " + 
                    "HAVING COUNT(MV.VillianId) > 3",conn   
                    );
                var reader = command1.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} -> {reader[1]}");
                }
            }
        }
    }
}
