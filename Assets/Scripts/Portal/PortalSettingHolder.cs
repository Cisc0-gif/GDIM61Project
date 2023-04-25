using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSettingHolder : MonoBehaviour
{

    [SerializeField]
    private Color SelectedColor;
    [SerializeField]
    private Color NotSelectedColor;

    public static Color PortalSelectedColor { get; private set; }
    public static Color PortalNotSelectedColor { get; private set; }
    #region Singleton
    public static PortalSettingHolder Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            PortalSelectedColor = SelectedColor;
            PortalNotSelectedColor = NotSelectedColor;
        }
    }
    #endregion

}
