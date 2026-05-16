import axios from "axios";
import secureLocalStorage from "react-secure-storage";

const apiLocal = "https://localhost:7044/api/"; //? Criar um endereco da API local. Ex: http://localhost:3000/api

const apiRemota = ""; //? Criar um endereco da API remota. Ex: https://api.minhaapp.com

//? Criar um endereco da API dentro do axios.
export const api = axios.create({
    baseURL: apiLocal
})

//? É um interceptor do Axios, ele intercepta toda requisição antes de ser enviada.
api.interceptors.request.use((config) => {
    const token = secureLocalStorage.getItem("Token");

    if(token)
    {
        config.headers.Authorization = "Bearer " + token;
    }

    return config;
});