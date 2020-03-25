using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoList.BLLayer;

namespace TodoList.Models
{
    public class Task
    {
        TaskBL Obj = new TaskBL();
        public string SaveTaskDetails(Int64 TaskID, string TaskTitle,string TaskDate,int TaskStatus)
        {
            return Obj.SaveTaskDetails(TaskID,TaskTitle, TaskDate, TaskStatus);
        }

        public object LoadTaskDetails(Int64 TaskID)
        {
            return Obj.LoadTaskDetails(TaskID);
        }



        public string DeleteTaskDetails(Int64 TaskID)
        {
            return Obj.DeleteTaskDetails(TaskID);
        }
        public string MarkAsCompleted(Int64 TaskID,int Status)
        {
            return Obj.MarkAsCompleted(TaskID, Status);
        }
    }
}