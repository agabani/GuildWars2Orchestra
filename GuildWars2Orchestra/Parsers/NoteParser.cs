using System;
using GuildWars2Orchestra.Domain.Values;

namespace GuildWars2Orchestra.Parsers
{
    public class NoteParser
    {
        public Note Parse(string text)
        {
            var key = ParseKey(text);
            var octave = ParseOctave(text);

            return new Note(key, octave);
        }

        private static Note.Keys ParseKey(string text)
        {
            Note.Keys key;
            switch (text)
            {
                case "Z,":
                case "Z":
                case "z":
                case "z'":
                    key = Note.Keys.None;
                    break;
                case "C,":
                case "C":
                case "c":
                case "c'":
                    key = Note.Keys.C;
                    break;
                case "D,":
                case "D":
                case "d":
                    key = Note.Keys.D;
                    break;
                case "E,":
                case "E":
                case "e":
                    key = Note.Keys.E;
                    break;
                case "F,":
                case "F":
                case "f":
                    key = Note.Keys.F;
                    break;
                case "G,":
                case "G":
                case "g":
                    key = Note.Keys.G;
                    break;
                case "A,":
                case "A":
                case "a":
                    key = Note.Keys.A;
                    break;
                case "B,":
                case "B":
                case "b":
                    key = Note.Keys.B;
                    break;
                default:
                    throw new NotSupportedException(text);
            }
            return key;
        }

        private static Note.Octaves ParseOctave(string text)
        {
            Note.Octaves octave;
            switch (text)
            {
                case "Z,":
                case "Z":
                case "z":
                case "z'":
                    octave = Note.Octaves.None;
                    break;
                case "C,":
                case "D,":
                case "E,":
                case "F,":
                case "G,":
                case "A,":
                case "B,":
                    octave = Note.Octaves.Low;
                    break;
                case "C":
                case "D":
                case "E":
                case "F":
                case "G":
                case "A":
                case "B":
                    octave = Note.Octaves.Middle;
                    break;
                case "c":
                case "d":
                case "e":
                case "f":
                case "g":
                case "a":
                case "b":
                    octave = Note.Octaves.High;
                    break;
                case "c'":
                    octave = Note.Octaves.Highest;
                    break;
                default:
                    throw new NotSupportedException(text);
            }
            return octave;
        }
    }
}