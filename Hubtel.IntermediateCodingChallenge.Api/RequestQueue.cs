using System;
using System.Collections.Concurrent;
using Hubtel.IntermediateCodingChallenge.Api.Models;

namespace Hubtel.IntermediateCodingChallenge.Api
{
    public static class RequestQueue
    {
        public static ConcurrentQueue<Tuple<string, BatchSmsModel>> SmsQueue = new ConcurrentQueue<Tuple<string, BatchSmsModel>>();
    }
}