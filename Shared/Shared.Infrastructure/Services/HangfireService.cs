using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gamification.Shared.Core.Interfaces.Services;
using Hangfire;

namespace Gamification.Shared.Infrastructure.Services
{
    public class HangfireService : IJobService
    {
        public string Enqueue(Expression<Func<Task>> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }
    }
}