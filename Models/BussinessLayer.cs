using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsApplication.Models
{
    public class BussinessLayer
    {

        public  List<SelectListItem> CreatDropDown(DataTable dt)
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "0", Text = "--Select--" });
            for (int i = 0;i<dt.Rows.Count; i++)
            {
                list.Add(new SelectListItem() { Value = dt.Rows[i][0].ToString(), Text = dt.Rows[i][1].ToString()});
            }
            return list;
        }

    }
}