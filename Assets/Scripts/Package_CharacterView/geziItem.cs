using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GeziData 
{
    public bool hasItem;//�Ƿ�����Ʒ
    public string name;//��Ʒ����
    public int id;//��Ʒ���
    public string iconPath;//��ƷIcon
    public int num;//��Ʒ����
    public string description;
}

public class GeziItem : MonoBehaviour
{
    public GeziData geziData;
    public Image icon;
    public Text num;
    public Text description;
    
    public void Refresh()//�����ݷ����ı䣬���ô˷���
    {
        if (geziData == null || geziData.hasItem == false)
        {
            num.gameObject.SetActive(false);//����
            icon.gameObject.SetActive(false);
            return;
        }
        num.gameObject.SetActive(true);//��ʾ
        icon.gameObject.SetActive(true);
        num.text = geziData.num.ToString();//���ݶ�Ӧ,geziData���num���ݸ�text��
        icon.sprite = Resources.Load<GameObject>(geziData.iconPath).GetComponent<SpriteRenderer>().sprite;//ͼ���Ӧ������gezidata���·���õ�ͼƬ��
        description.text = geziData.description;
    }
}

