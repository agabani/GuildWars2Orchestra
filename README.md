# Guild Wars 2 Orchestra

> A program to play music sheets using instruments inside of Guild Wars 2

## Supported Instruments

| Instrument | Supported |
|------------|-----------|
| Bell       | No        |
| Flute      | No        |
| Horn       | No        |
| Harp       | Yes       |
| Lute       | No        |
| Bell2      | No        |
| Bass       | No        |

## Features

* Play music sheets from [http://gw2mb.com](http://gw2mb.com) [v 0.0.0.1]
  * Only "Music Box Notation" is supported.
* Option to choose between "favor chords" or "favor notes" [v 0.0.0.3]
  * "favor chords": trys to play chords as smoothly as possible.
  * "favor notes": trys to play every note.
* Chords played using their "absolute time" instead of using "note queueing and sleep" [v 0.0.0.4]
  *  This means chords can to be played slowly without it reducing the tempo as a side effect, allowing for more unplayable songs to become playable.
  *  “note queueing and sleep” is the approach AutoHotkey uses, eg: `Sleep, 400`.

## Upcoming Features

* MIDI file support
  * Use a midi file (.mid) as an input to listen to it in Guild Wars 2
* Network Support
  * Allow for multiple instruments to be played in time with each other
* Keyboard inputs direct to Guild Wars 2
  * Allows users to remain in control of their keyboard inputs instead of forcing foreground window focus to Guild Wars 2

## Usage

1. Start Guild Wars 2
2. Ensure key bindings 1,2,3,4,5,6,7,8,9,0 are mapped to the default Guild Wars 2 key binding (skill 1, 2, 3, 4 5, etc...)
3. Ensure the harps current octave is "Middle Octave"
4. Using "command prompt" or "powershell", navigate to the directory holding `GuildWars2Orchestra.exe`
5. Using "command prompt" or "powershell", type the following command:
`GuildWars2Orchestra.exe "TestData\Final Fantasy XIII 2 - A Wish.xml"`

## Music Sheets

The music sheets provided as test data in this repository originated from [http://gw2mb.com/archive.php](http://gw2mb.com/archive.php). Guild Wars 2 Orchestra will make its best attempt to play the music sheets faithfully. However, Guild Wars 2 has a limit on speed; some notes may be missed if the score is too fast.

Example YouTube video: [Guild Wars 2 Orchestra - Guilty Crown - My Dearest ](https://www.youtube.com/watch?v=hgCDhFD71ZI)

## Minimum Requirements

- Guild Wars 2 (32 bit / 64 bit client)
- .NET 4.5
- Windows 7+

## Notes

This application simulates keyboard key presses using Windows API, therefore requires Guild Wars 2 to be the foreground window for the entire duration of the music sheet.

## Planned Work

This program is a slow work in progress, current efforts is to get any music played to be as faithful to the music sheet it is currently playing using the harp as a musical instrument. Once that it is completed to a satisfactory level, then support for other instruments will follow suit.