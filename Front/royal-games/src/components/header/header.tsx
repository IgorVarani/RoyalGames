import styles from "./header.module.css"

const Header = () => {
    return (
        <header id={styles.header}>
            <div>
                <div>
                    <img src="/imgs/logo.svg" alt="Logo do site."/>
                </div>
                <div>
                    <a href="">Catálogo</a>
                    <button id={styles.botao} onClick={() => window.location.href = "/login"}>Login</button>
                </div>
            </div>
        </header>
    );
}

export default Header;