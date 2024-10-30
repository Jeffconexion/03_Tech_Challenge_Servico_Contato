// --------------------------------------------------------------------------------------------------
// <copyright file="PagedRequest.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using LocalFriendzApi.Application.Response;

namespace LocalFriendzApi.Application.Request
{
    public abstract class PagedRequest
    {
        public int PageSize { get; set; } = ConfigurationPage.DefaultPageSize;

        public int PageNumber { get; set; } = ConfigurationPage.DefaultPageNumber;
    }
}
