using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class DialogData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/Excel/DialogData.xlsx";
	private static readonly string exportPath = "Assets/Resources/Excel/DialogData.asset";
	private static readonly string[] sheetNames = { "Dialog", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_Dialog data = (Entity_Dialog)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_Dialog));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_Dialog> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					Entity_Dialog.Sheet s = new Entity_Dialog.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Entity_Dialog.Param p = new Entity_Dialog.Param ();
						
					cell = row.GetCell(0); p.index = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.npc = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.gamestate = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.Dialog = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(4); p.changeState = (int)(cell == null ? 0 : cell.NumericCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
