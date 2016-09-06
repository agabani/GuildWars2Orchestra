using System;
using GuildWars2Orchestra.Domain.Values;
using NUnit.Framework;

namespace GuildWars2Orchestra.Domain.Tests.Unit.Values
{
    [TestFixture]
    internal class MetronomeMarkTests
    {
        [Test]
        [TestCase(060, 1, 4, "00:00:01.0000000")]
        [TestCase(070, 1, 4, "00:00:00.8571428")]
        [TestCase(120, 1, 4, "00:00:00.5000000")]
        [TestCase(130, 1, 4, "00:00:00.4615384")]
        [TestCase(060, 1, 2, "00:00:00.5000000")]
        [TestCase(070, 1, 2, "00:00:00.4285712")]
        [TestCase(120, 1, 2, "00:00:00.2500000")]
        [TestCase(130, 1, 2, "00:00:00.2307692")]
        [TestCase(060, 1, 8, "00:00:02.0000000")]
        [TestCase(070, 1, 8, "00:00:01.7142856")]
        [TestCase(120, 1, 8, "00:00:01.0000000")]
        [TestCase(130, 1, 8, "00:00:00.9230768")]
        public void it_calculates_length(int metronome, int nominator, int denominator, string expected)
        {
            var metronomeMark = new MetronomeMark(metronome, new Fraction(nominator, denominator));

            Assert.That(metronomeMark.WholeNoteLength, Is.EqualTo(TimeSpan.Parse(expected)));
        }
    }
}