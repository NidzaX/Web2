import React, { useState } from 'react';
import { registerUser } from '../../services/Users/RegisterUser/RegisterUser';
import '../../assets/register.css'

const Register = () => {
    const [username, setUsername] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [password, setPassword] = useState('');
    const [address, setAddress] = useState('');
    const [birthday, setBirthday] = useState('');
    const [userType, setUserType] = useState('user'); // default to 'user'
    const [email, setEmail] = useState('');
    const [file, setFile] = useState(null);
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();
        setError('');
        setSuccess('');
    
        const formData = new FormData();
        formData.append('username', username);
        formData.append('firstName', firstName);
        formData.append('lastName', lastName);
        formData.append('password', password);
        formData.append('address', address);
        formData.append('birthday', birthday);
        formData.append('userType', userType);
        formData.append('email', email);
        if (file) {
            formData.append('file', file);
        }
    
        try {
            const result = await registerUser(formData);
            setSuccess('User registered successfully!');
        } catch (error) {
            setError(error.message);
        }
    };
    

    return (
        <div className="container">
            <div className="row">
                <img 
                    id="postcard" 
                    src="http://upload.wikimedia.org/wikipedia/commons/6/6d/FolliesBergereBoxCostume.jpg" 
                    alt="postcard" 
                    className="img-responsive move"
                />
                <div id="content">
                    <h3>Register</h3>
                    <form onSubmit={handleSubmit} role="form">
                        <div className="form-group">
                            <label htmlFor="username" className="iconic user">Username <span className="required">*</span></label>
                            <input 
                                type="text" 
                                className="form-control" 
                                name="username" 
                                id="username"  
                                required="required" 
                                placeholder="Hi friend, how may I call you?"
                                value={username}
                                onChange={(e) => setUsername(e.target.value)}
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="firstName" className="iconic user">First Name <span className="required">*</span></label>
                            <input 
                                type="text" 
                                className="form-control" 
                                name="firstName" 
                                id="firstName"  
                                required="required" 
                                placeholder="Your first name"
                                value={firstName}
                                onChange={(e) => setFirstName(e.target.value)}
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="lastName" className="iconic user">Last Name <span className="required">*</span></label>
                            <input 
                                type="text" 
                                className="form-control" 
                                name="lastName" 
                                id="lastName"  
                                required="required" 
                                placeholder="Your last name"
                                value={lastName}
                                onChange={(e) => setLastName(e.target.value)}
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="password" className="iconic key">Password <span className="required">*</span></label>
                            <input 
                                type="password" 
                                className="form-control" 
                                name="password" 
                                id="password"  
                                required="required" 
                                placeholder="Your password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="address" className="iconic home">Address <span className="required">*</span></label>
                            <input 
                                type="text" 
                                className="form-control" 
                                name="address" 
                                id="address"  
                                required="required" 
                                placeholder="Your address"
                                value={address}
                                onChange={(e) => setAddress(e.target.value)}
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="birthday" className="iconic calendar">Birthday <span className="required">*</span></label>
                            <input 
                                type="date" 
                                className="form-control" 
                                name="birthday" 
                                id="birthday"  
                                required="required" 
                                placeholder="Your birthday"
                                value={birthday}
                                onChange={(e) => setBirthday(e.target.value)}
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="userType" className="iconic user">User Type <span className="required">*</span></label>
                            <select 
                                className="form-control"
                                name="userType"
                                id="userType"
                                value={userType}
                                onChange={(e) => setUserType(e.target.value)} 
                                required
                            >
                                <option value="user">User</option>
                                <option value="driver">Driver</option>
                            </select>
                        </div>

                        <div className="form-group">
                            <label htmlFor="usermail" className="iconic mail-alt">E-mail address <span className="required">*</span></label> 
                            <input 
                                type="email" 
                                className="form-control" 
                                name="email" 
                                id="usermail" 
                                placeholder="I promise I hate spam as much as you do"
                                required="required"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                            />
                        </div>

                        <div className="form-group">
                            <label htmlFor="file" className="iconic file">Upload File <span className="required">*</span></label>
                            <input 
                                type="file" 
                                className="form-control" 
                                name="file" 
                                id="file"  
                                required="required" 
                                onChange={(e) => setFile(e.target.files[0])}
                            />
                        </div>

                        <input type="submit" value=" ★  Register!" className="btn btn-primary"/>    	
                    </form>
                </div>
            </div>

            {error && <p style={{ color: 'red' }}>{error}</p>}
            {success && <p style={{ color: 'green' }}>{success}</p>}
        </div>
    );
};

export default Register;
