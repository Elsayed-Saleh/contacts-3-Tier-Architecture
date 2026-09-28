using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsDataCountry
    {

        public static bool GetCointryByName(string CountryName, ref int CountryID, ref string Code, ref string PhoneCode)
        {
            bool isFound = false;

            SqlConnection connection=new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from Countries where CountryName=@CountryName";
            SqlCommand command=new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try 
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    isFound = true;
                    CountryID = (int)reader["CountryID"];
                    Code = (string)reader["Code"];
                    PhoneCode = (string)reader["PhoneCode"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }
        
        public static int AddCountry(string CountryName, string Code, string PhoneCode)
        {
            int ID = -1;
            SqlConnection connection=new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Countries (CountryName, Code, PhoneCode) VALUES(@Name, @Code, @PhoneCode);
                             select SCOPE_IDENTITY();";

            SqlCommand command=new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", CountryName);
            command.Parameters.AddWithValue("@Code", Code);
            command.Parameters.AddWithValue("@PhoneCode", PhoneCode);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int Inserted))
                {
                    ID = Inserted;
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return ID;
        }

        public static bool UpdateCountry(int CountryID, string CountryName, string Code, string PhoneCode)
        {
            int RowsAffected = 0;

            SqlConnection connection=new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "update Countries set CountryName=@CountryName, Code=@Code, PhoneCode=@PhoneCode where CountryID=@CountryID;";

            SqlCommand Command=new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@CountryID", CountryID);
            Command.Parameters.AddWithValue("@CountryName", CountryName);
            Command.Parameters.AddWithValue("@Code", Code);
            Command.Parameters.AddWithValue("@PhoneCode", PhoneCode);

            try
            {
                connection.Open();
                RowsAffected = Command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close() ;

            }

            return (RowsAffected > 0);

        }

        public static bool DeleteCountry(string CountryName)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"delete from Countries where CountryName=;@CountryName";

            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName );

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close() ;
            }
            return (RowsAffected > 0);
        }
        public static bool DeleteCountry(int CountryID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"delete from Countries where CountryName=;@CountryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);
        }

        public static bool IsContryExist(string CountryName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found=1 from Countries where CountryName=@CountryName;";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;
        
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select * from Countries";

            SqlCommand command= new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                
            }
            
            finally
            {
                connection.Close();
            }
            return dt;
        }
    }
}
