using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gamification.Shared.Core.Interfaces.Services
{
    public interface IJobService
    {
        string Enqueue(Expression<Func<Task>> methodCall);
    }
}