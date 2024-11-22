﻿using Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Pattern.AzureMessageQueue.Query.GetQueueMessages
{
    public class GetQueueMessageQuery : BaseCommandQuery , IRequest<GetQueueMessageRespoance>
    {
    }
}