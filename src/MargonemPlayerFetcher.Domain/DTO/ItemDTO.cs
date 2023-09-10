using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.DTO
{
    public class ItemDTO
    {
        public int userId { get; set; }
        public int charId { get; set; }
        public string hid { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public int st { get; set; }
        public string stat { get; set; }
        public int tpl { get; set; }
    }
}
