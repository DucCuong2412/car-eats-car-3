using System;
using UnityEngine;

public class GUIControllsMessenger : MonoBehaviour
{

    public GUIControllsButton buttonTiltLeft;

    public GUIControllsButton buttonTiltRight;

    public GUIControllsButton buttonPower;

    public GUIControllsButton buttonWeapon;

    public GameObject topLeftBtn;

    private bool init;

    private Action onRotateLeft;

    private Action onRotateRight;

    private Action onRotateStop;

    private Action onPower;

    private Action onPowerStop;

    private Action onFire;

    private Action onFireStop;

    private Action onTopRightButton;

    public TutorialsHUD THUD;

    private bool l = true;

    private bool r = true;

    private bool t = true;

    private bool b = true;

    private void Start()
    {
        buttonTiltLeft.on = TiltLeftOn;
        buttonTiltLeft.off = TiltLeftOff;
        buttonTiltRight.on = TiltRightOn;
        buttonTiltRight.off = TiltRightOff;
        buttonPower.on = PowerOn;
        buttonPower.off = PowerOff;
        buttonWeapon.on = FireOn;
        buttonWeapon.off = FireOff;
    }
    private void Update()
    {
        
    }
    public void InitControlls(Action onRotateLeft, Action onRotateRight, Action onRotateStop, Action onTurbo, Action onTurboStop, Action onFire, Action onTopRightButton)
    {
        if (init)
        {
            UnityEngine.Debug.Log(base.name + " already is inited but you try do that!");
            return;
        }
        this.onRotateLeft = onRotateLeft;
        this.onRotateRight = onRotateRight;
        this.onRotateStop = onRotateStop;
        onPower = onTurbo;
        onPowerStop = onTurboStop;
        this.onFire = onFire;
        this.onTopRightButton = onTopRightButton;
        init = true;
    }

    public void TiltLeftOn()
    {
        if (onRotateLeft != null)
        {
            onRotateLeft();
        }
        Time.timeScale = 1f;
        THUD.clic();
    }

    public void TiltLeftOff()
    {
        if (onRotateStop != null)
        {
            onRotateStop();
        }
        Time.timeScale = 1f;
        THUD.clic();
    }

    public void TiltRightOn()
    {
        if (onRotateRight != null)
        {
            onRotateRight();
        }
        Time.timeScale = 1f;
        THUD.clic();
    }

    public void TiltRightOff()
    {
        if (onRotateStop != null)
        {
            onRotateStop();
        }
        Time.timeScale = 1f;
        THUD.clic();
    }

    public void PowerOn()
    {
        if (onPower != null)
        {
            onPower();
        }
        Time.timeScale = 1f;
        THUD.clic();
    }

    public void PowerOff()
    {
        if (onPowerStop != null)
        {
            onPowerStop();
        }
        Time.timeScale = 1f;
        THUD.clic();
    }

    public void FireOn()
    {
        if (onFire != null)
        {
            onFire();
        }
        Time.timeScale = 1f;
        THUD.clic();
    }

    public void FireOff()
    {
        if (onFireStop != null)
        {
            onFireStop();
        }
        Time.timeScale = 1f;
        THUD.clic();
    }

    public void onTopRightButtonPressed()
    {
        if (onTopRightButton != null)
        {
            onTopRightButton();
        }
    }

    public void Show()
    {
        buttonTiltLeft.Show();
        buttonTiltRight.Show();
        buttonPower.Show();
        buttonWeapon.Show();
        topLeftBtn.SetActive(value: true);
    }

    public void Hide()
    {
        buttonTiltLeft.Hide();
        buttonTiltRight.Hide();
        buttonPower.Hide();
        buttonWeapon.Hide();
        topLeftBtn.SetActive(value: false);
    }

    public void SetAllButtonsState(bool enabled)
    {
        SetLeftTiltButtonState(enabled);
        SetRightTiltButtonState(enabled);
        SetTurboButtonState(enabled);
        SetBombButtonState(enabled);
    }

    public void SetLeftTiltButtonState(bool enabled)
    {
        if (l != enabled)
        {
            buttonTiltLeft.Set(enabled ? GUIControllsButton.Type.Normal : GUIControllsButton.Type.Disabled);
            l = enabled;
        }
    }

    public void SetRightTiltButtonState(bool enabled)
    {
        if (r != enabled)
        {
            buttonTiltRight.Set(enabled ? GUIControllsButton.Type.Normal : GUIControllsButton.Type.Disabled);
            r = enabled;
        }
    }

    public void SetTurboButtonState(bool enabled)
    {
        if (t != enabled)
        {
            buttonPower.Set(enabled ? GUIControllsButton.Type.Normal : GUIControllsButton.Type.Disabled);
            t = enabled;
        }
    }

    public void SetBombButtonState(bool enabled)
    {
        if (b != enabled)
        {
            buttonWeapon.Set(enabled ? GUIControllsButton.Type.Normal : GUIControllsButton.Type.Disabled);
            b = enabled;
        }
    }
}
