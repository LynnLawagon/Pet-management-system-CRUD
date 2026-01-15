using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Pet_Information_Table
{
    internal class Pet
    {

        private MySqlConnection dbConn;
        private MySqlCommand sqlCommand;
        private MySqlDataAdapter da;
        private DataTable dt;

        private String strConn = "Server=127.0.0.1;Port=3306;Database=db_pet_test;Uid=root;Pwd=";
        //private String strConn = "server=localhost;uid=root;pwd=;database=";

        public void dbConnection()
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
            MessageBox.Show("Test Connection successful!");
            dbConn.Close();
        }

        public void dbConnection(String dbName)
        {
            dbConn = new MySqlConnection(strConn);
            dbConn.Open();
            MessageBox.Show("Test Connection successful!");
            dbConn.Close();
        }

        public void displayRecords(String strQuery, DataGridView DG)
        {
            dbConn.Open();
            da = new MySqlDataAdapter(strQuery, dbConn);
            dt = new DataTable();
            da.Fill(dt);
            DG.DataSource = dt;
            dbConn.Close();
        }

        public DataTable displayRecords(String strQuery)
        {
            dbConn.Open();
            da = new MySqlDataAdapter(strQuery, dbConn);
            dt = new DataTable();
            da.Fill(dt);
            dbConn.Close();
            return dt;
        }

        public void SQLManager(String strQuery)
        {
            dbConn.Open();
            sqlCommand = new MySqlCommand(strQuery, dbConn);
            sqlCommand.ExecuteNonQuery();
            dbConn.Close();
        }
    }
}
