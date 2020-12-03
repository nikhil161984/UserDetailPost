using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPostDetails
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public address address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public company company { get; set; }
    }
    public class address
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public geo geo { get; set; }
    }
   public class geo
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
    }
  public  class company
    {
        public string name { get; set; }
        public string catchPhrase { get; set; }
        public string bs { get; set; }
    }
}
