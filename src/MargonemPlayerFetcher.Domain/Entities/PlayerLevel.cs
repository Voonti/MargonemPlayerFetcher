using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Domain.Entities
{
    public class PlayerLevel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int Level { get; set; }
        public DateTime FetchDate { get; set; }
    }
    public class PlayerLevelConfiguration : IEntityTypeConfiguration<PlayerLevel>
    {
        public void Configure(EntityTypeBuilder<PlayerLevel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PlayerId);
            builder.Property(x => x.Level);
            builder.Property(x => x.FetchDate);

            builder.HasIndex(x => x.PlayerId);
        }
    }
}
