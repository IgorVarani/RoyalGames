import { Fragment, useEffect, useState } from "react";
import styles from "./jogo-lista.module.css";
import Card from "../jogo-card/jogo-card";
import { deletarJogo as deletarJogoService, listarJogo } from "@/pages/api/jogoService";
import { verificarAutenticacao } from "@/utils/auth";
import { toastConfirmarExclusao, notificao, erro } from "@/utils/toast";
import Toast from "../toast/toast";

type Jogo =
{
    jogoID: number;
    nome: string;
    preco: number;
    imagemUrl: string;
}

const Lista = () => {

    const [jogos, setJogos] = useState<Jogo[]>([]);
    const [paginaAtual, setPaginaAtual] = useState(1);
    const [ordenacao, setOrdenacao] = useState("");
    const [pesquisa, setPesquisa] = useState("");
    const estaLogado = verificarAutenticacao();
    const jogosPorPagina = 3;

    async function carregarJogos()
    {
        try
        {
            const lista = await listarJogo();
            setJogos(lista);
        }
        catch(error)
        {
            console.log(error);
        }
    }   

    function deletarJogo(jogoID: number)
    {
        toastConfirmarExclusao(async () => {
            try
            {
                await deletarJogoService(jogoID);

                setJogos((jogosAnteriores) =>
                    jogosAnteriores.filter((jogo) => jogo.jogoID !== jogoID));

                setPaginaAtual(1);
                notificao("Jogo excluído com sucesso!");
            }
            catch(error)
            {
                erro("Erro ao excluir jogo.");
            }
        });
    }

    useEffect(() => {
        carregarJogos();
    }, []);
    
    const jogosFiltrados = jogos.filter((jogo) =>
    jogo.nome.toLowerCase().includes(pesquisa.toLowerCase()));

    const jogosOrdenados = [...jogosFiltrados];

    if (ordenacao === "menor") { jogosOrdenados.sort((a, b) => a.preco - b.preco); }
    if (ordenacao === "maior") { jogosOrdenados.sort((a, b) => b.preco - a.preco); }
    if (ordenacao === "alfabetica") { jogosOrdenados.sort((a, b) => a.nome.localeCompare(b.nome)); }

    const indiceInicial = (paginaAtual - 1) * jogosPorPagina;
    const indiceFinal = indiceInicial + jogosPorPagina;
    const jogosPaginados = jogosOrdenados.slice(indiceInicial, indiceFinal);
    const totalPaginas = Math.ceil(jogosOrdenados.length / jogosPorPagina);

    return (
        <Fragment>
            <Toast/>
            <div className={styles.filtros}>
                <input type="text" name="Pesquisa" placeholder="Pesquise..." value={pesquisa}
                onChange={(e) => { setPesquisa(e.target.value); setPaginaAtual(1); }}/>
                <div className={styles.botoes}>
                    <select value={ordenacao} onChange={(e) => {setOrdenacao(e.target.value); setPaginaAtual(1);}}>
                        <option value="">Ordem Padrão</option>
                        <option value="menor">Menor Preço</option>
                        <option value="maior">Maior Preço</option>
                        <option value="alfabetica">A-Z</option>
                    </select>
                </div>
            </div>

            <ul className={styles.lista_jogo}>
                {jogosPaginados.map((jogo) => (
                <Card
                    key={jogo.jogoID}
                    imagem={jogo.imagemUrl}
                    nome={jogo.nome}
                    preco={jogo.preco}
                    jogoID={jogo.jogoID}
                    onDelete={deletarJogo}
                    estaLogado={estaLogado}
                />))}
            </ul>

            <nav className={styles.navegacao}>
                <ul>
                    {Array.from({ length: totalPaginas }, (_, index) => (
                    <li key={index + 1} onClick={() => setPaginaAtual(index + 1)}>
                        {index + 1}
                    </li>))}
                </ul>
            </nav>
        </Fragment>
    );
}

export default Lista;