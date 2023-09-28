import React, { useContext } from 'react'
import AuthContext from "./Context/AuthContext";
import { Outlet, useLocation, Navigate } from 'react-router-dom'

export const RequireAuth = () => {
    const {auth} = useContext(AuthContext);
    const location = useLocation();

    return (
        auth?.accessToken
        ? <Outlet/>
        : <Navigate to="/" state={{from:location}} replace/>
    )
}
