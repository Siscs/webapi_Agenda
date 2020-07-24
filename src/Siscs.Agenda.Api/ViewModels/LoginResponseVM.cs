namespace Siscs.Agenda.Api.ViewModels
{
    public class LoginResponseVM
    {
        public string Token{ get; set; }
        public int ExpiraEm { get; set; }
        public TokenUsuarioVM Usuario { get; set; }
    }
}