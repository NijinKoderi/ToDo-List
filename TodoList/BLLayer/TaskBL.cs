using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoList.BLLayer
{
    public class TaskBL
    {
        ToDoListEntities ObjEntity = new ToDoListEntities();
        public string SaveTaskDetails(Int64 TaskID, string sTaskTitle, string TaskDate,int TaskStatus)
        {
            var Result = "0";
            try
            {
                if (ObjEntity.tblTasks.Any(a => a.TaskID == TaskID))
                {
                    tblTask update = (from a in ObjEntity.tblTasks.Where(a => a.TaskID == TaskID) select a).Single();
                    update.TaskName = sTaskTitle;
                    update.TaskDescription = sTaskTitle;
                    update.TaskDate =Convert.ToDateTime(TaskDate);
                    update.IsCompleted =Convert.ToBoolean(TaskStatus);
                    update.ModifiedDate = DateTime.Now;
                    update.ModifiedBy = Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeID"]);
                    ObjEntity.SaveChanges();
                }

                else
                {
                    tblTask Insert = new tblTask { TaskName = sTaskTitle, TaskDescription = sTaskTitle, TaskDate = Convert.ToDateTime(TaskDate), CreatedDate = DateTime.Now, CreatedBy = Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeID"]),IsCompleted=false };
                    ObjEntity.tblTasks.Add(Insert);
                    ObjEntity.SaveChanges();
                }
                Result = "1";
            }
            catch (Exception e)
            {
                string msg = e.Message;
                Result = "0";
            }
            return Result;
        }

        public object LoadTaskDetails(Int64 TaskID)
        {


            // var Result = (from a in ObjEntity.tblTasks.Where(a => (TaskID == 0 || a.TaskID == TaskID))
            // select new { a.TaskID, a.TaskName, a.CreatedDate }).ToList();
            // var tasklist = ObjEntity.tblTasks.ToList();
            var Result = ObjEntity.spTaskDetailSel(Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeID"]), TaskID).ToList();
            return Result;
        }


        public string DeleteTaskDetails(Int64 TaskID)
        {
            var Result = "0";
            try
            {
                if (ObjEntity.tblTasks.Any(a => a.TaskID == TaskID))
                {
                    tblTask delete = (from a in ObjEntity.tblTasks.Where(a => a.TaskID == TaskID) select a).Single();
                    ObjEntity.tblTasks.Remove(delete);
                    ObjEntity.SaveChanges();
                }             
                Result = "1";
            }
            catch (Exception e)
            {
                string msg = e.Message;
                Result = "0";
            }
            return Result;
        }

        public string MarkAsCompleted(Int64 TaskID,int Status)
        {
            var Result = "0";
            try
            {
                if (ObjEntity.tblTasks.Any(a => a.TaskID == TaskID))
                {
                    tblTask update = (from a in ObjEntity.tblTasks.Where(a => a.TaskID == TaskID) select a).Single();
                    update.IsCompleted = Convert.ToBoolean(Status);
                    update.ModifiedDate = DateTime.Now;
                    update.ModifiedBy = Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeID"]);
                    ObjEntity.SaveChanges();
                }
                Result = "1";
            }
            catch (Exception e)
            {
                string msg = e.Message;
                Result = "0";
            }
            return Result;
        }
    }
}