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

        private static readonly IReadOnlyDictionary<int, Note> Map = new Dictionary<int, Note>
        {
            // Octave -2
            {000, new Note(Note.Keys.C, Note.Octaves.Low)},
            {001, new Note(Note.Keys.C, Note.Octaves.Low)},
            {002, new Note(Note.Keys.D, Note.Octaves.Low)},
            {003, new Note(Note.Keys.D, Note.Octaves.Low)},
            {004, new Note(Note.Keys.E, Note.Octaves.Low)},
            {005, new Note(Note.Keys.F, Note.Octaves.Low)},
            {006, new Note(Note.Keys.F, Note.Octaves.Low)},
            {007, new Note(Note.Keys.G, Note.Octaves.Low)},
            {008, new Note(Note.Keys.G, Note.Octaves.Low)},
            {009, new Note(Note.Keys.A, Note.Octaves.Low)},
            {010, new Note(Note.Keys.A, Note.Octaves.Low)},
            {011, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave -1
            {012, new Note(Note.Keys.C, Note.Octaves.Low)},
            {013, new Note(Note.Keys.C, Note.Octaves.Low)},
            {014, new Note(Note.Keys.D, Note.Octaves.Low)},
            {015, new Note(Note.Keys.D, Note.Octaves.Low)},
            {016, new Note(Note.Keys.E, Note.Octaves.Low)},
            {017, new Note(Note.Keys.F, Note.Octaves.Low)},
            {018, new Note(Note.Keys.F, Note.Octaves.Low)},
            {019, new Note(Note.Keys.G, Note.Octaves.Low)},
            {020, new Note(Note.Keys.G, Note.Octaves.Low)},
            {021, new Note(Note.Keys.A, Note.Octaves.Low)},
            {022, new Note(Note.Keys.A, Note.Octaves.Low)},
            {023, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave 0
            {024, new Note(Note.Keys.C, Note.Octaves.Low)},
            {025, new Note(Note.Keys.C, Note.Octaves.Low)},
            {0026, new Note(Note.Keys.D, Note.Octaves.Low)},
            {027, new Note(Note.Keys.D, Note.Octaves.Low)},
            {028, new Note(Note.Keys.E, Note.Octaves.Low)},
            {029, new Note(Note.Keys.F, Note.Octaves.Low)},
            {030, new Note(Note.Keys.F, Note.Octaves.Low)},
            {031, new Note(Note.Keys.G, Note.Octaves.Low)},
            {032, new Note(Note.Keys.G, Note.Octaves.Low)},
            {033, new Note(Note.Keys.A, Note.Octaves.Low)},
            {034, new Note(Note.Keys.A, Note.Octaves.Low)},
            {035, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave 1
            {036, new Note(Note.Keys.C, Note.Octaves.Low)},
            {037, new Note(Note.Keys.C, Note.Octaves.Low)},
            {038, new Note(Note.Keys.D, Note.Octaves.Low)},
            {039, new Note(Note.Keys.D, Note.Octaves.Low)},
            {040, new Note(Note.Keys.E, Note.Octaves.Low)},
            {041, new Note(Note.Keys.F, Note.Octaves.Low)},
            {042, new Note(Note.Keys.F, Note.Octaves.Low)},
            {043, new Note(Note.Keys.G, Note.Octaves.Low)},
            {044, new Note(Note.Keys.G, Note.Octaves.Low)},
            {045, new Note(Note.Keys.A, Note.Octaves.Low)},
            {046, new Note(Note.Keys.A, Note.Octaves.Low)},
            {047, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave 2
            {048, new Note(Note.Keys.C, Note.Octaves.Low)},
            {049, new Note(Note.Keys.C, Note.Octaves.Low)},
            {050, new Note(Note.Keys.D, Note.Octaves.Low)},
            {051, new Note(Note.Keys.D, Note.Octaves.Low)},
            {052, new Note(Note.Keys.E, Note.Octaves.Low)},
            {053, new Note(Note.Keys.F, Note.Octaves.Low)},
            {054, new Note(Note.Keys.F, Note.Octaves.Low)},
            {055, new Note(Note.Keys.G, Note.Octaves.Low)},
            {056, new Note(Note.Keys.G, Note.Octaves.Low)},
            {057, new Note(Note.Keys.A, Note.Octaves.Low)},
            {058, new Note(Note.Keys.A, Note.Octaves.Low)},
            {059, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave 3
            {060, new Note(Note.Keys.C, Note.Octaves.Low)},
            {061, new Note(Note.Keys.C, Note.Octaves.Low)},
            {062, new Note(Note.Keys.D, Note.Octaves.Low)},
            {063, new Note(Note.Keys.D, Note.Octaves.Low)},
            {064, new Note(Note.Keys.E, Note.Octaves.Low)},
            {065, new Note(Note.Keys.F, Note.Octaves.Low)},
            {066, new Note(Note.Keys.F, Note.Octaves.Low)},
            {067, new Note(Note.Keys.G, Note.Octaves.Low)},
            {068, new Note(Note.Keys.G, Note.Octaves.Low)},
            {069, new Note(Note.Keys.A, Note.Octaves.Low)},
            {070, new Note(Note.Keys.A, Note.Octaves.Low)},
            {071, new Note(Note.Keys.B, Note.Octaves.Low)},

            // Octave 4
            {072, new Note(Note.Keys.C, Note.Octaves.Middle)},
            {073, new Note(Note.Keys.C, Note.Octaves.Middle)},
            {074, new Note(Note.Keys.D, Note.Octaves.Middle)},
            {075, new Note(Note.Keys.D, Note.Octaves.Middle)},
            {076, new Note(Note.Keys.E, Note.Octaves.Middle)},
            {077, new Note(Note.Keys.F, Note.Octaves.Middle)},
            {078, new Note(Note.Keys.F, Note.Octaves.Middle)},
            {079, new Note(Note.Keys.G, Note.Octaves.Middle)},
            {080, new Note(Note.Keys.G, Note.Octaves.Middle)},
            {081, new Note(Note.Keys.A, Note.Octaves.Middle)},
            {082, new Note(Note.Keys.A, Note.Octaves.Middle)},
            {083, new Note(Note.Keys.B, Note.Octaves.Middle)},

            // Octave 5
            {084, new Note(Note.Keys.C, Note.Octaves.High)},
            {085, new Note(Note.Keys.C, Note.Octaves.Middle)},
            {086, new Note(Note.Keys.D, Note.Octaves.High)},
            {087, new Note(Note.Keys.D, Note.Octaves.High)},
            {088, new Note(Note.Keys.E, Note.Octaves.High)},
            {089, new Note(Note.Keys.F, Note.Octaves.High)},
            {090, new Note(Note.Keys.F, Note.Octaves.High)},
            {091, new Note(Note.Keys.G, Note.Octaves.High)},
            {092, new Note(Note.Keys.G, Note.Octaves.High)},
            {093, new Note(Note.Keys.A, Note.Octaves.High)},
            {094, new Note(Note.Keys.A, Note.Octaves.High)},
            {095, new Note(Note.Keys.B, Note.Octaves.High)},

            // Octave 6
            {096, new Note(Note.Keys.C, Note.Octaves.Highest)},
            {097, new Note(Note.Keys.C, Note.Octaves.Highest)},
            {098, new Note(Note.Keys.D, Note.Octaves.Highest)},
            {099, new Note(Note.Keys.D, Note.Octaves.Highest)},
            {100, new Note(Note.Keys.E, Note.Octaves.Highest)},
            {101, new Note(Note.Keys.F, Note.Octaves.Highest)},
            {102, new Note(Note.Keys.F, Note.Octaves.Highest)},
            {103, new Note(Note.Keys.G, Note.Octaves.Highest)},
            {104, new Note(Note.Keys.G, Note.Octaves.Highest)},
            {105, new Note(Note.Keys.A, Note.Octaves.Highest)},
            {106, new Note(Note.Keys.A, Note.Octaves.Highest)},
            {107, new Note(Note.Keys.B, Note.Octaves.Highest)},

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
            {127, new Note(Note.Keys.G, Note.Octaves.Highest)}
        };

        public MusicSheet Parse(string midiPath)
        {
            var midi = new MidiFile(midiPath);
            var musicSheet = new MusicSheet(ParseMetronomeMark(midi), ParseMelody(midi));
            return musicSheet;
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

            var notes = midiEvents
                .Select(@event =>
                {
                    chordDuration = @event.NoteLength < chordDuration ? @event.NoteLength : chordDuration;
                    return ToNote(@event);
                }).ToList();

            var fraction = new Fraction(chordDuration, 1000);
            return new ChordOffset(new Chord(notes, fraction), new Beat(0));
        }

        private static Note ToNote(NoteEvent note)
        {
            return Map[note.NoteNumber];
        }
    }
}