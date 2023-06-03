using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Rawson.DAL_Person
{
    public class DalPerson
    {
        public string connectionString =
            "Server=CHARLIE-PC;Database=Rawson;Integrated Security=SSPI";

        public void GetPeople()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM fourteenth.People";
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr["str_Names"].ToString());
                }
                connection.Close();
            }
        }

        public DataTable GetPeopleByIdentityNumber(string IdentityNumber)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery =
                    "SELECT * FROM fourteenth.People WHERE str_IdentityNum = '"
                    + IdentityNumber
                    + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);

                connection.Close();
            }
            return dt;
        }

        public void AddPerson(Person person, out int PersonId)
        {
            PersonId = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "Sp_AddPeople",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@IdentityNumber", SqlDbType.NVarChar, 50).Value =
                    person.IdentityNumber;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = person.Names;
                cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 50).Value = person.Surnames;
                cmd.Parameters.Add("@DateBirth", SqlDbType.DateTime2).Value = person.DateBirth;

                SqlParameter outParameter = new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(outParameter);

                connection.Open();

                cmd.ExecuteNonQuery();
                PersonId = Convert.ToInt32(outParameter.Value);
                connection.Close();
            }
        }
    }
}
