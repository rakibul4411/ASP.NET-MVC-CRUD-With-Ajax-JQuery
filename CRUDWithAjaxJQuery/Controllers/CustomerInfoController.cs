using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

namespace CRUDWithAjaxJQuery
{
    public class CustomerInfoController : Controller
    {
        #region crud with Entity Framework
        //private CRUDWithAjaxJQueryDB db = new CRUDWithAjaxJQueryDB();

        //// Get Customer List Data 
        //[HttpGet]
        //public async Task<ActionResult> GetCustomeList()
        //{
        //    try
        //    {
        //        //var data = await db.CustomerWiseBonusRate.ToListAsync();

        //        var data = await db.CustomerWiseBonusRate.Select(c => new
        //        {
        //            ID = c.Id,
        //            CustomerID = c.CustomerId,
        //            Name = c.CustomerName
        //            //BonusRate = c.BonusRate,
        //            //BonusDate = c.BonusDate.ToString()
        //        }).ToListAsync();

        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception EX)
        //    {
        //        Dictionary<string, object> content = new Dictionary<string, object>();
        //        content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
        //        return Json(content);
        //    }
        //}
        //// Add Customer data
        //[HttpPost]
        //public async Task<ActionResult> AddCustomer([Bind] CustomerWiseBonusRateVM cvm)
        //{
        //    try
        //    {

        //        CustomerWiseBonusRate cwbr = new CustomerWiseBonusRate();
        //        cwbr.Id = cvm.ID;
        //        cwbr.CustomerId = cvm.CustomerID;
        //        cwbr.CustomerName = cvm.Name;
        //        //cwbr.BonusRate = cvm.BonusRate;
        //        //cwbr.BonusDate = cvm.BonusDate;
        //        db.CustomerWiseBonusRate.Add(cwbr);
        //        await db.SaveChangesAsync();
        //        return Json(new { Message = "Add New Customer Successful" });
        //    }
        //    catch (Exception EX)
        //    {
        //        Dictionary<string, object> content = new Dictionary<string, object>();
        //        content.Add("ErrorMessage", $"Data Can't be Added. {EX.Message}");
        //        return Json(content);
        //    }
        //}
        //// Get Customer single data
        //[HttpGet]
        //public async Task<ActionResult> GetCustomerByID(int id)
        //{
        //    try
        //    {
        //        var cvm = await db.CustomerWiseBonusRate.Select(c => new
        //        {
        //            ID = c.Id,
        //            CustomerID = c.CustomerId,
        //            Name = c.CustomerName
        //            //BonusRate = c.BonusRate,
        //            //BonusDate = c.BonusDate.ToString()
        //        }).Where(i => i.ID == id).FirstOrDefaultAsync();
        //        return Json(cvm, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception EX)
        //    {
        //        Dictionary<string, object> content = new Dictionary<string, object>();
        //        content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
        //        return Json(content);
        //    }
        //}
        //// Edit Customer data
        //[HttpPost]
        //public async Task<ActionResult> EditCustomer([Bind] CustomerWiseBonusRateVM cvm)
        //{
        //    try
        //    {
        //        CustomerWiseBonusRate cwbr = new CustomerWiseBonusRate();
        //        cwbr.Id = cvm.ID;
        //        cwbr.CustomerId = cvm.CustomerID;
        //        cwbr.CustomerName = cvm.Name;
        //        //cwbr.BonusRate = cvm.BonusRate;
        //        //cwbr.BonusDate = cvm.BonusDate;
        //        db.Entry(cwbr).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return Json(new { Message = "Create sucessfully" });
        //    }
        //    catch (Exception EX)
        //    {
        //        Dictionary<string, object> content = new Dictionary<string, object>();
        //        content.Add("ErrorMessage", $"Data Can't be Edit. {EX.Message}");
        //        return Json(content);
        //    }
        //}
        //// Remove Customer data
        //[HttpPost]
        //public async Task<ActionResult> DeleteCustomer([Bind] CustomerWiseBonusRateVM cwbr)
        //{
        //    try
        //    {
        //        var emp = await db.CustomerWiseBonusRate.FindAsync(cwbr.ID);
        //        db.CustomerWiseBonusRate.Remove(emp);
        //        await db.SaveChangesAsync();
        //        return Json(new { message = "Delete successful" });
        //    }
        //    catch (Exception EX)
        //    {
        //        Dictionary<string, object> content = new Dictionary<string, object>();
        //        content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
        //        return Json(content);
        //    }
        //}

        //// Customer Uniqque ID Check
        //[HttpGet]
        //public async Task<ActionResult> GetCheckCustomerIDUniqueness(int id)
        //{
        //    try
        //    {
        //        var cvm = await db.CustomerWiseBonusRate.Select(c => new
        //        {
        //            CustomerID = c.CustomerId
        //        }).Where(i => i.CustomerID == id).FirstOrDefaultAsync();
        //        var errorMessage = new { Message = "Please Insert Unique Customer ID" };
        //        if (cvm != null)
        //        {
        //            return Json(errorMessage, JsonRequestBehavior.AllowGet);
        //        }
        //        return Json(cvm, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception EX)
        //    {
        //        Dictionary<string, object> content = new Dictionary<string, object>();
        //        content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
        //        return Json(content);
        //    }
        //}
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        #endregion


        /// <summary>
        /// ///////ASP.NET MVC CRUD WITH ADO.NET
        /// </summary>
         #region crud with ADO.NET
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["CRUDWithAjaxJQueryDB1"].ToString();
            con = new SqlConnection(constr);
        }

        // Get Customer List Data 
        [HttpGet]
        public ActionResult GetCustomeList()
        {
            try
            {
                connection();
                List<CustomerWiseBonusRateVM> CustomerList = new List<CustomerWiseBonusRateVM>();
                SqlCommand cmd = new SqlCommand("select Id,CustomerID,CustomerName from Customer.CustomerWiseBonusRate order by CustomerID", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();
                //Bind CustomerWiseBonusRateVM generic list using dataRow     
                foreach (DataRow dr in dt.Rows)
                {
                    CustomerList.Add(
                        new CustomerWiseBonusRateVM
                        {

                            ID = Convert.ToInt32(dr["Id"]),
                            CustomerID = Convert.ToInt64(dr["CustomerID"]),
                            Name = Convert.ToString(dr["CustomerName"])

                        }
                        );
                }

                var data = CustomerList;

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
                return Json(content);
            }
        }
        // Add Customer data
        [HttpPost]
        public ActionResult AddCustomer([Bind] CustomerWiseBonusRateVM cvm)
        {
            try
            {

                connection();
                SqlCommand cmd = new SqlCommand($"INSERT INTO Customer.CustomerWiseBonusRate(CustomerId,CustomerName)VALUES(@CustomerID,@Name)", con);
                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@CustomerID", cvm.CustomerID);
                cmd.Parameters.AddWithValue("@Name", cvm.Name);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {

                    return Json(new { Message = "Add New Customer Successful" });

                }
                else
                {
                    return Json(new { Message = "Add New Customer unuccessful" });
                }
                //return Json(new { Message = "Add New Customer Successful" });
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be Added. {EX.Message}");
                return Json(content);
            }
        }
        // Get Customer single data 
        [HttpGet]
        public ActionResult GetCustomerByID(int id)
        {
            try
            {
                connection();
                CustomerWiseBonusRateVM CustomerList = new CustomerWiseBonusRateVM();
                SqlCommand cmd = new SqlCommand($"select Id,CustomerID,CustomerName from Customer.CustomerWiseBonusRate where id ={id}", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();
                //Bind CustomerWiseBonusRateVM generic list using dataRow     

                foreach (DataRow dr in dt.Rows)
                {
                    CustomerList =
                        new CustomerWiseBonusRateVM
                        {

                            ID = Convert.ToInt32(dr["Id"]),
                            CustomerID = Convert.ToInt64(dr["CustomerID"]),
                            Name = Convert.ToString(dr["CustomerName"])

                        };

                }
                var data = CustomerList;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
                return Json(content);
            }
        }
        // Edit Customer data
        [HttpPost]
        public ActionResult EditCustomer([Bind] CustomerWiseBonusRateVM cvm)
        {
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("update Customer.CustomerWiseBonusRate set CustomerId = @CustomerId, CustomerName = @Name where id = @id", con);
                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", cvm.ID);
                cmd.Parameters.AddWithValue("@CustomerID", cvm.CustomerID);
                cmd.Parameters.AddWithValue("@Name", cvm.Name);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {

                    return Json(new { Message = "Update Successful" });

                }
                else
                {
                    return Json(new { Message = "Update unuccessful" });
                }
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be Edit. {EX.Message}");
                return Json(content);
            }
        }
        // Remove Customer data
        [HttpPost]
        public ActionResult DeleteCustomer([Bind] CustomerWiseBonusRateVM cvm)
        {
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("delete from Customer.CustomerWiseBonusRate where Id = @id", con);
                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", cvm.ID);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {

                    return Json(new { Message = "Update Successful" });

                }
                else
                {
                    return Json(new { Message = "Update unuccessful" });
                }
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
                return Json(content);
            }
        }

        // Customer Uniqque ID Check
        [HttpGet]
        public ActionResult GetCheckCustomerIDUniqueness(int id)
        {
            try
            {
                connection();
                CustomerWiseBonusRateVM CustomerList = new CustomerWiseBonusRateVM();
                SqlCommand cmd = new SqlCommand($"select Id,CustomerID from Customer.CustomerWiseBonusRate where CustomerID ={id}", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();
                //Bind CustomerWiseBonusRateVM generic list using dataRow     

                foreach (DataRow dr in dt.Rows)
                {
                    CustomerList =
                        new CustomerWiseBonusRateVM
                        {

                            ID = Convert.ToInt32(dr["Id"]),
                            CustomerID = Convert.ToInt64(dr["CustomerID"])

                        };

                }
                var data = CustomerList;
                var empty = new Dictionary<string, int>();
                var errorMessage = new { Message = "Please Insert Unique Customer ID" };
                if (data.CustomerID != 0)
                {
                    return Json(errorMessage, JsonRequestBehavior.AllowGet);
                }
                return Json(empty, JsonRequestBehavior.AllowGet);
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
                return Json(content);
            }
        }

        //Customer Data Transfer to one table to another
        [HttpGet]
        public ActionResult TransferCustomerData()
        {
            try
            {
                connection();
                var data = new Dictionary<string,string>();
                SqlCommand cmd = new SqlCommand("Customer.spTransferCustomerData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();
                //Bind CustomerWiseBonusRateVM generic list using dataRow     
                foreach (DataRow dr in dt.Rows)
                {
                    data.Add("Message", Convert.ToString(dr["Message"]));
                }

                if(data != null) { return Json(data, JsonRequestBehavior.AllowGet); }
                else 
                {
                    data.Add("Message", "Data Transfer Failed");
                    return Json(data, JsonRequestBehavior.AllowGet); 
                }
                
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
                return Json(content);
            }
        }
        #endregion
    }
}
