using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poseidon.AppPages.Statistics.ALL.ChartViewModel
{
    public class WorkDataALL
    {
        private string _work;
        private string _kladosname;
        private double _count;
        private int _syear;

        public WorkDataALL(int syear, string work, string kladosname, double count)
        {
            this._syear = syear;
            this._work = work;
            this._kladosname = kladosname;
            this._count = count;
        }

        public string WorkName
        {
            get { return this._work; }
        }

        public string KladosName
        {
            get { return this._kladosname; }
        }

        public double Count
        {
            get { return this._count; }
        }

        public int SchoolYear
        {
            get { return this._syear; }
            set { this._syear = value; }
        }

    }   // class WorkData
}
