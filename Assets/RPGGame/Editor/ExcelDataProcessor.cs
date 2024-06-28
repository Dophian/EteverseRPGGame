using UnityEngine;

// 에디터 모드에서만 사용이 가능하며
// 이 네임 스페이스를 사용하는 스크립트는 Editor 폴더 내에 있어야 함
using UnityEditor;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace RPGGame
{
    public class ExcelDataProcessor : AssetPostprocessor
    {
        // 엑셀 파일 경로.
        private static readonly string exelFilePath = "Assets/RPGGame/Editor/Data/RPGSampleData.xlsx";

        // So 파일 경로.
        private static readonly string exportFilePath = "Assets/RPGGame/Resources/GameData/RPG Data.asset";
        private static void OnPostprocessAllAssets(
            string[] importedAssets, 
            string[] deletedAssets, 
            string[] movedAssets, 
            string[] movedFromAssetPaths)
        {
            // 테스트.
            //if (importedAssets.Length > 0)
            //{
            //    Debug.Log(importedAssets[0]);
            //}

            foreach (var assetPath in importedAssets)
            {
                // 임포트된 파일이 우리가 관심있는 파일이 아니라면 건너뜀.
                if (assetPath.Equals(exelFilePath) == false)
                {
                    continue;
                }

                Logger.Log("테스트 실행");

                // SO 데이터 파일 열고, 초기화.
                var soData = AssetDatabase.LoadAssetAtPath<RPGData>(exportFilePath);

                // 배열 초기화.
                soData.data.Clear();

                // 우리가 관심있는 파일.
                //Debug.Log(assetPath);

                // 파일 읽기.
                // 파일을 열었으면 다쓰고 꼭 닫아야 함.
                // File.ReadAllText()
                using (FileStream stream = File.OpenRead(exelFilePath))
                {
                    // 엑셀 파일로 열기 (NPOI 라이브러리).
                    IWorkbook book = new XSSFWorkbook(stream);

                    // 시트 열기.
                    ISheet sheet = book.GetSheetAt(0);

                    // 루프로 데이터 처리.
                    for (int ix = 1; ix < 3; ++ix)
                    {
                        // 행 열기.
                        IRow row = sheet.GetRow(ix);

                        // 엵 읽기.
                        RPGData.Attribute newData = new RPGData.Attribute();
                        newData.name = row.GetCell(0).StringCellValue;
                        newData.maxHP = (float)row.GetCell(1).NumericCellValue;
                        newData.damage = (float)row.GetCell(2).NumericCellValue;
                        newData.moveSpeed = (float)row.GetCell(3).NumericCellValue;
                        newData.rotateSpeed = (float)row.GetCell(4).NumericCellValue;
                        newData.attackRange = (float)row.GetCell(5).NumericCellValue;

                        // SO에 데이터 추가.
                        soData.data.Add(newData);
                    }

                    //// 행 열기.
                    //IRow row = sheet.GetRow(1);

                    //// 열 열기.
                    //string log = $"{row.GetCell(0)}→{row.GetCell(1)}:{row.GetCell(2)}:{row.GetCell(3)}:{row.GetCell(4)}:{row.GetCell(5)}";

                    //// 테스트로 출력.
                    //Debug.Log(log);
                }

                // 엑셀 파일 처리 끝난 뒤에 SO이 변경됐다고 에디터에게 알리기.
                var soFile = AssetDatabase.LoadAssetAtPath<RPGData>(exportFilePath);
                
                // 변경됐다고 설정하기.
                EditorUtility.SetDirty(soFile);

                //FileStream stream = File.OpenRead(exelFilePath)
                //stream.Close();
            }
        }
    }
}