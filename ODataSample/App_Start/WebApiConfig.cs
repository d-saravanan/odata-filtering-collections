using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;

namespace ODataSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Project>("Projects");
            modelBuilder.EntitySet<ProjectStatus>("ProjectStatusses");
            modelBuilder.EntitySet<ProjectStatusText>("ProjectStatusTexts");
            modelBuilder.EntitySet<Language>("Languages");
            modelBuilder.EntitySet<User>("Users");
            var edmModel = modelBuilder.GetEdmModel();

            var projects = (EdmEntitySet)edmModel.EntityContainers().Single().FindEntitySet("Projects");
            var projectStatusTexts = (EdmEntitySet)edmModel.EntityContainers().Single().FindEntitySet("ProjectStatusTexts");
            var projectType = (EdmEntityType)edmModel.FindDeclaredType("ODataSample.Project");
            var projectStatusTextsType = (EdmEntityType)edmModel.FindDeclaredType("ODataSample.ProjectStatusText");

            var partsProperty = new EdmNavigationPropertyInfo();
            partsProperty.TargetMultiplicity = EdmMultiplicity.Many;
            partsProperty.Target = projectStatusTextsType;
            partsProperty.ContainsTarget = false;
            partsProperty.OnDelete = EdmOnDeleteAction.Cascade;
            partsProperty.Name = "ProjectStatusTexts";

            //projects.AddNavigationTarget(projectType.AddUnidirectionalNavigation(partsProperty), projectStatusTexts);

            var navigationProperty = projectType.AddUnidirectionalNavigation(partsProperty);
            projects.AddNavigationTarget(navigationProperty, projectStatusTexts);

            var linkBuilder = edmModel.GetEntitySetLinkBuilder(projects);
            linkBuilder.AddNavigationPropertyLinkBuilder(navigationProperty,
                new NavigationLinkBuilder((context, property) =>
                    context.GenerateNavigationPropertyLink(property, false), true));

            config.Routes.MapODataRoute("odata", "odata", edmModel);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
