import { api } from "./api";

type JogoForm =
{
    nome: string,
    descricao: string,
    preco: string,
    imagem: File | null,
    generoID: number[],
    plataformaID: number[],

    //? A escrita do "Id" é diferente dos outros porque o backend espera dessa forma.
    classificacaoId: number[],
}

interface JogoList
{
    nome: string,
    descricao: string,
    preco: string,
    imagemUrl: string,
    statusJogo: boolean,
    generoID: number[],
    plataformaID: number[],

    //? A escrita do "Id" é diferente dos outros porque o backend espera dessa forma.
    classificacaoId: number[],
}

export async function cadastrarJogo(dados: JogoForm)
{
    try
    {
        const formData = new FormData();
        formData.append("nome", dados.nome);
        formData.append("descricao", dados.descricao);
        formData.append("preco", dados.preco);
        if(dados.imagem)
        {
            formData.append("imagem", dados.imagem);
        }
        dados.generoID.forEach((id) => formData.append("generoID", id.toString()));
        dados.plataformaID.forEach((id) => formData.append("plataformaID", id.toString()));
        dados.classificacaoId.forEach((id) => formData.append("classificacaoId", id.toString()));

        await api.post("Jogo", formData);
        console.log("Jogo cadastrado com sucesso!");
    }
    catch(error: any)
    {
        throw new Error(error.response.data);
    }
}

export async function listarJogo()
{
    try
    {
        const response = await api.get("Jogo");

        const jogosAtivos = response.data.filter
        (
            (jogo: JogoList) => jogo.statusJogo === true
        );

        const jogos = jogosAtivos.map((jogo: JogoList) => ({
            ...jogo,
            imagemUrl: `${api.defaults.baseURL}${jogo.imagemUrl}`
        }));

        return jogos;
    }
    catch(error: any)
    {
        throw new Error(error.response.data);
    }
}

export async function listarPorId(id: number)
{
    try
    {
        const response = await api.get("Jogo/" + id);

        const jogo =
        {
            ...response.data,
            imagemUrl: `${api.defaults.baseURL}${response.data.imagemUrl}`
        };

        return jogo;
    }
    catch(error: any)
    {
        throw new Error(error.response.data);
    }
}

export async function editarJogo(jogoId : number, dados : JogoForm)
{
    try
    {
        const formData = new FormData();
        formData.append("nome", dados.nome);
        formData.append("descricao", dados.descricao);
        formData.append("preco", dados.preco);
        if(dados.imagem)
        {
            formData.append("imagem", dados.imagem);
        }
        dados.generoID.forEach((id) => formData.append("generoID", id.toString()));
        dados.plataformaID.forEach((id) => formData.append("plataformaID", id.toString()));
        dados.classificacaoId.forEach((id) => formData.append("classificacaoId", id.toString()));

        await api.put("Jogo/" + jogoId, formData);
    }
    catch(error: any)
    {
        throw new Error(error.response.data);
    }
}