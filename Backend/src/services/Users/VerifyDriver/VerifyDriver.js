// src/services/Users/VerifyUser/verifyUser.js
import axios from 'axios';
const API_URL = "https://localhost:44325/api/users/verify";

export const verifyDriver =  async (user) => {
    if (user.userType !== 'driver') {
      throw new Error('Only drivers can be verified.');
    }
  try {
    const response = await axios.put(API_URL, {
        email: user.email,
        v: user.verified,
    });
    return response.data;
  } catch (error) {
    console.error('Verification failed:', error.response?.data || error.message);
    throw error;
  }
};
