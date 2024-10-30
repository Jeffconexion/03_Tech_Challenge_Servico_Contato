// --------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using LocalFriendzApi.Domain.Models;
using System.Linq.Expressions;

namespace LocalFriendzApi.Domain.IRepositories
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        Task Add(TEntity entity);

        Task<TEntity> GetById(Guid id);

        Task<List<TEntity>> GetAll();

        Task Update(TEntity entity);

        Task Delete(Guid id);

        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveChanges();
    }
}
