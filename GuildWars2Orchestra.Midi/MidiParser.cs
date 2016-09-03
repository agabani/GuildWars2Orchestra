using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GuildWars2Orchestra.Domain;
using GuildWars2Orchestra.Domain.Values;
using NAudio.Midi;

namespace GuildWars2Orchestra.Midi
{
    public class MidiParser
    {
        private static readonly Fraction BeatsPerMeasure = new Fraction(1, 4);
        private static readonly Regex Regex = new Regex(@"Ch:\ \d+\ ([a-zA-Z]#?)(\d) .* Len: (\d+)");

        public MusicSheet Parse(string midiPath)
        {
            var midi = new MidiFile(midiPath);
            return new MusicSheet(ParseMetronomeMark(midi), ParseMelody(midi));
        }

        private static MetronomeMark ParseMetronomeMark(MidiFile midi)
        {
            return new MetronomeMark(midi.DeltaTicksPerQuarterNote/4, BeatsPerMeasure);
        }

        private static IEnumerable<ChordOffset> ParseMelody(MidiFile midi)
        {
            var chords = midi.Events
                .SelectMany(@event => @event)
                .Where(MidiEvent.IsNoteOn)
                .GroupBy(@event => @event.AbsoluteTime)
                .OrderBy(group => group.Key)
                .Select(ToChordOffset)
                .ToList();

            return chords;
        }

        private static ChordOffset ToChordOffset(IGrouping<long, MidiEvent> midiEvents)
        {
            var chordDuration = TimeSpan.MaxValue;

            var notes = midiEvents
                .Select(ToMidiNote)
                .Select(midiNote =>
                {

                    chordDuration = midiNote.Duration < chordDuration ? midiNote.Duration : chordDuration;
                    return ToNote(midiNote);
                });

            var fraction = new Fraction(chordDuration.Milliseconds, 500);
            return new ChordOffset(new Chord(notes, fraction), new Beat(0));
        }

        private static MidiNote ToMidiNote(MidiEvent note)
        {
            var match = Regex.Match(note.ToString());

            return new MidiNote
            {
                Duration = TimeSpan.FromMilliseconds(double.Parse(match.Groups[3].Value)),
                Note = match.Groups[1].Value,
                Octave = int.Parse(match.Groups[2].Value)
            };
        }

        private static Note ToNote(MidiNote note)
        {
            var key = ParseKey(note);
            var octave = ParseOctave(note);
            return new Note(key, octave);
        }

        private static Note.Keys ParseKey(MidiNote note)
        {
            switch (note.Note[0].ToString())
            {
                case "C":
                    return Note.Keys.C;
                case "D":
                    return Note.Keys.D;
                case "E":
                    return Note.Keys.E;
                case "F":
                    return Note.Keys.F;
                case "G":
                    return Note.Keys.G;
                case "A":
                    return Note.Keys.A;
                case "B":
                    return Note.Keys.B;
                default:
                    throw new NotSupportedException();
            }
        }

        private static Note.Octaves ParseOctave(MidiNote note)
        {
            switch (note.Octave)
            {
                case 3:
                case 4:
                case 5:
                    return Note.Octaves.Low;
                case 6:
                    return Note.Octaves.Middle;
                case 7:
                    return Note.Octaves.High;
                case 8:
                    return Note.Octaves.Highest;
                default:
                    throw new NotSupportedException();
            }
        }

        public struct MidiNote
        {
            public TimeSpan Duration;
            public string Note;
            public int Octave;
        }
    }
}