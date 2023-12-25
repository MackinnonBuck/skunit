﻿using Microsoft.SemanticKernel;
using skUnit.Tests.SemanticKernelTests.ChatScenarioTests.Plugins;
using skUnit.Tests.SemanticKernelTests.InvokeScenarioTests;
using Xunit.Abstractions;

namespace skUnit.Tests.SemanticKernelTests.ChatScenarioTests
{
    public class KernelChatTests : SemanticTestBase
    {
        public KernelChatTests(ITestOutputHelper output) : base(output)
        {
            
        }

        [Fact]
        public async Task EiffelTallChat_MustWork()
        {
            var scenarios = await LoadChatScenarioAsync("EiffelTallChat");
            await SemanticKernelAssert.CheckChatScenarioAsync(Kernel, scenarios);
        }

        [Fact(Skip = "It doesn't work functions yet.")]
        public async Task PocomoPriceChat_MustWork()
        {
            //var dir = Path.Combine(Environment.CurrentDirectory, "SemanticKernel", "ChatScenarioTests", "Plugins");
            //Kernel.ImportPluginFromPromptDirectory(dir);

            Kernel.ImportPluginFromType<PocomoPlugin>();

            var scenarios = await LoadChatScenarioAsync("PocomoPriceChat");
            await SemanticKernelAssert.CheckChatScenarioAsync(Kernel, scenarios);
        }
    }

    
}
