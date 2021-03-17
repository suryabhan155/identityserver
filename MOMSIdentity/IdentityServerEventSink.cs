using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MOM.IS4Host.Data;
using MOM.IS4Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MOM.IS4Host
{
    public class IdentityServerEventSink : IEventSink
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public IdentityServerEventSink(IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext applicationDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }

        public async Task PersistAsync(Event evnt)
        {
            try
            {
                ApplicationEventStore applicationEventStore;
                ApplicationUser user = null;
                user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                if (user != null)
                {
                    applicationEventStore = new ApplicationEventStore
                    {
                        ActivityId = evnt.ActivityId,
                        Category = evnt.Category,
                        EventType = evnt.EventType.ToString(),
                        LocalIpAddress = evnt.LocalIpAddress,
                        Message = evnt.Message,
                        Name = evnt.Name,
                        ProcessId = evnt.ProcessId,
                        RemoteIpAddress = evnt.RemoteIpAddress,
                        TimeStamp = evnt.TimeStamp,
                        UserName = user.UserName
                    };
                }
                else
                {
                    if (evnt.Id.Equals(EventIds.UserLoginFailure))
                    {
                        var @event = (UserLoginFailureEvent)evnt;
                        applicationEventStore = new ApplicationEventStore
                        {
                            ActivityId = evnt.ActivityId,
                            Category = evnt.Category,
                            EventType = evnt.EventType.ToString(),
                            LocalIpAddress = evnt.LocalIpAddress,
                            Message = evnt.Message,
                            Name = evnt.Name,
                            ProcessId = evnt.ProcessId,
                            RemoteIpAddress = evnt.RemoteIpAddress,
                            TimeStamp = evnt.TimeStamp,
                            UserName = @event.Username
                        };
                    }
                    else if (evnt.Id.Equals(EventIds.UserLoginSuccess))
                    {
                        var @event = (UserLoginSuccessEvent)evnt;
                        applicationEventStore = new ApplicationEventStore
                        {
                            ActivityId = evnt.ActivityId,
                            Category = evnt.Category,
                            EventType = evnt.EventType.ToString(),
                            LocalIpAddress = evnt.LocalIpAddress,
                            Message = evnt.Message,
                            Name = evnt.Name,
                            ProcessId = evnt.ProcessId,
                            RemoteIpAddress = evnt.RemoteIpAddress,
                            TimeStamp = evnt.TimeStamp,
                            UserName = @event.Username
                        };
                    }
                    else if (evnt.Id.Equals(EventIds.UserLogoutSuccess))
                    {
                        var @event = (UserLogoutSuccessEvent)evnt;
                        applicationEventStore = new ApplicationEventStore
                        {
                            ActivityId = evnt.ActivityId,
                            Category = evnt.Category,
                            EventType = evnt.EventType.ToString(),
                            LocalIpAddress = evnt.LocalIpAddress,
                            Message = evnt.Message,
                            Name = evnt.Name,
                            ProcessId = evnt.ProcessId,
                            RemoteIpAddress = evnt.RemoteIpAddress,
                            TimeStamp = evnt.TimeStamp,
                            UserName = @event.DisplayName
                        };
                    }
                    else
                    {
                        applicationEventStore = new ApplicationEventStore
                        {
                            ActivityId = evnt.ActivityId,
                            Category = evnt.Category,
                            EventType = evnt.EventType.ToString(),
                            LocalIpAddress = evnt.LocalIpAddress,
                            Message = evnt.Message,
                            Name = evnt.Name,
                            ProcessId = evnt.ProcessId,
                            RemoteIpAddress = evnt.RemoteIpAddress,
                            TimeStamp = evnt.TimeStamp
                        };
                    }
                }

                await _applicationDbContext.AddAsync(applicationEventStore);
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
