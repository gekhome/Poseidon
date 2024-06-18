using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poseidon.AppPages.Statistics.SM.ChartViewModel
{
    public class CategoryDataSM
    {
        private string _category;
        private string _kladosname;
        private double _count;
        private int _syear;

        public CategoryDataSM(int syear, string category, string kladosname, double count)
        {
            this._syear = syear;
            this._category = category;
            this._kladosname = kladosname;
            this._count = count;
        }

        public string CategoryName
        {
            get { return this._category; }
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

    }   // class CategoryData
}
