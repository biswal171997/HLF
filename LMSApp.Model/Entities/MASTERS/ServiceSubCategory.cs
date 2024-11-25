﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSApp.Model.Entities.MASTERS
{
    public class ServiceSubCategory
    {
        public int SubCategoryID { get; set; } = 0;
        public string SubCategoryName { get; set; } = "";
        public int CategoryID { get; set; } = 0;
        public string CategoryName { get; set; } = "";
        public int CreatedBy { get; set; } = 0;
        public int IsDeleted { get; set; } = 0;
        public int IsActive { get; set; } = 0;
    }
}