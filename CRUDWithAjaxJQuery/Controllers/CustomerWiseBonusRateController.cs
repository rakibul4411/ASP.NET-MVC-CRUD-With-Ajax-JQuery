using RoseITAssignment;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CRUDWithAjaxJQuery
{
    public class CustomerWiseBonusRateController : Controller
    {

        private RoseITAssignmentDB db = new RoseITAssignmentDB();

        public ActionResult CustomerWiseBonus()
        {
            return View();
        }
        // Get customer id with Autocomplete search
        [HttpGet]
        public async Task<ActionResult> AutoCompleteSerach(string id)
        {
            try
            {

                var data = await (from c in db.CustomerWiseBonusRate
                                  where c.CustomerId.ToString().StartsWith(id) && ((c.BonusRate == null || c.BonusRate == 0) || (c.BonusDate == null || c.BonusDate.ToString() == ""))
                                  select new {CustomerID = c.CustomerId }).ToListAsync();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
                return Json(content);
            }
        }

        // Get Customer List Data with bonus rate
        [HttpGet]
        public async Task<ActionResult> GetCustomeWiseBonusRateList()
        {
            try
            {
                //var data = await db.CustomerWiseBonusRate.ToListAsync();

                var data = await db.CustomerWiseBonusRate.Select(c => new
                {
                    ID = c.Id,
                    CustomerID = c.CustomerId,
                    Name = c.CustomerName,
                    BonusRate = c.BonusRate,
                    BonusDate = c.BonusDate.ToString()
                }).Where(i => (i.BonusRate != null && i.BonusRate != 0) || (i.BonusDate != null && i.BonusDate !="")).ToListAsync();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
                return Json(content);
            }
        }

        // Get Customer single data with bonus info
        [HttpGet]
        public async Task<ActionResult> GetCustomerWiseForBonusRateByCustomerID(int id)
        {
            try
            {
                var cvm = await db.CustomerWiseBonusRate.Select(c => new
                {
                    ID = c.Id,
                    CustomerID = c.CustomerId,
                    Name = c.CustomerName
                }).Where(i => i.CustomerID == id).FirstOrDefaultAsync();
                return Json(cvm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
                return Json(content);
            }
        }

        // Get Customer single data for customer wise bonus rate update
        [HttpGet]
        public async Task<ActionResult> GetCustomerWiseBonusRateByCustomerIDforUpdate(int id)
        {
            try
            {
                var cvm = await db.CustomerWiseBonusRate.Select(c => new
                {
                    ID = c.Id,
                    BonusRate = c.BonusRate,
                    BonusDate = c.BonusDate.ToString()
                }).Where(i => i.ID == id).FirstOrDefaultAsync();
                return Json(cvm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
                return Json(content);
            }
        }
        // Add Customer bonus rate data
        [HttpPost]
        public async Task<ActionResult> AddCustomerWiseBonusRate([Bind] CustomerWiseBonusRateVM cvm)
        {
            try
            {
                var data = db.CustomerWiseBonusRate.FirstOrDefault(i => i.Id == cvm.ID);

                if (data != null)
                {
                    data.BonusRate = cvm.BonusRate;
                    data.BonusDate = cvm.BonusDate;
                    db.Entry(data).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return Json(new { Message = "Data Update sucessful",IsUpdate = true });
                }
                else
                {
                    return Json(new { Message = "Data update unsucessful", IsUpdate = true });
                }
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                var data = EX.Message;
                content.Add("IsUpdate", false);
                content.Add("Message", $"Bonus rate can't be added over 99.Please enter a number between 1 to 99.");
                return Json(content);
            }
        }
    }
}