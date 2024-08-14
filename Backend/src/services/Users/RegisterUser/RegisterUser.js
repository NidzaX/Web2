import axios from "axios";

const API_URL = "https://localhost:44325/api/users/register";

export const registerUser = async (formData) => {
    try {
        const response = await axios.post(API_URL, formData); // Let Axios set the Content-Type
        return response.data;
    } catch (error) {
        throw new Error(error.response?.data?.message || 'Registration failed');
    }
};
