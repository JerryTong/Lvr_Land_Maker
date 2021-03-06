﻿using Lvr_Land_Maker.BLL;
using Lvr_Land_Maker.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lvr_Land_Maker.Models
{
    public class TransformProperty
    {
        public int Version { get; set; }

        public List<string> FilesName { get; set; }

        public List<FilesDict> FilesDirectory { get; set; }
        
        public string ErrorMessage { get; set; }
    }
}
