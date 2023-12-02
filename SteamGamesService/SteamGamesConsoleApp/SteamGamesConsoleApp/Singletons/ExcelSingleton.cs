using OfficeOpenXml;
using SteamGamesConsoleApp.Models;
using SteamGamesConsoleApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesConsoleApp.Singletons {
    public static class ExcelSingleton {

        private static string FilePath = Context.Configuration["ExcelGamesFilePath"];
        private static string FileName = Context.Configuration["ExcelGamesFileName"];

        private static ExcelPackage Archivo;

        public static void AbrirExcel() {
            try {
                FileInfo fileInfo = new FileInfo(FilePath + FileName);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                Archivo = new ExcelPackage(fileInfo);
            } catch (Exception e){
                throw new Exception("No se pudo abrir el Excel.", e);
            }
        }

        public static void CerrarExcel() {
            try {
                Dispose();
            }catch(Exception e) {
                throw new Exception("No se pudo Cerrar el excel.", e);
            }
        }

        private static void Dispose() {
            if (Archivo.Workbook != null)
                Archivo.Dispose();
        }

        public static void EscribirExcel(Duracion juego) {
            try {
                if (Archivo.Workbook != null) {

                    ExcelWorksheet worksheet = Archivo.Workbook.Worksheets[juego._Duracion.ToString()]; // Obtiene la hoja de duracion

                    int rowCount = worksheet.Dimension.Rows;

                    worksheet.Cells["A" + (rowCount + 1).ToString()].Value = juego.Juego;
                    worksheet.Cells["B" + (rowCount + 1).ToString()].Value = juego.Horas;
                    worksheet.Cells["D" + (rowCount + 1).ToString()].Value = "Pendiente";
                    worksheet.Cells["E" + (rowCount + 1).ToString()].Value = "2023";

                }
            }catch(Exception e) {
                throw new Exception("No se pudo escribir el excel.", e);
            }
        }

        public static void GuardarExcel() {
            try {
                Archivo.Save();
            }catch(Exception e){
                throw new Exception("No se pudo guardar el excel.", e);

            }
        }
    }
}
