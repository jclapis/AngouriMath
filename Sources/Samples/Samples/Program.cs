using AngouriMath;
using HonkSharp.Fluency;

using System;
using static AngouriMath.Entity;

var lambda = new Lambda("x", "x ^ x + 3");
var applied = lambda.Apply(3);
Console.WriteLine(applied);
Console.WriteLine(applied.InnerSimplified);