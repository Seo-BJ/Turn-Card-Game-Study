
/*     ===== Do not touch this. Auto Generated Code. =====    */
/*     If you want custom code generation modify this => 'CodeGeneratorUnityEngine.cs'  */
using GoogleSheet.Protocol.v2.Res;
using GoogleSheet.Protocol.v2.Req;
using UGS;
using System;
using UGS.IO;
using GoogleSheet;
using System.Collections.Generic;
using System.IO;
using GoogleSheet.Type;
using System.Reflection;
using UnityEngine;


namespace Example2.Item
{
    [GoogleSheet.Attribute.TableStruct]
    public partial class Weapons : ITable
    { 

        public delegate void OnLoadedFromGoogleSheets(List<Weapons> loadedList, Dictionary<int, Weapons> loadedDictionary);

        static bool _isLoaded = false;
        static string _spreadSheetID = "10_CFs1W-uF7ETsrhrmpVX_j4QQMKA7f5gNtLiU3-VU0"; // it is file id
        static string _sheetID = "2009728146"; // it is sheet id
        static UnityFileReader _reader = new UnityFileReader();

/* Your Loaded Data Storage. */
    
        public static Dictionary<int, Weapons> WeaponsMap = new Dictionary<int, Weapons>();  
        public static List<Weapons> WeaponsList = new List<Weapons>();   

        /// <summary>
        /// Get Weapons List 
        /// Auto Load
        /// </summary>
        public static List<Weapons> GetList()
        {{
           if (_isLoaded == false) Load();
           return WeaponsList;
        }}

        /// <summary>
        /// Get Weapons Dictionary, keyType is your sheet A1 field type.
        /// - Auto Load
        /// </summary>
        public static Dictionary<int, Weapons>  GetDictionary()
        {{
           if (_isLoaded == false) Load();
           return WeaponsMap;
        }}

    

/* Fields. */

		public System.Int32 ItemIndex;
		public System.String LocaleID;
		public System.String Type;
		public System.Int32 Grade;
		public System.Int32 Str;
		public System.Int32 Dex;
		public System.Int32 INT;
		public System.Int32 Luk;
		public System.String IconName;
		public System.Int32 Price;
  

#region fuctions


        public static void Load(bool forceReload = false)
        {
            if(_isLoaded && forceReload == false)
            {
#if UGS_DEBUG
                 Debug.Log("Weapons is already loaded! if you want reload then, forceReload parameter set true");
#endif
                 return;
            }

            string text = _reader.ReadData("Example2.Item"); 
            if (text != null)
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ReadSpreadSheetResult>(text);
                CommonLoad(result.jsonObject, forceReload); 
                if(!_isLoaded)_isLoaded = true;
            }
      
        }
 

        public static void LoadFromGoogle(System.Action<List<Weapons>, Dictionary<int, Weapons>> onLoaded, bool updateCurrentData = false)
        {      
                IHttpProtcol webInstance = null;
    #if UNITY_EDITOR
                if (Application.isPlaying == false)
                {
                    webInstance = UnityEditorWebRequest.Instance as IHttpProtcol;
                }
                else 
                {
                    webInstance = UnityPlayerWebRequest.Instance as IHttpProtcol;
                }
    #endif
    #if !UNITY_EDITOR
                     webInstance = UnityPlayerWebRequest.Instance as IHttpProtcol;
    #endif
          
 
                var mdl = new ReadSpreadSheetReqModel(_spreadSheetID);
                webInstance.ReadSpreadSheet(mdl, OnError, (data) => {
                    var loaded = CommonLoad(data.jsonObject, updateCurrentData); 
                    onLoaded?.Invoke(loaded.list, loaded.map);
                });
        }

               


    public static (List<Weapons> list, Dictionary<int, Weapons> map) CommonLoad(Dictionary<string, Dictionary<string, List<string>>> jsonObject, bool forceReload){
            Dictionary<int, Weapons> map = new Dictionary<int, Weapons>();
            List<Weapons> list = new List<Weapons>();     
            TypeMap.Init();
            FieldInfo[] fields = typeof(Weapons).GetFields(BindingFlags.Public | BindingFlags.Instance);
            List<(string original, string propertyName, string type)> typeInfos = new List<(string, string, string)>(); 
            List<List<string>> rows = new List<List<string>>();
            var sheet = jsonObject["Weapons"];

            foreach (var column in sheet.Keys)
            {
                string[] split = column.Replace(" ", null).Split(':');
                         string columnField = split[0];
                string   columnType = split[1];

                typeInfos.Add((column, columnField, columnType));
                          List<string> typeValues = sheet[column];
                rows.Add(typeValues);
            }

          // 실제 데이터 로드
                    if (rows.Count != 0)
                    {
                        int rowCount = rows[0].Count;
                        for (int i = 0; i < rowCount; i++)
                        {
                            Weapons instance = new Weapons();
                            for (int j = 0; j < typeInfos.Count; j++)
                            {
                                try
                                {
                                    var typeInfo = TypeMap.StrMap[typeInfos[j].type];
                                    //int, float, List<..> etc
                                    string type = typeInfos[j].type;
                                    if (type.StartsWith(" < ") && type.Substring(1, 4) == "Enum" && type.EndsWith(">"))
                                    {
                                         Debug.Log("It's Enum");
                                    }

                                    var readedValue = TypeMap.Map[typeInfo].Read(rows[j][i]);
                                    fields[j].SetValue(instance, readedValue);

                                }
                                catch (Exception e)
                                {
                                    if (e is UGSValueParseException)
                                    {
                                        Debug.LogError("<color=red> UGS Value Parse Failed! </color>");
                                        Debug.LogError(e);
                                        return (null, null);
                                    }

                                    //enum parse
                                    var type = typeInfos[j].type;
                                    type = type.Replace("Enum<", null);
                                    type = type.Replace(">", null);

                                    var readedValue = TypeMap.EnumMap[type].Read(rows[j][i]);
                                    fields[j].SetValue(instance, readedValue); 
                                }
                              
                            }
                            list.Add(instance); 
                            map.Add(instance.ItemIndex, instance);
                        }
                        if(_isLoaded == false || forceReload)
                        { 
                            WeaponsList = list;
                            WeaponsMap = map;
                            _isLoaded = true;
                        }
                    } 
                    return (list, map); 
}


 

        public static void Write(Weapons data, System.Action<WriteObjectResult> onWriteCallback = null)
        { 
            TypeMap.Init();
            FieldInfo[] fields = typeof(Weapons).GetFields(BindingFlags.Public | BindingFlags.Instance);
            var datas = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                string writeRule = null;
                if(type.IsEnum)
                {
                    writeRule = TypeMap.EnumMap[type.Name].Write(fields[i].GetValue(data));
                }
                else
                {
                    writeRule = TypeMap.Map[type].Write(fields[i].GetValue(data));
                } 
                datas[i] = writeRule; 
            }  
           
#if UNITY_EDITOR
if(Application.isPlaying == false)
{
                UnityPlayerWebRequest.Instance.WriteObject(new WriteObjectReqModel(_spreadSheetID, _sheetID, datas[0], datas), OnError, onWriteCallback);

}
else
{
            UnityPlayerWebRequest.Instance.WriteObject(new  WriteObjectReqModel(_spreadSheetID, _sheetID, datas[0], datas), OnError, onWriteCallback);

}
#endif

#if !UNITY_EDITOR
   UnityPlayerWebRequest.Instance.WriteObject(new  WriteObjectReqModel(spreadSheetID, sheetID, datas[0], datas), OnError, onWriteCallback);

#endif
        } 
          

 


#endregion

#region OdinInsepctorExtentions
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.Button("UploadToSheet")]
    public void Upload()
    {
        Write(this);
    }
 
    
#endif


 
#endregion
    public static void OnError(System.Exception e){
         UnityGoogleSheet.OnTableError(e);
    }
 
    }
}
        