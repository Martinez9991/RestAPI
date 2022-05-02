using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPI.Core
{
    public class Medication
    {
        //construtor
        public Medication()
        {

        }

        //class atributes

        public int Medication_Code {set; get;}

        public string Name { set; get; }

        //note: maybe change data type
        public int Quantity { set; get; }

        public DateTime Creation_Date { set; get; }
    }
}
