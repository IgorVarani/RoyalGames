import "@/styles/globals.css";
import type { AppProps } from "next/app";
import { Orbitron, Exo_2 } from "next/font/google";
import { ToastContainer } from "react-toastify";

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

export default function App({ Component, pageProps }: AppProps) {
  return (
  <main className={`${orbitron.variable} ${exo2.variable}`}>
    <Component {...pageProps} />
    <ToastContainer/>
  </main>
  );
}