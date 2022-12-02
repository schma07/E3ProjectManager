﻿using System;
using System.Collections.Generic;
using Schma.E3ProjectManager.Core.Domain;

namespace Schma.E3ProjectManager.Core.Application.ReadModels
{
    public class UserReadModel
    {
        public string ProfilePicture { get; set; }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IReadOnlyCollection<RoleEnum> Roles { get; set; }
        public IReadOnlyCollection<string> LocalizedRoles { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public bool IsActive { get; set; }
    }
}