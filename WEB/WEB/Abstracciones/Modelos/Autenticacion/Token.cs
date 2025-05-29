namespace Abstracciones.Modelos.Autenticacion
{
    public class Token
    {
        public bool ValidacionExitosa { get; set; }
        public string AccessToken { get; set; }
        public string? MensajeError { get; set; }
    }
}