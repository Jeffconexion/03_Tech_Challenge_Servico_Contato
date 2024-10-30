// --------------------------------------------------------------------------------------------------
// <copyright file="ContactRepository.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using LocalFriendzApi.Domain.IRepositories;
using LocalFriendzApi.Domain.Models;
using LocalFriendzApi.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LocalFriendzApi.Infrastructure.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext context)
            : base(context) { }

        public IEnumerable<Contact> GetContactByCodeRegion(string codeRegion)
        {
            return Db.Contacts.AsNoTracking()
                     .Where(c => c.AreaCode.CodeRegion.Equals(codeRegion))
                     .Include(c => c.AreaCode);
        }

        public IEnumerable<Contact> GetAllContactWithAreaCode()
        {
            return Db.Contacts.AsNoTracking()
                     .Include(c => c.AreaCode);
        }
    }
}
