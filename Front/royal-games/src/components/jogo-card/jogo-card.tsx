import styles from "./jogo-card.module.css"

const Card = () =>{
    return(
        <article className={styles.card}>
            <img src="/imgs/jogo-exemplo.svg" alt="" />
            <span>League of Legends</span>
            <p>R$ 00,00</p>
            <button>Detalhes</button>
        </article>
    );
}

export default Card;