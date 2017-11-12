using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cardozo_Claudia_Clima
{
    public class main
    {
        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double humidity { get; set; }

    } // fin clase
    public class InfoPronostico
    {
        public city city { get; set; }
        public List<list> list { get; set; }

    } // fin clase

   public class weather
    {
        public string main { get; set; }
        public string description { get; set; }

    }// fin clase

   public class list
    {
        public double dt { get; set; }
        public double pressure { get; set; }        
        public double speed { get; set; }
        public main main { get; set; }
        public List<weather> weather { get; set; }



    } //fin clase

   public class city
    {

    }//fin clase
   
} 
