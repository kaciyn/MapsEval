using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MapsEval.Models;

namespace MapsEval.Controllers
{
    public partial class AddressController : Controller
    {
        ////list of checkboxes
        //public List<string> ItemList = new List<string>
        //{
        //    "OfficeCheckbox",
        //    "ProdLogCheckbox",
        //    "ResidentialCheckbox",
        //    "RetailCheckbox"
        //};

        public class PagedAddressModel
        {
            public int PageSize { get; set; }
            public int PageNumber { get; set; }
            public IEnumerable<AddsForEval_0623> AddressRowItems { get; set; }
            public IEnumerable<AddsForEval_0623Companies> CompanyRowItems { get; set; }
            public int TotalRows { get; set; }
            public string MapUrl { get; set; }
            public string GmapsUrl { get; set; }
            public string MapyUrl { get; set; }

            public List<NameAddressAndTurnov> CompaniesOnAddress { get; set; }
        }

        public class NameAddressAndTurnov
        {
            public int? AddressID { get; set; }
            public string Name { get; set; }
            public string TurnovCatFinancialsSSO { get; set; }
        }

        //gets total amount of rows on first load
        private static int totalRows;
        private static int TotalRows
        {
            get
            {
                if (totalRows == 0)
                {
                    using (var db = new DBConnect())
                    {
                        db.Configuration.ProxyCreationEnabled = false;

                        totalRows = db.AddsForEval_0623.Count();
                    }
                }
                return totalRows;
            }
        }

        //loads address data
        public ActionResult ShowAddress(int page = 1)
        {
            const int pageSize = 1;

            using (var db = new DBConnect())
            {
                db.Configuration.ProxyCreationEnabled = false;

                var addresses = db.AddsForEval_0623.OrderBy(address => address.AddressID).Skip(pageSize * (page - 1)).Take(pageSize).AsNoTracking().ToArray();

                var firstAddress = addresses.First();

                var mapUrl = $"https://maps.google.com/maps?layer=c&cbll={firstAddress.AddressLatitude},{firstAddress.AddressLongitude}&cbp=12,0,,0,0&source=embed&output=svembed";

                var gmapsUrl = $"http://maps.google.com/maps?q={firstAddress.AddressLatitude},{firstAddress.AddressLongitude}&layer=tc&t=m&z=18&source=embed&output=svembed";

                var mapyUrl = $"https://mapy.cz/zakladni?x={firstAddress.AddressLongitude}&y={firstAddress.AddressLatitude}&z=17&source=coor&id={firstAddress.AddressLongitude}%2C{firstAddress.AddressLatitude}&q={firstAddress.AddressLatitude}%2C{firstAddress.AddressLongitude}";

                var result = new PagedAddressModel()
                {
                    PageSize = pageSize,
                    PageNumber = page,
                    AddressRowItems = addresses,
                    TotalRows = TotalRows,
                    MapUrl = mapUrl,
                    GmapsUrl = gmapsUrl,
                    MapyUrl = mapyUrl

                };

                var companiesOnAddress = (from c in db.AddsForEval_0623Companies
                    join a in db.AddsForEval_0623 on c.AddressID equals a.AddressID
                    where c.AddressID == firstAddress.AddressID
                    select new NameAddressAndTurnov()
                    {
                        AddressID = c.AddressID,
                        Name = c.Name,
                        TurnovCatFinancialsSSO = c.TurnovCatFinancialsSSO
                    }).ToList();

                result.CompaniesOnAddress = companiesOnAddress;

                return View(result);
            }
        }

        ////loads company data
        //public ActionResult ShowCompany(int AddressID)
        //{

        //    using (var db = new DBConnect())
        //    {
        //        db.Configuration.ProxyCreationEnabled = false;

        //        var companies = db.AddsForEval_0623Companies.AsNoTracking().ToArray();

        //        var result = new PagedAddressModel()
        //        {
        //            CompanyRowItems = companies,

        //        };
        //        return View(result);
        //    }

        //    //var qq = (from c in db.AddsForEval_0623Companies join a in db.AddsForEval_0623 on c.AddressID equals a.AddressID
        //    //          where c.AddressID == AddressID
        //    //          select new Name
        //    //    {
        //    //        AddressID = c.AddressID,
        //    //        Name = c.Name,
        //    //        EmpName = c.EmpName,
        //    //        Age = e.Age,
        //    //        Department = d.department_name
        //    //    }).ToList();
        //    //return PartialView("EmployeeView", qq);
        // }

        public class DataHolder
        {
            public int AddressId { get; set; }
            public bool IsChecked0 { get; set; }
            public bool IsChecked1 { get; set; }
            public bool IsChecked2 { get; set; }
            public bool IsChecked3 { get; set; }
            public bool IsChecked4 { get; set; }
            public bool IsChecked5 { get; set; }
            public bool IsChecked6 { get; set; }
            public int PremiseStandard { get; set; }
            public int StandardOfCars { get; set; }
            public int StandardOfArea { get; set; }
            public int AgeOfPremises { get; set; }

            public string Lat { get; set; }
            public string Long { get; set; }
            public string Mapurl { get; set; }
        }


        public JsonResult SaveItem(DataHolder dataHolder)
        {

            using (var db = new DBConnect())
            {
                db.Configuration.ProxyCreationEnabled = false;

                var theAddress = db.AddsForEval_0623.First(address => address.AddressID == dataHolder.AddressId);

                theAddress.UseOfPremises_Office = dataHolder.IsChecked0;
                theAddress.UseOfPremises_ProductionLogistics = dataHolder.IsChecked1;
                theAddress.UseOfPremises_Residential = dataHolder.IsChecked2;
                theAddress.UseOfPremises_Retail = dataHolder.IsChecked3;
                theAddress.AddressNotFound = dataHolder.IsChecked4;
                theAddress.PremisesNotVisible = dataHolder.IsChecked5;
                theAddress.UnclearReviewNeeded = dataHolder.IsChecked6;
                theAddress.StandardOfPremises = dataHolder.PremiseStandard;
                theAddress.StandardOfParkedCars = dataHolder.StandardOfCars;
                theAddress.StandardOfSurroundingArea = dataHolder.StandardOfArea;
                theAddress.EstimatedAgeOfPremises = dataHolder.AgeOfPremises;

                db.SaveChanges();
            }

            return Json(new { });
        }


    }
}
