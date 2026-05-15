import { Fragment } from "react/jsx-runtime";
import styles from "./jogo-lista.module.css"
import Card from "../jogo-card/jogo-card";

const Lista = () => {
    return (
        <Fragment>
            <div className={styles.filtros}>
                <input type="text" name="Pesquisa" placeholder="Pesquise..." />
                <div className={styles.botoes}>
                    <button>Menor preço</button>
                    <button>Categoria</button>
                </div>
            </div>

            <ul className={styles.lista_jogo}>
                <Card />
                <Card />
                <Card />
            </ul>

            <nav className={styles.navegacao}>
                <button className={styles.navegacao_botao}>
                    <img src="../svg/seta-esquerda.svg" alt="" />
                </button>
            <ul>
                <li>1</li>
                <li>2</li>
                <li>3</li>
                <li>4</li>
                <li>5</li>
            </ul>
            <button className={styles.navegacao_botao}>
                <img src="../svg/seta-direita.svg" alt="" />
            </button>
            </nav>
        </Fragment>
    );
}

export default Lista;