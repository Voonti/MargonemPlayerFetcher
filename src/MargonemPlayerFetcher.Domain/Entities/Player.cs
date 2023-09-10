using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
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
        public int UserId { get; set; }
        [JsonProperty("c")]
        public int CharId { get; set; }
        [JsonProperty("n")]
        public string Nick { get; set; }
        public string Server { get; set; }
        [JsonProperty("p")]
        public string Profession { get; set; }
        [JsonProperty("r")]
        public string Rank { get; set; }
        [JsonProperty("l")]
        public int Level { get; set; }
        public DateTime LastFetchDate { get; set; }
        public DateTime FirstFetchDate { get; set; }
    }

    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId);
            builder.Property(x => x.CharId);
            builder.Property(x => x.Nick);
            builder.Property(x => x.Server);
            builder.Property(x => x.Profession);
            builder.Property(x => x.Rank);
            builder.Property(x => x.Level);
            builder.Property(x => x.LastFetchDate);
            builder.Property(x => x.FirstFetchDate);

            builder.HasIndex(x => new { x.UserId, x.CharId, x.Server });
        }
    }
}
