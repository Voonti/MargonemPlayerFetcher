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
        public int userId { get; set; }
        public int charId { get; set; }
        public string nick { get; set; }
        public string server { get; set; }
        public string profession { get; set; }
        public string rank { get; set; }
        public int level { get; set; }
    }
}
