using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CustomerApplication.Model;


namespace CustomerApplication.Repository
{

    public class CustomerRepository
    {
        public DataTable ComboLoad()
        {
            try
            {
                string connstring = @"server=FARHANAMOSTO-PC; Database = CustomerDB;Integrated Security=true";
                SqlConnection conn = new SqlConnection(connstring);
                conn.Open();

                string cmdstring = @"select * from tblDistrict";
                SqlCommand cmd = new SqlCommand(cmdstring, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                conn.Close();
                if (datatable.Rows.Count > 0)
                {
                    return datatable;
                }
                else
                    return null;

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<DistrictModel> ComboLoadByList()
        {
            List<DistrictModel> distList = new List<DistrictModel>(); 
            try
            {
                string connstring = @"server=FARHANAMOSTO-PC; Database = CustomerDB;Integrated Security=true";
                SqlConnection conn = new SqlConnection(connstring);
                conn.Open();

                string cmdstring = @"select * from tblDistrict";
                SqlCommand cmd = new SqlCommand(cmdstring, conn);

                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    DistrictModel d = new DistrictModel();

                    d.DId = Convert.ToInt16(sdr["DId"].ToString());
                    d.District = sdr["District"].ToString();

                    distList.Add(d);
                }
                conn.Close();
            }
            catch (Exception)
            {
                
                throw;
            }

            return distList;
        }

        public bool IsCodeUnique(string Code, int CustId)
        {
            try
            {
                string cmdstring = "";
                string connstring = @"server=FARHANAMOSTO-PC; Database = CustomerDB;Integrated Security=true";
                SqlConnection conn = new SqlConnection(connstring);
                conn.Open();

                if (Code != null && CustId == 0)
                {
                    cmdstring = @"select * from Customer where Code='" + Code + "'";
                }
                else if (Code != null && CustId > 0)
                {
                    cmdstring = @"select * from Customer where Code='" + Code + "'" + " and CustId!=" + CustId.ToString();
                }

                SqlCommand cmd = new SqlCommand(cmdstring, conn);


                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                conn.Close();

                if (datatable.Rows.Count > 0)
                {
                    return true;
                }
                else
                    return false;

            }
            catch (Exception)
            {
                
                throw;
            }
            

        }

        public bool IsContactUnique(string Contact, int CustId)
        {
            try
            {
                string cmdstring = "";
                string connstring = @"server=FARHANAMOSTO-PC; Database = CustomerDB;Integrated Security=true";
                SqlConnection conn = new SqlConnection(connstring);
                conn.Open();

                if (Contact != null && CustId == 0)
                {
                    cmdstring = @"select * from Customer where Contact='" + Contact + "'";
                }
                else if (Contact != null && CustId > 0)
                {
                    cmdstring = @"select * from Customer where Contact='" + Contact + "'" + " and CustId!=" + CustId.ToString();
                }

                SqlCommand cmd = new SqlCommand(cmdstring, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                conn.Close();

                if (datatable.Rows.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }

            catch (Exception)
            {
                
                throw;
            }
            
        }

        public bool Insert(Customer C)
        {
            try
            {
                string connstring = @"server=FARHANAMOSTO-PC; Database = CustomerDB;Integrated Security=true";
                SqlConnection conn = new SqlConnection(connstring);
                conn.Open();

                string cmdstring = @"Insert into Customer(Code,Name,Address,Contact,DId) values('" + C.Code + "','" + C.Name + "','" + C.Address + "','" + C.Contact + "','" + C.DId.ToString() + "')";
                SqlCommand cmd = new SqlCommand(cmdstring, conn);

                int isExecuted = cmd.ExecuteNonQuery();
                conn.Close();

                if (isExecuted > 0)
                {
                    return true;
                }
                else
                    return false;
            }

            catch (Exception)
            {
                
                throw;
            }
            
        }

        public DataTable ShowData()
        {
            try
            {
                string connstring = @"server=FARHANAMOSTO-PC; Database = CustomerDB;Integrated Security=true";
                SqlConnection conn = new SqlConnection(connstring);
                conn.Open();

                string cmdstring = @"select * from CustomerDetails";
                SqlCommand cmd = new SqlCommand(cmdstring, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                conn.Close();

                if (datatable.Rows.Count > 0)
                {
                    return datatable;
                }
                else
                    return null;
            }

            catch (Exception)
            {
                
                throw;
            }
            

        }

        public bool Update(Customer c)
        {
            try
            {
                string connstring = @"server=FARHANAMOSTO-PC; Database = CustomerDB;Integrated Security=true";
                SqlConnection conn = new SqlConnection(connstring);
                conn.Open();

                string cmdstring = @"update Customer set Code='" + c.Code + "',Name='" + c.Name + "',Address='" + c.Address + "',Contact='" + c.Contact + "',DId='" + c.DId + "'where CustId=" + c.CustId;
                SqlCommand cmd = new SqlCommand(cmdstring, conn);

                int isExecuted = cmd.ExecuteNonQuery();
                conn.Close();

                if (isExecuted > 0)
                {
                    return true;
                }
                else
                    return false;
            }

            catch (Exception)
            {
                
                throw;
            }
            
        }

        public DataTable Search(string Code)
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CustomerDB;Integrated Security=True";
                SqlConnection sqlconnection = new SqlConnection(ConnectionString);
                sqlconnection.Open();

                string CommandString = @"select * from CustomerDetails where Code='" + Code + "'";
                SqlCommand sqlcommand = new SqlCommand(CommandString, sqlconnection);

                SqlDataAdapter sqladapter = new SqlDataAdapter(sqlcommand);
                DataTable datatable = new DataTable();
                sqladapter.Fill(datatable);
                sqlconnection.Close();

                if (datatable.Rows.Count > 0)
                {
                    return datatable;
                }
                else
                {
                    return null;

                }
            }

            catch (SqlException ex)
            {
                //string msg =ex.Message.ToString();
                throw;
                
            }
        }

    }
}
