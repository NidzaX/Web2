import axios from 'axios';

export const getAllRides = async () => {
    try {
        const token = localStorage.getItem('token');
        if (!token) {
            throw new Error('No token found in local storage');
        }
        const trueToken = JSON.parse(token).accessToken;    
        const response = await axios.get('https://localhost:44325/api/rides/getAllRides', {
            headers: {
                Authorization: `Bearer ${trueToken}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('Failed to fetch available rides:', error);
        throw error;
    }
};