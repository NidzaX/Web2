import React, { useState } from 'react';
import { changePassword } from '../../services/Users/ChangeUserPassword/ChangeUserPassword';
import '../../assets/changePassword.css'
const ChangePassword = () => {
    const [email, setEmail] = useState('');
    const [oldPassword, setOldPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();
        setError('');
        setSuccess('');

        try {
            await changePassword(email, oldPassword, newPassword);
            setSuccess('Password changed successfully!');
        } catch (error) {
            setError(error.message);
        }
    };

    return (
        <div className="form-body">
            <div className="row">
                <div className="form-holder">
                    <div className="form-content">
                        <div className="form-items">
                            <h3>Change Password</h3>
                            <p>Fill in the data below.</p>
                            <form className="requires-validation" onSubmit={handleSubmit} noValidate>
                                <div className="col-md-12">
                                    <input
                                        className="form-control"
                                        type="email"
                                        name="email"
                                        placeholder="Email"
                                        value={email}
                                        onChange={(e) => setEmail(e.target.value)}
                                        required
                                    />
                                    <div className="valid-feedback">Email field is valid!</div>
                                    <div className="invalid-feedback">Email field cannot be blank!</div>
                                </div>

                                <div className="col-md-12">
                                    <input
                                        className="form-control"
                                        type="password"
                                        name="oldPassword"
                                        placeholder="Old Password"
                                        value={oldPassword}
                                        onChange={(e) => setOldPassword(e.target.value)}
                                        required
                                    />
                                    <div className="valid-feedback">Old Password field is valid!</div>
                                    <div className="invalid-feedback">Old Password field cannot be blank!</div>
                                </div>

                                <div className="col-md-12">
                                    <input
                                        className="form-control"
                                        type="password"
                                        name="newPassword"
                                        placeholder="New Password"
                                        value={newPassword}
                                        onChange={(e) => setNewPassword(e.target.value)}
                                        required
                                    />
                                    <div className="valid-feedback">New Password field is valid!</div>
                                    <div className="invalid-feedback">New Password field cannot be blank!</div>
                                </div>
                                <div className="form-button mt-3">
                                    <button type="submit" className="btn btn-primary">Change Password</button>
                                </div>
                            </form>
                            {error && <p style={{ color: 'red' }}>{error}</p>}
                            {success && <p style={{ color: 'green' }}>{success}</p>}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ChangePassword;
