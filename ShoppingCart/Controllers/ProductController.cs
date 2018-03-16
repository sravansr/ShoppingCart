using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
namespace ShoppingCart.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View();
        }

        [HttpGet]
        public JsonResult SaveProductDetails(string ProductName, string ProductQuality, int Price, string Description)
        {


            string connctionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connctionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "usp_SaveProductDetails";

            cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = ProductName;

            cmd.Parameters.Add("@ProductQuality", SqlDbType.NVarChar).Value = ProductQuality;

            cmd.Parameters.Add("@Price", SqlDbType.Int).Value = Price;

            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description;


            cmd.Connection = con;

            try

            {

                con.Open();

                cmd.ExecuteNonQuery();

                // lblMessage.Text = "Record inserted successfully";

            }

            catch (Exception ex)

            {

                throw ex;

            }

            finally

            {

                con.Close();

                con.Dispose();

            }
            return Json("saved successfully.", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductDetails()
        {
            DataTable dt = new DataTable();
            List<ProductResponse> lstProductResponse = new List<ProductResponse>();
            try
            {
                string connctionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connctionString))
                using (SqlCommand cmd = new SqlCommand("usp_GetProductDetails", conn))
                {


                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapt.Fill(dt);

                    foreach (DataRow productrow in dt.Rows)
                    {
                        ProductResponse produtResponse = new ProductResponse();
                        produtResponse.ProductID = Convert.ToInt32(productrow["ProductId"]);
                        produtResponse.ProductQuality = productrow["ProductQuality"].ToString();
                        produtResponse.ProductName = productrow["ProductName"].ToString();
                        produtResponse.ProductDescription = productrow["ProductDescription"].ToString();
                        produtResponse.Price = Convert.ToInt32(productrow["Price"]);
                        lstProductResponse.Add(produtResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

            return Json(lstProductResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetProductDetailsById(int ProductId)
        {
            try
            {
                string connctionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connctionString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "usp_GetProductDetailsByID";
                    cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = ProductId;
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader productData = cmd.ExecuteReader();
                    ProductResponse produtResponse = new ProductResponse();
                    if (productData.HasRows)
                    {
                        while (productData.Read())
                        {
                            produtResponse.ProductID = productData.GetInt32(0);
                            produtResponse.ProductQuality = productData.GetString(2);
                            produtResponse.ProductName = productData.GetString(1);
                            produtResponse.ProductDescription = productData.GetString(4);
                            produtResponse.Price = productData.GetInt32(3);
                        }
                    }
                    return Json(produtResponse, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdateProductDetails(string ProductName, string ProductQuality, int Price, string Description,int ProductId)
        {
           
            //getting the connection string from web.config file
            string connctionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            //creating connection string
            SqlConnection con = new SqlConnection(connctionString);
            //creating sql command for executing queries or  store procedures
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "usp_UpdateProductDetails";

            //assigning parameters to store procedure
            cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = ProductName;

            cmd.Parameters.Add("@ProductQuality", SqlDbType.NVarChar).Value = ProductQuality;

            cmd.Parameters.Add("@Price", SqlDbType.Int).Value = Price;

            cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description;

            cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = ProductId;

            cmd.Connection = con;

            try

            {
                //opening conection
                con.Open();
                //executing the command
                cmd.ExecuteNonQuery();


            }

            catch (Exception ex)

            {

                throw ex;

            }

            finally

            {

                con.Close();

                con.Dispose();

            }
            return Json("Product updated  successfully.", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteProductDetailsById(int ProductId)
        {

            //getting the connection string from web.config file
            string connctionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            //creating connection string
            SqlConnection con = new SqlConnection(connctionString);
            //creating sql command for executing queries or  store procedures
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "usp_DeleteProductById";

            //assigning parameters to store procedure
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductId;

            cmd.Connection = con;

            try

            {
                //opening conection
                con.Open();
                //executing the command
                cmd.ExecuteNonQuery();


            }

            catch (Exception ex)

            {

                throw ex;

            }

            finally

            {

                con.Close();

                con.Dispose();

            }
            return Json("Product updated  successfully.", JsonRequestBehavior.AllowGet);
        }
    }
}