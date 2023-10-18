import React, {createContext, useEffect, useState} from 'react';
import {Container} from 'reactstrap';
import {ToastContainer, toast} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Loading from "../loading/Loading";
import {apiEndpoint} from "../../api";
import {useNavigate} from "react-router-dom";

export const launchError = (error) => {
    if (error?.response?.data?.message) {
        setTimeout(() => toast.error(error.response.data.message));
    } else if (error?.response?.data) {
        setTimeout(() => toast.error(error?.response?.data));
    } else
        setTimeout(() => toast.error('Unknown Error'));
}
export const launchSuccess = (response) => {
    if (response?.data?.message) {
        setTimeout(() => toast.success(response.data.message));
    } else if (response?.data) {
        setTimeout(() => toast.success(response.data));
    } else
        setTimeout(() => toast.success('Success'));
}

export const launchInfo = (message) => {
    setTimeout(() => toast.info(message));
}

export const LoadingContext = createContext(null);
export const AuthContext = createContext({loggedIn: false, role: null});

const Layout = (props) => {
    const [loading, setLoading] = useState(false);
    const [auth, setAuth] = useState({loggedIn: false, role: null});
    const navigate = useNavigate();

    useEffect(() => {
        apiEndpoint('user/me')
            .fetch()
            .then(response => setAuth({loggedIn: true, role: response.data.role}))
            .catch(() => setAuth({loggedIn: false, role: null}));
    }, [localStorage.getItem('token'), navigate]);

    return (
        <div>
            <ToastContainer/>
            <LoadingContext.Provider value={setLoading}>
                <AuthContext.Provider value={auth}>
                    <Loading loading={loading}/>
                    <Container tag="main">
                        {props.children}
                    </Container>
                </AuthContext.Provider>
            </LoadingContext.Provider>
        </div>
    );
}

export default Layout;