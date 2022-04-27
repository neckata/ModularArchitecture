using System;
using System.Linq;
using AutoMapper;

namespace Gamification.Shared.Core.Mappings.Converters
{
    public class OrderByConverter : IValueConverter<string, string[]>, IValueConverter<string[], string>
    {
        public string[] Convert(string orderBy, ResolutionContext context = null)
        {
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                return orderBy
                    .Split(',')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim()).ToArray();
            }

            return Array.Empty<string>();
        }

        public string Convert(string[] orderBy, ResolutionContext context = null) => orderBy?.Any() == true ? string.Join(",", orderBy) : null;
    }

    //Example
    //CreateMap<PaginatedEventLogsFilter, GetEventLogsRequest>()
    //            .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
}