﻿using System;
using System.Collections.Generic;
using System.Text;
using Model.Enums;

namespace Logic
{
    class Cleaning
    {
        Tram Train { get; set; }
        EnumCollection.Size ServiceSize { get; set; }

        bool Priority { get; set; }

    }
}
