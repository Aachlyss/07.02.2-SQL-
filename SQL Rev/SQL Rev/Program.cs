using System;
using System.Data.SqlClient;

namespace SQL_Rev
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Microsoft SQL Server:");
            Console.WriteLine(new string('-', 25));

            SqlConnection dbConn = new SqlConnection
                (
                "Server=.;Database=master;Integrated Security=true"
                );
            dbConn.Open();

            using (dbConn)
            {
                SqlCommand command1 = new SqlCommand
                    (
                    "CREATE DATABASE Minions", dbConn
                    );
                command1.ExecuteNonQuery();

                SqlCommand command2 = new SqlCommand
                    (
                    "USE Minions", dbConn
                    );
                command2.ExecuteNonQuery();

                SqlCommand command3 = new SqlCommand
                    (
                    "CREATE TABLE Countries (Id INT IDENTITY PRIMARY KEY, Name VARCHAR(100) NOT NULL)" +
                    "CREATE TABLE Towns (Id INT IDENTITY PRIMARY KEY, Name VARCHAR(100) NOT NULL,CountryCode INT," +
                    "CONSTRAINT FK_Towns_Countries FOREIGN KEY(CountryCode) REFERENCES Countries(Id))" +
                    "CREATE TABLE Minions(Id INT IDENTITY PRIMARY KEY, Name VARCHAR(100) NOT NULL, Age INT, TownId INT," +
                    "CONSTRAINT FK_Minions_Towns FOREIGN KEY(TownId) REFERENCES Towns(Id))" +
                    "CREATE TABLE EvilnessFactors(Id INT IDENTITY PRIMARY KEY, Name VARCHAR(100) NOT NULL)" +
                    "CREATE TABLE Villains(Id INT IDENTITY PRIMARY KEY, Name VARCHAR(100), EvilnessFactorId INT," +
                    "CONSTRAINT FK_Villains_EvilnessFactors FOREIGN KEY(EvilnessFactorId) REFERENCES EvilnessFactors(Id))" +
                    "CREATE TABLE MinionsVillains(MinionId INT, VillainId INT, CONSTRAINT PK_MinionsVillains PRIMARY KEY(MinionId, VillainId)," +
                    "CONSTRAINT FK_MinionsVillains_Minions FOREIGN KEY(MinionId) REFERENCES Minions(Id)," +
                    "CONSTRAINT FK_MinionsVillains_Villains FOREIGN KEY(VillainId) REFERENCES Villains(Id))"
                    , dbConn

                    );
                command3.ExecuteNonQuery();

                SqlCommand command5 = new SqlCommand
                    (
                    "SELECT name,age FROM minions;", dbConn
                    );
                SqlDataReader reader = command5.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("Name: {0}, Age: {1}", reader[0], reader[1]);
                }
            }
        }
    }
}
