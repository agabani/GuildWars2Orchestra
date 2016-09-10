using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.GuildWars2.Controls;

namespace GuildWars2Orchestra.GuildWars2.Instrument
{
    public class Harp
    {
        private static readonly TimeSpan NoteTimeout = TimeSpan.FromMilliseconds(5);
        private static readonly TimeSpan OctaveTimeout = TimeSpan.FromTicks(500);

        private static readonly Dictionary<HarpNote.Keys, GuildWarsKeyboard.GuildWarsSkill> NoteMap = new Dictionary<HarpNote.Keys, GuildWarsKeyboard.GuildWarsSkill>
        {
            {HarpNote.Keys.Note1, GuildWarsKeyboard.GuildWarsSkill.Skill1},
            {HarpNote.Keys.Note2, GuildWarsKeyboard.GuildWarsSkill.Skill2},
            {HarpNote.Keys.Note3, GuildWarsKeyboard.GuildWarsSkill.Skill3},
            {HarpNote.Keys.Note4, GuildWarsKeyboard.GuildWarsSkill.Skill4},
            {HarpNote.Keys.Note5, GuildWarsKeyboard.GuildWarsSkill.Skill5},
            {HarpNote.Keys.Note6, GuildWarsKeyboard.GuildWarsSkill.Skill6},
            {HarpNote.Keys.Note7, GuildWarsKeyboard.GuildWarsSkill.Skill7},
            {HarpNote.Keys.Note8, GuildWarsKeyboard.GuildWarsSkill.Skill8}
        };

        private readonly IKeyboard _keyboard;

        private HarpNote.Octaves _currentOctave = HarpNote.Octaves.Middle;

        public Harp(IKeyboard keyboard)
        {
            _keyboard = keyboard;
        }

        public async Task PlayNote(Note note)
        {
            var harpNote = HarpNote.From(note);

            if (RequiresAction(harpNote))
            {
                harpNote = OptimizeNote(harpNote);
                await PressNote(NoteMap[harpNote.Key]);
            }
        }

        public async Task GoToOctave(Note note)
        {
            var harpNote = HarpNote.From(note);

            if (RequiresAction(harpNote))
            {
                harpNote = OptimizeNote(harpNote);

                while (_currentOctave != harpNote.Octave)
                {
                    if (_currentOctave < harpNote.Octave)
                    {
                        await IncreaseOctave();
                    }
                    else
                    {
                        await DecreaseOctave();
                    }
                }
            }
        }

        private static bool RequiresAction(HarpNote harpNote)
        {
            return harpNote.Key != HarpNote.Keys.None;
        }

        private HarpNote OptimizeNote(HarpNote note)
        {
            if (note.Equals(new HarpNote(HarpNote.Keys.Note1, HarpNote.Octaves.Middle)) && _currentOctave == HarpNote.Octaves.Low)
            {
                note = new HarpNote(HarpNote.Keys.Note8, HarpNote.Octaves.Low);
            }
            else if (note.Equals(new HarpNote(HarpNote.Keys.Note1, HarpNote.Octaves.High)) && _currentOctave == HarpNote.Octaves.Middle)
            {
                note = new HarpNote(HarpNote.Keys.Note8, HarpNote.Octaves.Middle);
            }
            return note;
        }

        private async Task IncreaseOctave()
        {
            switch (_currentOctave)
            {
                case HarpNote.Octaves.Low:
                    _currentOctave = HarpNote.Octaves.Middle;
                    break;
                case HarpNote.Octaves.Middle:
                    _currentOctave = HarpNote.Octaves.High;
                    break;
                case HarpNote.Octaves.High:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _keyboard.Press(GuildWarsKeyboard.GuildWarsSkill.Skill0);
            _keyboard.Release(GuildWarsKeyboard.GuildWarsSkill.Skill0);

            await Task.Delay(OctaveTimeout);
        }

        private async Task DecreaseOctave()
        {
            switch (_currentOctave)
            {
                case HarpNote.Octaves.Low:
                    break;
                case HarpNote.Octaves.Middle:
                    _currentOctave = HarpNote.Octaves.Low;
                    break;
                case HarpNote.Octaves.High:
                    _currentOctave = HarpNote.Octaves.Middle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _keyboard.Press(GuildWarsKeyboard.GuildWarsSkill.Skill9);
            _keyboard.Release(GuildWarsKeyboard.GuildWarsSkill.Skill9);

            await Task.Delay(OctaveTimeout);
        }

        private async Task PressNote(GuildWarsKeyboard.GuildWarsSkill key)
        {
            _keyboard.Press(key);
            _keyboard.Release(key);

            await Task.Delay(NoteTimeout);
        }
    }
}