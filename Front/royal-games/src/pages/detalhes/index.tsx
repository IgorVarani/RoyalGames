import { ToastContainer } from "react-toastify";
import { Fragment } from "react/jsx-runtime";
import styles from "./detalhes.module.css"
import Header from "@/components/header/header";
import Footer from "@/components/footer/footer";

const Detalhes = () => {
    return (
        <Fragment>
            <ToastContainer/>
            <Header/>
            <main id={styles.main}>
                <section id={styles.section}>
                    <h1>Detalhes do Jogo</h1>
                    <hr />
                    <div id={styles.container_acima}>
                        <img src="/imgs/jogo-exemplo.svg" alt="" />
                        <div>    
                            <h2>League of Legends</h2>
                            <p>
                                League of Legends (LoL) é um jogo eletrônico do gênero MOBA (Multiplayer Online Battle Arena) 
                                onde duas equipes de cinco jogadores competem entre si com o objetivo de destruir a base adversária. 
                                Cada jogador controla um campeão com habilidades únicas, exigindo estratégia, trabalho em equipe e 
                                tomada de decisões rápidas durante as partidas.
                            </p>
                            <p>
                                O jogo possui diversos modos, mapas e estilos de jogo, além de oferecer atualizações frequentes com 
                                novos personagens, eventos e ajustes de balanceamento. League of Legends é conhecido pelo seu cenário 
                                competitivo mundial, reunindo milhões de jogadores e campeonatos profissionais ao redor do mundo.
                            </p>
                        </div>
                    </div>

                    <div id={styles.container_abaixo}>
                        <aside id={styles.esquerda}>
                            <h3 className={styles.esquerda_h3}>Classificação Indicativa:</h3>
                            <p className={styles.esquerda_p}>18 Anos</p>

                            <h3 className={styles.esquerda_h3}>Preço:</h3>
                            <p className={styles.esquerda_p}>R$00,00</p>
                        </aside>

                        <aside id={styles.direita}>
                            <h3 className={styles.direita_h3}>Gênero(s)</h3>
                            <ul>
                                <li>MOBA</li>
                            </ul>

                            <h3 className={styles.direita_h3}>Plataforma(s)</h3>
                            <ul>
                                <li>PC</li>
                            </ul>
                        </aside>
                    </div>
                </section>
            </main>
            <Footer/>
        </Fragment>
    );
}

export default Detalhes;