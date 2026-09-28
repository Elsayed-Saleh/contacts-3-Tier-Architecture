using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ContactsDataAccessLayer
{
    public class clsDataContact
    {


        public static bool GetContactInfoByID(int ID, ref string FirstName, ref string LastName, ref string Email,
        ref string Phone, ref string Address, ref DateTime DateOfBirth, ref string ImagePath, ref int CountryID)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from Contacts where ContactID=@ContactID;";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("ContactID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];
                    ImagePath = (string)reader["ImagePath"];

                    

                }
                else
                {
                    isFound = false;
                }
                reader.Close();


            }
            catch
            {
                //Console.WriteLine("Error: "+ex.Message); 
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static bool GetContactByFirstName(ref int ContactID, string FirstName, ref string LastName, ref string Email,
        ref string Phone, ref string Address, ref DateTime DateOfBirth, ref string ImagePath, ref int CountryID)
        {
            bool isFound = false;

            SqlConnection connection=new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $"select* from Contacts where FirstName = @FirstName; ";

            SqlCommand command=new SqlCommand(query, connection);

            command.Parameters.AddWithValue("FirstName", FirstName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    isFound=true;
                    ContactID = (int)reader["ContactID"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];
                    ImagePath = (string)reader["ImagePath"];
                
                }
                else
                {
                    isFound = false;
                }
                reader.Close();

            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return isFound;


        }

        public static int AddNewContact(string FirstName, string LastName, string Email,
        string Phone, string Address, DateTime DateOfBirth, string ImagePath, int CountryID)
        {
            int ContactID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = (@"INSERT INTO Contacts (FirstName, LastName, Email, Phone, Address, 
                           DateOfBirth, CountryID, ImagePath) VALUES (@FirstName, @LastName, @Email
                           ,@Phone, @Address, @DateOfBirth,@CountryID, @ImagePath);
                            select scope_identity();");

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            if (ImagePath != null)
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ContactID = insertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return ContactID;

        }


        public static bool UpdateContact(int ContactID, string FirstName, string LastName, string Email,
        string Phone, string Address, DateTime DateOfBirth, string ImagePath, int CountryID)
        {
            int RowsAfeected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = (@"UPDATE Contacts 
                        SET FirstName = @FirstName,
                            LastName =@LastName,
                            Email = @Email,
                            Phone =@Phone,
                            Address =@Address,
                            DateOfBirth = @DateOfBirth,
                            CountryID = @CountryID,
                            ImagePath = @ImagePath 
                            WHERE ContactID=@ContactID;");
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("ContactID", ContactID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            if (ImagePath != null)
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                RowsAfeected = command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (RowsAfeected > 0);

        }

        public static bool DeleteContact(int ContactID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Contacts where ContactID=@ContactID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ContactID", ContactID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);


        }

        public static DataTable GetAllContacts()
        {

            DataTable dt = new DataTable();

            SqlConnection connection= new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Contacts;";

            SqlCommand command=new SqlCommand(query, connection);

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
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close( );
            }

            return dt;
        }

        public static bool IsContactExist(int ContactID)
        {
            bool isFound = false;

            SqlConnection connection=new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Found=1 from Contacts where ContactID=@ContactID;";

            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);

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


    }
}
