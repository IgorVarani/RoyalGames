import React, { Fragment, useEffect, useState } from "react";
import styles from "./jogo.module.css";

import Header from "@/components/header/header";
import Footer from "@/components/footer/footer";
import Lista from "@/components/jogo-lista/jogo-lista";
import Toast from "@/components/toast/toast";

import { useRouter } from "next/router";

import {
  listarClassificacaoIndicativa,
  listarGenero,
  listarPlataforma,
} from "../../api/genericService";

import {
  cadastrarJogo,
  editarJogo,
  listarPorId,
} from "../../api/jogoService";

import { erro, notificao } from "@/utils/toast";
import { verificarAutenticacao } from "@/utils/auth";

interface ClassificacaoIndicativa {
  classificacaoIndicativaId: number;
  classificacao: string;
}

interface Genero {
  generoId: number;
  nome: string;
}

interface Plataforma {
  plataformaId: number;
  nome: string;
}

const Cadastrar = () => {
  const [classificacoes, setClassificacoes] = useState<
    ClassificacaoIndicativa[]
  >([]);

  const [generos, setGeneros] = useState<Genero[]>([]);
  const [plataformas, setPlataformas] = useState<Plataforma[]>([]);

  const [nome, setNome] = useState("");
  const [descricao, setDescricao] = useState("");
  const [preco, setPreco] = useState("");
  const [imagem, setImagem] = useState<File | null>(null);

  const [classificacoesSelecionadas, setClassificacoesSelecionadas] =
    useState<number[]>([]);

  const [generosSelecionados, setGenerosSelecionados] =
    useState<number[]>([]);

  const [plataformasSelecionadas, setPlataformasSelecionadas] =
    useState<number[]>([]);

  const [estaAutenticado, setEstaAutenticado] =
    useState(false);

  const router = useRouter();

  const id =
    typeof router.query.id === "string"
      ? router.query.id
      : undefined;

  const telaEditar = id !== undefined;

  async function listarClassificacaoEmJogo() {
    try {
      const lista =
        await listarClassificacaoIndicativa();

      setClassificacoes(lista.data);
    } catch (error: any) {
      erro(error.message);
    }
  }

  async function listarGeneroEmJogo() {
    try {
      const lista = await listarGenero();

      setGeneros(lista.data);
    } catch (error: any) {
      erro(error.message);
    }
  }

  async function listarPlataformaEmJogo() {
    try {
      const lista = await listarPlataforma();

      setPlataformas(lista.data);
    } catch (error: any) {
      erro(error.message);
    }
  }

  async function carregarInformacoes() {
    if (!id) return;

    try {
      const jogo = await listarPorId(Number(id));

      setNome(jogo.nome || "");
      setDescricao(jogo.descricao || "");
      setPreco(jogo.preco || "");

      setClassificacoesSelecionadas(
        jogo.classificacaoId
          ? [jogo.classificacaoId]
          : []
      );

      setGenerosSelecionados(
        jogo.generoID || []
      );

      setPlataformasSelecionadas(
        jogo.plataformaID || []
      );
    } catch (error: any) {
      erro(error.message);
    }
  }

  async function salvarJogo(
    e: React.FormEvent<HTMLFormElement>
  ) {
    e.preventDefault();

    try {
      if (
        classificacoesSelecionadas.length === 0
      ) {
        erro(
          "Selecione uma classificação indicativa."
        );

        return;
      }

      const dados = {
        nome,
        descricao,
        preco,
        imagem,

        generoID: generosSelecionados,

        plataformaID:
          plataformasSelecionadas,

        classificacaoId:
          classificacoesSelecionadas[0],
      };

      if (telaEditar) {
        await editarJogo(Number(id), dados);

        notificao(
          "Jogo editado com sucesso!"
        );
      } else {
        await cadastrarJogo(dados);

        notificao(
          "Jogo cadastrado com sucesso!"
        );
      }

      router.push("/home");
    } catch (error: any) {
      erro(error.message);
    }
  }

  useEffect(() => {
    if (!router.isReady) return;

    if (!verificarAutenticacao()) {
      router.push("/home");
      return;
    }

    setEstaAutenticado(true);

    listarClassificacaoEmJogo();
    listarGeneroEmJogo();
    listarPlataformaEmJogo();

    if (telaEditar) {
      carregarInformacoes();
    }
  }, [router.isReady, id]);

  if (!estaAutenticado) return null;

  return (
    <Fragment>
      <Header page="cadastrar" />

      <Toast />

      <main id={styles.main}>
        <section id={styles.section}>
          <form
            id={styles.campo_cadastro}
            onSubmit={salvarJogo}
          >
            <h1>
              {telaEditar
                ? "Editar Jogo Existente"
                : "Cadastrar Novo Jogo"}
            </h1>

            <hr />

            <div id={styles.formulario}>
              <div id={styles.esquerda}>

                <div className={styles.linha}>
                  <div className={styles.campo}>
                    <label>Nome</label>

                    <input
                      type="text"
                      value={nome}
                      onChange={(e) =>
                        setNome(e.target.value)
                      }
                      required
                    />
                  </div>

                  <div className={styles.campo}>
                    <label>Preço</label>

                    <input
                      type="text"
                      value={preco}
                      onChange={(e) =>
                        setPreco(e.target.value)
                      }
                      required
                    />
                  </div>
                </div>

                <div className={styles.linha}>
                  <div className={styles.campo}>
                    <label>Gênero</label>

                    <select
                      multiple
                      size={4}
                      value={generosSelecionados.map(
                        String
                      )}
                      onChange={(e) =>
                        setGenerosSelecionados(
                          Array.from(
                            e.target.selectedOptions
                          ).map((o) =>
                            Number(o.value)
                          )
                        )
                      }
                    >
                      {generos.map((g) => (
                        <option
                          key={g.generoId}
                          value={g.generoId}
                        >
                          {g.nome}
                        </option>
                      ))}
                    </select>
                  </div>

                  <div className={styles.campo}>
                    <label>Plataforma</label>

                    <select
                      multiple
                      size={4}
                      value={plataformasSelecionadas.map(
                        String
                      )}
                      onChange={(e) =>
                        setPlataformasSelecionadas(
                          Array.from(
                            e.target.selectedOptions
                          ).map((o) =>
                            Number(o.value)
                          )
                        )
                      }
                    >
                      {plataformas.map((p) => (
                        <option
                          key={p.plataformaId}
                          value={p.plataformaId}
                        >
                          {p.nome}
                        </option>
                      ))}
                    </select>
                  </div>
                </div>

                <div className={styles.linha}>
                  <div className={styles.campo}>
                    <label>
                      Classificação Indicativa
                    </label>

                    <select
                      value={
                        classificacoesSelecionadas[0]?.toString() ||
                        ""
                      }
                      onChange={(e) =>
                        setClassificacoesSelecionadas([
                          Number(e.target.value),
                        ])
                      }
                      required
                    >
                      <option value="">
                        Selecione
                      </option>

                      {classificacoes.map((c) => (
                        <option
                          key={
                            c.classificacaoIndicativaId
                          }
                          value={
                            c.classificacaoIndicativaId
                          }
                        >
                          {c.classificacao}
                        </option>
                      ))}
                    </select>
                  </div>

                  <div className={styles.campo}>
                    <label>Imagem</label>

                    <input
                      type="file"
                      accept="image/*"
                      onChange={(e) => {
                        if (
                          e.target.files &&
                          e.target.files[0]
                        ) {
                          setImagem(
                            e.target.files[0]
                          );
                        }
                      }}
                    />
                  </div>
                </div>

              </div>

              <div id={styles.direita}>
                <label>Descrição</label>

                <textarea
                  value={descricao}
                  onChange={(e) =>
                    setDescricao(e.target.value)
                  }
                  required
                />
              </div>
            </div>

            <button
              type="submit"
              id={styles.botao}
            >
              {telaEditar
                ? "Salvar"
                : "Cadastrar"}
            </button>
          </form>
        </section>

        <section
          id="lista"
          className={styles.lista}
        >
          <h2>Lista de Jogos</h2>

          <hr id={styles.linha_h2} />

          <Lista />
        </section>
      </main>

      <Footer />
    </Fragment>
  );
};

export default Cadastrar;