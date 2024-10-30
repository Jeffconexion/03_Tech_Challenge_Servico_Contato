// --------------------------------------------------------------------------------------------------
// <copyright file="AppExtension.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace LocalFriendzApi.UI.Configuration
{
    public static class AppExtension
    {
        public static void ConfigureDevEnvironment(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            // app.MapSwagger().RequireAuthorization();
        }
    }
}
