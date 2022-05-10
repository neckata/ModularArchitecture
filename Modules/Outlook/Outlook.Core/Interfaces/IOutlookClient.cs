﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ModularArchitecture.Shared.Core.Wrapper;

namespace Outlook.Core.Services
{
    public interface IOutlookClient
    {
        public Task<IResult<List<string>>> GetEmails();
    }
}
