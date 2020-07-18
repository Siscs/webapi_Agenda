namespace Siscs.Agenda.Api.Services
{
    public class TokenConfig
    {

        public string Secret { get; set; }
        public int ExpiracaoHoras { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }

    }
}