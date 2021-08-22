using AngouriMath;
using AngouriMath.Extensions;
using FluentAssertions;
using HonkSharp.Fluency;
using System.Linq;
using Xunit;
using static AngouriMath.Entity;

namespace UnitTests.Common
{
    public class LambdaCalculusTest
    {
        private readonly Variable x = "x";
        private readonly Variable y = "y";

        [Fact] public void ApplicationDoesNothingToVariable()
            => x.Apply(4).Alias(out var expected).InnerSimplified.Should().Be(expected);

        [Fact] public void ApplicationDoesNothingToNumber()
            => 5.ToNumber().Apply(4).Alias(out var expected).InnerSimplified.Should().Be(expected);

        [Fact] public void LambdaIdentity()
            => x.LambdaOver(x).Apply(67).InnerSimplified.ShouldBe(67);

        [Fact] public void LambdaTest1()
            => (2 * x).LambdaOver(x).Apply(4).InnerSimplified.ShouldBe(8);

        [Fact] public void LambdaTest2()
            => (2 * x * y).LambdaOver(x).LambdaOver(y).Apply(4).Apply(3).InnerSimplified.ShouldBe(24);

        [Fact] public void LambdaTest3()
            => (2 * x * y).LambdaOver(x).LambdaOver(y).Apply(4).InnerSimplified.ShouldBe((2 * x * 4).LambdaOver(x));
    }
}
