import { api } from "./api";

export async function listarClassificacaoIndicativa()
{
    try
    {
        const response = await api.get("ClassificacaoIndicativa");
        return response;
    }
    catch(error: any)
    {
        throw new Error(error.response.data);
    }
}

export async function listarGenero()
{
    try
    {
        const response = await api.get("Genero");
        return response;
    }
    catch(error: any)
    {
        throw new Error(error.response.data);
    }
}

export async function listarPlataforma()
{
    try
    {
        const response = await api.get("Plataforma");
        return response;
    }
    catch(error: any)
    {
        throw new Error(error.response.data);
    }
}