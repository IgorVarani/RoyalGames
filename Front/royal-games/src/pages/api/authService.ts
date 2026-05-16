import secureLocalStorage from "react-secure-storage";
import { api } from "./api";
export async function login(email: string, senha: string){
    try
    {
        //? Requisição:
        const response = await api.post("Autenticacao/login", {email, senha});
        const token = response.data.token;
        secureLocalStorage.setItem("Token", token);
    }
    catch(error: any)
    {
        throw new Error("Email ou senha inválidos.");
    }
}