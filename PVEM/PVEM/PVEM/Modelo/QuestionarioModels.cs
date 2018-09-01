using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PVEM.Modelo
{
    /*
    public class QuestionarioForm
    {
        public int IdtQuestionario { get; set; }

        public short Ano { get; set; }

        public Ciclo Ciclo { get; set; }

        public FrenteAtuacao FrenteAtuacao { get; set; }

        public string NomQuestionario { get; set; }

        public List<Competencia> Competencias { get; set; }

        public List<QuestionarioCompetenciasForms> Itens { get; set; }

        public List<QuestionarioDetalheForm> Detalhes { get; set; }
    }*/

        /*
    public class QuestionarioDetalheForm
    {
        public PerfilRespondenteForm Perfil { get; set; }

        public FrenteAtuacao FrenteAtuacao { get; set; }

        public short Tipo { get; set; }

        public string NomQuestionario { get; set; }
    }
    */
    /*
    public class QuestionarioCompetenciasForms
    {

        public int IdtCompetencia { get; set; }

        public string DescCompetencia { get; set; }

        public List<QuestionarioHabilidadeForm> Habilidades { get; set; }

    }

    public class QuestionarioHabilidadeForm
    {

        public int IdtHabilidade { get; set; }

        public string DescHabilidade { get; set; }

        public List<PerguntasForm> Perguntas { get; set; }

    }

    public class PerguntasForm
    {

        public int? IdtQuestionarioPergunta { get; set; }

        public int IdtRelacionamentoHabilidadePergunta { get; set; }

        public string DescPergunta { get; set; }

        public short Etapa { get; set; }

        public TipoResposta TipoResposta { get; set; }

        public List<short> TiposDireto { get; set; }

        public List<short> TiposIndireto { get; set; }

    }

    public class PerguntasRelacionamentoQuestionario
    {
        public int IdtCompetencia { get; set; }

        public string DescCompetencia { get; set; }

        public int IdtHabilidade { get; set; }

        public string DescHabilidade { get; set; }

        public string DescPergunta { get; set; }

        public short Etapa { get; set; }

        public short? TipoResposta { get; set; }

        public int IdtRelacionamentoHabilidadePergunta { get; set; }

    }

    public class QuestionarioUnico
    {
        public Ciclo Ciclo { get; set; }

        public short Ano { get; set; }

        public FrenteAtuacao FrenteAtuacao { get; set; }
    }

    public class GerarQuestionarioForm
    {
        public Ciclo Ciclo { get; set; }

        public short Ano { get; set; }

        public FrenteAtuacao FrenteAtuacao { get; set; }

        public Questionario Questionario { get; set; }

        public PerfilRespondenteForm PerfilRespondente { get; set; }

        public FrenteAtuacao FrenteAtuacaoAvalia { get; set; }

        public short Tipo { get; set; }

        public Municipio Municipio { get; set; }

        public bool Impresso { get; set; }
    }

    public class PerfilRespondenteForm
    {
        public short IdtPerfil { get; set; }

        public string DescPerfil { get; set; }
    }
    */

    public class RespostaQuestionarioForm
    {
        public Int64 IdtRespostaPrincipal { get; set; }

        public Int64 IdtRespostaQuestionario { get; set; }

        public short PerfilRespondente { get; set; }

        public int IdtFrenteAtuacaoAvalia { get; set; }

        public short Tipo { get; set; }

        public int IdtCabecalhoQuestionario { get; set; }

        public string NomeRelatorio { get; set; }

        public string TextoCabecalho { get; set; }

        public Municipio Municipio { get; set; }

        public List<RespostaPerfilForm> RespostasPerfil { get; set; }

        public List<CompetenciaForm> Competencias { get; set; }

        public Int64 Formulario { get; set; }

        public short Ano { get; set; }

        public int IdtCiclo { get; set; }

    }

    public class CompetenciaForm
    {
        public int IdtCompetencia { get; set; }

        public string DescCompetencia { get; set; }

        public List<RespostaForm> Respostas { get; set; }

        public int Ordem { get; set; }
    }

    public class RespostaForm
    {
        public int IdtQuestionarioPergunta { get; set; }

        public string DescPergunta { get; set; }

        public int IdtOpcaoTipoResposta { get; set; }

        public int IdtTipoResposta { get; set; }

        public int OrdemHabilidade { get; set; }

        public int OrdemPergunta { get; set; }
    }

    public class RespostaPerfilForm
    {
        public int IdtItemCabecalhoQuestionario { get; set; }

        public string DescPergunta { get; set; }

        public int IdtAlternativaICQ { get; set; }
    }

    public class GerarQuestionarioListaForm
    {
        public Int64 IdtRespostaQuestionario { get; set; }

        public string NomQuestionario  { get; set; }

        public int Tipo { get; set; }

        public int Ano { get; set; }

        public string Ciclo { get; set; }

        public string Perfil { get; set; }

        public string Frente { get; set; }

        public string AvaliaFrente { get; set; }

    }

}
