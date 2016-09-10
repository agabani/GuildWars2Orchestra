using System.Collections.Generic;
using GuildWars2Orchestra.Audio.Sound;
using GuildWars2Orchestra.GuildWars2.Controls;
using GuildWars2Orchestra.GuildWars2.Instrument;
using NAudio.Vorbis;

namespace GuildWars2Orchestra.Audio.Repositories
{
    public class HarpSoundRepository
    {
        private static readonly Dictionary<string, string> Map = new Dictionary<string, string>
        {
            {$"{GuildWarsKeyboard.Controls.WeaponSkill1}{HarpNote.Octaves.Low}", "C3"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill2}{HarpNote.Octaves.Low}", "D3"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill3}{HarpNote.Octaves.Low}", "E3"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill4}{HarpNote.Octaves.Low}", "F3"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill5}{HarpNote.Octaves.Low}", "G3"},
            {$"{GuildWarsKeyboard.Controls.HealingSkill}{HarpNote.Octaves.Low}", "A3"},
            {$"{GuildWarsKeyboard.Controls.UtilitySkill1}{HarpNote.Octaves.Low}", "B3"},
            {$"{GuildWarsKeyboard.Controls.UtilitySkill2}{HarpNote.Octaves.Low}", "C4"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill1}{HarpNote.Octaves.Middle}", "C4"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill2}{HarpNote.Octaves.Middle}", "D4"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill3}{HarpNote.Octaves.Middle}", "E4"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill4}{HarpNote.Octaves.Middle}", "F4"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill5}{HarpNote.Octaves.Middle}", "G4"},
            {$"{GuildWarsKeyboard.Controls.HealingSkill}{HarpNote.Octaves.Middle}", "A4"},
            {$"{GuildWarsKeyboard.Controls.UtilitySkill1}{HarpNote.Octaves.Middle}", "B4"},
            {$"{GuildWarsKeyboard.Controls.UtilitySkill2}{HarpNote.Octaves.Middle}", "C5"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill1}{HarpNote.Octaves.High}", "C5"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill2}{HarpNote.Octaves.High}", "D5"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill3}{HarpNote.Octaves.High}", "E5"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill4}{HarpNote.Octaves.High}", "F5"},
            {$"{GuildWarsKeyboard.Controls.WeaponSkill5}{HarpNote.Octaves.High}", "G5"},
            {$"{GuildWarsKeyboard.Controls.HealingSkill}{HarpNote.Octaves.High}", "A5"},
            {$"{GuildWarsKeyboard.Controls.UtilitySkill1}{HarpNote.Octaves.High}", "B5"},
            {$"{GuildWarsKeyboard.Controls.UtilitySkill2}{HarpNote.Octaves.High}", "C6"}
        };

        private static readonly Dictionary<string, CachedSound> Sound = new Dictionary<string, CachedSound>
        {
            {"C3", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\C3.ogg")))},
            {"D3", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\D3.ogg")))},
            {"E3", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\E3.ogg")))},
            {"F3", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\F3.ogg")))},
            {"G3", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\G3.ogg")))},
            {"A3", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\A3.ogg")))},
            {"B3", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\B3.ogg")))},
            {"C4", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\C4.ogg")))},
            {"D4", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\D4.ogg")))},
            {"E4", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\E4.ogg")))},
            {"F4", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\F4.ogg")))},
            {"G4", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\G4.ogg")))},
            {"A4", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\A4.ogg")))},
            {"B4", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\B4.ogg")))},
            {"C5", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\C5.ogg")))},
            {"D5", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\D5.ogg")))},
            {"E5", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\E5.ogg")))},
            {"F5", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\F5.ogg")))},
            {"G5", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\G5.ogg")))},
            {"A5", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\A5.ogg")))},
            {"B5", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\B5.ogg")))},
            {"C6", new CachedSound(new AutoDisposeFileReader(new VorbisWaveReader(@"Resources\Harp\C6.ogg")))}
        };

        public CachedSound Get(string id)
        {
            return Sound[id];
        }

        public CachedSound Get(GuildWarsKeyboard.Controls key, HarpNote.Octaves octave)
        {
            return Sound[Map[$"{key}{octave}"]];
        }
    }
}