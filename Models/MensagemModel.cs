using Newtonsoft.Json;

namespace WeChip.Models
{
    public enum TipoMensagem
    {
        Informacao,

        Erro
    }

    public class MensagemModel
    {
        public TipoMensagem Tipo { get; set; }

        public string Texto { get; set; }

        public MensagemModel(string mensagem, TipoMensagem tipo = TipoMensagem.Informacao)
        {
            this.Tipo = tipo;
            this.Texto = mensagem;
        }

        public static string Serializar (string mensagem, TipoMensagem tipo = TipoMensagem.Informacao)
        {
            var MensagemModel = new MensagemModel(mensagem, tipo);
            return JsonConvert.SerializeObject(MensagemModel);
        }
        
        public static MensagemModel Desserializar (string MensagemString)
        {
            return JsonConvert.DeserializeObject<MensagemModel>(MensagemString);
        }
    }    
}