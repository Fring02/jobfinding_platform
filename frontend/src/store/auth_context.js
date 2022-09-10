import Cookies from "js-cookie";
import jwtDecode from "jwt-decode";
import React, { useEffect } from "react";
import { useState } from "react";
import { revokeToken } from '../api/tokens_api';
import { login } from "../api/users_api";
const AuthContext = React.createContext({
    isAuthenticated: false,
    accessToken: '',
    refreshToken: '',
    authError: '',
    role: '',
    setAuthError: () => {},
    onLogout: () => {},
    onLogin: () => {}
});

export const AuthContextProvider = (props) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [access, setAccess] = useState('');
    const [refresh, setRefresh] = useState('');
    const [role, setRole] = useState('');
    const [error, setError] = useState('');
    const onLogout = (e) => {
        e.preventDefault();
        revokeToken(access, refresh).then(() => {
            window.location.href = '/';
        }).catch(e => setError(e.message));
    }

    const onLogin = (e, data, role) => {
        e.preventDefault();
        if (data.email.length === 0 && data.password.length === 0){
            setError('Fill all fields'); return;
        }
        login(data, role).then(() => {
        window.location.href = '/';
        }).catch(err => setError(err.message));
    }

    useEffect(() => {
        if(Cookies.get('jwt-access') === undefined && Cookies.get('jwt-refresh') === undefined) {
            setIsAuthenticated(false);
            setAccess('');
            setRefresh('');
            setRole('');
        } else {
            setIsAuthenticated(true);
            setAccess(Cookies.get('jwt-access'));
            setRefresh(Cookies.get('jwt-refresh'));
            setRole(jwtDecode(Cookies.get('jwt-access'))['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
        }
    }, []);

    return <AuthContext.Provider value={{isAuthenticated: isAuthenticated, accessToken: access, refreshToken: refresh,
    role: role, authError: error, setAuthError: setError, onLogout: onLogout, onLogin: onLogin}}>
                {props.children}
           </AuthContext.Provider>
}
export default AuthContext;