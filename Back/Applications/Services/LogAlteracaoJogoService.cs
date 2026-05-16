using Royal_Games.Domains;
using Royal_Games.DTOs.LogAlteracaoJogoDto;
using Royal_Games.Interfaces;

namespace Royal_Games.Applications.Services
{
    public class LogAlteracaoJogoService
    {
        private readonly ILogAlteracaoJogoRepository _repository;

        public LogAlteracaoJogoService(ILogAlteracaoJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerLogJogoDto> Listar()
        {
            List<Log_AlteracaoJogo> logs = _repository.Listar();

            List<LerLogJogoDto> listaLogJogoDto = logs.Select(log => new LerLogJogoDto
            {
                Log_AlteracaoJogoID = log.Log_AlteracaoJogoID,
                JogoID = log.JogoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao = log.DataAlteracao
            }).ToList();

            return listaLogJogoDto;
        }

        public List<LerLogJogoDto> ListarPorJogo(int jogoId)
        {
            List<Log_AlteracaoJogo> logs = _repository.ListarPorJogo(jogoId);

            List<LerLogJogoDto> listaLogJogos = logs.Select(l => new LerLogJogoDto
            {
                Log_AlteracaoJogoID = l.Log_AlteracaoJogoID,
                JogoID = l.JogoID,
                NomeAnterior = l.NomeAnterior,
                PrecoAnterior = l.PrecoAnterior,
                DataAlteracao = l.DataAlteracao
            }).ToList();

            return listaLogJogos;
        }

    }
}
