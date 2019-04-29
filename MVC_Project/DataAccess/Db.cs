using System.Data.SqlClient;
using MVC_Project.Models;
using System.Web;
using System.Data;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
namespace MVC_Project.DataAccess
{

    public class Db
    {
        private SqlConnection con;
        private SqlCommand com;
        private void connection()
        {
            string constr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con = new SqlConnection(constr);
        }
        Company cp = new Company();
        public string Add_companys(Company cp)
        {
            SqlCommand com = new SqlCommand("Company_insert", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@symbol", cp.Symbol);
            com.Parameters.AddWithValue("@date",cp.Date);
            com.Parameters.AddWithValue("@iexId", cp.IexId);
            com.Parameters.AddWithValue("@isEnabled", cp.IsEnabled);
            com.Parameters.AddWithValue("@name", cp.Name);
            com.Parameters.AddWithValue("@type",cp.Type);
            con.Open();
            int i=com.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                return "New Company is added.";
            }
            else
            {
                return "No company added";
            }
        }
    }
}
