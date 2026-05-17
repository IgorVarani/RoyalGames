import { Fragment } from "react/jsx-runtime";
import styles from "./home.module.css"
import { ToastContainer } from "react-toastify/unstyled";
import Footer from "@/components/footer/footer";
import Header from "@/components/header/header";
import Lista from "@/components/jogo-lista/jogo-lista";

const Home = () => {
    return (
        <Fragment>
            <Header page="home"/>
            <ToastContainer/>
            <main id={styles.main}>
                <section id={styles.banner}>
                    <div id={styles.banner_esquerda}>
                        <h1>Conheça nossos jogos!</h1>
                        <p>
                            Navegue por títulos de todas as gerações, descubra plataformas, 
                            gêneros e detalhes completos antes de escolher sua próxima aventura. 
                            Seu próximo jogo favorito começa aqui.
                        </p>
                    </div>
                    <div id={styles.banner_direita}>
                        <img src="/imgs/home.svg" alt="Imagem de uma mulher em um estilo cyberpunk." />
                    </div>
                </section>

                <section id="catalogo" className={styles.catalogo}>
                    <h2>Catálogo de Jogos</h2>
                    <hr id={styles.linha_h2}></hr>
                    <Lista/>
                </section>

                <section id={styles.comportamento}>
                        <h3>Jogos online podem afetar o comportamento humano?</h3>
                        <hr id={styles.linha_h3}></hr>
                    <div id={styles.comportamento_container}>
                        <img src="/imgs/lol.svg" alt="Imagem do jogo League of Legends." />
                        <img src="/imgs/cs.svg" alt="Imagem do jogo Counter-Strike." />
                    </div>
                    <p className={styles.comportamento_texto}>Estudos indicam que jogos podem alterar o comportamento humano...</p>
                    <p className={styles.comportamento_texto}>Principalmente quando o time resolve testar sua paciência em plena partida ranqueada.</p>
                </section>
            </main>
            <Footer/>
        </Fragment>
    );
}

export default Home;