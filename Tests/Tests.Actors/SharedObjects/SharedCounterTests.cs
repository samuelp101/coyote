﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Threading.Tasks;
using Microsoft.Coyote.Actors.SharedObjects;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Coyote.Actors.Tests.SharedObjects
{
    public class SharedCounterTests : BaseActorTest
    {
        public SharedCounterTests(ITestOutputHelper output)
            : base(output)
        {
        }

        private class E : Event
        {
            public SharedCounter Counter;
            public TaskCompletionSource<bool> Tcs;

            public E(SharedCounter counter, TaskCompletionSource<bool> tcs)
            {
                this.Counter = counter;
                this.Tcs = tcs;
            }
        }

        private class M1 : StateMachine
        {
            [Start]
            [OnEntry(nameof(InitOnEntry))]
            private class Init : State
            {
            }

            private void InitOnEntry(Event e)
            {
                var counter = (e as E).Counter;
                var tcs = (e as E).Tcs;

                for (int i = 0; i < 100000; i++)
                {
                    counter.Increment();

                    var v1 = counter.GetValue();
                    this.Assert(v1 is 1 || v1 is 2);

                    counter.Decrement();

                    var v2 = counter.GetValue();
                    this.Assert(v2 is 0 || v2 is 1);

                    counter.Add(1);

                    var v3 = counter.GetValue();
                    this.Assert(v3 is 1 || v3 is 2);

                    counter.Add(-1);

                    var v4 = counter.GetValue();
                    this.Assert(v4 is 0 || v4 is 1);
                }

                tcs.SetResult(true);
            }
        }

        private class M2 : StateMachine
        {
            [Start]
            [OnEntry(nameof(InitOnEntry))]
            private class Init : State
            {
            }

#pragma warning disable CA1822 // Mark members as static
            private void InitOnEntry(Event e)
#pragma warning restore CA1822 // Mark members as static
            {
                var counter = (e as E).Counter;
                var tcs = (e as E).Tcs;

                for (int i = 0; i < 1000000; i++)
                {
                    int v;

                    do
                    {
                        v = counter.GetValue();
                    }
                    while (v != counter.CompareExchange(v + 5, v));

                    counter.Add(15);
                    counter.Add(-10);
                }

                tcs.SetResult(true);
            }
        }

        [Fact(Timeout = 5000)]
        public void TestProductionSharedCounter1()
        {
            var runtime = RuntimeFactory.Create();
            var counter = SharedCounter.Create(runtime, 0);
            var tcs1 = new TaskCompletionSource<bool>();
            var tcs2 = new TaskCompletionSource<bool>();
            var failed = false;

            runtime.OnFailure += (ex) =>
            {
                failed = true;
                tcs1.SetResult(true);
                tcs2.SetResult(true);
            };

            var m1 = runtime.CreateActor(typeof(M1), new E(counter, tcs1));
            var m2 = runtime.CreateActor(typeof(M1), new E(counter, tcs2));

            Task.WaitAll(tcs1.Task, tcs2.Task);
            Assert.False(failed);
        }

        [Fact(Timeout = 5000)]
        public void TestProductionSharedCounter2()
        {
            var runtime = RuntimeFactory.Create();
            var counter = SharedCounter.Create(runtime, 0);
            var tcs1 = new TaskCompletionSource<bool>();
            var tcs2 = new TaskCompletionSource<bool>();
            var failed = false;

            runtime.OnFailure += (ex) =>
            {
                failed = true;
                tcs1.SetResult(true);
                tcs2.SetResult(true);
            };

            var m1 = runtime.CreateActor(typeof(M2), new E(counter, tcs1));
            var m2 = runtime.CreateActor(typeof(M2), new E(counter, tcs2));

            Task.WaitAll(tcs1.Task, tcs2.Task);
            Assert.False(failed);
            Assert.True(counter.GetValue() is 1000000 * 20);
        }
    }
}
