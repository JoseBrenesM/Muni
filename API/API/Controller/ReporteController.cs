using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : ControllerBase, IReporteAPI
    {
        private readonly IReporteBW _reporteBW;

        public ReporteController(IReporteBW reporteBW)
        {
            _reporteBW = reporteBW;
        }

        [HttpGet("descargar")]
        public async Task<IActionResult> DescargarReporteHorarios([FromQuery] FiltroReporteHorario filtro)
        {
            var horarios = await _reporteBW.ObtenerReporteHorario(filtro);

            var archivoExcel = GenerarExcelDesdeHorarios(horarios);

            return File(archivoExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteHorarios.xlsx");
        }

        private byte[] GenerarExcelDesdeHorarios(List<HorarioDetalle> horarios)
        {
            using (var ms = new MemoryStream())
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Reporte de Horarios");

                worksheet.Cell(1, 1).Value = "Persona";
                worksheet.Cell(1, 2).Value = "Fecha";
                worksheet.Cell(1, 3).Value = "Entrada";
                worksheet.Cell(1, 4).Value = "Inicio Almuerzo";
                worksheet.Cell(1, 5).Value = "Fin Almuerzo";
                worksheet.Cell(1, 6).Value = "Salida";

                worksheet.Range(1, 1, 1, 6).Style.Font.Bold = true;

                for (int col = 1; col <= 6; col++)
                {
                    worksheet.Column(col).Width = 17.5;
                }

                int row = 2;
                foreach (var horario in horarios)
                {
                    worksheet.Cell(row, 1).Value = $"{horario.Nombre} {horario.Apellido}";
                    worksheet.Cell(row, 2).Value = horario.Fecha.ToString("dd/MM/yyyy");
                    worksheet.Cell(row, 3).Value = horario.HoraEntrada?.ToString("HH:mm") ?? "Aún sin marca";
                    worksheet.Cell(row, 4).Value = horario.HoraInicioAlmuerzo?.ToString("HH:mm") ?? "Aún sin marca";
                    worksheet.Cell(row, 5).Value = horario.HoraFinAlmuerzo?.ToString("HH:mm") ?? "Aún sin marca";
                    worksheet.Cell(row, 6).Value = horario.HoraSalida?.ToString("HH:mm") ?? "Aún sin marca";
                    row++;
                }

                // Solo aplicar estilos si hay datos
                if (horarios.Any())
                {
                    var dataRange = worksheet.Range(2, 1, row - 1, 6);
                    dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                }

                var fullRange = worksheet.Range(worksheet.FirstCellUsed(), worksheet.LastCellUsed());
                fullRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                fullRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                workbook.SaveAs(ms);
                return ms.ToArray();
            }
        }




    }


}
