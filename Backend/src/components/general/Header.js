import React, { useState, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { Link } from 'react-router-dom';
import { Nav, Button } from 'react-bootstrap';
import '../../assets/style.css';

function Header() {
    const nav = useNavigate();
    const [userRole, setUserRole] = useState(null);
    const [userId, setUserId] = useState(null); // Add state for userId
    const location = useLocation();

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            // Dekodiraj token i postavi userRole ako je potrebno
            const base64Url = token.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = decodeURIComponent(atob(base64).split('').map((c) => {
                return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
            }).join(''));
            const payload = JSON.parse(jsonPayload);
            const role = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            const id = payload['Id'];
            setUserRole(role);
            setUserId(id);
        }
    }, []);

    function logout() {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        localStorage.removeItem('encodedtoken');
        setUserRole(null); // Postavi userRole na null nakon logout-a
        nav('/');
    }

    return (
        <div style={{ backgroundColor: '#0074D9' }}>
            <nav style={{ borderBottom: '3px solid #b0b0b0' }}>
            {userRole && (
                <Nav.Item>
                    <Link to='ChangePassword' className={`nav-link ${location.pathname === '/home/ChangePassword' ? 'bg-light link-dark' : 'link-light'}`}>
                        Change password
                    </Link>
                </Nav.Item>
            )}
            {userRole && (
                <Nav.Item>
                    <Button variant="outline-dark" onClick={logout} className='btn btn-link link-light text-decoration-none'>
                        Log out
                    </Button>
                </Nav.Item>
            )}
              {userRole && (
                    <Nav.Item>
                        <Link to='EditProfile' className={`nav-link  ${location.pathname === '/home/EditProfile' ? 'bg-light link-dark' : 'link-light'}`}>
                            Edit profile
                        </Link>
                    </Nav.Item>
                )}
                {userRole && (
                    <Nav.Item>
                        <Link to={`/Profile/${userId}`} className={`nav-link ${location.pathname === `/home/Profile/${userId}` ? 'bg-light link-dark' : 'link-light'}`}>
                        Profile
                        </Link>
                    </Nav.Item>
                )}
                {userRole && (
                    <Nav.Item>
                        <Link to='NewRide' className={`nav-link ${location.pathname === `/home/NewRide` ? 'bg-light link-dark' : 'link-light'}`}>
                        Nova vožnja
                        </Link>
                    </Nav.Item>
                )}
            </nav>
        </div>
    );
}

export default Header;
