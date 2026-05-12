import { Fragment } from "react/jsx-runtime";
import styles from "./home.module.css"
import { ToastContainer } from "react-toastify/unstyled";
import Footer from "@/components/footer/footer";
import Header from "@/components/header/header";

const Home = () => {
    return (
        <Fragment>
            <Header/>
            <ToastContainer/>
            <main id={styles.main}>

            </main>
            <Footer/>
        </Fragment>
    );
}

export default Home;