import axios from "axios";

const API_URL = "https://localhost:44325/api/users/login";

export const loginUser = async (username, password) => {
    try{
        const response = await axios.post(API_URL, {
            Username: username,
            Password: password
        });
        return await response.data;
        
    }catch(error) {
        return { accessToken: 'Error', message: error.response?.data?.message || 'Login failed' };
    }
};