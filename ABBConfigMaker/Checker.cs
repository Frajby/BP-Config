using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABBConfigMaker
{
    abstract class Checker
    {
        //public ErrorDataModel errorData { get; protected set; }
        public abstract ErrorDataModel check();
    }
}
