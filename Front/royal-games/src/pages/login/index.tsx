import { Fragment } from "react/jsx-runtime";
import styles from "./login.module.css"
import { login } from "../api/authService";
import { useState } from "react";
import { useRouter } from "next/router";
import Link from "next/link";
import { sucesso, erro } from "@/utils/toast";

const Login = () => {

    const [email, setEmail] = useState<string>("");
    const [senha, setSenha] = useState<string>("");
    const router = useRouter();
    const [loading, setLoading] = useState(false);

    async function autenticar(e: React.FormEvent<HTMLFormElement>)
    {
        e.preventDefault();
        if (loading) return;

        try
        {
            setLoading(true);
            await login(email, senha);

            sucesso("Login bem sucedido!", () => router.push("/home"));
        }
        catch (error: any)
        {
            erro(error.message);
            setLoading(false);
        }
    }

    return (
        <Fragment>
            <main id={styles.main}>
                <img className={styles.imagem} src="/imgs/login.svg" alt="Imagem de uma mulher combinando com o tema do site."/>
                <section id={styles.section}>
                    <div id={styles.campo_login}>
                        <div>
                            <Link href="/home">
                                <img src="/imgs/logo.svg" alt="Logo do site."/>
                            </Link>
                        </div>
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
                                <button type="submit" disabled={loading}>
                                    {loading ? "Entrando..." : "Entrar"}
                                </button>
                            </div>
                        </form>
                    </div>
                </section>
            </main>
        </Fragment>
    );
}

export default Login;