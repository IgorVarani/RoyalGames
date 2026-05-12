import styles from "./footer.module.css"

const Footer = () => {
    return (
        <footer id={styles.footer}>
            <div id={styles.container}>
                <div>
                    <img src="/imgs/logo.svg" alt="Logo do site."/>
                </div>

                <div>
                    <ul id={styles.ul}>
                        <li><a href="https://www.gmail.com">royalgames@email.com</a></li>
                        <li><a href="">(11)99999-9999</a></li>
                        <li><a href="https://www.instagram.com">@RoyalGames</a></li>
                    </ul>
                </div>
            </div>
            <hr id={styles.linha}></hr>
            <p>Copyright ©2026 Royal Games | Todos os direitos reservados.</p>
        </footer>
    );
}

export default Footer;