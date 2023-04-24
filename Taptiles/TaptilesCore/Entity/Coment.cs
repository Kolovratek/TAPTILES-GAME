﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taptiles.Entity
{
    [Serializable]
    public class Coment
    {
        public int Id { get; set; }
        public string Player { get; set; }

        public string Comment { get; set; }

        public DateTime PlayedAt { get; set; }
    }
}
