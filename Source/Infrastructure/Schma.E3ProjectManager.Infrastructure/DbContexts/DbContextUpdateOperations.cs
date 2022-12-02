﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Schma.E3ProjectManager.Infrastructure.Models;

namespace Schma.E3ProjectManager.Infrastructure.DbContexts
{
    /// <summary>
    /// Contains a set of automatic operations against Database
    /// </summary>
    internal static class DbContextUpdateOperations
    {
        /// <summary>
        /// Automatically updates the BaseEntity properties without any developer action
        /// </summary>
        /// <param name="changes">The collection of BaseEntity entities for save to DB</param>
        /// <param name="username">The logged in user</param>
        public static void UpdateDates(IEnumerable<EntityEntry<AuditableEntity>> changes, string username)
        {
            var now = DateTime.UtcNow;
            foreach (var change in changes)
                switch (change.State)
                {
                    case EntityState.Added:
                        change.Entity.DateCreated = now;
                        change.Entity.DateModified = now;
                        if (!string.IsNullOrEmpty(username))
                        {
                            change.Entity.CreatedBy = username;
                            change.Entity.ModifiedBy = username;
                        }
                        break;

                    case EntityState.Modified:
                        if (!string.IsNullOrEmpty(username))
                            change.Entity.ModifiedBy = username;
                        break;
                }
        }
    }
}