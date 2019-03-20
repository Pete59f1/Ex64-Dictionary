using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Dictionary
{
    public class DBController
    {
        private static string connectionString =
        "Server = ealSQL1.eal.local; Database = A_DB28_2018; User Id = A_STUDENT28; Password = A_OPENDB28;";
        
        public void AddMember(int id, string name, string status)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd1 = new SqlCommand("spAddMember", con);
                    cmd1.CommandType = CommandType.StoredProcedure; //SP

                    cmd1.Parameters.Add(new SqlParameter("@Id", id));
                    cmd1.Parameters.Add(new SqlParameter("@FName", name));
                    cmd1.Parameters.Add(new SqlParameter("@MStatus", status));

                    cmd1.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Ups " + e.Message);
                }
            }
        }

        public Dictionary<string, Member> GetAllMembers()
        {
            Dictionary<string, Member> members = new Dictionary<string, Member>();
            Member m;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("spGetAllMembers", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader read = cmd.ExecuteReader();
                    
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            int id = int.Parse(read["Id"].ToString());
                            string name = read["FName"].ToString();
                            string status = read["MStatus"].ToString();
                            members.Add(status, m = new Member { Id = id, Name = name });
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Duuuhh " + e.Message);
                }
            }
            return members;
        }
    }
}
