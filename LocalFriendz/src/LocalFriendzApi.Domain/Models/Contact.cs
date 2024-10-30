// --------------------------------------------------------------------------------------------------
// <copyright file="Contact.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace LocalFriendzApi.Domain.Models
{
    public class Contact : Entity
    {
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public AreaCode? AreaCode { get; set; }
    }
}
