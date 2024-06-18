using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poseidon.AppPages.Statistics.ALL.ChartViewModel
{
    class StudentNationalityALL
    {
        private string _nationality;
        private double _count;

        public StudentNationalityALL(string nationality, double count)
        {
            this._nationality = nationality;
            this._count = count;
        }

        public string Nationality
        {
            get { return this._nationality; }
        }

        public double Count
        {
            get { return this._count; }
        }



    }
}
