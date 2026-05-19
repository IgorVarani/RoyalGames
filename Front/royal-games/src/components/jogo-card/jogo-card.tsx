import Link from "next/link";
import styles from "./jogo-card.module.css"
import { formatarPreco } from "@/utils/formatacao";

type Jogo =
{
    imagem: string,
    nome: string,
    preco: number,
    jogoID: number,
    onDelete: (jogoId: number) => void,
    estaLogado: boolean,
}

const Card = ({imagem, nome, preco, jogoID, onDelete, estaLogado} : Jogo) => {
    return(
        <article className={styles.card}>
            <Link href={"/detalhes/" + jogoID}>
                <img src={imagem} alt="Jogo no catálogo da loja." />
            </Link>
            <span>{nome}</span>
            <p>{formatarPreco(preco ?? 0)}</p>
            {!estaLogado && (
                <>
                    <Link href={"/detalhes/" + jogoID}>
                        <button>Detalhes</button>
                    </Link>
                </>
            )}
            {estaLogado && (
                <div className={styles.acoes}>
                    <button onClick={() => onDelete(jogoID)}>Excluir</button>

                    <Link href={"/jogo/" + jogoID}>
                        <button>Editar</button>
                    </Link>
                </div>
            )}
        </article>
    );
}

export default Card;