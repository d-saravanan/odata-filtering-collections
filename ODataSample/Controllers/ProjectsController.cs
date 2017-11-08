using ODataSample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;

namespace ODataSample.Controllers
{
    public class ProjectsController : ODataController
    {
        [Queryable]
        public IQueryable<Project> Get()
        {
            return new ProjectsService().GetProjects();
        }
    }
}
