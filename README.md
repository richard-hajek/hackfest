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
2. Clone repo `git clone --recursive git@github.com:meowxiik/hackfest.git`
3. Open Godot, open the cloned project
4. Navigate to `scenes/Demo` in FileSystem explorer
5. Run the scene using an icon in upper right corner

## Discord

If you need any help or have any questions pop up to this support Discord channel: [Invite link](https://discord.gg/3m27P4T) 

## Diagnostics

If you encounter any problems it is a good idea to enable diagnostics. You can enable them by going to `Project/Project Settings` and set variable `Diagnostics` in `Global` scope to true.

Diagnostics will attempt to start Vagrant Virtual Box virtual machine and create a simple container. It will report success. If you need help please run the diagnostics first and then open an issue.

There are sets of tests, `DIAGNOSTICS A` and `DIAGNOSTICS B`. `A` runs from GDScript while `B` runs from Mono. If you see just `A`, there is a good chance the Mono doesn't launch right.

## Screenshots

![Screenshot of hallway](https://i.imgur.com/ujAetXO.png)

![Screenshot of terminal](https://i.imgur.com/yX2SClH.png)

## Controls

WASD - move

Left click - open terminal

SHIFT+ESC - close terminal

## Spoilers! - Demo Level Solutions

Expand details for solutions

<details>
  
  For all levels, you need to click left on the little panel on the right to open the door controller command line.
  
  Exit the terminal with SHIFT+ESC
  
  Level 1, "Door is a device; Key is the number 1": Door is located in `/dev/by_id/door/in` You need to  `echo 1 > /dev/by_id/door/in`
  
  Level 2, "SSH Port is?": You need to `echo 22 > /dev/by_id/door/in`
  
  Level 3, "Find alpha": Key is hidden in `/bin/key_alpha`, it is 78963, so `echo 78963 > /dev/by_id/door/in`
  
  Level 4, "Double door conundrum": There are two doors, which open and close in for opposite signals. On `echo 1 > /dev/by_id/double_door/in` door 1 closes and door 2 opens. On `echo 0` the opposite. You need to `echo 0 > /dev/by_id/double_door/in && sleep 5 && echo 1 > /dev/by_id/double_door/in`. Then go stand between the doors.
  
  Level 5, "The Door will tell": You need to output `cat /dev/by_id/door/out`. The key is 1452, so the answer is to `echo 1452 > /dev/by_id/door/in`
  
</detials>
