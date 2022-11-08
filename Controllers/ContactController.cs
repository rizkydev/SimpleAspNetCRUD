using SimpleAspNetCRUD.Model;
using SimpleAspNetCRUD.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace SimpleAspNetCRUD.Controllers
{
    public class ContactController
    {
        //SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4OCA0H5;Initial Catalog=TestDB;Integrated Security=true;");
        //SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-4OCA0H5;Initial Catalog=TestDB;Integrated Security=true;User ID=sa;Password=jalanwalet1");
        //SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["BaseConnectionString"].ToString()); //Ambil dari web.config

        public DataTable GetListData()
        {
            var dat = new DataTable();
            using (var Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BaseConnectionString"].ToString()))
            {
                //Karena menggunakan using. maka koneksi atu dispose. tidak perlu cek koneksi sudah di tutup atau belum
                //try
                //{
                //    if (Conn.State == ConnectionState.Closed)
                //    {
                //        Conn.Open();
                //    }
                //    var sqlDa = new SqlDataAdapter("ContactViewAll", Conn);
                //    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                //    sqlDa.Fill(dat);
                //    if (Conn.State == ConnectionState.Open)
                //    {
                //        Conn.Close();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    if (Conn.State == ConnectionState.Open)
                //    {
                //        Conn.Close();
                //    }
                //}

                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }
                var sqlDa = new SqlDataAdapter("ContactViewAll", Conn);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dat);
            }
            return dat;
        }

        public DataTable GetListData(Int32 id)
        {
            var dat = new DataTable();
            using (var Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BaseConnectionString"].ToString()))
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }
                var sqlDa = new SqlDataAdapter("ContactViewByID", Conn);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("@ContactID", id);
                sqlDa.Fill(dat);
            }
            return dat;
        }

        public bool SaveData(ContactEntity inputDat)
        {
            var returnDat = false;
            using (var Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BaseConnectionString"].ToString()))
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }
                SqlTransaction trans = Conn.BeginTransaction();
                try
                {                    
                    var sqlCmd = new SqlCommand("ContactCreateOrUpdate", Conn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = trans;
                    sqlCmd.Parameters.AddWithValue("@ContactID", inputDat.ContactID);
                    sqlCmd.Parameters.AddWithValue("@Name", inputDat.Name);
                    sqlCmd.Parameters.AddWithValue("@Mobile", inputDat.Mobile);
                    sqlCmd.Parameters.AddWithValue("@Address", inputDat.Address);
                    sqlCmd.ExecuteNonQuery();

                    trans.Commit();
                    returnDat = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
                
            }
            return returnDat;
        }

        public bool DeleteData(Int32 id)
        {
            var returnDat = false;
            using (var Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BaseConnectionString"].ToString()))
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }
                SqlTransaction trans = Conn.BeginTransaction();
                try
                {
                    var sqlCmd = new SqlCommand("ContactDeleteByID", Conn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Transaction = trans;
                    sqlCmd.Parameters.AddWithValue("@ContactID", id);
                    sqlCmd.ExecuteNonQuery();

                    trans.Commit();
                    returnDat = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }

            }
            return returnDat;
        }
    }
}