// Copyright (c) KriaSoft, LLC.  All rights reserved.
// Licensed under the Apache License, Version 2.0.  See LICENSE.txt in the project root for license information.

using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using IST.Models.IdentityModels;
using IST.Repository.BaseRepository;
using Microsoft.AspNet.Identity;

namespace IST.Repository.Repositories
{
    public class UserRoleStore : IQueryableRoleStore<UserRole, string>
    {
        private readonly BaseDbContext _db;

        public UserRoleStore(BaseDbContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _db = db;
        }

        // IQueryableRoleStore<UserRole, TKey>

        public IQueryable<UserRole> Roles
        {
            get { return _db.UserRoles; }
        }

        // IRoleStore<UserRole, TKey>

        public virtual Task CreateAsync(UserRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            if (string.IsNullOrEmpty(role.Id))
            {
                role.Id = Guid.NewGuid().ToString();
            }

            _db.UserRoles.Add(role);
            return _db.SaveChangesAsync();
        }

        public Task DeleteAsync(UserRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            _db.UserRoles.Remove(role);
            return _db.SaveChangesAsync();
        }

        public Task<UserRole> FindByIdAsync(string roleId)
        {
            return _db.UserRoles.FindAsync(new object[] { roleId });
        }

        public Task<UserRole> FindByNameAsync(string roleName)
        {
            return _db.UserRoles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public Task UpdateAsync(UserRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            _db.Entry(role).State = EntityState.Modified;
            return _db.SaveChangesAsync();
        }

        // IDisposable

        public void Dispose()
        {
        }
    }
}