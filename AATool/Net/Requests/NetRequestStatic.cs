﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using AATool.Utilities;

namespace AATool.Net
{
    public abstract partial class NetRequest
    {
        protected static readonly HttpClient Client = new HttpClient
        {
            Timeout = TimeSpan.FromMilliseconds(Protocol.Requests.TimeoutNormalMs)
        };

        private static readonly Queue<NetRequest> Pending = new ();
        private static readonly List<NetRequest> TimedOut = new ();
        private static readonly HashSet<string> Abandoned = new ();
        private static readonly HashSet<string> Completed = new ();
        private static readonly HashSet<string> Active    = new ();
        private static readonly HashSet<string> Submitted = new ();
        private static readonly Timer RequestDelay = new (Protocol.Requests.UpdateRate);

        public static int CompletedCount => Completed.Count;
        public static int TimedOutCount => TimedOut.Count;
        public static int AbandonedCount => Abandoned.Count;
        public static int PendingCount => Pending.Count;
        public static int ActiveCount => Active.Count;
        public static int SubmittedCount => Submitted.Count;

        public static void ClearHistory()
        {
            Submitted.Clear();
            Completed.Clear();
        }

        private static void Enqueue(NetRequest request)
        {
            //add request to pending queue if unique
            if (!Submitted.Contains(request.Url))
            {
                Pending.Enqueue(request);
                Submitted.Add(request.Url);
            }
        }

        public static void Update(Time time)
        {
            UpdateTimeouts(time);
            RequestDelay.Update(time);
            if (RequestDelay.IsExpired)
            {
                RequestDelay.Reset();   
                UpdatePending();
            }
        }

        private static void UpdateTimeouts(Time time)
        {
            for (int i = TimedOut.Count - 1; i >= 0 ; i--)
            {
                NetRequest request = TimedOut[i];
                request.UpdateCooldown(time);
                if (!request.IsOnCooldown)
                {
                    //move back into pending queue
                    TimedOut.RemoveAt(i);
                    Pending.Enqueue(request);
                }
            }
        }

        private static void UpdatePending()
        {
            for (int i = Active.Count; i < Protocol.Requests.MaxConcurrent; i++)
            {
                if (!Pending.Any())
                    return;

                //start next request
                NetRequest next = Pending.Dequeue();
                next.SendAsync();
            }
        }
    }
}
