import MainPage from "../../components/main-page/MainPage";
import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import StudentPage from "../../components/student-page/StudentPage";
import {useContext} from "react";
import {AuthContext} from "../../components/layout/Layout";

const Home = () => {
    const authContext = useContext(AuthContext);

    return (
        <div>
            <Header/>
            {authContext.role === 'Student' ? <StudentPage/> : <MainPage/>}
            <Footer/>
        </div>
    )
}

export default Home;