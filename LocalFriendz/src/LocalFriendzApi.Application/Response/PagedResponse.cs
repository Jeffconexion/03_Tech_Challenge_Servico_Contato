// --------------------------------------------------------------------------------------------------
// <copyright file="PagedResponse.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace LocalFriendzApi.Application.Response
{
    public class PagedResponse<TData> : Response<TData>
    {
        [JsonConstructor]
        public PagedResponse(
        TData? data,
        int totalCount,
        int currentPage = 1,
        int pageSize = ConfigurationPage.DefaultPageSize,
        string? message = null)
        : base(data)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            Message = message;
        }

        public PagedResponse(
            TData? data,
            int code = ConfigurationPage.DefaultStatusCode,
            string? message = null)
            : base(data, code, message)
        {
        }

        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public int PageSize { get; set; } = ConfigurationPage.DefaultPageSize;

        public int TotalCount { get; set; }
    }
}
