# Hackfest

Hackfest is a geeky game about hacking. It uses Virtual Box to simulate in-game computers to simulate an uber realistic experience!

## Compatibility

Hackfest should run on Windows, Linux, however Windows is untested.

## Dependencies

- Vagrant [Download](https://www.vagrantup.com/downloads.html)
- Virtual Box [Download](https://www.virtualbox.org/wiki/Downloads) (You're looking for <Your OS> host)
- Godot [Download](https://godotengine.org/download/linux) (You need Mono version)
- C# Mono [Download](https://www.mono-project.com/download/stable) 

## Instructions ( Arch Linux )

* Instructions for other platforms / distros should be the same, except for 1. step

1. Install packages `mono` `virtualbox` `virtualbox-host-modules-arch` `vagrant` `godot-mono-bit`<sup>AUR</sup>
2. Clone repo `git clone git@github.com:meowxiik/hackfest.git`
3. Open Godot, open the cloned project
4. Navigate to `scenes/Demo` in FileSystem explorer
5. Run the scene using an icon in upper right corner

## Discord

If you need any help or have any questions pop up to this support Discord channel: [Invite link](https://discord.gg/BZBCH45) 

## Diagnostics

If you encounter any problems it is a good idea to enable diagnostics. You can enable them by going to `Project/Project Settings` and set variable `Diagnostics` in `Global` scope to true.

Diagnostics will attempt to start Vagrant Virtual Box virtual machine and create a simple container. It will report success. If you need help please run the diagnostics first and then open an issue.

There are sets of tests, `DIAGNOSTICS A` and `DIAGNOSTICS B`. `A` runs from GDScript while `B` runs from Mono. If you see just `A`, there is a good chance the Mono doesn't launch right.
