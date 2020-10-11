﻿using AngouriMath;
using static AngouriMath.Entity.Number;
using Xunit;

namespace UnitTests.Common
{
    public sealed class CircleTest
    {
        public static readonly Entity.Variable x = MathS.Var(nameof(x));

        [Theory]
        [InlineData("1 + 2 * log(2, 3)")]
        [InlineData("2 ^ 3 + sin(3)")]
        [InlineData("x!")]
        [InlineData("(-x)!")]
        [InlineData("-x!")]
        [InlineData("(3i)!")]
        public void Circle(string inputIsOutput) =>
            Assert.Equal(inputIsOutput, MathS.FromString(inputIsOutput).Stringize());
        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("-1")]
        [InlineData("i")]
        [InlineData("2i")]
        [InlineData("233i")]
        [InlineData("-i")]
        [InlineData("-2i")]
        [InlineData("-4i")]
        public void Number(string inputIsOutput)
        {
            Assert.Equal(inputIsOutput, Complex.Parse(inputIsOutput).Stringize());
            Assert.True(Complex.TryParse(inputIsOutput, out var output));
            Assert.Equal(inputIsOutput, output?.Stringize());
        }
        [Theory]
        [InlineData("0i", "0")]
        [InlineData("1i", "i")]
        [InlineData("-1i", "-i")]
        public void NumberSimplify(string input, string output)
        {
            Assert.Equal(output, Complex.Parse(input).Stringize());
            Assert.True(Complex.TryParse(input, out var o));
            Assert.Equal(output, o?.Stringize());
        }
        [Theory]
        [InlineData("quack")]
        [InlineData("i1")]
        [InlineData("ii")]
        [InlineData("")]
        public void NotNumber(string input)
        {
            Assert.Equal($"Cannot parse number from {input}",
                Assert.Throws<AngouriMath.Core.Exceptions.ParseException>(() => Complex.Parse(input).Stringize()).Message);
            Assert.False(Complex.TryParse(input, out var output));
            Assert.Null(output);
        }

        [Theory]
        [InlineData("23.3", "233/10")]
        [InlineData("5.3", "53/10")]
        [InlineData("-5.3i", "-53/10i")]
        public void DecimalToRational(string @decimal, string rational)
        {
            string expr = $"{@decimal} + 3 / 3 + {@decimal} + i";
            var exprActual = MathS.FromString(expr);
            Assert.Equal($"{rational} + 3 / 3 + {rational} + i", exprActual.Stringize());
        }

        [Fact]
        public void Test4() => MathS.FromString((MathS.Sin(x) / MathS.Cos(x)).Derive(x).Stringize() ?? "");

        [Fact]
        public void Test7() => Assert.Equal(3 * x, MathS.Sin(MathS.Arcsin(x * 3)).Simplify());

        [Fact]
        public void Test8() => Assert.Equal(3 * x, MathS.Arccotan(MathS.Cotan(x * 3)).Simplify());

        [Theory]
        [InlineData("x / y + x * x * y")]
        [InlineData("x / 1 + 2")]
        [InlineData("(x + y + x + 1 / (x + 4 + 4 + sin(x))) / (x + x + 3 / y) + 3")]
        public void TestLinch(string inputIsOutput) =>
            Assert.Equal(inputIsOutput, MathS.FromString(inputIsOutput).Stringize());

        [Theory]
        [InlineData("sgn(x)")]
        [InlineData("abs(x)")]
        public void TestDiscrete(string inputIsOutput) =>
            Assert.Equal(inputIsOutput, MathS.FromString(inputIsOutput).Stringize());
    }
}