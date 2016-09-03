using System;
using System.Collections.Generic;
using System.Linq;
using GuildWars2Orchestra.Domain;
using GuildWars2Orchestra.Domain.Values;
using NAudio.Midi;

namespace GuildWars2Orchestra.Midi
{
    public class MidiParser
    {
        private static readonly Fraction BeatsPerMeasure = new Fraction(1, 4);
        //private static readonly Regex Regex = new Regex(@"([a-zA-Z]#?)(\d)");

        private static readonly IReadOnlyDictionary<int, Note> Map = new Dictionary<int, Note>
        {
            // Octave -2
            {0, new Note(Note.Keys.C, Note.Octaves.Low)},
            {1, new Note(Note.Keys.C, Note.Octaves.Low)},
            {2, new Note(Note.Keys.D, Note.Octaves.Low)},
            {3, new Note(Note.Keys.D, Note.Octaves.Low)},
            {4, new Note(Note.Keys.E, Note.Octaves.Low)},
            {5, new Note(Note.Keys.F, Note.Octaves.Low)},
            {6, new Note(Note.Keys.F, Note.Octaves.Low)},
            {7, new Note(Note.Keys.G, Note.Octaves.Low)},
            {8, new Note(Note.Keys.G, Note.Octaves.Low)},
            {9, new Note(Note.Keys.A, Note.Octaves.Low)},
            {10, new Note(Note.Keys.A, Note.Octaves.Low)},
            {11, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave -1
            {12, new Note(Note.Keys.C, Note.Octaves.Low)},
            {13, new Note(Note.Keys.C, Note.Octaves.Low)},
            {14, new Note(Note.Keys.D, Note.Octaves.Low)},
            {15, new Note(Note.Keys.D, Note.Octaves.Low)},
            {16, new Note(Note.Keys.E, Note.Octaves.Low)},
            {17, new Note(Note.Keys.F, Note.Octaves.Low)},
            {18, new Note(Note.Keys.F, Note.Octaves.Low)},
            {19, new Note(Note.Keys.G, Note.Octaves.Low)},
            {20, new Note(Note.Keys.G, Note.Octaves.Low)},
            {21, new Note(Note.Keys.A, Note.Octaves.Low)},
            {22, new Note(Note.Keys.A, Note.Octaves.Low)},
            {23, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave 0
            {24, new Note(Note.Keys.C, Note.Octaves.Low)},
            {25, new Note(Note.Keys.C, Note.Octaves.Low)},
            {26, new Note(Note.Keys.D, Note.Octaves.Low)},
            {27, new Note(Note.Keys.D, Note.Octaves.Low)},
            {28, new Note(Note.Keys.E, Note.Octaves.Low)},
            {29, new Note(Note.Keys.F, Note.Octaves.Low)},
            {30, new Note(Note.Keys.F, Note.Octaves.Low)},
            {31, new Note(Note.Keys.G, Note.Octaves.Low)},
            {32, new Note(Note.Keys.G, Note.Octaves.Low)},
            {33, new Note(Note.Keys.A, Note.Octaves.Low)},
            {34, new Note(Note.Keys.A, Note.Octaves.Low)},
            {35, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave 1
            {36, new Note(Note.Keys.C, Note.Octaves.Low)},
            {37, new Note(Note.Keys.C, Note.Octaves.Low)},
            {38, new Note(Note.Keys.D, Note.Octaves.Low)},
            {39, new Note(Note.Keys.D, Note.Octaves.Low)},
            {40, new Note(Note.Keys.E, Note.Octaves.Low)},
            {41, new Note(Note.Keys.F, Note.Octaves.Low)},
            {42, new Note(Note.Keys.F, Note.Octaves.Low)},
            {43, new Note(Note.Keys.G, Note.Octaves.Low)},
            {44, new Note(Note.Keys.G, Note.Octaves.Low)},
            {45, new Note(Note.Keys.A, Note.Octaves.Low)},
            {46, new Note(Note.Keys.A, Note.Octaves.Low)},
            {47, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave 2
            {48, new Note(Note.Keys.C, Note.Octaves.Low)},
            {49, new Note(Note.Keys.C, Note.Octaves.Low)},
            {50, new Note(Note.Keys.D, Note.Octaves.Low)},
            {51, new Note(Note.Keys.D, Note.Octaves.Low)},
            {52, new Note(Note.Keys.E, Note.Octaves.Low)},
            {53, new Note(Note.Keys.F, Note.Octaves.Low)},
            {54, new Note(Note.Keys.F, Note.Octaves.Low)},
            {55, new Note(Note.Keys.G, Note.Octaves.Low)},
            {56, new Note(Note.Keys.G, Note.Octaves.Low)},
            {57, new Note(Note.Keys.A, Note.Octaves.Low)},
            {58, new Note(Note.Keys.A, Note.Octaves.Low)},
            {59, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave 3
            {60, new Note(Note.Keys.C, Note.Octaves.Low)},
            {61, new Note(Note.Keys.C, Note.Octaves.Low)},
            {62, new Note(Note.Keys.D, Note.Octaves.Low)},
            {63, new Note(Note.Keys.D, Note.Octaves.Low)},
            {64, new Note(Note.Keys.E, Note.Octaves.Low)},
            {65, new Note(Note.Keys.F, Note.Octaves.Low)},
            {66, new Note(Note.Keys.F, Note.Octaves.Low)},
            {67, new Note(Note.Keys.G, Note.Octaves.Low)},
            {68, new Note(Note.Keys.G, Note.Octaves.Low)},
            {69, new Note(Note.Keys.A, Note.Octaves.Low)},
            {70, new Note(Note.Keys.A, Note.Octaves.Low)},
            {71, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave 4
            {72, new Note(Note.Keys.C, Note.Octaves.Low)},
            {73, new Note(Note.Keys.C, Note.Octaves.Low)},
            {74, new Note(Note.Keys.D, Note.Octaves.Low)},
            {75, new Note(Note.Keys.D, Note.Octaves.Low)},
            {76, new Note(Note.Keys.E, Note.Octaves.Low)},
            {77, new Note(Note.Keys.F, Note.Octaves.Low)},
            {78, new Note(Note.Keys.F, Note.Octaves.Low)},
            {79, new Note(Note.Keys.G, Note.Octaves.Low)},
            {80, new Note(Note.Keys.G, Note.Octaves.Low)},
            {81, new Note(Note.Keys.A, Note.Octaves.Low)},
            {82, new Note(Note.Keys.A, Note.Octaves.Low)},
            {83, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave 5
            {84, new Note(Note.Keys.C, Note.Octaves.Middle)},
            {85, new Note(Note.Keys.C, Note.Octaves.Middle)},
            {86, new Note(Note.Keys.D, Note.Octaves.Middle)},
            {87, new Note(Note.Keys.D, Note.Octaves.Middle)},
            {88, new Note(Note.Keys.E, Note.Octaves.Middle)},
            {89, new Note(Note.Keys.F, Note.Octaves.Middle)},
            {90, new Note(Note.Keys.F, Note.Octaves.Middle)},
            {91, new Note(Note.Keys.G, Note.Octaves.Middle)},
            {92, new Note(Note.Keys.G, Note.Octaves.Middle)},
            {93, new Note(Note.Keys.A, Note.Octaves.Middle)},
            {94, new Note(Note.Keys.A, Note.Octaves.Middle)},
            {95, new Note(Note.Keys.B, Note.Octaves.Middle)},

            // Octave 6
            {96, new Note(Note.Keys.C, Note.Octaves.High)},
            {97, new Note(Note.Keys.C, Note.Octaves.High)},
            {98, new Note(Note.Keys.D, Note.Octaves.High)},
            {99, new Note(Note.Keys.D, Note.Octaves.High)},
            {100, new Note(Note.Keys.E, Note.Octaves.High)},
            {101, new Note(Note.Keys.F, Note.Octaves.High)},
            {102, new Note(Note.Keys.F, Note.Octaves.High)},
            {103, new Note(Note.Keys.G, Note.Octaves.High)},
            {104, new Note(Note.Keys.G, Note.Octaves.High)},
            {105, new Note(Note.Keys.A, Note.Octaves.High)},
            {106, new Note(Note.Keys.A, Note.Octaves.High)},
            {107, new Note(Note.Keys.B, Note.Octaves.High)},

            // Octave 7
            {108, new Note(Note.Keys.C, Note.Octaves.Highest)},
            {109, new Note(Note.Keys.C, Note.Octaves.Highest)},
            {110, new Note(Note.Keys.D, Note.Octaves.Highest)},
            {111, new Note(Note.Keys.D, Note.Octaves.Highest)},
            {112, new Note(Note.Keys.E, Note.Octaves.Highest)},
            {113, new Note(Note.Keys.F, Note.Octaves.Highest)},
            {114, new Note(Note.Keys.F, Note.Octaves.Highest)},
            {115, new Note(Note.Keys.G, Note.Octaves.Highest)},
            {116, new Note(Note.Keys.G, Note.Octaves.Highest)},
            {117, new Note(Note.Keys.A, Note.Octaves.Highest)},
            {118, new Note(Note.Keys.A, Note.Octaves.Highest)},
            {119, new Note(Note.Keys.B, Note.Octaves.Highest)},

            // Octave 8
            {120, new Note(Note.Keys.C, Note.Octaves.Highest)},
            {121, new Note(Note.Keys.C, Note.Octaves.Highest)},
            {122, new Note(Note.Keys.D, Note.Octaves.Highest)},
            {123, new Note(Note.Keys.D, Note.Octaves.Highest)},
            {124, new Note(Note.Keys.E, Note.Octaves.Highest)},
            {125, new Note(Note.Keys.F, Note.Octaves.Highest)},
            {126, new Note(Note.Keys.F, Note.Octaves.Highest)},
            {127, new Note(Note.Keys.G, Note.Octaves.Highest)},
        };

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
                .OfType<NoteOnEvent>()
                .Where(MidiEvent.IsNoteOn)
                .GroupBy(@event => @event.AbsoluteTime)
                .OrderBy(group => group.Key)
                .Select(ToChordOffset)
                .ToList();

            return chords;
        }

        private static ChordOffset ToChordOffset(IGrouping<long, NoteOnEvent> midiEvents)
        {
            var chordDuration = int.MaxValue;

            /*     var notes = midiEvents
                .Select(ToMidiNote)
                .Select(midiNote =>
                {
                    chordDuration = midiNote.Duration < chordDuration ? midiNote.Duration : chordDuration;
                    return ToNote(midiNote);
                });
*/

            var notes = midiEvents
                .Select(@event =>
                {
                    chordDuration = @event.NoteLength < chordDuration ? @event.NoteLength : chordDuration;
                    return ToNote(@event);
                });

            var fraction = new Fraction(chordDuration, 1000);
            return new ChordOffset(new Chord(notes, fraction), new Beat(0));
        }

        /*    private static MidiNote ToMidiNote(NoteOnEvent note)
        {
            var match = Regex.Match(note.NoteName);

            return new MidiNote
            {
                Duration = TimeSpan.FromMilliseconds(note.NoteLength),
                Note = match.Groups[1].Value,
                Octave = int.Parse(match.Groups[2].Value)
            };
        }

        private static Note ToNote(MidiNote note)
        {
            var key = ParseKey(note);
            var octave = ParseOctave(note);
            return new Note(key, octave);
        }*/

        private static Note ToNote(NoteEvent note)
        {
            try
            {
                return Map[note.NoteNumber];
            }
            catch (Exception exception)
            {
                throw;
            }
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