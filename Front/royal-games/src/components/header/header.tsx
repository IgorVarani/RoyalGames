import Link from "next/link";
import styles from "./header.module.css"

type HeaderProps = {
    page?: string;
}

const Header = ({ page } : HeaderProps) => {
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
                    <button id={styles.botao} onClick={() => window.location.href = "/login"}>Login</button>
                </div>
            </div>
        </header>
    );
}

export default Header;