﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Coyote.Random;
using Microsoft.Coyote.Specifications;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Coyote.Tasks.SystematicTesting.Tests
{
    public class TaskRandomBooleanTests : BaseTaskTest
    {
        public TaskRandomBooleanTests(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact(Timeout = 5000)]
        public void TestRandomBooleanInSynchronousTask()
        {
            this.TestWithError(async () =>
            {
                Generator generator = Generator.Create();
                SharedEntry entry = new SharedEntry();

                async Task WriteAsync()
                {
                    await Task.CompletedTask;
                    if (generator.NextBoolean())
                    {
                        entry.Value = 3;
                    }
                    else
                    {
                        entry.Value = 5;
                    }
                }

                await WriteAsync();
                AssertSharedEntryValue(entry, 5);
            },
            configuration: GetConfiguration().WithTestingIterations(200),
            expectedError: "Value is 3 instead of 5.",
            replay: true);
        }

        [Fact(Timeout = 5000)]
        public void TestRandomBooleanInAsynchronousTask()
        {
            this.TestWithError(async () =>
            {
                Generator generator = Generator.Create();
                SharedEntry entry = new SharedEntry();

                async Task WriteWithDelayAsync()
                {
                    await Task.Delay(1);
                    if (generator.NextBoolean())
                    {
                        entry.Value = 3;
                    }
                    else
                    {
                        entry.Value = 5;
                    }
                }

                await WriteWithDelayAsync();
                AssertSharedEntryValue(entry, 5);
            },
            configuration: GetConfiguration().WithTestingIterations(200),
            expectedError: "Value is 3 instead of 5.",
            replay: true);
        }

        [Fact(Timeout = 5000)]
        public void TestRandomBooleanInParallelTask()
        {
            this.TestWithError(async () =>
            {
                Generator generator = Generator.Create();
                SharedEntry entry = new SharedEntry();

                await Task.Run(() =>
                {
                    if (generator.NextBoolean())
                    {
                        entry.Value = 3;
                    }
                    else
                    {
                        entry.Value = 5;
                    }
                });

                AssertSharedEntryValue(entry, 5);
            },
            configuration: GetConfiguration().WithTestingIterations(200),
            expectedError: "Value is 3 instead of 5.",
            replay: true);
        }

        [Fact(Timeout = 5000)]
        public void TestRandomBooleanInParallelSynchronousTask()
        {
            this.TestWithError(async () =>
            {
                Generator generator = Generator.Create();
                SharedEntry entry = new SharedEntry();

                await Task.Run(async () =>
                {
                    await Task.CompletedTask;
                    if (generator.NextBoolean())
                    {
                        entry.Value = 3;
                    }
                    else
                    {
                        entry.Value = 5;
                    }
                });

                AssertSharedEntryValue(entry, 5);
            },
            configuration: GetConfiguration().WithTestingIterations(200),
            expectedError: "Value is 3 instead of 5.",
            replay: true);
        }

        [Fact(Timeout = 5000)]
        public void TestRandomBooleanInParallelAsynchronousTask()
        {
            this.TestWithError(async () =>
            {
                Generator generator = Generator.Create();
                SharedEntry entry = new SharedEntry();

                await Task.Run(async () =>
                {
                    await Task.Delay(1);
                    if (generator.NextBoolean())
                    {
                        entry.Value = 3;
                    }
                    else
                    {
                        entry.Value = 5;
                    }
                });

                AssertSharedEntryValue(entry, 5);
            },
            configuration: GetConfiguration().WithTestingIterations(200),
            expectedError: "Value is 3 instead of 5.",
            replay: true);
        }

        [Fact(Timeout = 5000)]
        public void TestRandomBooleanInNestedParallelSynchronousTask()
        {
            this.TestWithError(async () =>
            {
                Generator generator = Generator.Create();
                SharedEntry entry = new SharedEntry();

                await Task.Run(async () =>
                {
                    await Task.Run(async () =>
                    {
                        await Task.CompletedTask;
                        if (generator.NextBoolean())
                        {
                            entry.Value = 3;
                        }
                        else
                        {
                            entry.Value = 5;
                        }
                    });
                });

                AssertSharedEntryValue(entry, 5);
            },
            configuration: GetConfiguration().WithTestingIterations(200),
            expectedError: "Value is 3 instead of 5.",
            replay: true);
        }
    }
}
