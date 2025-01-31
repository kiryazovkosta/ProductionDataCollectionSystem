﻿namespace CollectingProductionDataSystem.Web.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Caching;
    using System.Web.Mvc;
    using System.Web.Routing;
    using AutoMapper;
    using CollectingProductionDataSystem.Data.Contracts;
    using CollectingProductionDataSystem.Models.Identity;
    using CollectingProductionDataSystem.Models.UtilityEntities;
    using CollectingProductionDataSystem.Web.Infrastructure.Filters;
    using System.Text;

    public abstract class BaseController : Controller
    {
        protected readonly IProductionData data;

        public UserProfile UserProfile { get; private set; }

        public BaseController(IProductionData dataParam)
        {
            this.data = dataParam;
            Mapper.CreateMap<ApplicationUser, ApplicationUser>();
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            InitializeUserProfileAsync(requestContext);
            return base.BeginExecute(requestContext, callback, state);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            JsonResult result = base.Json(data, contentType, contentEncoding);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            JsonResult result = base.Json(data, contentType, contentEncoding, behavior);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        private void InitializeUserProfileAsync(System.Web.Routing.RequestContext requestContext)
        {
            var userIdentity = requestContext.HttpContext.User.Identity;
            if (userIdentity.IsAuthenticated)
            {
                var cached = requestContext.HttpContext.Cache.Get(userIdentity.Name + "_profile");
                if (cached != null)
                {
                    this.UserProfile = (UserProfile)cached;
                }
                else
                {
                    RenewApplicationUser(userIdentity.Name, requestContext);
                }
                
            }
            else
            {
                this.UserProfile = null;
            }
        }
 
        /// <summary>
        /// Renews the application user.
        /// </summary>
        internal void RenewApplicationUser(string userName , RequestContext context)
        {
            var rolsStore = data.Roles.All();
                var user = this.data.Users.All()
                    .Include(x => x.Roles)
                    .Include(x => x.ApplicationUserProcessUnits.Select(y=>y.ProcessUnit))
                    .Include(x => x.ApplicationUserParks.Select(y=>y.Park))
                    .FirstOrDefault(x => x.UserName == userName);
                var roles = user.Roles.Select(x=>x.RoleId).ToList();
                user.UserRoles = rolsStore.Where(rol => roles.Any(x => rol.Id == x)).ToList();
                
                this.UserProfile= new UserProfile();
                Mapper.Map(user, this.UserProfile);
                context.HttpContext.Cache.Add(userName + "_profile", this.UserProfile,null,DateTime.Now.AddMinutes(2), Cache.NoSlidingExpiration, CacheItemPriority.High, RemovedCallback);
        }
 
        /// <summary>
        /// Removeds the callback.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="reason">The reason.</param>
        private void RemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            Debug.WriteLine(string.Format("-------------------------------------------- Cache expired {0} because {1} ------------------------------------------------------", key,reason));
        }

        protected override void Dispose(bool disposing)
        {
            data.Dispose();
            base.Dispose(disposing);
        }
    }
}
