// --------------------------------------------------------------------------------------------------
// <copyright file="IAreaCodeExternalService.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using LocalFriendzApi.Domain.Models;
using Refit;

namespace LocalFriendzApi.Infrastructure.ExternalServices.Interfaces
{
    public interface IAreaCodeExternalService
    {
        [Get("/ddd/v1/{areaCode}")]
        Task<ExternalAreaCode> GetAreaCode(int areaCode);
    }
}
