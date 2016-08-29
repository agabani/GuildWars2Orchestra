using System;
using GuildWars2Orchestra.Values;
using NUnit.Framework;

namespace GuildWars2Orchestra.Tests.Unit.Values
{
    [TestFixture]
    internal class MetronomeMarkTests
    {
        [Test]
        [TestCase(060, 1, 4, "00:00:04.0000000")]
        [TestCase(070, 1, 4, "00:00:03.4285712")]
        [TestCase(120, 1, 4, "00:00:02.0000000")]
        [TestCase(130, 1, 4, "00:00:01.8461536")]
        public void it_calculates_length(int metronome, int nominator, int denominator, string expected)
        {
            var metronomeMark = new MetronomeMark(metronome, new Fraction(nominator, denominator));

            Assert.That(metronomeMark.WholeNoteLength, Is.EqualTo(TimeSpan.Parse(expected)));
        }
    }
}