﻿using Ornithology.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Data
{
    public interface IAveRepository : IRepository<Ave>
    {
        void DeleteCascade(string codigoAve);
    }
}
