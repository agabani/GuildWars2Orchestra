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
        //public MusicSheet Parse(MidiFile midi)
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
            var allEvents = new List<MidiEvent>();
            foreach (var events in midi.Events)
            {
                allEvents.AddRange(events);
            }

            var orderedAllEvents = allEvents.OrderBy(a => a.AbsoluteTime).ToList();


            //var midiEventCollection = midi.Events;
            //var midiEvents = midiEventCollection[0];

            var midiEvents = orderedAllEvents;

            var noteOnEvent = midiEvents.Where(@event => @event.CommandCode == MidiCommandCode.NoteOn && MidiEvent.IsNoteOn(@event)).ToList();

            var enumerable = noteOnEvent.Select(note =>
            {
                var match = Regex.Match(note.ToString());

                return new MidiNote
                {
                    Duration = TimeSpan.FromMilliseconds(double.Parse(match.Groups[3].Value)),
                    Note = match.Groups[1].Value,
                    Octave = int.Parse(match.Groups[2].Value)
                };
            }).ToList();

            var chordOffsets = enumerable.Select(ToChord);

            return chordOffsets;
        }

        private static ChordOffset ToChord(MidiNote note)
        {
            var key = ParseKey(note);
            var octave = ParseOctave(note);

            var fraction = new Fraction(note.Duration.Milliseconds, 1000);

            return new ChordOffset(new Chord(new List<Note> {new Note(key, octave)}, fraction), new Beat(0));
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