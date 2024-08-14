import axios from 'axios';

const API_URL = "https://localhost:44325/api/users/changePassword";

export const changePassword = async (email, oldPassword, newPassword) => {
    try {
        const response = await axios.put(API_URL, {
            email,
            oldPassword,
            newPassword
        });
        return response.data;
    } catch (error) {
        throw new Error(error.response?.data?.message || 'Password change failed');
    }
};
