
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


namespace EventData
{
    [GoogleSheet.Attribute.TableStruct]
    public partial class Data : ITable
    { 

        public delegate void OnLoadedFromGoogleSheets(List<Data> loadedList, Dictionary<int, Data> loadedDictionary);

        static bool _isLoaded = false;
        static string _spreadSheetID = "1zSFDOgDBGSTlWncTcDuZ2tLxIFULfpltWEnKQ2twyN8"; // it is file id
        static string _sheetID = "0"; // it is sheet id
        static UnityFileReader _reader = new UnityFileReader();

/* Your Loaded Data Storage. */
    
        public static Dictionary<int, Data> DataMap = new Dictionary<int, Data>();  
        public static List<Data> DataList = new List<Data>();   

        /// <summary>
        /// Get Data List 
        /// Auto Load
        /// </summary>
        public static List<Data> GetList()
        {{
           if (_isLoaded == false) Load();
           return DataList;
        }}

        /// <summary>
        /// Get Data Dictionary, keyType is your sheet A1 field type.
        /// - Auto Load
        /// </summary>
        public static Dictionary<int, Data>  GetDictionary()
        {{
           if (_isLoaded == false) Load();
           return DataMap;
        }}

    

/* Fields. */

		public System.Int32 EventID;
		public System.String EventTitle;
		public System.String EventText;
		public System.Int32 ButtonCount;
		public System.String Button1;
		public System.String Button2;
		public System.String Button3;
		public System.String Button4;
		public System.String Button5;
  

#region fuctions


        public static void Load(bool forceReload = false)
        {
            if(_isLoaded && forceReload == false)
            {
#if UGS_DEBUG
                 Debug.Log("Data is already loaded! if you want reload then, forceReload parameter set true");
#endif
                 return;
            }

            string text = _reader.ReadData("EventData"); 
            if (text != null)
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ReadSpreadSheetResult>(text);
                CommonLoad(result.jsonObject, forceReload); 
                if(!_isLoaded)_isLoaded = true;
            }
      
        }
 

        public static void LoadFromGoogle(System.Action<List<Data>, Dictionary<int, Data>> onLoaded, bool updateCurrentData = false)
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

               


    public static (List<Data> list, Dictionary<int, Data> map) CommonLoad(Dictionary<string, Dictionary<string, List<string>>> jsonObject, bool forceReload){
            Dictionary<int, Data> map = new Dictionary<int, Data>();
            List<Data> list = new List<Data>();     
            TypeMap.Init();
            FieldInfo[] fields = typeof(Data).GetFields(BindingFlags.Public | BindingFlags.Instance);
            List<(string original, string propertyName, string type)> typeInfos = new List<(string, string, string)>(); 
            List<List<string>> rows = new List<List<string>>();
            var sheet = jsonObject["Data"];

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
                            Data instance = new Data();
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
                            map.Add(instance.EventID, instance);
                        }
                        if(_isLoaded == false || forceReload)
                        { 
                            DataList = list;
                            DataMap = map;
                            _isLoaded = true;
                        }
                    } 
                    return (list, map); 
}


 

        public static void Write(Data data, System.Action<WriteObjectResult> onWriteCallback = null)
        { 
            TypeMap.Init();
            FieldInfo[] fields = typeof(Data).GetFields(BindingFlags.Public | BindingFlags.Instance);
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
        