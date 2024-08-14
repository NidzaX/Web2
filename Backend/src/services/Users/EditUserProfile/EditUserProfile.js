import axios from 'axios';

const API_URL = "https://localhost:44325/api/users/updateProfile";

export const updateUserProfile = async (formData) => {
    try {
        const response = await axios.put(API_URL, formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });
        return response.data;
    } catch (error) {
        throw new Error(error.response?.data?.message || 'Profile update failed');
    }
};
