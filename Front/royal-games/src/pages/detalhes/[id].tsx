import { ToastContainer } from "react-toastify";
import { Fragment, useEffect, useState } from "react";
import styles from "./detalhes.module.css";
import Header from "@/components/header/header";
import Footer from "@/components/footer/footer";
import { useRouter } from "next/router";
import { listarPorId } from "../api/jogoService";
import { formatarPreco } from "@/utils/formatacao";

type jogo =
{
    nome: string;
    descricao: string;
    preco: number;
    imagemUrl: string;
    classificacao?: string;
    generos?: string[];
    plataformas?: string[];
}

const Detalhes = () => {

    const [jogo, setJogo] = useState<jogo | null>(null);

    const router = useRouter();
    const { id } = router.query;

    async function carregarJogo()
    {
        if(!id) return;

        try
        {
            const response = await listarPorId(Number(id));
            setJogo(response);
            console.log(response);
        }
        catch(error)
        {
            console.log(error);
        }
    }

    useEffect(() => {
    if(router.isReady)
    {
        carregarJogo();
    }
    }, [router.isReady, id]);

    if(!jogo)
    {
        return null;
    }

    return (
        <Fragment>
            <ToastContainer/>
            <Header page="detalhes"/>
            <main id={styles.main}>
                <section id={styles.section}>
                    <h1>Detalhes do Jogo</h1>
                    <hr />
                    <div id={styles.container_acima}>
                        <img src={jogo.imagemUrl} alt={jogo.nome} />
                        <div>    
                            <h2>{jogo.nome}</h2>
                            <p>{jogo.descricao}</p>
                        </div>
                    </div>

                    <div id={styles.container_abaixo}>
                        <aside id={styles.esquerda}>
                            <div className={styles.info}>    
                                <h3 className={styles.esquerda_h3}>Classificação Indicativa:</h3>
                                <p className={styles.esquerda_p}>{jogo.classificacao}</p>
                            </div>

                            <div className={styles.info}>    
                                <h3 className={styles.esquerda_h3}>Preço:</h3>
                                <p className={styles.esquerda_p}>{formatarPreco(jogo.preco)}</p>
                            </div>
                        </aside>

                        <aside id={styles.direita}>
                            <div className={styles.info}>
                                <h3 className={styles.direita_h3}>Gênero(s):</h3>
                                <ul>
                                    {jogo.generos?.map((genero) => (<li key={genero}>{genero}</li>))}
                                </ul>
                            </div>

                            <div className={styles.info}>
                                <h3 className={styles.direita_h3}>Plataforma(s):</h3>
                                <ul>
                                    {jogo.plataformas?.map((plataforma) => (<li key={plataforma}>{plataforma}</li>))}
                                </ul>
                            </div>
                        </aside>
                    </div>
                </section>
            </main>
            <Footer/>
        </Fragment>
    );
}

export default Detalhes;