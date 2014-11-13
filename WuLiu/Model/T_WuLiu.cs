using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WuLiu
{
    public class T_WuLiu : T_Base
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int SRoadId { get; set; }
        public int ERoadId { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public decimal Freight { get; set; }
        public string Things { get; set; }
    }
}