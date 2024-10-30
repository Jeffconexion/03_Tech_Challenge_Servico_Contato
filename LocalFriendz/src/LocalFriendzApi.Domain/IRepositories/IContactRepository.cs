// --------------------------------------------------------------------------------------------------
// <copyright file="IContactRepository.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using LocalFriendzApi.Domain.Models;

namespace LocalFriendzApi.Domain.IRepositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        IEnumerable<Contact> GetContactByCodeRegion(string codeRegion);

        IEnumerable<Contact> GetAllContactWithAreaCode();
    }
}
