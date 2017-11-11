using ODataSample.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ODataSample.Services
{
    public class ProjectsService
    {
        public IQueryable<Project> GetProjects()
        {
            var ctx = new ProjectsContext();

            return ctx.Projects;
        }
    }
}