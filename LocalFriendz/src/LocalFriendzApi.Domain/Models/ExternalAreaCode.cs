// --------------------------------------------------------------------------------------------------
// <copyright file="ExternalAreaCode.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace LocalFriendzApi.Domain.Models
{
    public class ExternalAreaCode
    {
        public string? State { get; set; }

        public List<string>? Cities { get; set; }
    }
}
