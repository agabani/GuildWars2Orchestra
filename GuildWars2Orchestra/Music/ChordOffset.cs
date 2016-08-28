using GuildWars2Orchestra.Values;

namespace GuildWars2Orchestra.Music
{
    public class ChordOffset
    {
        public ChordOffset(Chord chord, Beat offest)
        {
            Chord = chord;
            Offest = offest;
        }

        public Chord Chord { get; private set; }
        public Beat Offest { get; private set; }
    }
}