// --------------------------------------------------------------------------------------------------
// <copyright file="ContactMapping.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using LocalFriendzApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalFriendzApi.Infrastructure.Data.Mapping
{
    public class ContactMapping : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("TB_CONTACT");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("id_contact");

            builder.Property(c => c.Name)
                .IsRequired(true)
                .HasColumnName("name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(c => c.Phone)
                .IsRequired(true)
                .HasColumnName("phone")
                .HasColumnType("VARCHAR")
                .HasMaxLength(20);

            builder.Property(c => c.Email)
                .IsRequired(true)
                .HasColumnName("email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(40);
        }
    }
}
