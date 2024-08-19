import React, { useState } from 'react';
import { verifyDriver } from '../../../services/Users/VerifyDriver/VerifyDriver';
import User from '../../model/User';

const VerifyDriverComponent = () => {
  const [email, setEmail] = useState('');
  const [isVerified, setIsVerified] = useState(false);
  const [message, setMessage] = useState('');

  const handleVerification = async () => {
    try {
      const result = await verifyDriver(email, isVerified);
      setMessage(result ? 'User successfully verified' : 'Verification failed');
    } catch (error) {
      setMessage('Error during verification. Please try again.');
    }

    const handleVerification = async () => {
        const user = new User(
          null, // id (optional here)
          '', // firstName
          '', // lastName
          email, // email
          '', // username
          '', // password
          '', // address
          '', // birthday
          'driver', // userType (assuming you're testing for a driver)
          null, // file (optional here)
          isVerified // verified
        );

        try {
            const result = await verifyDriver(user);
            setMessage(result ? 'Driver successfully verified' : 'Verification failed');
          } catch (error) {
            setMessage('Error during verification. Please try again.');
          }
        };
  };

  

  return (
    <div>
      <h2>Verify Driver</h2>
      <input
        type="email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        placeholder="Driver Email"
      />
      <label>
        <input
          type="checkbox"
          checked={isVerified}
          onChange={(e) => setIsVerified(e.target.checked)}
        />
        Verify
      </label>
      <button onClick={handleVerification}>Submit</button>
      {message && <p>{message}</p>}
    </div>
  );
};

export default VerifyDriverComponent;