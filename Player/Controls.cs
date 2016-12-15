using UnityEngine;
using System.Linq;

public class ControlScheme {

    public ControlScheme(string vertical, string horizontal, string lookVertical, string lookHorizontal, string shoot, string cycleUp, string cycleDown, string respawn, string flip, string slowAim, string start, string back) {
        this.vertical = vertical;
        this.horizontal = horizontal;
        this.lookVertical = lookVertical;
        this.lookHorizontal = lookHorizontal;
        this.shoot = shoot;
        this.cycleUp = cycleUp;
        this.cycleDown = cycleDown;
        this.respawn = respawn;
        this.flip = flip;
        this.slowAim = slowAim;
        this.start = start;
        this.back = back;
    }

    private string vertical, horizontal, lookVertical, lookHorizontal, shoot, respawn, cycleUp, cycleDown, flip, slowAim, start, back;

    public string Vertical { get { return vertical; } }
    public string Horizontal { get { return horizontal; } }
    public string LookVertical { get { return lookVertical; } }
    public string LookHorizontal { get { return lookHorizontal; } }
    public string Shoot { get { return shoot; } }
    public string CycleUp { get { return cycleUp; } }
    public string CycleDown { get { return cycleDown; } }
    public string Respawn { get { return respawn; } }
    public string Flip { get { return flip; } }
    public string SlowAim { get { return slowAim; } }
    public string Start { get { return start; } }
    public string Back { get { return back; } }

    public static implicit operator bool (ControlScheme c) {
        return c != null;
    }

}

public static class Controls {

    public static ControlScheme keyboard = new ControlScheme("Vertical", "Horizontal", "Arrow Vertical", "Arrow Horizontal", "Space", "Q", "E", "R", "Left Shift", "Right Control", "Return", "Escape");

    public static ControlScheme joystick1 = new ControlScheme("J1 Vertical", "J1 Horizontal", "J1 5th Axis", "J1 4th Axis", "J1 0 Button", "J1 4th Button", "J1 5th Button", "J1 3rd Button", "J1 10th Axis", "J1 9th Axis", "J1 7th Button", "J1 6th Button");
    public static ControlScheme joystick2 = new ControlScheme("J2 Vertical", "J2 Horizontal", "J2 5th Axis", "J2 4th Axis", "J2 0 Button", "J2 4th Button", "J2 5th Button", "J2 3rd Button", "J2 10th Axis", "J2 9th Axis", "J2 7th Button", "J2 6th Button");
    public static ControlScheme joystick3 = new ControlScheme("J3 Vertical", "J3 Horizontal", "J3 5th Axis", "J3 4th Axis", "J3 0 Button", "J3 4th Button", "J3 5th Button", "J3 3rd Button", "J3 10th Axis", "J3 9th Axis", "J3 7th Button", "J3 6th Button");
    public static ControlScheme joystick4 = new ControlScheme("J4 Vertical", "J4 Horizontal", "J4 5th Axis", "J4 4th Axis", "J4 0 Button", "J4 4th Button", "J4 5th Button", "J4 3rd Button", "J4 10th Axis", "J4 9th Axis", "J4 7th Button", "J4 6th Button");
    public static string allHorizontalAxes = "All Horizontal";
    public static string allVerticalAxes = "All Vertical";
    public static string allStartButtonsAxes = "All 7th Button/Return";
    public static string allBackButtonsAxes = "All 6th Button/Escape";

    public static ControlScheme GetControls(ControlOptions c) {
        if (c == ControlOptions.Keyboard)
            return keyboard;
        else if (c == ControlOptions.Joystick1)
            return joystick1;
        else if (c == ControlOptions.Joystick2)
            return joystick2;
        else if (c == ControlOptions.Joystick3)
            return joystick3;
        else if (c == ControlOptions.Joystick4)
            return joystick4;
        else
            return null;
    }

    public static ControlOptions GetEnum(ControlScheme c) {
        if (c == joystick1)
            return ControlOptions.Joystick1;
        else if (c == joystick2)
            return ControlOptions.Joystick2;
        else if (c == joystick3)
            return ControlOptions.Joystick3;
        else if (c == joystick4)
            return ControlOptions.Joystick4;
        else
            return ControlOptions.Keyboard;//keyboard by default because enum is non-nullable
    }

    public static int NumberOfControllers() {
        return Input.GetJoystickNames().Where(s => s != "").ToArray().Length;
    }

}

public enum ControlOptions {
    Keyboard, Joystick1, Joystick2, Joystick3, Joystick4
}