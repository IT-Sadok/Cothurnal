using DataAccounts.Entitys.MovieEntitys;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccounts.Configurations
{
    class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new Genre(1, "thriller"),
                new Genre(2, "horror"),
                new Genre(3, "comedy")
            );
        }
    }
}
