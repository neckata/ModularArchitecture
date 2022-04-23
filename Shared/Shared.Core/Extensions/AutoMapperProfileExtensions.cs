using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Gamification.Shared.Core.Domain;
using Gamification.Shared.DTOs.ExtendedAttributes;

namespace Gamification.Shared.Core.Extensions
{
    public static class AutoMapperProfileExtensions
    {
        public static Profile CreateExtendedAttributesMappings(this Profile profile, Assembly assembly)
        {
            return profile;
        }
    }
}