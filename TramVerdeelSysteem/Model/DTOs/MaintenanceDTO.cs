﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTOs
{
    public class MaintenanceDTO
    {
        public int Target { get; set; }
        public string Annotation { get; set; }
        public List<Logic.MaintenanceTramModel> MaintenanceList { get; set; }
    }
}
