using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poseidon.AppPages.Statistics.ALL.ChartViewModel
{
    public class GenderDataALL
    {
        private string _gender;
        private string _kladosname;
        private double _count;
        private int _syear;

        public GenderDataALL(int syear, string gender, string kladosname, double count)
        {
            this._syear = syear;
            this._gender = gender;
            this._kladosname = kladosname;
            this._count = count;
        }

        public string GenderName
        {
            get { return this._gender; }
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

    }   // class GenderData
}
