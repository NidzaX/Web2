import React from 'react'
import { Navigate } from 'react-router-dom';

export default function PrivateRouteA({children}) {
    const tokenString = localStorage.getItem('token');
    if (!tokenString) {
        return children;
    }

    const token = JSON.parse(tokenString);
    const base64Url = token.accessToken.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(atob(base64).split('').map((c) => {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
    const payload = JSON.parse(jsonPayload);

    const currentTime = Math.floor(Date.now() / 1000);
    if (payload.exp && payload.exp < currentTime) {
        return children;
    } else {
        return <Navigate to="/home" />
    }
}
