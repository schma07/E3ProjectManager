﻿namespace Schma.E3ProjectManager.Core.Application.Queries.Base
{
    public interface IPagedQuery
    {
        public int PageSize { get; set; }
        public int Start { get; set; }
    }
}
