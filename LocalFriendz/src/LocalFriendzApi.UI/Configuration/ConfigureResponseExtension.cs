// --------------------------------------------------------------------------------------------------
// <copyright file="ConfigureResponseExtension.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using LocalFriendzApi.Application.Response;
using LocalFriendzApi.Domain.Models;

namespace LocalFriendzApi.UI.Configuration
{
    public static class ConfigureResponseExtension
    {
        public static IResult ConfigureResponseStatus(this PagedResponse<List<Contact>?> response)
        {
            switch (response.Code)
            {
                case 200:
                    return TypedResults.Ok(response);
                case 201:
                    return TypedResults.Created(response.Data.FirstOrDefault().Name);
                case 400:
                    return TypedResults.BadRequest(response);
                case 404:
                    return TypedResults.NotFound(response);
                default:
                    return TypedResults.NoContent();
            }
        }

        public static IResult ConfigureResponseStatus(this Response<Contact>? response)
        {
            switch (response.Code)
            {
                case 200:
                    return TypedResults.Ok(response);
                case 201:
                    return TypedResults.Created(response.Data.Name);
                case 400:
                    return TypedResults.BadRequest(response);
                case 404:
                    return TypedResults.NotFound(response);
                default:
                    return TypedResults.NoContent();
            }
        }
    }
}
