import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom'; // Importuj useNavigate
import { loginUser } from '../../services/Users/LoginUser/LoginUser';

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate(); // Inicijalizuj useNavigate

    const handleSubmit = async (event) => {
        event.preventDefault();
        setLoading(true);
        setError('');

        try {
            const data = await loginUser(username, password);
            console.log(data); // Proveri strukturu odgovora

            if (data && data.accessToken && data.accessToken !== 'Error') {
                // Sačuvaj token u localStorage
                localStorage.setItem('token', JSON.stringify(data));

                // Декодирање JWT и добијање типа корисника
                const base64Url = data.accessToken.split('.')[1];
                const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
                const jsonPayload = decodeURIComponent(atob(base64).split('').map((c) => {
                    return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
                }).join(''));
                const payload = JSON.parse(jsonPayload);
                const userType = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
                console.log('User type:', userType);

                // Redirektuj korisnika nakon prijave
                navigate('/home'); // Možeš promeniti rutu prema potrebi

            } else {
                setError('Invalid access token or login failed');
            }
        } catch (error) {
            setError(error.message);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="loginBox container">
            <img 
                className="user" 
                src="https://i.ibb.co/yVGxFPR/2.png" 
                alt="User" 
                height="100" 
                width="100" 
            />
            <h3>Sign in here</h3>
            <form onSubmit={handleSubmit}>
                <div className="inputBox">
                    <input 
                        id="uname" 
                        type="text" 
                        name="Username" 
                        placeholder="Username" 
                        value={username} 
                        onChange={(e) => setUsername(e.target.value)} 
                        required 
                    />
                    <input 
                        id="pass" 
                        type="password" 
                        name="Password" 
                        placeholder="Password" 
                        value={password} 
                        onChange={(e) => setPassword(e.target.value)} 
                        required 
                    />
                </div>
                <button type="submit" className="btn btn-primary">
                    {loading ? 'Loading...' : 'Login'}
                </button>
            </form>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <div className="text-center">
                <Link to="/ChangePassword" style={{ color: '#59238F' }}>Change Password</Link>
                <br />
                <Link to="/register" style={{ color: '#59238F' }}>Sign-Up</Link>
            </div>
        </div>
    );
};

export default Login;
