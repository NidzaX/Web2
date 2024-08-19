import React, { useState, useEffect, useContext } from 'react';
import { useNavigate, useLocation, Link } from 'react-router-dom';
import { Nav, Button } from 'react-bootstrap';
import '../../assets/style.css';
import { TimerContext } from './Context';
import { wait } from '@testing-library/user-event/dist/utils';

function Header() {
    const nav = useNavigate();
    const location = useLocation();
    const { waitingTime } = useContext(TimerContext);
    const [userRole, setUserRole] = useState(null);
    const [userId, setUserId] = useState(null);

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            // Decode token and set userRole and userId
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
        if(waitingTime === null || waitingTime <= 0)
        {
            localStorage.removeItem('token');
            localStorage.removeItem('user');
            localStorage.removeItem('encodedtoken');
            setUserRole(null); // Set userRole to null after logout
            nav('/');
        }
    }

    // Determine if links should be disabled
    const isDisabled = waitingTime !== null && waitingTime > 0;

    return (
        <div style={{ backgroundColor: '#0074D9' }}>
            <nav style={{ borderBottom: '3px solid #b0b0b0' }}>
                {userRole && (
                    <>
                        <Nav.Item>
                            <Link
                                to="ChangePassword"
                                className={`nav-link ${location.pathname === '/home/ChangePassword' ? 'bg-light link-dark' : 'link-light'}`}
                                onClick={(e) => isDisabled && e.preventDefault()} // Disable link if waitingTime is active
                            >
                                Change Password
                            </Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Link
                                to="EditProfile"
                                className={`nav-link ${location.pathname === '/home/EditProfile' ? 'bg-light link-dark' : 'link-light'}`}
                                onClick={(e) => isDisabled && e.preventDefault()}
                            >
                                Edit Profile
                            </Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Link
                                to={`/Profile/${userId}`}
                                className={`nav-link ${location.pathname === `/home/Profile/${userId}` ? 'bg-light link-dark' : 'link-light'}`}
                                onClick={(e) => isDisabled && e.preventDefault()}
                            >
                                Profile
                            </Link>
                        </Nav.Item>
                    </>
                )}
                {userRole === "user" && (
                    <>
                        <Nav.Item>
                            <Link
                                to="NewRide"
                                className={`nav-link ${location.pathname === '/home/NewRide' ? 'bg-light link-dark' : 'link-light'}`}
                                onClick={(e) => isDisabled && e.preventDefault()}
                            >
                                Nova vožnja
                            </Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Link
                                to="GetPreviousRides"
                                className={`nav-link ${location.pathname === '/home/GetPreviousRides' ? 'bg-light link-dark' : 'link-light'}`}
                                onClick={(e) => isDisabled && e.preventDefault()}
                            >
                                Prethodne vožnje
                            </Link>
                        </Nav.Item>
                    </>
                )}
                {userRole === "driver" &&(
                    <>
                    <Nav.Item>
                        <Link
                            to="GetAvailableRides"
                            className={`nav-link ${location.pathname === '/home/GetAvailableRides' ? 'bg-light link-dark' : 'link-light'}`}
                            onClick={(e) => isDisabled && e.preventDefault()}
                            >
                            Nove vožnje
                        </Link>
                    </Nav.Item>
                    <Nav.Item>
                        <Link
                            to="GetCompletedRides"
                            className={`nav-link ${location.pathname === '/home/GetCompletedRides' ? 'bg-light link-dark' : 'link-light'}`}
                            onClick={(e) => isDisabled && e.preventDefault()}
                        >
                            Moje vožnje
                        </Link>
                    </Nav.Item>
                    </>
                )}
                {userRole === "admin" &&(
                    <>
                    <Nav.Item>
                        <Link
                            to="GetAllRides"
                            className={`nav-link ${location.pathname === '/home/GetAllRides' ? 'bg-light link-dark' : 'link-light'}`}
                        >
                        Sve vožnje
                        </Link>
                    </Nav.Item>
                    <Nav.Item>
                        <Link
                            to="VerifyDriver"
                            className={`nav-link ${location.pathname === '/home/VerifyDriver' ? 'bg-light link-dark' : 'link-light'}`}
                        >
                        Verifikacija
                        </Link>
                    </Nav.Item>
                    </>
                )}
                {userRole && (
                    <Nav.Item>
                        <Button
                            variant="outline-dark"
                            onClick={(e) => {
                                if (isDisabled) e.preventDefault();
                                else logout();
                            }}
                            className={`btn btn-link link-light text-decoration-none ${isDisabled ? 'disabled' : ''}`}
                        >
                            Log out
                        </Button>
                    </Nav.Item>
                )}
            </nav>
        </div>
    );
}

export default Header;
