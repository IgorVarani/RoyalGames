import { Fragment } from "react/jsx-runtime";
import styles from "./login.module.css"
import { ToastContainer } from "react-toastify/unstyled";

const Login = () => {
    return (
        <Fragment>
            <ToastContainer/>
            <main id={styles.main}>
                <img src="/imgs/login.svg" alt="Imagem de uma mulher combinando com o tema do site."/>
                <section id={styles.section}>
                    <div id={styles.campo_login}>
                        <img src="/imgs/logo.svg" alt="Logo do site."/>
                        <form id={styles.formulario}>
                            <div className={styles.campo_form}>
                                <label htmlFor="email">Email</label>
                                <input type="text" name="email" placeholder="email@exemplo.com"/>
                            </div>
                            
                            <div className={styles.campo_form}>
                                <label htmlFor="senha">Senha</label>
                                <input type="password" name="senha" placeholder="********"/>
                            </div>
                            
                            <div className={styles.campo_button}>    
                                <button>Entrar</button>
                            </div>
                        </form>
                    </div>
                </section>
            </main>
        </Fragment>
    );
}

export default Login;