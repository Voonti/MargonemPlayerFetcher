using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Domain.Entities
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }
        public int charId { get; set; }
        public string hid { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public int st { get; set; }
        public string stat { get; set; }
        public int tpl { get; set; }
        public string rarity { get; set; }
        public DateTime lastFetchDate { get; set; }
        public DateTime fetchDate { get; set; }
    }
}
