using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using MVC_part1.Models;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Web.Helpers;
using System.Xml;

namespace WebApplication1.Controllers
{
    public class CentreController : Controller
    {
        private StudentGatewayDB db = new StudentGatewayDB();

        // GET: /Centre/
        public ActionResult Index()
        {
            var centres = db.Centres.Include(c => c.District).Include(c => c.Thana);
            return View(centres.ToList());
        }

        // GET: /Centre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Centre centre = db.Centres.Find(id);
            if (centre == null)
            {
                return HttpNotFound();
            }
            return View(centre);
        }

        // GET: /Centre/Create
        public ActionResult Create()
        {
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "Name");
            ViewBag.ThanaID = new SelectList(db.Thanas, "ThanaID", "Name");
            return View();
        }

        // POST: /Centre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CentreID,Name,ThanaID,DistrictID")] Centre centre)
        {
            if (ModelState.IsValid)
            {
                db.Centres.Add(centre);
                db.SaveChanges();
                return RedirectToAction("ShowPassword");
            }

            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "Name", centre.DistrictID);
            ViewBag.ThanaID = new SelectList(db.Thanas, "ThanaID", "Name", centre.ThanaID);
            return View(centre);
        }

        // GET: /Centre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Centre centre = db.Centres.Find(id);
            if (centre == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "Name", centre.DistrictID);
            ViewBag.ThanaID = new SelectList(db.Thanas, "ThanaID", "Name", centre.ThanaID);
            return View(centre);
        }

        // POST: /Centre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CentreID,Name,ThanaID,DistrictID")] Centre centre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(centre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "Name", centre.DistrictID);
            ViewBag.ThanaID = new SelectList(db.Thanas, "ThanaID", "Name", centre.ThanaID);
            return View(centre);
        }

        // GET: /Centre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Centre centre = db.Centres.Find(id);
            if (centre == null)
            {
                return HttpNotFound();
            }
            return View(centre);
        }

        // POST: /Centre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Centre centre = db.Centres.Find(id);
            db.Centres.Remove(centre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public int GetPassword()
        {
            Random rndNumbers = new Random();
            int rndNumber = rndNumbers.Next();
            return rndNumber;
        }
        public ActionResult ShowPassword()
        {
            int pass = GetPassword();
            ViewBag.Password = pass;
            return View();
        }
        public ActionResult MediceneOrder()
        {
           
            ViewBag.District = new SelectList(db.Districts, "Name", "Name");
            ViewBag.Medicine = new SelectList(db.Medicines, "MedidineName", "MedidineName");
            return View();
        }
       


        public JsonResult FindThana(string  DistrictName)
        {
            List<int> districtList = db.Districts.Where(n => n.Name == DistrictName).Select(n => n.DistrictID).ToList();
           int districID = 0;
            foreach (int aDistrict in districtList)
            {
                districID = aDistrict;
            }
            // int districID = Convert.ToInt32(districStr);
            List<string> thanaList = db.Thanas.Where(p => p.DistrictID == districID).Select(p => p.Name).ToList();
            //  List<string>  thanaList = db.Thanas.Select(p => p.Name).ToList();
            return Json(thanaList);
        }

        public JsonResult FindCentre(string ThanaName)
        {
            List<int> thanaList = db.Thanas.Where(n => n.Name == ThanaName).Select(n => n.ThanaID).ToList();
            int thanaID = 0;
            foreach (int aThana in thanaList)
            {
                thanaID = aThana;
            }
            // int districID = Convert.ToInt32(districStr);
            List<string> centreList = db.Centres.Where(p => p.ThanaID == thanaID).Select(p => p.Name).ToList();
            //  List<string>  thanaList = db.Thanas.Select(p => p.Name).ToList();
            return Json(centreList);
        }

        public JsonResult FindMedicine(string MedicineName)
        {
            List<string> mgMl = db.Medicines.Where(n => n.MedidineName == MedicineName).Select(n => n.Power).ToList();
            return Json(mgMl);
        }

        public ActionResult MedicineCentre(List<string >DistrictName, List<string> ThanaName, List<string> CentreName)
        {
            //ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "Name");
            //ViewBag.ThanaID = new SelectList(db.Thanas, "ThanaID", "Name");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
      
        //Medicine allocate for Centre
        [HttpPost]

        public ActionResult AddMedicineFromTable(JsonArray[] tables)
        {
            foreach (JsonArray json  in tables)
            {
                MedicineOrder medicine = new MedicineOrder();
                medicine.Medicine = json.Medicine;
                medicine.Centre = json.Centre;
                medicine.Quantity = json.Quantity;
            db.MedicineOrders.Add(medicine);
               db.SaveChanges();
            }
          
         
       return Json("Success");
        }

        public ActionResult Treatment()
        {
            ViewBag.Medicine = new SelectList(db.Medicines, " MedicineID", "MedidineName");
            return View();
        }
        public ActionResult VoterInformation(string Voter_id)
        {
            string uri = "http://nerdcastlebd.com/web_service/voterdb/index.php/voters/voter/" + Voter_id + "";

            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            XmlDocument xmlDoc = new XmlDocument();
           xmlDoc.Load(response.GetResponseStream());
            XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("voter");
            Voter aVoter = new Voter();

            foreach (XmlNode node in nodes)
            {
                aVoter.VoterID = node.SelectSingleNode("id").InnerText;
                aVoter.VoterName = node.SelectSingleNode("name").InnerText;
                aVoter.VoterAddress = node.SelectSingleNode("address").InnerText;
                aVoter.VoterDob= node.SelectSingleNode("date_of_birth").InnerText;
            }

        int treatmentCount = db.TreatmentServices.Where(x => x.VotersID == aVoter.VoterID).ToList().Count;
          aVoter.ServiceCount = treatmentCount;
           //List<Voter> aList = new List<Voter>();
         //   if (aVoter.ServiceCount != null)
      //      {
          List<string> aList = new List<string>();
               aList.Add(aVoter.VoterID); 
                aList.Add(aVoter.VoterName);
                aList.Add(aVoter.VoterAddress);
                aList.Add(aVoter.VoterDob);
           // }


                return Json(new{aList = aList}, JsonRequestBehavior.AllowGet);
        }
    }
    
}
