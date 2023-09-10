using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.Entities
{
    public class PlayerNick
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Nick { get; set; }
        public DateTime FetchDate { get; set; }
    }
    public class PlayerNameConfiguration : IEntityTypeConfiguration<PlayerNick>
    {
        public void Configure(EntityTypeBuilder<PlayerNick> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PlayerId);
            builder.Property(x => x.Nick);
            builder.Property(x => x.FetchDate);

            builder.HasIndex(x => x.PlayerId); 
        }
    }
}
