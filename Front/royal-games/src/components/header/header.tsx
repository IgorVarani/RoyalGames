import Link from "next/link";
import { useEffect, useState } from "react";
import styles from "./header.module.css"
import { logout } from "@/pages/api/authService";
import { verificarAutenticacao } from "@/utils/auth";
import { useRouter } from "next/router";

type HeaderProps = {
    page?: string;
}

const Header = ({ page } : HeaderProps) => {

    const [estaLogado, setEstaLogado] = useState(false);
    const router = useRouter();

    useEffect(() => {setEstaLogado(verificarAutenticacao()); }, []);

    return (
        <header id={styles.header}>
            <div>
                <div>
                    <Link href="/home">
                        <img src="/imgs/logo.svg" alt="Logo do site."/>
                    </Link>
                </div>
                <div>
                    {page === "home" && (<a href="#catalogo">Catálogo</a>)}
                    {page === "detalhes" && (<Link href="/jogo" className={styles.link}>Cadastrar Jogos</Link>)}
                    {page === "cadastrar" && (<a href="#lista">Lista de Jogos</a>)}
                    {!estaLogado && (<button id={styles.botao} onClick={() => router.push("/login")}>Login</button>)}
                    {estaLogado && (<button id={styles.botao} onClick={() => {logout(); setEstaLogado(false); router.push("/home");}}>Logout</button>)}
                </div>
            </div>
        </header>
    );
}

export default Header;