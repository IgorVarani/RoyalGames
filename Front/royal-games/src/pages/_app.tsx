import "@/styles/globals.css";
import type { AppProps } from "next/app";
import { Orbitron, Exo_2 } from "next/font/google";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const orbitron = Orbitron({
    subsets: ["latin"],
    weight: ["400", "500", "700", "900"],
    variable: "--font-orbitron",
});

const exo2 = Exo_2({
    subsets: ["latin"],
    weight: ["400", "500", "700"],
    variable: "--font-exo2",
});

export default function App({ Component, pageProps }: AppProps)
{
    return (
        <main className={`${orbitron.variable} ${exo2.variable}`}>
            <Component {...pageProps} />

            <ToastContainer
                position="top-right"
                autoClose={2000}
                hideProgressBar={false}
                closeOnClick
                pauseOnHover
                draggable
                theme="dark"
            />
        </main>
    );
}