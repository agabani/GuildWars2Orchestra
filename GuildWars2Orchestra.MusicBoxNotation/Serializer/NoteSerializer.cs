using System;
using GuildWars2Orchestra.Domain.Values;

namespace GuildWars2Orchestra.MusicBoxNotation.Serializer
{
    public class NoteSerializer
    {
        public string Serialize(Note note)
        {
            // pause
            if (note.Key == Note.Keys.None)
                return "z";

            // low octave
            if (note.Key == Note.Keys.C && note.Octave == Note.Octaves.Low)
                return "C,";

            if (note.Key == Note.Keys.D && note.Octave == Note.Octaves.Low)
                return "D,";

            if (note.Key == Note.Keys.E && note.Octave == Note.Octaves.Low)
                return "E,";

            if (note.Key == Note.Keys.F && note.Octave == Note.Octaves.Low)
                return "F,";

            if (note.Key == Note.Keys.G && note.Octave == Note.Octaves.Low)
                return "G,";

            if (note.Key == Note.Keys.A && note.Octave == Note.Octaves.Low)
                return "A,";

            if (note.Key == Note.Keys.B && note.Octave == Note.Octaves.Low)
                return "B,";

            // middle octave
            if (note.Key == Note.Keys.C && note.Octave == Note.Octaves.Middle)
                return "C";

            if (note.Key == Note.Keys.D && note.Octave == Note.Octaves.Middle)
                return "D";

            if (note.Key == Note.Keys.E && note.Octave == Note.Octaves.Middle)
                return "E";

            if (note.Key == Note.Keys.F && note.Octave == Note.Octaves.Middle)
                return "F";

            if (note.Key == Note.Keys.G && note.Octave == Note.Octaves.Middle)
                return "G";

            if (note.Key == Note.Keys.A && note.Octave == Note.Octaves.Middle)
                return "A";

            if (note.Key == Note.Keys.B && note.Octave == Note.Octaves.Middle)
                return "B";

            // high octave
            if (note.Key == Note.Keys.C && note.Octave == Note.Octaves.High)
                return "c";

            if (note.Key == Note.Keys.D && note.Octave == Note.Octaves.High)
                return "d";

            if (note.Key == Note.Keys.E && note.Octave == Note.Octaves.High)
                return "e";

            if (note.Key == Note.Keys.F && note.Octave == Note.Octaves.High)
                return "f";

            if (note.Key == Note.Keys.G && note.Octave == Note.Octaves.High)
                return "g";

            if (note.Key == Note.Keys.A && note.Octave == Note.Octaves.High)
                return "a";

            if (note.Key == Note.Keys.B && note.Octave == Note.Octaves.High)
                return "b";

            // highest octave
            if (note.Key == Note.Keys.C && note.Octave == Note.Octaves.Highest)
                return "c'";

            throw new NotSupportedException();
        }
    }
}