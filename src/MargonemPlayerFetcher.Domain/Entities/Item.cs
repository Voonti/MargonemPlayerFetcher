using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Hid { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int St { get; set; }
        public string Stat { get; set; }
        public int Tpl { get; set; }
        public char Rarity { get; set; }
        public DateTime LastFetchDate { get; set; }
        public DateTime FetchDate { get; set; }
    }

    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PlayerId);
            builder.Property(x => x.Hid);
            builder.Property(x => x.Name);
            builder.Property(x => x.Icon);
            builder.Property(x => x.St);
            builder.Property(x => x.Stat);
            builder.Property(x => x.Tpl);
            builder.Property(x => x.Rarity);
            builder.Property(x => x.LastFetchDate);
            builder.Property(x => x.FetchDate);

            builder.HasIndex(x => new { x.PlayerId, x.Hid });
        }
    }
}
