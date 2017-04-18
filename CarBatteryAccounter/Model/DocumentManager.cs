using CarBatteryAccounter.Model.Entities;
using CarBatteryAccounter.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;

namespace CarBatteryAccounter.Model
{
    public class DocumentManager : IDisposable
    {
        private const string format = @".docx";
        private const string writeOfDocName = @"Акт на списание изношенных АКБ";
        private const string setSubreportedDocName = @"Акт на списание с установкой подотчетной АКБ";
        private const string subreportDocName = @"Акт на списание АКБ с установкой на подотчет (при списании автомобиля)";

        private Word.Application app;

        public DocumentManager()
        {
            app = new Word.Application();
            //app.Visible = true;
        }

        private void ReplaceText(Dictionary<string, string> replaceStrings)
        {
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            foreach(var pair in replaceStrings)
            {
                object findText = pair.Key;
                object replaceTextWith = pair.Value;
                if(string.IsNullOrEmpty((string)replaceTextWith))
                {
                    replaceTextWith = findText;
                }
                app.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceTextWith, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
            }
            
        }

        private string GenerateFilePath(DocumentType docType, bool toSave = false)
        {
            string newFilePath = string.Empty;
            string fileName = string.Empty;

            switch (docType)
            {
                case DocumentType.writeOfDoc:
                    fileName = writeOfDocName;
                    break;

                case DocumentType.subreportDoc:
                    fileName = subreportDocName;
                    break;

                case DocumentType.setSubreportedDoc:
                    fileName = setSubreportedDocName;
                    break;

                default:
                    return null;
            }
            if(toSave)
            {
                newFilePath = Directory.GetCurrentDirectory() +
                       @"\data\documents\" +
                       fileName +
                       "_" +
                       DateTime.Now.ToString("dd.MM.yyyy-hh.mm") +
                       format;
            }
            else
            {
                newFilePath = Directory.GetCurrentDirectory() +
                       @"\data\templates\" +
                       fileName +
                       format;
            }

            return newFilePath;


        }

        public void GenerateDoc(DocumentType docType, CarViewModel car, BattaryViewModel battary, BattaryViewModel subBattary)
        {
            if (app == null)
            {
                app = new Word.Application();
            }

            Word.Application objWord;
            objWord = (Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            if(objWord.Windows.Count == 0)
            {
                app = new Word.Application();
            }
            app.Visible = false;

            var replaceRows = new Dictionary<string, string>();

            switch (docType)
            {
                case DocumentType.writeOfDoc:
                    app.Documents.Open(GenerateFilePath(DocumentType.writeOfDoc), ReadOnly:false);
                    replaceRows.Add("[CarDriverName]", car.DriverName);
                    replaceRows.Add("[CarDriverName2]", car.DriverName2);
                    replaceRows.Add("[CarModel]", car.Model);
                    replaceRows.Add("[CarNumber]", car.Number);

                    replaceRows.Add("[B1Type]", battary.Type.ToString());
                    replaceRows.Add("[B1SerialNumber]", battary.SerialNumber);
                    replaceRows.Add("[B1NomenclatureNumber]", battary.NomenclatureNumber);
                    replaceRows.Add("[B1SetDate]", battary.SetDate);

                    ReplaceText(replaceRows);
                    app.Documents[1].SaveAs2(GenerateFilePath(DocumentType.writeOfDoc, true));
                    break;

                case DocumentType.subreportDoc:
                    app.Documents.Open(GenerateFilePath(DocumentType.subreportDoc), ReadOnly: false);
                    replaceRows.Add("[CarDriverName]", car.DriverName);
                    replaceRows.Add("[CarDriverName2]", car.DriverName2);
                    replaceRows.Add("[CarModel]", car.Model);
                    replaceRows.Add("[CarNumber]", car.Number);

                    replaceRows.Add("[B1Type]", battary.Type.ToString());
                    replaceRows.Add("[B1SerialNumber]", battary.SerialNumber);
                    replaceRows.Add("[B1NomenclatureNumber]", battary.NomenclatureNumber);
                    replaceRows.Add("[B1SetDate]", battary.SetDate);

                    ReplaceText(replaceRows);
                    app.Documents[1].SaveAs2(GenerateFilePath(DocumentType.subreportDoc, true));
                    break;

                case DocumentType.setSubreportedDoc:

                    app.Documents.Open(GenerateFilePath(DocumentType.setSubreportedDoc), ReadOnly: false);
                    replaceRows.Add("[CarDriverName]", car.DriverName);
                    replaceRows.Add("[CarDriverName2]", car.DriverName2);
                    replaceRows.Add("[CarModel]", car.Model);
                    replaceRows.Add("[CarNumber]", car.Number);

                    replaceRows.Add("[B1Type]", battary.Type.ToString());
                    replaceRows.Add("[B1SerialNumber]", battary.SerialNumber);
                    replaceRows.Add("[B1NomenclatureNumber]", battary.NomenclatureNumber);
                    replaceRows.Add("[B1SetDate]", battary.SetDate);

                    replaceRows.Add("[B2Type]", subBattary.Type.ToString());
                    replaceRows.Add("[B2SerialNumber]", subBattary.SerialNumber);
                    replaceRows.Add("[B2NomenclatureNumber]", subBattary.NomenclatureNumber);
                    replaceRows.Add("[B2SetDate]", subBattary.SetDate);

                    ReplaceText(replaceRows);
                    app.Documents[1].SaveAs2(GenerateFilePath(DocumentType.setSubreportedDoc, true));
                    break;
            }
            app.Visible = true;
        }

        public void Dispose()
        {
            try
            {
                if (app != null)
                {
                    if (!app.Visible)
                    {
                        app.Quit();
                    }
                }
            }
            catch { }      
        }
    }

    public enum DocumentType
    {
        writeOfDoc,
        subreportDoc,
        setSubreportedDoc
    }
}
