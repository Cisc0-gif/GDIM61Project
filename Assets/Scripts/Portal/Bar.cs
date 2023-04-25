using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField]
    private List<PortalEnterance> Portals;



    public void EnableBar()
    {
        StartCoroutine(WaitForSettings(PortalSettingHolder.PortalSelectedColor, true));
    }

    public void DisableBar()
    {
        StartCoroutine(WaitForSettings(PortalSettingHolder.PortalNotSelectedColor, false));
    }

    IEnumerator WaitForSettings(Color color, bool Enabled)
    {
        int runs = 0;
        while (runs > 5)
        {
            if(PortalSettingHolder.PortalNotSelectedColor != null)
            {
                runs = 5;
            }
            runs++;
            yield return new WaitForSeconds(0.5f);
        }
        UpdatePannels(color, Enabled);
    }

    private void UpdatePannels (Color c, bool Enable)
    {
        foreach (PortalEnterance portal in Portals)
        {   
            portal.SwitchMaterial(c);
            portal.gameObject.SetActive(Enable);
        }
    }
}
