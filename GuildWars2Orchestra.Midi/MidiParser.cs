using System.Collections.Generic;
using System.IO;
using System.Linq;
using GuildWars2Orchestra.Domain;
using GuildWars2Orchestra.Domain.Values;
using NAudio.Midi;

namespace GuildWars2Orchestra.Midi
{
    public class MidiParser
    {
        private const int MidiNoteC3 = 48;
        private const int MidiNoteC6 = 84;
        private const int DefaultTempo = 120;

        public MusicSheet Parse(string midiPath)
        {
            var midi = new MidiFile(midiPath);

            var metronomeMark = new MetronomeMark(Metronome(midi), BeatsPerMeasure(midi));

            return new MusicSheet(Path.GetFileNameWithoutExtension(midiPath), metronomeMark, Melody(midi, metronomeMark));
        }

        private static int Metronome(MidiFile midi)
        {
            return (int) (midi.Events
                .SelectMany(x => x)
                .OfType<TempoEvent>()
                .OrderBy(@event => @event.AbsoluteTime)
                .LastOrDefault()?.Tempo ?? DefaultTempo);
        }

        private static Fraction BeatsPerMeasure(MidiFile midi)
        {
            var timeSignatureEvent = midi.Events
                .SelectMany(x => x)
                .OfType<TimeSignatureEvent>()
                .OrderBy(@event => @event.AbsoluteTime)
                .Last();

            return new Fraction(timeSignatureEvent.Numerator, timeSignatureEvent.Denominator);
        }

        private static IEnumerable<ChordOffset> Melody(MidiFile midi, MetronomeMark metronomeMark)
        {
            return midi.Events
                .SelectMany(@event => @event)
                .OfType<NoteOnEvent>()
                .Where(MidiEvent.IsNoteOn)
                .Where(note => note.NoteNumber >= MidiNoteC3 && note.NoteNumber <= MidiNoteC6)
                .GroupBy(@event => @event.AbsoluteTime)
                .OrderBy(group => group.Key)
                .Select(x => ChordOffset(x, metronomeMark));
        }

        private static ChordOffset ChordOffset(IGrouping<long, NoteOnEvent> midiEvents, MetronomeMark metronomeMark)
        {
            return new ChordOffset(Chord(midiEvents), Offset(midiEvents.Key, metronomeMark));
        }

        private static Chord Chord(IGrouping<long, NoteOnEvent> midiEvents)
        {
            return new Chord(Notes(midiEvents), Length(midiEvents));
        }

        private static IEnumerable<Note> Notes(IEnumerable<NoteOnEvent> midiEvents)
        {
            return midiEvents.Select(@event => MidiMapper.ToNote[@event.NoteNumber]);
        }

        private static Fraction Length(IEnumerable<NoteOnEvent> midiEvents)
        {
            return new Fraction(midiEvents.Min(x => x.NoteLength), 1000);
        }

        private static Beat Offset(long absoluteTime, MetronomeMark metronomeMark)
        {
            decimal
                time = absoluteTime,
                wholeNoteLength = (decimal) metronomeMark.WholeNoteLength.TotalMilliseconds;

            var beatsPerMeasure = metronomeMark.BeatsPerMeasure;

            return new Beat(time/wholeNoteLength/beatsPerMeasure);
        }
    }
}