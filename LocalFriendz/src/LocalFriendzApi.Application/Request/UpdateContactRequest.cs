// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateContactRequest.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace LocalFriendzApi.Application.Request
{
    public class UpdateContactRequest
    {
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? AreaCode { get; set; }
    }
}
