using GoogleSheet.Protocol.v2.Req;
using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;
using UnityEngine.Serialization;

public class ReadMeScene : MonoBehaviour
{
    public GameObject wait;
    [FormerlySerializedAs("step3_hide_btn")] public GameObject step3HideBtn;
    [FormerlySerializedAs("step3_succesfulltText")] public GameObject step3SuccesfulltText;

    public GameObject clickMe;
    
    public void OnClickCopy()
    {
        wait.SetActive(true);
        UnityPlayerWebRequest.Instance.CopyExample(new CopyExampleReqModel(), null, (x) => {
            wait.SetActive(false);
            Application.OpenURL($"https://drive.google.com/drive/u/0/folders/{x}");
            UGSettingObjectWrapper.GoogleFolderID = x.createdFolderId;
            step3HideBtn?.SetActive(true);
            step3SuccesfulltText.SetActive(true);
            clickMe.SetActive(false);
        });
    }
}
