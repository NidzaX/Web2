import React from "react";
import { Navigate } from "react-router-dom";

export default function PrivateRoute({ children, allowedRoles }) {
    const tokenString = localStorage.getItem('token');
    if (!tokenString) {
        return <Navigate to='/login' />;
    }
    const token = JSON.parse(tokenString);
    const base64Url = token.accessToken.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(atob(base64).split('').map((c) => {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
    const payload = JSON.parse(jsonPayload);

    const role = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    if (!allowedRoles.includes(role)) {
        return <Navigate to='/unauthorized' />;
    }

    if (allowedRoles[0] === 'driver' && token.verified.toLowerCase() === 'false') {
        return <Navigate to='/unauthorized' />;
    }

    return children;
}