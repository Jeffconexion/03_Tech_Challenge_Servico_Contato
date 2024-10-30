// --------------------------------------------------------------------------------------------------
// <copyright file="GetByCodeRegionRequest.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace LocalFriendzApi.Application.Request
{
    public class GetByCodeRegionRequest : PagedRequest
    {
        public string? CodeRegion { get; set; }

        public static GetByCodeRegionRequest RequestMapper(string codeRegion)
        {
            return new GetByCodeRegionRequest() { CodeRegion = codeRegion };
        }
    }
}
