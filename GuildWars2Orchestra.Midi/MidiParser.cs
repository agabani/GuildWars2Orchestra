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
            return midi.Events[0]
                //.SelectMany(@event => @event)
                .OfType<NoteOnEvent>()
                .Where(MidiEvent.IsNoteOn)
                .Where(note => note.NoteNumber >= 60 && note.NoteNumber <= 96)
                .GroupBy(@event => @event.AbsoluteTime)
                .OrderBy(group => group.Key)
                .Select(ToChordOffset);
        }

        private static ChordOffset ToChordOffset(IGrouping<long, NoteOnEvent> midiEvents)
        {
            var chordLength = midiEvents.Min(x => x.NoteLength);

            var notes = midiEvents
                .Select(@event => MidiMapper.ToNote[@event.NoteNumber]);

            var fraction = new Fraction(chordLength, 1000);

            return new ChordOffset(new Chord(notes, fraction), new Beat(0));
        }
    }
}