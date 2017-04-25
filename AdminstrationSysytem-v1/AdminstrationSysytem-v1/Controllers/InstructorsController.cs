﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AdminstrationSysytem_v1.Models;

namespace AdminstrationSysytem_v1.Controllers
{
    public class InstructorsController : Controller
    {
        // GET: Instructors
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult UserProfile()
        {
            return View();
        }


        [Authorize(Roles = "Instructor")]
        [HttpGet]
        public ActionResult GivePermission()
        {
            var InstructorDeprtmentId = (TempData["Instructor"] as Instructors).DepartmentId;

            var AttendanceElement = new AttendanceModel();
            List<AttendanceModel> TargetList = new List<AttendanceModel>();

            var ListOfStudents = db.Students.Where(m => m.DepartmentId == InstructorDeprtmentId).ToList();

            var StudentsInDepOfIns = (from std in db.Students
                                      from att in db.Attendance.Where(m=>m.StudentId == std.Id)
                                      select new AttendanceModel() { name = std.Name , id = std.Id , IsPermitted = att.IsPermitted , NoOfPermissions = std.NoOfPermissions ,  GradeOfAbsence = std.GradeOfAbsence  }).ToList();


            foreach (var item in ListOfStudents)
            {
                foreach (var Attender in StudentsInDepOfIns)
                {
                    if (Attender.id == item.Id)
                    {
                        AttendanceElement = new AttendanceModel { id = item.Id, name = item.Name, NoOfPermissions = item.NoOfPermissions, GradeOfAbsence = item.GradeOfAbsence, IsPermitted = Attender.IsPermitted };
                    }
                    else
                    {
                        AttendanceElement = new AttendanceModel { id = item.Id, name = item.Name, NoOfPermissions = item.NoOfPermissions, GradeOfAbsence = item.GradeOfAbsence, IsPermitted = Attender.IsPermitted };
                    }
                }
                TargetList.Add(AttendanceElement);
            }
            return PartialView(TargetList);
        }
    }
}