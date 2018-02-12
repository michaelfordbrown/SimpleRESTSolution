using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using SimpleRESTServer.Models;

namespace SimpleRESTServer
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonPersistence
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        /// <summary>
        /// 
        /// </summary>
        public PersonPersistence()
        {
            string myConnectionString;
            myConnectionString = "server=localhost;uid=root;database=employeedb";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Change assigned persion (ID) with new values.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="personToSave"></param>
        /// <returns></returns>
        public bool UpdatePerson(long ID, Person personToSave)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection
            {
                ConnectionString = "server=localhost;uid=root;database=employeedb"
            };

            try
            {
                conn.Open();

                MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

                string sqlString = "SELECT * FROM tblpersonnel WHERE ID = " + ID.ToString();
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    mySQLReader.Close();

                    sqlString = "UPDATE tblPersonnel SET FirstName='" + personToSave.FirstName +
                        "', LastName= '" + personToSave.LastName +
                        "', PayRate= '" + personToSave.PayRate +
                        "', StartDate= '" + personToSave.StartDate.ToString("yyyy-MM-dd HH:mm:ss") +
                        "', EndDate= '" + personToSave.EndDate.ToString("yyyy-MM-dd HH:mm:ss") +
                        "' WHERE ID = " + ID.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Remove Person resource using ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeletePerson(long ID)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection
            {
                ConnectionString = "server=localhost;uid=root;database=employeedb"
            };

            try
            {
                conn.Open();

                Person p = new Person();
                MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

                string sqlString = "SELECT * FROM tblpersonnel WHERE ID = " + ID.ToString();
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    mySQLReader.Close();

                    sqlString = "DELETE FROM tblpersonnel WHERE ID = " + ID.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }


        }

        /// <summary>
        /// Obtain details of all Person resources
        /// </summary>
        /// <returns></returns>
        public ArrayList GetPersons()
        {
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection
            {
                ConnectionString = "server=localhost;uid=root;database=employeedb"
            };
            ArrayList personsArray = new ArrayList();

            try
            {
                conn.Open();

                MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

                string sqlString = "SELECT * FROM tblpersonnel";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {
                    Person p = new Person();
                    p.ID = mySQLReader.GetInt32(0);
                    p.FirstName = mySQLReader.GetString(1);
                    p.LastName = mySQLReader.GetString(2);
                    p.PayRate = mySQLReader.GetFloat(3);
                    p.StartDate = mySQLReader.GetDateTime(4);
                    p.EndDate = mySQLReader.GetDateTime(5);
                    personsArray.Add(p);
                }

                return personsArray;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Obtain details of a single Person resource (by ID)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Person GetPerson(long ID)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection
            {
                ConnectionString = "server=localhost;uid=root;database=employeedb"
            };

            Person p = new Person();

            try
            {
                conn.Open();

                MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

                string sqlString = "SELECT * FROM tblpersonnel WHERE ID = " + ID.ToString();
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    p.ID = mySQLReader.GetInt32(0);
                    p.FirstName = mySQLReader.GetString(1);
                    p.LastName = mySQLReader.GetString(2);
                    p.PayRate = mySQLReader.GetFloat(3);
                    p.StartDate = mySQLReader.GetDateTime(4);
                    p.EndDate = mySQLReader.GetDateTime(5);
                    return p;
                }
                else
                {
                    return null;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Create a new Person resource, returning ID
        /// </summary>
        /// <param name="personToSave"></param>
        /// <returns></returns>
        public long SavePerson(Person personToSave)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection
            {
                ConnectionString = "server=localhost;uid=root;database=employeedb"
            };
            long id = 0;

            try
            {
                conn.Open(); String sqlString = "INSERT INTO tblPersonnel (FirstName, LastName, PayRate, StartDate, EndDate) VALUES ('" + personToSave.FirstName + "','" + personToSave.LastName + "','" + personToSave.PayRate + "','" + personToSave.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + personToSave.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                id = cmd.LastInsertedId;
                return id;
            }
            catch(MySql.Data.MySqlClient.MySqlException ex)
            {
                return id;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}