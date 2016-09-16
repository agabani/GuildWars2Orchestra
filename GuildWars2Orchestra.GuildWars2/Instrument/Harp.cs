using System;
using System.Collections.Generic;
using System.Threading;
using GuildWars2Orchestra.Domain.Values;
using GuildWars2Orchestra.GuildWars2.Controls;

namespace GuildWars2Orchestra.GuildWars2.Instrument
{
    public class Harp
    {
        private static readonly TimeSpan NoteTimeout = TimeSpan.FromMilliseconds(5);
        private static readonly TimeSpan OctaveTimeout = TimeSpan.FromTicks(500);

        private static readonly Dictionary<HarpNote.Keys, GuildWarsControls> NoteMap = new Dictionary<HarpNote.Keys, GuildWarsControls>
        {
            {HarpNote.Keys.Note1, GuildWarsControls.WeaponSkill1},
            {HarpNote.Keys.Note2, GuildWarsControls.WeaponSkill2},
            {HarpNote.Keys.Note3, GuildWarsControls.WeaponSkill3},
            {HarpNote.Keys.Note4, GuildWarsControls.WeaponSkill4},
            {HarpNote.Keys.Note5, GuildWarsControls.WeaponSkill5},
            {HarpNote.Keys.Note6, GuildWarsControls.HealingSkill},
            {HarpNote.Keys.Note7, GuildWarsControls.UtilitySkill1},
            {HarpNote.Keys.Note8, GuildWarsControls.UtilitySkill2}
        };

        private readonly IKeyboard _keyboard;

        private HarpNote.Octaves _currentOctave = HarpNote.Octaves.Middle;

        public Harp(IKeyboard keyboard)
        {
            _keyboard = keyboard;
        }

        public void PlayNote(Note note)
        {
            var harpNote = HarpNote.From(note);

            if (RequiresAction(harpNote))
            {
                harpNote = OptimizeNote(harpNote);
                PressNote(NoteMap[harpNote.Key]);
            }
        }

        public void GoToOctave(Note note)
        {
            var harpNote = HarpNote.From(note);

            if (RequiresAction(harpNote))
            {
                harpNote = OptimizeNote(harpNote);

                while (_currentOctave != harpNote.Octave)
                {
                    if (_currentOctave < harpNote.Octave)
                    {
                        IncreaseOctave();
                    }
                    else
                    {
                        DecreaseOctave();
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

        private void IncreaseOctave()
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

            _keyboard.Press(GuildWarsControls.EliteSkill);
            _keyboard.Release(GuildWarsControls.EliteSkill);

            Thread.Sleep(OctaveTimeout);
        }

        private void DecreaseOctave()
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

            _keyboard.Press(GuildWarsControls.UtilitySkill3);
            _keyboard.Release(GuildWarsControls.UtilitySkill3);

            Thread.Sleep(OctaveTimeout);
        }

        private void PressNote(GuildWarsControls key)
        {
            _keyboard.Press(key);
            _keyboard.Release(key);

            Thread.Sleep(NoteTimeout);
        }
    }
}