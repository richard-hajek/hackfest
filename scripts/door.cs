using Godot;
using System;
using hackfest2.addons.Pure;

public class door : Spatial, Peripheralable
{
    
    public override void _Ready()
    {
        
    }

    public void Open()
    {
        GetNode<DoorAnimation>("AnimationPlayer").SetAnimation("opening");
    }

    public void Close()
    {
        GetNode<DoorAnimation>("AnimationPlayer").SetAnimation("closing");
    }

    public void OnData(Peripheral peripheral, string data)
    {
        switch (data)
        {
            case "0":
                Close();
                break;
            case "1":
                Open();
                break;
        }
    }
}
