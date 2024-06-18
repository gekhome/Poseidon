using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poseidon.AppPages.Statistics.SM.ChartViewModel
{
    class ProsontaDataSM
    {
        private bool _msc;
        private bool _phd;
        private string _kladosname;
        private double _count;
        private int _syear;

        public ProsontaDataSM(int syear, bool msc, bool phd, string kladosname, double count)
        {
            this._msc = msc;
            this._phd = phd;
            this._syear = syear;
            this._kladosname = kladosname;
            this._count = count;
        }

        public bool Msc
        {
            get { return this._msc; }
        }

        public bool Phd
        {
            get { return this._phd; }
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


    }   // class ProsontaDataSM
}
