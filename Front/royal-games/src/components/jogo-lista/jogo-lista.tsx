import { Fragment, useEffect, useState } from "react";
import styles from "./jogo-lista.module.css";
import Card from "../jogo-card/jogo-card";
import { listarJogo } from "@/pages/api/jogoService";
import { verificarAutenticacao } from "@/utils/auth";

type Jogo =
{
    jogoID: number;
    nome: string;
    preco: number;
    imagemUrl: string;
}

const Lista = () => {

    const [jogos, setJogos] = useState<Jogo[]>([]);
    const estaLogado = verificarAutenticacao();

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
        console.log("Excluir jogo:", jogoID);
    }

    useEffect(() => {
        carregarJogos();
    }, []);

    return (
        <Fragment>
            <div className={styles.filtros}>
                <input type="text" name="Pesquisa" placeholder="Pesquise..." />
                <div className={styles.botoes}>
                    <button>Menor Preço</button>
                    <button>Categoria</button>
                </div>
            </div>

            <ul className={styles.lista_jogo}>
                {jogos.map((jogo) => (
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
                <button className={styles.navegacao_botao}>
                    <img src="/imgs/seta-esquerda.svg" alt="" />
                </button>
            <ul>
                <li>1</li>
                <li>2</li>
                <li>3</li>
                <li>4</li>
                <li>5</li>
            </ul>
            <button className={styles.navegacao_botao}>
                <img src="/imgs/seta-direita.svg" alt="" />
            </button>
            </nav>
        </Fragment>
    );
}

export default Lista;