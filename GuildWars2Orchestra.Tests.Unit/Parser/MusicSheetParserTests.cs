﻿using System;
using System.Linq;
using GuildWars2Orchestra.Parsers;
using GuildWars2Orchestra.TestData;
using GuildWars2Orchestra.Values;
using NUnit.Framework;

namespace GuildWars2Orchestra.Tests.Unit.Parser
{
    [TestFixture]
    internal class MusicSheetParserTests
    {
        private static void AssertChord(ChordOffset chordOffset, Note.Keys expectedNote, Note.Octaves expectedOctave, decimal expectedOffset, Fraction expectedFraction)
        {
            Assert.That(chordOffset.Offest.Value, Is.EqualTo(expectedOffset));
            Assert.That(chordOffset.Chord.Length, Is.EqualTo(expectedFraction));
            var note = chordOffset.Chord.Notes.ElementAt(0);
            Assert.That(note.Key, Is.EqualTo(expectedNote));
            Assert.That(note.Octave, Is.EqualTo(expectedOctave));
        }

        [Test]
        public void it_parses()
        {
            var notesPerBeat = new Fraction(
                Melodies.FinalFantasyXiii2.AWish.Nominator,
                Melodies.FinalFantasyXiii2.AWish.Denominator);

            var musicSheetParser = new MusicSheetParser(new ChordParser(new NoteParser()).WithNotesPerBeat(notesPerBeat));

            var musicSheet = musicSheetParser.Parse(
                Melodies.FinalFantasyXiii2.AWish.Melody,
                Melodies.FinalFantasyXiii2.AWish.Tempo,
                Melodies.FinalFantasyXiii2.AWish.Nominator,
                Melodies.FinalFantasyXiii2.AWish.Denominator);

            Assert.That(musicSheet.MetronomeMark.Metronome, Is.EqualTo(75));
            Assert.That(musicSheet.MetronomeMark.WholeNoteLength, Is.EqualTo(TimeSpan.FromMilliseconds(1600)));

            var melody = musicSheet.Melody.ToArray();

            AssertChord(melody[0], Note.Keys.Note4, Note.Octaves.Middle, 0m, new Fraction(1, 10));
            AssertChord(melody[1], Note.Keys.Note6, Note.Octaves.Middle, 0.1m, new Fraction(1, 10));
            AssertChord(melody[2], Note.Keys.Note1, Note.Octaves.High, 0.2m, new Fraction(1, 10));
            AssertChord(melody[3], Note.Keys.Note3, Note.Octaves.High, 0.3m, new Fraction(1, 10));
            AssertChord(melody[4], Note.Keys.Note4, Note.Octaves.High, 0.4m, new Fraction(1, 10));
            AssertChord(melody[5], Note.Keys.Note6, Note.Octaves.High, 0.5m, new Fraction(5, 2));
            AssertChord(melody[6], Note.Keys.Note2, Note.Octaves.Middle, 3.0m, new Fraction(1, 10));
            AssertChord(melody[7], Note.Keys.Note4, Note.Octaves.Middle, 3.1m, new Fraction(1, 10));
            AssertChord(melody[8], Note.Keys.Note6, Note.Octaves.Middle, 3.2m, new Fraction(1, 10));
            AssertChord(melody[9], Note.Keys.Note1, Note.Octaves.High, 3.3m, new Fraction(1, 10));
            AssertChord(melody[10], Note.Keys.Note2, Note.Octaves.High, 3.4m, new Fraction(1, 10));
            AssertChord(melody[11], Note.Keys.Note4, Note.Octaves.High, 3.5m, new Fraction(5, 2));
        }
    }
}