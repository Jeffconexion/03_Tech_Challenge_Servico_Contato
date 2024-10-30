// --------------------------------------------------------------------------------------------------
// <copyright file="AreaCodeMapping.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using LocalFriendzApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalFriendzApi.Infrastructure.Data.Mapping
{
    public class AreaCodeMapping : IEntityTypeConfiguration<AreaCode>
    {
        public void Configure(EntityTypeBuilder<AreaCode> builder)
        {
            builder.ToTable("TB_AREA_CODE");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id_area_code");

            builder.Property(c => c.CodeRegion)
                .IsRequired(true)
                .HasColumnName("code_region")
                .HasColumnType("VARCHAR")
                .HasMaxLength(2);
        }
    }
}
