using AngouriMath;
using HonkSharp.Fluency;

using System;
using static AngouriMath.Entity;
using AngouriMath.Extensions;

var sinApplied = "derivative".ToEntity().Apply("x ^ 3");
Console.WriteLine(sinApplied.InnerSimplified);