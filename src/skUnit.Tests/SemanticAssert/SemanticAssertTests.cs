using skUnit.Exceptions;
using Xunit.Abstractions;

namespace skUnit.Tests.SemanticAssert
{
    public class SemanticAssertTests
    {
        private ITestOutputHelper Output { get; set; }
        public SemanticAssertTests(ITestOutputHelper output)
        {
            Output = output;

            var apiKey =
                Environment.GetEnvironmentVariable("openai-api-key", EnvironmentVariableTarget.User) ??
                throw new Exception("No ApiKey in environment variables.");
            var endpoint =
                Environment.GetEnvironmentVariable("openai-endpoint", EnvironmentVariableTarget.User) ??
                throw new Exception("No Endpoint in environment variables.");
            var deploymentName =
                Environment.GetEnvironmentVariable("openai-deployment-name", EnvironmentVariableTarget.User) ??
                throw new Exception("No DeploymentName in environment variables.");

            skUnit.SemanticAssert.Initialize(deploymentName,endpoint, apiKey);
        }

        [Theory]
        [MemberData(nameof(GetSameData))]
        public void Same_True_MustWork(string first, string second)
        {
            skUnit.SemanticAssert.Same(first, second);
        }

        [Theory]
        [MemberData(nameof(GetNotSameData))]
        public void Same_False_MustWork(string first, string second)
        {
            var exception = Assert.Throws<SemanticAssertException>(() => skUnit.SemanticAssert.Same(first, second));
            Output.WriteLine($"""
                [Explanation]
                {exception.Message}
                """);
        }

        [Theory]
        [MemberData(nameof(GetNotSameData))]
        public void NotSame_True_MustWork(string first, string second)
        {
            skUnit.SemanticAssert.NotSame(first, second);
        }

        [Theory]
        [MemberData(nameof(GetSameData))]
        public void NotSame_False_MustWork(string first, string second)
        {
            var exception = Assert.Throws<SemanticAssertException>(() => skUnit.SemanticAssert.NotSame(first, second));
            Output.WriteLine($"""
                [Explanation]
                {exception.Message}
                """);
        }

        public static IEnumerable<object[]> GetNotSameData()
        {
            yield return new object[]
            {
                "This car is red",
                "The car is blue"
            };
            yield return new object[]
            {
                "This bicycle is red",
                "The car is red"
            };
        }

        public static IEnumerable<object[]> GetSameData()
        {
            yield return new object[]
            {
                "This car is red",
                "The car is red"
            };
            yield return new object[]
            {
                "This automobile is red",
                "The car is red"
            };
        }
    }
}