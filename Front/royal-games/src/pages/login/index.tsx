import { Fragment } from "react/jsx-runtime";
import styles from "./login.module.css"
import { ToastContainer } from "react-toastify/unstyled";
import { login } from "../api/authService";
import { useState } from "react";
import { useRouter } from "next/router";
import { toast } from "react-toastify";

const Login = () => {

    const [email, setEmail] = useState<string>("");
    const [senha, setSenha] = useState<string>("");

    const router = useRouter();
    const notificao = (msg: string ) => toast.success(msg);
    const erro = (msg: string) => toast.error(msg);

    async function autenticar(e: React.FormEvent<HTMLFormElement>)
    {
        e.preventDefault();
        try
        {
            await login(email, senha);
            notificao("Login bem sucedido!");

            setTimeout(() => {
                router.push("/home");
            }, 2000);
        }
        catch(error: any)
        {
            erro(error.message);
        }
    }

    return (
        <Fragment>
            <ToastContainer/>
            <main id={styles.main}>
                <img src="/imgs/login.svg" alt="Imagem de uma mulher combinando com o tema do site."/>
                <section id={styles.section}>
                    <div id={styles.campo_login}>
                        <img src="/imgs/logo.svg" alt="Logo do site."/>
                        <form id={styles.formulario} onSubmit={autenticar}>
                            <div className={styles.campo_form}>
                                <label htmlFor="email">Email</label>
                                <input type="text" name="email" placeholder="email@exemplo.com"
                                value={email} onChange={(e) => setEmail(e.target.value)}/>
                            </div>
                            
                            <div className={styles.campo_form}>
                                <label htmlFor="senha">Senha</label>
                                <input type="password" name="senha" placeholder="********"
                                value={senha} onChange={(e) => setSenha(e.target.value)}/>
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