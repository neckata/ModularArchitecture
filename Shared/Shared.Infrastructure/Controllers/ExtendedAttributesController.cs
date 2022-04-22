using System;
using System.Threading.Tasks;
using Gamification.Shared.Core.Contracts;
using Gamification.Shared.Core.Domain;
using Gamification.Shared.Core.Features.ExtendedAttributes.Commands;
using Gamification.Shared.Core.Features.ExtendedAttributes.Filters;
using Gamification.Shared.Core.Features.ExtendedAttributes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.Shared.Infrastructure.Controllers
{
    [ApiController]
    public abstract class ExtendedAttributesController<TEntityId, TEntity> : CommonBaseController
        where TEntity : class, IEntity<TEntityId>
    {
        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync([FromQuery] PaginatedExtendedAttributeFilter<TEntityId, TEntity> filter)
        {
            var extendedAttributes = await Mediator.Send(new GetExtendedAttributesQuery<TEntityId, TEntity>(filter));
            return Ok(extendedAttributes);
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync(AddExtendedAttributeCommand<TEntityId, TEntity> command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<TEntityId, TEntity> command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveExtendedAttributeCommand<TEntityId, TEntity>(id)));
        }
    }
}