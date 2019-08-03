using Godot;
using System;
using System.Linq;
using Godot.Collections;

public class DoorAnimation : AnimationPlayer
{
    
    public Dictionary<string, string[]> states = new Dictionary<string, string[]>
    {
        {"closed", new []{"opening"}},
        {"opening", new []{"open"}},
        {"open", new []{"closing"}},
        {"closing", new []{"closed"}}
    };

    public string CurrentState;

    public override void _Ready()
    {
        SetAnimation("closed");
        Connect("animation_finished", this,  "AnimationEnded");
    }

    public bool SetAnimation(string animationName)
    {
        GD.Print("Switching from " + CurrentState + " to " + animationName);
        if (animationName == CurrentState)
        {
            GD.Print("Animation already running!");
            return true;
        }

        if (HasAnimation(animationName))
        {
            if (CurrentState != null)
            {
                var possibleAnimations = states[CurrentState];

                if (possibleAnimations.All(s => s != animationName))
                {
                    GD.Print($"Cannot change to {animationName} from {CurrentState}");
                    return false;
                }

                CurrentState = animationName;
                Play(animationName);
                return true;
            }

            CurrentState = animationName;
            Play(animationName);
            return true;
        }

        GD.Print("Unknown animation!");
        return false;
    }

    public void AnimationEnded(string s)
    {
        switch (CurrentState)
        {
            case "closed":
                return;
            case "opening":
                SetAnimation("open");
                break;
            case "open":
                return;
            case "closing":
                SetAnimation("closed");
                break;
        }
    }
}
