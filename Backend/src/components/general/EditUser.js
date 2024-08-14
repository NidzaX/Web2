import React, { useState } from 'react';
import { updateUserProfile } from '../../services/Users/EditUserProfile/EditUserProfile';
import '../../assets/editUser.css';

const EditUserProfile = () => {
    const [username, setUsername] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [password, setPassword] = useState('');
    const [address, setAddress] = useState('');
    const [birthday, setBirthday] = useState('');
    const [userType, setUserType] = useState('');
    const [email, setEmail] = useState('');
    const [file, setFile] = useState(null);
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        setError('');
        setSuccess('');

        const formData = new FormData();
        formData.append('Username', username);
        formData.append('FirstName', firstName);
        formData.append('LastName', lastName);
        formData.append('Password', password);
        formData.append('Address', address);
        formData.append('Birthday', birthday);
        formData.append('UserType', userType);
        formData.append('Email', email);
        formData.append('File', file);

        try {
            await updateUserProfile(formData);
            setSuccess('Profile updated successfully!');
        } catch (error) {
            setError(error.message);
        }
    };

    return (
        <div className="container-fluid px-1 py-5 mx-auto">
            <div className="row d-flex justify-content-center">
                <div className="col-xl-7 col-lg-8 col-md-9 col-11 text-center">
                    <h3>Edit Profile</h3>
                    <p className="blue-text">Please fill out the form below to update your profile.</p>
                    <div className="card">
                        <h5 className="text-center mb-4">Update your details</h5>
                        <form className="form-card" onSubmit={handleSubmit}>
                            <div className="row justify-content-between text-left">
                                <div className="form-group col-sm-6 flex-column d-flex"> 
                                    <label className="form-control-label px-3">Username<span className="text-danger"> *</span></label> 
                                    <input 
                                        type="text" 
                                        value={username} 
                                        onChange={(e) => setUsername(e.target.value)} 
                                        placeholder="Enter your username" 
                                        required 
                                    /> 
                                </div>
                                <div className="form-group col-sm-6 flex-column d-flex"> 
                                    <label className="form-control-label px-3">Email<span className="text-danger"> *</span></label> 
                                    <input 
                                        type="email" 
                                        value={email} 
                                        onChange={(e) => setEmail(e.target.value)} 
                                        placeholder="Enter your email" 
                                        required 
                                    /> 
                                </div>
                            </div>
                            <div className="row justify-content-between text-left">
                                <div className="form-group col-sm-6 flex-column d-flex"> 
                                    <label className="form-control-label px-3">First Name<span className="text-danger"> *</span></label> 
                                    <input 
                                        type="text" 
                                        value={firstName} 
                                        onChange={(e) => setFirstName(e.target.value)} 
                                        placeholder="Enter your first name" 
                                        required 
                                    /> 
                                </div>
                                <div className="form-group col-sm-6 flex-column d-flex"> 
                                    <label className="form-control-label px-3">Last Name<span className="text-danger"> *</span></label> 
                                    <input 
                                        type="text" 
                                        value={lastName} 
                                        onChange={(e) => setLastName(e.target.value)} 
                                        placeholder="Enter your last name" 
                                        required 
                                    /> 
                                </div>
                            </div>
                            <div className="row justify-content-between text-left">
                                <div className="form-group col-sm-6 flex-column d-flex"> 
                                    <label className="form-control-label px-3">Password<span className="text-danger"> *</span></label> 
                                    <input 
                                        type="password" 
                                        value={password} 
                                        onChange={(e) => setPassword(e.target.value)} 
                                        placeholder="Enter your password" 
                                        required 
                                    /> 
                                </div>
                                <div className="form-group col-sm-6 flex-column d-flex"> 
                                    <label className="form-control-label px-3">Birthday<span className="text-danger"> *</span></label> 
                                    <input 
                                        type="date" 
                                        value={birthday} 
                                        onChange={(e) => setBirthday(e.target.value)} 
                                        required 
                                    /> 
                                </div>
                            </div>
                            <div className="row justify-content-between text-left">
                                <div className="form-group col-12 flex-column d-flex"> 
                                    <label className="form-control-label px-3">Address<span className="text-danger"> *</span></label> 
                                    <input 
                                        type="text" 
                                        value={address} 
                                        onChange={(e) => setAddress(e.target.value)} 
                                        placeholder="Enter your address" 
                                        required 
                                    /> 
                                </div>
                            </div>
                            <div className="row justify-content-between text-left">
                                <div className="form-group col-12 flex-column d-flex"> 
                                    <label className="form-control-label px-3">User Type<span className="text-danger"> *</span></label> 
                                    <input 
                                        type="text" 
                                        value={userType} 
                                        onChange={(e) => setUserType(e.target.value)} 
                                        placeholder="Enter your user type" 
                                        required 
                                    /> 
                                </div>
                            </div>
                            <div className="row justify-content-between text-left">
                                <div className="form-group col-12 flex-column d-flex"> 
                                    <label className="form-control-label px-3">Profile Picture<span className="text-danger"> *</span></label> 
                                    <input 
                                        type="file" 
                                        onChange={handleFileChange} 
                                        required 
                                    /> 
                                </div>
                            </div>
                            <div className="row justify-content-end">
                                <div className="form-group col-sm-6"> 
                                    <button type="submit" className="btn btn-primary">Update Profile</button> 
                                </div>
                            </div>
                        </form>
                        {error && <p style={{ color: 'red' }}>{error}</p>}
                        {success && <p style={{ color: 'green' }}>{success}</p>}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default EditUserProfile;