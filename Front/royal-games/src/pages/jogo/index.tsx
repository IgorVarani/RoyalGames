import { Fragment } from "react/jsx-runtime";
import styles from "./jogo.module.css";
import { ToastContainer } from "react-toastify";
import Header from "@/components/header/header";
import Footer from "@/components/footer/footer";
import Lista from "@/components/jogo-lista/jogo-lista";

const Cadastrar = () => {
    return (
        <Fragment>
            <Header page="cadastrar"/>
            <ToastContainer />
            <main id={styles.main}>
                <section id={styles.section}>
                    <div id={styles.campo_cadastro}>
                        <h1>Cadastrar Novo Jogo</h1>
                        <hr />
                        <div id={styles.formulario}>
                            <div id={styles.esquerda}>
                                <div className={styles.campo}>
                                    <label>Nome</label>
                                    <input type="text" />
                                </div>

                                <div className={styles.linha}>

                                    <div className={styles.campo}>
                                        <label>Valor</label>
                                        <input type="text" />
                                    </div>

                                    <div className={styles.campo}>
                                        <label>Gênero</label>
                                        <select>
                                            <option></option>
                                        </select>
                                    </div>

                                    <div className={styles.campo}>
                                        <label>Classificação Indicativa</label>
                                        <select>
                                            <option></option>
                                        </select>
                                    </div>
                                </div>

                                <div className={styles.linha}>

                                    <div className={styles.campo}>
                                        <label>Plataforma</label>
                                        <select>
                                            <option></option>
                                        </select>
                                    </div>

                                    <div className={styles.campo}>
                                        <label>Imagem</label>
                                        <input type="file" />
                                    </div>
                                </div>
                            </div>

                            <div id={styles.direita}>
                                <label>Descrição</label>
                                <textarea></textarea>
                            </div>

                        </div>
                        <button id={styles.botao}>Cadastrar</button>
                    </div>
                </section>

                <section id="lista" className={styles.lista}>
                    <h2>Lista de Jogos</h2>
                    <hr id={styles.linha_h2}></hr>
                    <Lista/>
                </section>
            </main>
            <Footer />
        </Fragment>
    );
}

export default Cadastrar;