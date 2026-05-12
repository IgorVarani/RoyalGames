import { Fragment } from "react/jsx-runtime";
import styles from "./home.module.css"
import { ToastContainer } from "react-toastify/unstyled";
import Footer from "@/components/footer/footer";
import Header from "@/components/header/header";

const Home = () => {
    return (
        <Fragment>
            <Header/>
            <ToastContainer/>
            <main id={styles.main}>
                <section>
                    <div>
                        <h1>Conheça nossos jogos!</h1>
                        <p>
                            Navegue por títulos de todas as gerações, descubra plataformas, 
                            gêneros e detalhes completos antes de escolher sua próxima aventura. 
                            Seu próximo jogo favorito começa aqui.
                        </p>
                    </div>
                    <div>
                        <img src="" alt="Imagem de uma mulher em um estilo cyberpunk." />
                    </div>
                </section>

                <section>
                    <h2>Catálogo de Jogos</h2>
                    <hr id={styles.linha_h2}></hr>
                </section>

                <section>
                    <h3>Jogos online podem afetar o comportamento humano?</h3>
                    <hr id={styles.linha_h3}></hr>
                    <div id={styles.container}>
                        <img src="" alt="Imagem do jogo League of Legends." />
                        <img src="" alt="Imagem do jogo Counter-Strike." />
                    </div>
                    <p>Estudos indicam que jogos podem alterar o comportamento humano...</p>
                    <p>Principalmente quando o time resolve testar sua paciência em plena partida ranqueada.</p>
                </section>
            </main>
            <Footer/>
        </Fragment>
    );
}

export default Home;