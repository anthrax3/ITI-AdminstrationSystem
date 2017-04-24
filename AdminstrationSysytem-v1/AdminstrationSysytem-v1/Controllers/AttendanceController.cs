﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminstrationSysytem_v1.Models;

namespace AdminstrationSysytem_v1.Controllers
{
    public class AttendanceController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }



        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult AttendanceReception()
        {
            var departments = db.Departments.ToList();
            ViewBag.Deps = new SelectList(departments, "DepartmentId", "Name"); 
            var students = db.Students.ToList();
            return PartialView(students);
        }


        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult GetStudents(int id)
        {
            var StudentInDepartments = db.Students.Where(dep => dep.DepartmentId == id).ToList();
            return PartialView("attList",StudentInDepartments);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Attendance(string[] Attend)
        {
            var Students = db.Students.ToList();
            var StudentData  = new Attendance();
            var date = DateTime.Now.Date;
            var hourse = DateTime.Now.ToString("hh:mm:ss");
            var Adb = db.Attendance.ToList();

            foreach(var std in Attend)
            {

            }

            /*foreach (var std in Students)
            {
                if (std.Name == Request.Form[std.Name])
                {
                    StudentData = new Attendance() { ArrivalTime = hourse, StudentId = std.Id, Date = date, IsPermitted = false };
                    Adb.Add(StudentData);
                }
            }*/

            db.SaveChanges();
            return View();
        }
    }
}