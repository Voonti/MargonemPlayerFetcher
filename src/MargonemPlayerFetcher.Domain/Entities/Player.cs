using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.Entities
{
    public class Player
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [JsonProperty("a")]
        public int userId { get; set; }
        [JsonProperty("c")]
        public int charId { get; set; }
        [JsonProperty("n")]
        public string nick { get; set; }
        public string server { get; set; }
        [JsonProperty("p")]
        public string profession { get; set; }
        [JsonProperty("r")]
        public string rank { get; set; }
        [JsonProperty("l")]
        public int level { get; set; }
    }
}
