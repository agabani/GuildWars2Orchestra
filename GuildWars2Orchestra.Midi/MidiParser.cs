using System.Collections.Generic;
using System.Linq;
using GuildWars2Orchestra.Domain;
using GuildWars2Orchestra.Domain.Values;
using NAudio.Midi;

namespace GuildWars2Orchestra.Midi
{
    public class MidiParser
    {
        public MusicSheet Parse(string midiPath)
        {
            var midi = new MidiFile(midiPath);

            var tempo = GetTempo(midi);
            var beatsPerMeasure = GetBeatsPerMeasure(midi);

            var metronomeMark = new MetronomeMark((int) tempo, beatsPerMeasure);

            return new MusicSheet(metronomeMark, ParseMelody(midi, beatsPerMeasure));
        }

        private static Fraction GetBeatsPerMeasure(MidiFile midi)
        {
            var timeSignatureEvent = midi.Events
                .SelectMany(x => x)
                .OfType<TimeSignatureEvent>()
                .OrderBy(@event => @event.AbsoluteTime)
                .Last();

            return new Fraction(1, timeSignatureEvent.Denominator);
        }

        private static double GetTempo(MidiFile midi)
        {
            return midi.Events
                .SelectMany(x => x)
                .OfType<TempoEvent>()
                .OrderBy(@event => @event.AbsoluteTime)
                .LastOrDefault()?.Tempo ?? 120;
        }

        private static IEnumerable<ChordOffset> ParseMelody(MidiFile midi, Fraction beatsPerMeasure)
        {
            return midi.Events
                .SelectMany(@event => @event)
                .OfType<NoteOnEvent>()
                .Where(MidiEvent.IsNoteOn)
                .Where(note => note.NoteNumber >= 48 && note.NoteNumber <= 84)
                .GroupBy(@event => @event.AbsoluteTime)
                .OrderBy(group => group.Key)
                .Select(x => ToChordOffset(x, beatsPerMeasure));
        }

        private static ChordOffset ToChordOffset(IGrouping<long, NoteOnEvent> midiEvents, Fraction beatsPerMeasure)
        {
            var chordLength = midiEvents.Min(x => x.NoteLength);

            var notes = midiEvents
                .Select(@event => MidiMapper.ToNote[@event.NoteNumber]);

            var fraction = new Fraction(chordLength, 1000);

            var absoluteBeat = midiEvents.Key/(1000*beatsPerMeasure.Nominator/(beatsPerMeasure.Denominator*2));

            return new ChordOffset(new Chord(notes, fraction), new Beat(absoluteBeat));
        }
    }
}