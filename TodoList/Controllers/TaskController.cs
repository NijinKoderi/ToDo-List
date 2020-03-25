using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoList.Models;

namespace TodoList.Controllers
{
    [SessionExpire]
    public class TaskController : Controller
    {
        Task TaskObj = new Task();
        [HttpPost]
        public ActionResult SaveTaskDetails(Int64 TaskID,string TaskTitle, string TaskDate,int TaskStatus)
        {
            return new JsonResult() { Data = TaskObj.SaveTaskDetails(TaskID,TaskTitle, TaskDate, TaskStatus) };
        }

        [HttpPost]
        public ActionResult LoadTaskDetails()
        {
           
            var item = TaskObj.LoadTaskDetails(0);
            return Json(new { data = item }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteTaskDetails(Int64 TaskID)
        {
            return new JsonResult() { Data = TaskObj.DeleteTaskDetails(TaskID) };
        }

        [HttpPost]
        public ActionResult MarkAsCompleted(Int64 TaskID,int Status)
        {
            return new JsonResult() { Data = TaskObj.MarkAsCompleted(TaskID, Status) };
        }

    }
}