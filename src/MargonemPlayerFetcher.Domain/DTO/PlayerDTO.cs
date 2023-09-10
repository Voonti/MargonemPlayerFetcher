using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.DTO
{
    public class PlayerDTO
    {
        public int userId { get; set; }
        public int charId { get; set; }
        public string nick { get; set; }
        public string server { get; set; }
        public string profession { get; set; }
        public string rank { get; set; }
        public int level { get; set; }
    }
}
