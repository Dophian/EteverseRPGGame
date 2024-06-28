using UnityEngine;

// ������ ��忡���� ����� �����ϸ�
// �� ���� �����̽��� ����ϴ� ��ũ��Ʈ�� Editor ���� ���� �־�� ��
using UnityEditor;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace RPGGame
{
    public class ExcelDataProcessor : AssetPostprocessor
    {
        // ���� ���� ���.
        private static readonly string exelFilePath = "Assets/RPGGame/Editor/Data/RPGSampleData.xlsx";

        // So ���� ���.
        private static readonly string exportFilePath = "Assets/RPGGame/Resources/GameData/RPG Data.asset";
        private static void OnPostprocessAllAssets(
            string[] importedAssets, 
            string[] deletedAssets, 
            string[] movedAssets, 
            string[] movedFromAssetPaths)
        {
            // �׽�Ʈ.
            //if (importedAssets.Length > 0)
            //{
            //    Debug.Log(importedAssets[0]);
            //}

            foreach (var assetPath in importedAssets)
            {
                // ����Ʈ�� ������ �츮�� �����ִ� ������ �ƴ϶�� �ǳʶ�.
                if (assetPath.Equals(exelFilePath) == false)
                {
                    continue;
                }

                Logger.Log("�׽�Ʈ ����");

                // SO ������ ���� ����, �ʱ�ȭ.
                var soData = AssetDatabase.LoadAssetAtPath<RPGData>(exportFilePath);

                // �迭 �ʱ�ȭ.
                soData.data.Clear();

                // �츮�� �����ִ� ����.
                //Debug.Log(assetPath);

                // ���� �б�.
                // ������ �������� �پ��� �� �ݾƾ� ��.
                // File.ReadAllText()
                using (FileStream stream = File.OpenRead(exelFilePath))
                {
                    // ���� ���Ϸ� ���� (NPOI ���̺귯��).
                    IWorkbook book = new XSSFWorkbook(stream);

                    // ��Ʈ ����.
                    ISheet sheet = book.GetSheetAt(0);

                    // ������ ������ ó��.
                    for (int ix = 1; ix < 3; ++ix)
                    {
                        // �� ����.
                        IRow row = sheet.GetRow(ix);

                        // �� �б�.
                        RPGData.Attribute newData = new RPGData.Attribute();
                        newData.name = row.GetCell(0).StringCellValue;
                        newData.maxHP = (float)row.GetCell(1).NumericCellValue;
                        newData.damage = (float)row.GetCell(2).NumericCellValue;
                        newData.moveSpeed = (float)row.GetCell(3).NumericCellValue;
                        newData.rotateSpeed = (float)row.GetCell(4).NumericCellValue;
                        newData.attackRange = (float)row.GetCell(5).NumericCellValue;

                        // SO�� ������ �߰�.
                        soData.data.Add(newData);
                    }

                    //// �� ����.
                    //IRow row = sheet.GetRow(1);

                    //// �� ����.
                    //string log = $"{row.GetCell(0)}��{row.GetCell(1)}:{row.GetCell(2)}:{row.GetCell(3)}:{row.GetCell(4)}:{row.GetCell(5)}";

                    //// �׽�Ʈ�� ���.
                    //Debug.Log(log);
                }

                // ���� ���� ó�� ���� �ڿ� SO�� ����ƴٰ� �����Ϳ��� �˸���.
                var soFile = AssetDatabase.LoadAssetAtPath<RPGData>(exportFilePath);
                
                // ����ƴٰ� �����ϱ�.
                EditorUtility.SetDirty(soFile);

                //FileStream stream = File.OpenRead(exelFilePath)
                //stream.Close();
            }
        }
    }
}