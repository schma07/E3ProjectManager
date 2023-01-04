﻿using System;
using System.Collections.Generic;
using Schma.E3ProjectManager.Core.Application.ReadModels.Projects;

namespace Schma.E3ProjectManager.Core.Application.ReadModels.Customers
{
    public class CustomerReadModel
    {
        public Guid Id { get; set; }
        public List<ProjectReadModel> Projects { get; set; }        
        public string Name { get; set; }
    }
}
