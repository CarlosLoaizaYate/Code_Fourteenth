using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Rawson.DAL_ContactInformation
{
    public class DalContactInformation
    {
        public string connectionString =
            "Server=CHARLIE-PC;Database=Rawson;Integrated Security=SSPI";

        public void AddContactInformation(ContactInformation ContactInformation, int PersonId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "Sp_AddContactInformation",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add("@PersonId", SqlDbType.Int).Value = PersonId;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value =
                    ContactInformation.Email;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 50).Value =
                    ContactInformation.Address;
                cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 50).Value =
                    ContactInformation.PhoneNumber;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
